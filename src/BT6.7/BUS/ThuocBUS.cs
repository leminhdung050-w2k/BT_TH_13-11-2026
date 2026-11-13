using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BT6._7.DAL;
using BT6._7.DTO;

namespace BT6._7.BUS
{
    public class ThuocBUS
    {
        private readonly ThuocDAL dal = new ThuocDAL();

        public static readonly string[] DonViTinh = { "Viên", "Vỉ", "Chai", "Hộp", "Tuýp" };

        public List<ThuocDTO> LayDanhSach()
        {
            return dal.LayDanhSach();
        }

        public void Them(ThuocDTO t)
        {
            KiemTra(t);
            if (t.SoLuongTon < 0) throw new LoiNhapLieu("SoLuongTon", "Số lượng tồn phải >= 0!");
            dal.Them(t);
        }

        public void Sua(ThuocDTO t)
        {
            if (t.MaThuoc <= 0) throw new Exception("Vui lòng chọn thuốc cần sửa!");
            KiemTra(t);
            dal.Sua(t);
        }

        public void NhapThem(int maThuoc, int soLuong)
        {
            if (maThuoc <= 0) throw new Exception("Vui lòng chọn thuốc cần nhập kho!");
            if (soLuong <= 0) throw new LoiNhapLieu("NhapThem", "Số lượng nhập thêm phải lớn hơn 0!");
            dal.NhapThem(maThuoc, soLuong);
        }

        public void Xoa(int maThuoc)
        {
            if (maThuoc <= 0) throw new Exception("Vui lòng chọn thuốc cần xóa!");
            try
            {
                dal.Xoa(maThuoc);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new Exception("Thuốc đã được kê trong đơn, không thể xóa!");
            }
        }

        private void KiemTra(ThuocDTO t)
        {
            t.TenThuoc = (t.TenThuoc ?? "").Trim();
            if (t.TenThuoc == "") throw new LoiNhapLieu("TenThuoc", "Tên thuốc không được để trống!");
            if (string.IsNullOrEmpty(t.DonViTinh)) throw new LoiNhapLieu("DonViTinh", "Vui lòng chọn đơn vị tính!");
            if (t.DonGia <= 0) throw new LoiNhapLieu("DonGia", "Đơn giá phải lớn hơn 0!");
        }
    }
}
