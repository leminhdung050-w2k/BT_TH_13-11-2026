using System;
using System.Data;
using Microsoft.Data.SqlClient;
using BT6._8.DTO;

namespace BT6._8.DAL
{
    public class HoaDonDAL
    {
        private const string SELECT_CHI_TIET = "SELECT MaHD, MaDV, SoLuong, DonGia FROM ChiTietHoaDon WHERE MaHD = @MaHD";

        // Mở bàn (1 transaction): tạo HoaDon với GioBatDau = GETDATE() -> bàn chuyển sang "Đang chơi"
        public int MoBan(int maBan)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    object trangThai = TaoLenh("SELECT TrangThai FROM BanBida WITH (UPDLOCK) WHERE MaBan = @MaBan", conn, trans,
                        new SqlParameter("@MaBan", SqlDbType.Int) { Value = maBan }).ExecuteScalar();
                    if (trangThai == null || trangThai.ToString() != "Trống")
                        throw new Exception("Bàn này đang có khách chơi!");

                    int maHD = (int)TaoLenh("INSERT INTO HoaDon (MaBan, GioBatDau) OUTPUT INSERTED.MaHD VALUES (@MaBan, GETDATE())", conn, trans,
                        new SqlParameter("@MaBan", SqlDbType.Int) { Value = maBan }).ExecuteScalar();
                    TaoLenh("UPDATE BanBida SET TrangThai = N'Đang chơi' WHERE MaBan = @MaBan", conn, trans,
                        new SqlParameter("@MaBan", SqlDbType.Int) { Value = maBan }).ExecuteNonQuery();

                    trans.Commit();
                    return maHD;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        // Hóa đơn chưa thanh toán của bàn đang chơi (không có thì null)
        public HoaDonDTO LayHoaDonDangMo(int maBan)
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(
                "SELECT h.MaHD, h.MaBan, b.TenBan, b.GiaTheoGio, h.GioBatDau FROM HoaDon h JOIN BanBida b ON h.MaBan = b.MaBan " +
                "WHERE h.MaBan = @MaBan AND h.TrangThai = 0",
                new SqlParameter("@MaBan", SqlDbType.Int) { Value = maBan });
            if (dt.Rows.Count == 0) return null;
            DataRow r = dt.Rows[0];
            return new HoaDonDTO
            {
                MaHD = (int)r["MaHD"],
                MaBan = (int)r["MaBan"],
                TenBan = r["TenBan"].ToString(),
                GiaTheoGio = (decimal)r["GiaTheoGio"],
                GioBatDau = (DateTime)r["GioBatDau"]
            };
        }

        // Mô hình ngắt kết nối: nạp các món bàn đã gọi vào DataTable bằng SqlDataAdapter
        public DataTable LayDichVuDaGoi(int maHD)
        {
            DataTable dt = new DataTable("ChiTietHoaDon");
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlDataAdapter da = TaoAdapter(conn, maHD))
            {
                da.Fill(dt);
            }

