using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BT6._7.DTO;

namespace BT6._7.DAL
{
    public class BenhNhanDAL
    {
        // Hồ sơ khám + bệnh nhân + trạng thái đơn (chưa có đơn = "Chờ khám")
        private const string SELECT_HO_SO =
            "SELECT hs.MaHoSo, hs.MaBN, bn.HoTen, bn.SoDienThoai, bn.NamSinh, hs.NgayKham, hs.TrieuChung, hs.ChanDoan, " +
            "ISNULL(d.TrangThai, N'Chờ khám') AS TrangThai " +
            "FROM HoSoKham hs JOIN BenhNhan bn ON hs.MaBN = bn.MaBN LEFT JOIN DonThuoc d ON d.MaHoSo = hs.MaHoSo ";

        // Tra cứu bệnh nhân theo số điện thoại (SqlParameter)
        public BenhNhanDTO TimTheoSDT(string sdt)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT MaBN, HoTen, SoDienThoai, NamSinh FROM BenhNhan WHERE SoDienThoai = @SDT", conn))
            {
                cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 15).Value = sdt;
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;
                    return new BenhNhanDTO
                    {
                        MaBN = (int)dr["MaBN"],
                        HoTen = dr["HoTen"].ToString(),
                        SoDienThoai = dr["SoDienThoai"].ToString(),
                        NamSinh = dr["NamSinh"] == DBNull.Value ? (int?)null : (int)dr["NamSinh"]
                    };
                }
            }
        }

        // Bệnh nhân mới: thêm BenhNhan và tạo HoSoKham trong 1 transaction
        public int ThemMoiVaTaoHoSo(BenhNhanDTO bn, string trieuChung)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO BenhNhan (HoTen, SoDienThoai, NamSinh) OUTPUT INSERTED.MaBN VALUES (@HoTen, @SDT, @NamSinh)", conn, trans);
                    cmd.Parameters.AddRange(ThamSoBN(bn));
                    bn.MaBN = (int)cmd.ExecuteScalar();
                    int maHoSo = TaoHoSo(bn.MaBN, trieuChung, conn, trans);
                    trans.Commit();
                    return maHoSo;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        // Bệnh nhân cũ: cập nhật thông tin và tạo HoSoKham mới
        public int CapNhatVaTaoHoSo(BenhNhanDTO bn, string trieuChung)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE BenhNhan SET HoTen = @HoTen, NamSinh = @NamSinh WHERE MaBN = @MaBN", conn, trans);
                    cmd.Parameters.AddRange(ThamSoBN(bn));
                    cmd.ExecuteNonQuery();
                    int maHoSo = TaoHoSo(bn.MaBN, trieuChung, conn, trans);
                    trans.Commit();
                    return maHoSo;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        private static int TaoHoSo(int maBN, string trieuChung, SqlConnection conn, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO HoSoKham (MaBN, NgayKham, TrieuChung) OUTPUT INSERTED.MaHoSo VALUES (@MaBN, GETDATE(), @TrieuChung)", conn, trans);
            cmd.Parameters.Add("@MaBN", SqlDbType.Int).Value = maBN;
            cmd.Parameters.Add("@TrieuChung", SqlDbType.NVarChar, -1).Value = trieuChung;
            return (int)cmd.ExecuteScalar();
        }

        // Hồ sơ đang chờ khám (chưa kê đơn)
        public List<HoSoKhamDTO> LayHoSoChoKham()
        {
            return DocHoSo(SELECT_HO_SO + "WHERE d.MaDon IS NULL ORDER BY hs.NgayKham");
        }

        // Lịch sử khám của 1 bệnh nhân
        public List<HoSoKhamDTO> LayHoSoCuaBN(int maBN)
        {
            return DocHoSo(SELECT_HO_SO + "WHERE hs.MaBN = @MaBN ORDER BY hs.NgayKham DESC",
                new SqlParameter("@MaBN", SqlDbType.Int) { Value = maBN });
        }

        public int DemChoKham()
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM HoSoKham hs WHERE NOT EXISTS (SELECT 1 FROM DonThuoc d WHERE d.MaHoSo = hs.MaHoSo)"));
        }

        private static List<HoSoKhamDTO> DocHoSo(string sql, params SqlParameter[] thamSo)
        {
            List<HoSoKhamDTO> ds = new List<HoSoKhamDTO>();
            foreach (DataRow r in DatabaseHelper.ExecuteQuery(sql, thamSo).Rows)
            {
                ds.Add(new HoSoKhamDTO
                {
                    MaHoSo = (int)r["MaHoSo"],
                    MaBN = (int)r["MaBN"],
                    HoTen = r["HoTen"].ToString(),
                    SoDienThoai = r["SoDienThoai"].ToString(),
                    NamSinh = r["NamSinh"] == DBNull.Value ? (int?)null : (int)r["NamSinh"],
                    NgayKham = (DateTime)r["NgayKham"],
                    TrieuChung = r["TrieuChung"].ToString(),
                    ChanDoan = r["ChanDoan"].ToString(),
                    TrangThai = r["TrangThai"].ToString()
                });
            }
            return ds;
        }

        private static SqlParameter[] ThamSoBN(BenhNhanDTO bn)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = bn.HoTen },
                new SqlParameter("@SDT", SqlDbType.VarChar, 15) { Value = bn.SoDienThoai },
                new SqlParameter("@NamSinh", SqlDbType.Int) { Value = bn.NamSinh.HasValue ? (object)bn.NamSinh.Value : DBNull.Value },
                new SqlParameter("@MaBN", SqlDbType.Int) { Value = bn.MaBN }
            };
        }
    }
}
