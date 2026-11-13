using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BT6._6.DTO;

namespace BT6._6.DAL
{
    public class DauSachDAL
    {
        public List<DauSachDTO> LayDanhSach()
        {
            List<DauSachDTO> ds = new List<DauSachDTO>();
            foreach (DataRow r in DatabaseHelper.ExecuteQuery("SELECT MaSach, TenSach, TheLoai, SoLuongCon FROM DauSach ORDER BY MaSach").Rows)
            {
                ds.Add(new DauSachDTO
                {
                    MaSach = (int)r["MaSach"],
                    TenSach = r["TenSach"].ToString(),
                    TheLoai = r["TheLoai"].ToString(),
                    SoLuongCon = (int)r["SoLuongCon"]
                });
            }
            return ds;
        }

        public int Them(DauSachDTO s)
        {
            return DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO DauSach (TenSach, TheLoai, SoLuongCon) VALUES (@Ten, @TheLoai, @SL)", ThamSo(s));
        }

        public int Sua(DauSachDTO s)
        {
            return DatabaseHelper.ExecuteNonQuery(
                "UPDATE DauSach SET TenSach = @Ten, TheLoai = @TheLoai, SoLuongCon = @SL WHERE MaSach = @Ma", ThamSo(s));
        }

        public int Xoa(int maSach)
        {
            return DatabaseHelper.ExecuteNonQuery("DELETE FROM DauSach WHERE MaSach = @Ma",
                new SqlParameter("@Ma", SqlDbType.Int) { Value = maSach });
        }

        private SqlParameter[] ThamSo(DauSachDTO s)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@Ten", SqlDbType.NVarChar, 200) { Value = s.TenSach },
                new SqlParameter("@TheLoai", SqlDbType.NVarChar, 50) { Value = s.TheLoai },
                new SqlParameter("@SL", SqlDbType.Int) { Value = s.SoLuongCon },
                new SqlParameter("@Ma", SqlDbType.Int) { Value = s.MaSach }
            };
        }
    }
}
