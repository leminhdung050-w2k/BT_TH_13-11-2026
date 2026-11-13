using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BT6._7.DTO;

namespace BT6._7.DAL
{
    public class DonThuocDAL
    {
        // "Lưu & Xuất đơn" trong 1 SqlTransaction:
        // 1. Tạo DonThuoc -> 2. Tạo các ChiTietDonThuoc -> 3. Trừ SoLuongTon
        // -> 4. Thuốc nào không đủ tồn thì trans.Rollback() và báo lỗi
        public int LuuVaXuatDon(int maHoSo, string chanDoan, decimal tienKham, decimal tongTien, List<ChiTietDonDTO> chiTiet)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    TaoLenh("UPDATE HoSoKham SET ChanDoan = @ChanDoan WHERE MaHoSo = @MaHoSo", conn, trans,
                        new SqlParameter("@ChanDoan", SqlDbType.NVarChar, 250) { Value = chanDoan },
                        new SqlParameter("@MaHoSo", SqlDbType.Int) { Value = maHoSo }).ExecuteNonQuery();

                    int maDon = (int)TaoLenh("INSERT INTO DonThuoc (MaHoSo, TienKham, TongTien, TrangThai) OUTPUT INSERTED.MaDon " +
                                             "VALUES (@MaHoSo, @TienKham, @TongTien, N'Chờ lấy thuốc')", conn, trans,
                        new SqlParameter("@MaHoSo", SqlDbType.Int) { Value = maHoSo },
                        new SqlParameter("@TienKham", SqlDbType.Decimal) { Value = tienKham, Precision = 18, Scale = 2 },
                        new SqlParameter("@TongTien", SqlDbType.Decimal) { Value = tongTien, Precision = 18, Scale = 2 }).ExecuteScalar();

                    foreach (ChiTietDonDTO ct in chiTiet)
                    {
                        TaoLenh("INSERT INTO ChiTietDonThuoc (MaDon, MaThuoc, SoLuong, LieuDung) VALUES (@MaDon, @MaThuoc, @SL, @LieuDung)", conn, trans,
                            new SqlParameter("@MaDon", SqlDbType.Int) { Value = maDon },
                            new SqlParameter("@MaThuoc", SqlDbType.Int) { Value = ct.MaThuoc },
                            new SqlParameter("@SL", SqlDbType.Int) { Value = ct.SoLuong },
                            new SqlParameter("@LieuDung", SqlDbType.NVarChar, 100) { Value = ct.LieuDung }).ExecuteNonQuery();

                        int n = TaoLenh("UPDATE Thuoc SET SoLuongTon = SoLuongTon - @SL WHERE MaThuoc = @MaThuoc AND SoLuongTon >= @SL", conn, trans,
                            new SqlParameter("@SL", SqlDbType.Int) { Value = ct.SoLuong },
                            new SqlParameter("@MaThuoc", SqlDbType.Int) { Value = ct.MaThuoc }).ExecuteNonQuery();
                        if (n == 0)
                        {
                            object ton = TaoLenh("SELECT SoLuongTon FROM Thuoc WHERE MaThuoc = @MaThuoc", conn, trans,
                                new SqlParameter("@MaThuoc", SqlDbType.Int) { Value = ct.MaThuoc }).ExecuteScalar();
                            trans.Rollback();
                            throw new Exception("Không đủ thuốc trong kho: " + ct.TenThuoc + " chỉ còn " + ton + " " + ct.DonViTinh +
                                                ", đơn kê " + ct.SoLuong + " " + ct.DonViTinh + ".\n\nĐã hủy (Rollback) toàn bộ đơn thuốc.");
                        }
                    }
                    trans.Commit();
                    return maDon;
                }
                catch
                {
                    if (trans.Connection != null) trans.Rollback();
                    throw;
                }
            }
        }

        // trangThai = null: tất cả
        public List<DonThuocDTO> LayDanhSach(string trangThai)
        {
            List<DonThuocDTO> ds = new List<DonThuocDTO>();
            foreach (DataRow r in DatabaseHelper.ExecuteQuery(
                "SELECT d.MaDon, d.MaHoSo, bn.HoTen, hs.NgayKham, hs.ChanDoan, d.TienKham, d.TongTien, d.TrangThai " +
                "FROM DonThuoc d JOIN HoSoKham hs ON d.MaHoSo = hs.MaHoSo JOIN BenhNhan bn ON hs.MaBN = bn.MaBN " +
                "WHERE (@TrangThai IS NULL OR d.TrangThai = @TrangThai) ORDER BY d.MaDon DESC",
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)trangThai ?? DBNull.Value }).Rows)
            {
                ds.Add(new DonThuocDTO
                {
                    MaDon = (int)r["MaDon"],
                    MaHoSo = (int)r["MaHoSo"],
                    HoTen = r["HoTen"].ToString(),
                    NgayKham = (DateTime)r["NgayKham"],
                    ChanDoan = r["ChanDoan"].ToString(),
                    TienKham = (decimal)r["TienKham"],
                    TongTien = (decimal)r["TongTien"],
                    TrangThai = r["TrangThai"].ToString()
                });
            }
            return ds;
        }

        public List<ChiTietDonDTO> LayChiTiet(int maDon)
        {
            List<ChiTietDonDTO> ds = new List<ChiTietDonDTO>();
            foreach (DataRow r in DatabaseHelper.ExecuteQuery(
                "SELECT c.MaThuoc, t.TenThuoc, t.DonViTinh, c.SoLuong, t.DonGia, c.LieuDung " +
                "FROM ChiTietDonThuoc c JOIN Thuoc t ON c.MaThuoc = t.MaThuoc WHERE c.MaDon = @MaDon ORDER BY c.MaThuoc",
                new SqlParameter("@MaDon", SqlDbType.Int) { Value = maDon }).Rows)
            {
                ds.Add(new ChiTietDonDTO
                {
                    MaThuoc = (int)r["MaThuoc"],
                    TenThuoc = r["TenThuoc"].ToString(),
                    DonViTinh = r["DonViTinh"].ToString(),
                    SoLuong = (int)r["SoLuong"],
                    DonGia = (decimal)r["DonGia"],
                    LieuDung = r["LieuDung"].ToString()
                });
            }
            return ds;
        }

        // Thanh toán: lưu tổng chi phí và chuyển trạng thái sang "Đã thanh toán"
        public int ThanhToan(int maDon, decimal tongTien)
        {
            return DatabaseHelper.ExecuteNonQuery(
                "UPDATE DonThuoc SET TongTien = @TongTien, TrangThai = N'Đã thanh toán' WHERE MaDon = @MaDon AND TrangThai <> N'Đã thanh toán'",
                new SqlParameter("@TongTien", SqlDbType.Decimal) { Value = tongTien, Precision = 18, Scale = 2 },
                new SqlParameter("@MaDon", SqlDbType.Int) { Value = maDon });
        }

        private static SqlCommand TaoLenh(string sql, SqlConnection conn, SqlTransaction trans, params SqlParameter[] thamSo)
        {
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            cmd.Parameters.AddRange(thamSo);
            return cmd;
        }
    }
}
