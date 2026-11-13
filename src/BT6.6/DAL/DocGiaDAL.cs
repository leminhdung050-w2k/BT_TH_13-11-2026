using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BT6._6.DTO;

namespace BT6._6.DAL
{
    public class DocGiaDAL
    {
        public List<DocGiaDTO> LayDanhSach()
        {
            List<DocGiaDTO> ds = new List<DocGiaDTO>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT MaDocGia, HoTen, Email, NgayHetHanThe FROM DocGia ORDER BY MaDocGia", conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ds.Add(new DocGiaDTO
                        {
                            MaDocGia = (int)dr["MaDocGia"],
                            HoTen = dr["HoTen"].ToString(),
                            Email = dr["Email"].ToString(),
                            NgayHetHanThe = dr["NgayHetHanThe"] == DBNull.Value ? (DateTime?)null : (DateTime)dr["NgayHetHanThe"]
                        });
                    }
                }
            }
            return ds;
        }

        public int Them(DocGiaDTO dg)
        {
            return DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO DocGia (HoTen, Email, NgayHetHanThe) VALUES (@HoTen, @Email, @HetHan)", ThamSo(dg));
        }

        public int Sua(DocGiaDTO dg)
        {
            return DatabaseHelper.ExecuteNonQuery(
                "UPDATE DocGia SET HoTen = @HoTen, Email = @Email, NgayHetHanThe = @HetHan WHERE MaDocGia = @Ma", ThamSo(dg));
        }

        public int Xoa(int maDocGia)
        {
            return DatabaseHelper.ExecuteNonQuery("DELETE FROM DocGia WHERE MaDocGia = @Ma",
                new SqlParameter("@Ma", SqlDbType.Int) { Value = maDocGia });
        }

        // Kiểm tra hạn thẻ: NgayHetHanThe >= ngày hiện tại
        public bool ConHanThe(int maDocGia)
        {
            object kq = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM DocGia WHERE MaDocGia = @Ma AND NgayHetHanThe >= CAST(GETDATE() AS DATE)",
                new SqlParameter("@Ma", SqlDbType.Int) { Value = maDocGia });
            return Convert.ToInt32(kq) > 0;
        }

        private SqlParameter[] ThamSo(DocGiaDTO dg)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = dg.HoTen },
                new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(dg.Email) ? (object)DBNull.Value : dg.Email },
                new SqlParameter("@HetHan", SqlDbType.DateTime) { Value = dg.NgayHetHanThe.HasValue ? (object)dg.NgayHetHanThe.Value : DBNull.Value },
                new SqlParameter("@Ma", SqlDbType.Int) { Value = dg.MaDocGia }
            };
        }
    }
}
