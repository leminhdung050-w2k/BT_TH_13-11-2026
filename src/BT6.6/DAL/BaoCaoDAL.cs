using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BT6._6.DTO;

namespace BT6._6.DAL
{
    public class BaoCaoDAL
    {
        // SqlDataReader: phiếu mượn đã vượt quá HanTra mà TrangThai != N'Đã trả'
        public List<PhieuMuonDTO> PhieuQuaHan()
        {
            return PhieuMuonDAL.Doc(PhieuMuonDAL.SELECT_PHIEU +
                "WHERE pm.HanTra < GETDATE() AND pm.TrangThai <> N'Đã trả' ORDER BY pm.HanTra");
        }

        // ExecuteScalar: số phiếu quá hạn
        public int DemQuaHan()
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM PhieuMuon WHERE HanTra < GETDATE() AND TrangThai <> N'Đã trả'"));
        }

        // ExecuteScalar: tổng số lượt mượn (tổng số cuốn đã cho mượn)
        public int TongLuotMuon()
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT ISNULL(SUM(SoLuong), 0) FROM ChiTietMuon"));
        }

        // GROUP BY: tổng số lượt mượn theo từng thể loại sách
        public List<ThongKeDTO> ThongKeTheLoai()
        {
            List<ThongKeDTO> ds = new List<ThongKeDTO>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ISNULL(ds.TheLoai, N'(Khác)') AS TheLoai, COUNT(DISTINCT ct.MaPhieu) AS SoPhieu, SUM(ct.SoLuong) AS SoLuotMuon " +
                "FROM ChiTietMuon ct JOIN DauSach ds ON ct.MaSach = ds.MaSach " +
                "GROUP BY ds.TheLoai ORDER BY SoLuotMuon DESC", conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ds.Add(new ThongKeDTO
                        {
                            TheLoai = dr["TheLoai"].ToString(),
                            SoPhieu = (int)dr["SoPhieu"],
                            SoLuotMuon = (int)dr["SoLuotMuon"]
                        });
                    }
                }
            }
            return ds;
        }
    }
}