            // Cột hiển thị thêm: tên dịch vụ + thành tiền (CommandBuilder bỏ qua khi Update)
            dt.Columns.Add("TenDV", typeof(string));
            dt.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");
            DataTable ten = DatabaseHelper.ExecuteQuery("SELECT MaDV, TenDV FROM MenuDichVu");
            foreach (DataRow r in dt.Rows)
            {
                DataRow[] t = ten.Select("MaDV = " + (int)r["MaDV"]);
                r["TenDV"] = t.Length > 0 ? t[0]["TenDV"] : "";
            }
            dt.AcceptChanges();
            return dt;
        }

        // Đồng bộ các dòng thêm / sửa trong DataTable về SQL Server (SqlCommandBuilder + adapter.Update)
        public int LuuDichVu(DataTable dt, int maHD)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlDataAdapter da = TaoAdapter(conn, maHD))
            using (SqlCommandBuilder cb = new SqlCommandBuilder(da))
            {
                cb.ConflictOption = ConflictOption.OverwriteChanges;   // UPDATE theo khóa (MaHD, MaDV)
                return da.Update(dt);
            }
        }

        // Thanh toán trong 1 SqlTransaction:
        // 1. Cập nhật GioKetThuc, TienGioChoi, TienDichVu, TrangThai = 1 -> 2. Trừ TonKho (bỏ qua dịch vụ thuê)
        // -> 3. Bàn về "Trống" -> 4. Commit
        public void ThanhToan(HoaDonDTO hd)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    hd.TienDichVu = Convert.ToDecimal(TaoLenh(
                        "SELECT ISNULL(SUM(SoLuong * DonGia), 0) FROM ChiTietHoaDon WHERE MaHD = @MaHD", conn, trans,
                        new SqlParameter("@MaHD", SqlDbType.Int) { Value = hd.MaHD }).ExecuteScalar());

                    int n = TaoLenh("UPDATE HoaDon SET GioKetThuc = @GioKetThuc, TienGioChoi = @TienGio, TienDichVu = @TienDV, TrangThai = 1 " +
                                    "WHERE MaHD = @MaHD AND TrangThai = 0", conn, trans,
                        new SqlParameter("@GioKetThuc", SqlDbType.DateTime) { Value = hd.GioKetThuc.Value },
                        new SqlParameter("@TienGio", SqlDbType.Decimal) { Value = hd.TienGioChoi, Precision = 18, Scale = 2 },
                        new SqlParameter("@TienDV", SqlDbType.Decimal) { Value = hd.TienDichVu, Precision = 18, Scale = 2 },
                        new SqlParameter("@MaHD", SqlDbType.Int) { Value = hd.MaHD }).ExecuteNonQuery();
                    if (n == 0) throw new Exception("Hóa đơn đã được thanh toán trước đó!");

                    // Trừ tồn kho các món (TonKho IS NULL = dịch vụ thuê gậy, không trừ kho)
                    DataTable ct = new DataTable();
                    SqlCommand docCT = TaoLenh("SELECT c.MaDV, d.TenDV, c.SoLuong, d.TonKho FROM ChiTietHoaDon c JOIN MenuDichVu d ON c.MaDV = d.MaDV " +
                                               "WHERE c.MaHD = @MaHD AND d.TonKho IS NOT NULL", conn, trans,
                        new SqlParameter("@MaHD", SqlDbType.Int) { Value = hd.MaHD });
                    using (SqlDataReader dr = docCT.ExecuteReader()) ct.Load(dr);
                    foreach (DataRow r in ct.Rows)
                    {
                        if ((int)r["TonKho"] < (int)r["SoLuong"])
                        {
                            trans.Rollback();
                            throw new Exception("Không đủ hàng trong kho: " + r["TenDV"] + " còn " + r["TonKho"] + ", bàn đã gọi " + r["SoLuong"] +
                                                "\n\nĐã hủy (Rollback) thanh toán.");
                        }
                        TaoLenh("UPDATE MenuDichVu SET TonKho = TonKho - @SL WHERE MaDV = @MaDV", conn, trans,
                            new SqlParameter("@SL", SqlDbType.Int) { Value = (int)r["SoLuong"] },
                            new SqlParameter("@MaDV", SqlDbType.Int) { Value = (int)r["MaDV"] }).ExecuteNonQuery();
                    }

                    TaoLenh("UPDATE BanBida SET TrangThai = N'Trống' WHERE MaBan = @MaBan", conn, trans,
                        new SqlParameter("@MaBan", SqlDbType.Int) { Value = hd.MaBan }).ExecuteNonQuery();

                    trans.Commit();
                    hd.TrangThai = true;
                }
                catch
                {
                    if (trans.Connection != null) trans.Rollback();   // chưa rollback thì rollback
                    throw;
                }
            }
        }

        private static SqlDataAdapter TaoAdapter(SqlConnection conn, int maHD)
        {
            SqlDataAdapter da = new SqlDataAdapter(SELECT_CHI_TIET, conn);
            da.SelectCommand.Parameters.Add("@MaHD", SqlDbType.Int).Value = maHD;
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;      // lấy khóa chính (MaHD, MaDV)
            return da;
        }

        private static SqlCommand TaoLenh(string sql, SqlConnection conn, SqlTransaction trans, params SqlParameter[] thamSo)
        {
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            cmd.Parameters.AddRange(thamSo);
            return cmd;
        }
    }
}
