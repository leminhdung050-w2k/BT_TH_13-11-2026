using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BT6._7.DTO;

namespace BT6._7.DAL
{
    public class ThuocDAL
    {
        public List<ThuocDTO> LayDanhSach()
        {
            List<ThuocDTO> ds = new List<ThuocDTO>();
            foreach (DataRow r in DatabaseHelper.ExecuteQuery("SELECT MaThuoc, TenThuoc, DonViTinh, DonGia, SoLuongTon FROM Thuoc ORDER BY MaThuoc").Rows)
            {
                ds.Add(new ThuocDTO
                {
                    MaThuoc = (int)r["MaThuoc"],
                    TenThuoc = r["TenThuoc"].ToString(),
                    DonViTinh = r["DonViTinh"].ToString(),
                    DonGia = (decimal)r["DonGia"],
                    SoLuongTon = (int)r["SoLuongTon"]
                });
            }
            return ds;
        }

        public int Them(ThuocDTO t)
        {
            return DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO Thuoc (TenThuoc, DonViTinh, DonGia, SoLuongTon) VALUES (@Ten, @DVT, @Gia, @Ton)", ThamSo(t));
        }

        // Cập nhật thông tin / đơn giá thuốc
        public int Sua(ThuocDTO t)
        {
            return DatabaseHelper.ExecuteNonQuery(
                "UPDATE Thuoc SET TenThuoc = @Ten, DonViTinh = @DVT, DonGia = @Gia WHERE MaThuoc = @Ma", ThamSo(t));
        }

        // Nhập thêm tồn kho
        public int NhapThem(int maThuoc, int soLuong)
        {
            return DatabaseHelper.ExecuteNonQuery("UPDATE Thuoc SET SoLuongTon = SoLuongTon + @SL WHERE MaThuoc = @Ma",
                new SqlParameter("@SL", SqlDbType.Int) { Value = soLuong },
                new SqlParameter("@Ma", SqlDbType.Int) { Value = maThuoc });
        }

        public int Xoa(int maThuoc)
        {
            return DatabaseHelper.ExecuteNonQuery("DELETE FROM Thuoc WHERE MaThuoc = @Ma",
                new SqlParameter("@Ma", SqlDbType.Int) { Value = maThuoc });
        }

        private SqlParameter[] ThamSo(ThuocDTO t)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@Ten", SqlDbType.NVarChar, 150) { Value = t.TenThuoc },
                new SqlParameter("@DVT", SqlDbType.NVarChar, 20) { Value = t.DonViTinh },
                new SqlParameter("@Gia", SqlDbType.Decimal) { Value = t.DonGia, Precision = 18, Scale = 2 },
                new SqlParameter("@Ton", SqlDbType.Int) { Value = t.SoLuongTon },
                new SqlParameter("@Ma", SqlDbType.Int) { Value = t.MaThuoc }
            };
        }
    }
}
