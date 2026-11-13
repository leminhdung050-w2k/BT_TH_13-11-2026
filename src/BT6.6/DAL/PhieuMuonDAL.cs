using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BT6._6.DTO;

namespace BT6._6.DAL
{
    public class PhieuMuonDAL
    {
        // Phiếu mượn + họ tên độc giả + danh sách sách trong phiếu (FOR XML PATH ghép chuỗi)
        internal const string SELECT_PHIEU =
            "SELECT pm.MaPhieu, pm.MaDocGia, dg.HoTen, pm.NgayMuon, pm.HanTra, pm.TrangThai, " +
            "STUFF((SELECT N', ' + ds.TenSach + CASE WHEN ct.SoLuong > 1 THEN N' x' + CAST(ct.SoLuong AS NVARCHAR(10)) ELSE N'' END " +
            "       FROM ChiTietMuon ct JOIN DauSach ds ON ct.MaSach = ds.MaSach WHERE ct.MaPhieu = pm.MaPhieu " +
            "       FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS SachMuon " +
            "FROM PhieuMuon pm JOIN DocGia dg ON pm.MaDocGia = dg.MaDocGia ";

        // Phiếu chưa trả mà đã quá HanTra -> chuyển sang "Quá hạn"
        public int CapNhatQuaHan()
        {
            return DatabaseHelper.ExecuteNonQuery(
                "UPDATE PhieuMuon SET TrangThai = N'Quá hạn' WHERE TrangThai = N'Đang mượn' AND HanTra < GETDATE()");
        }

        // Lọc phiếu mượn theo độc giả (0 = tất cả) và trạng thái ("" = tất cả)
        public List<PhieuMuonDTO> LayDanhSach(int maDocGia, string trangThai)
        {
            return Doc(SELECT_PHIEU +
                "WHERE (@MaDocGia = 0 OR pm.MaDocGia = @MaDocGia) AND (@TrangThai = N'' OR pm.TrangThai = @TrangThai) " +
                "ORDER BY pm.MaPhieu DESC",
                new SqlParameter("@MaDocGia", SqlDbType.Int) { Value = maDocGia },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = trangThai ?? "" });
        }

        public List<ChiTietMuonDTO> LayChiTiet(int maPhieu)
        {
            List<ChiTietMuonDTO> ds = new List<ChiTietMuonDTO>();
            foreach (DataRow r in DatabaseHelper.ExecuteQuery(
                "SELECT ct.MaSach, ds.TenSach, ds.TheLoai, ct.SoLuong FROM ChiTietMuon ct JOIN DauSach ds ON ct.MaSach = ds.MaSach " +
                "WHERE ct.MaPhieu = @MaPhieu ORDER BY ct.MaSach",
                new SqlParameter("@MaPhieu", SqlDbType.Int) { Value = maPhieu }).Rows)
            {
                ds.Add(new ChiTietMuonDTO
                {
                    MaSach = (int)r["MaSach"],
                    TenSach = r["TenSach"].ToString(),
                    TheLoai = r["TheLoai"].ToString(),
                    SoLuong = (int)r["SoLuong"]
                });
            }
            return ds;
        }

        // Lập phiếu mượn (1 transaction): tạo PhieuMuon -> ChiTietMuon -> trừ SoLuongCon; thiếu sách thì Rollback
        public int LapPhieu(int maDocGia, DateTime hanTra, List<ChiTietMuonDTO> chiTiet)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int maPhieu = (int)TaoLenh("INSERT INTO PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) OUTPUT INSERTED.MaPhieu " +
                                               "VALUES (@MaDocGia, GETDATE(), @HanTra, N'Đang mượn')", conn, trans,
                        new SqlParameter("@MaDocGia", SqlDbType.Int) { Value = maDocGia },
                        new SqlParameter("@HanTra", SqlDbType.DateTime) { Value = hanTra }).ExecuteScalar();

                    foreach (ChiTietMuonDTO ct in chiTiet)
                    {
                        TaoLenh("INSERT INTO ChiTietMuon (MaPhieu, MaSach, SoLuong) VALUES (@MaPhieu, @MaSach, @SL)", conn, trans,
                            new SqlParameter("@MaPhieu", SqlDbType.Int) { Value = maPhieu },
                            new SqlParameter("@MaSach", SqlDbType.Int) { Value = ct.MaSach },
                            new SqlParameter("@SL", SqlDbType.Int) { Value = ct.SoLuong }).ExecuteNonQuery();

                        int n = TaoLenh("UPDATE DauSach SET SoLuongCon = SoLuongCon - @SL WHERE MaSach = @MaSach AND SoLuongCon >= @SL", conn, trans,
                            new SqlParameter("@SL", SqlDbType.Int) { Value = ct.SoLuong },
                            new SqlParameter("@MaSach", SqlDbType.Int) { Value = ct.MaSach }).ExecuteNonQuery();
                        if (n == 0)
                        {
                            trans.Rollback();
                            throw new Exception("Sách \"" + ct.TenSach + "\" không còn đủ " + ct.SoLuong + " cuốn trong thư viện!\n\nĐã hủy (Rollback) phiếu mượn.");
                        }
                    }
                    trans.Commit();
                    return maPhieu;
                }
                catch
                {
                    if (trans.Connection != null) trans.Rollback();
                    throw;
                }
            }
        }

        // Trả sách: Parameterized UPDATE chuyển trạng thái sang "Đã trả" đồng thời cộng lại SoLuongCon
        public void TraSach(int maPhieu)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int n = TaoLenh("UPDATE PhieuMuon SET TrangThai = N'Đã trả' WHERE MaPhieu = @MaPhieu AND TrangThai <> N'Đã trả'", conn, trans,
                        new SqlParameter("@MaPhieu", SqlDbType.Int) { Value = maPhieu }).ExecuteNonQuery();
                    if (n == 0) throw new Exception("Phiếu mượn #" + maPhieu + " đã được trả trước đó!");

                    TaoLenh("UPDATE ds SET ds.SoLuongCon = ds.SoLuongCon + ct.SoLuong " +
                            "FROM DauSach ds JOIN ChiTietMuon ct ON ds.MaSach = ct.MaSach WHERE ct.MaPhieu = @MaPhieu", conn, trans,
                        new SqlParameter("@MaPhieu", SqlDbType.Int) { Value = maPhieu }).ExecuteNonQuery();

                    trans.Commit();
                }
                catch
                {
                    if (trans.Connection != null) trans.Rollback();
                    throw;
                }
            }
        }

        // Đọc danh sách phiếu bằng SqlDataReader
        internal static List<PhieuMuonDTO> Doc(string sql, params SqlParameter[] thamSo)
        {
            List<PhieuMuonDTO> ds = new List<PhieuMuonDTO>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddRange(thamSo);
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ds.Add(new PhieuMuonDTO
                        {
                            MaPhieu = (int)dr["MaPhieu"],
                            MaDocGia = (int)dr["MaDocGia"],
                            HoTen = dr["HoTen"].ToString(),
                            NgayMuon = (DateTime)dr["NgayMuon"],
                            HanTra = (DateTime)dr["HanTra"],
                            TrangThai = dr["TrangThai"].ToString(),
                            SachMuon = dr["SachMuon"].ToString()
                        });
                    }
                }
            }
            return ds;
        }

        private static SqlCommand TaoLenh(string sql, SqlConnection conn, SqlTransaction trans, params SqlParameter[] thamSo)
        {
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            cmd.Parameters.AddRange(thamSo);
            return cmd;
        }
    }
}
