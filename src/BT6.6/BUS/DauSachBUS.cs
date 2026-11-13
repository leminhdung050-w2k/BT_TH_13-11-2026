using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BT6._6.DAL;
using BT6._6.DTO;

namespace BT6._6.BUS
{
    public class DauSachBUS
    {
        private readonly DauSachDAL dal = new DauSachDAL();

        public static readonly string[] TheLoai = { "Công nghệ thông tin", "Kinh tế", "Văn học" };

        public List<DauSachDTO> LayDanhSach()
        {
            return dal.LayDanhSach();
        }

        public void Them(DauSachDTO s)
        {
            KiemTra(s);
            dal.Them(s);
        }

        public void Sua(DauSachDTO s)
        {
            if (s.MaSach <= 0) throw new Exception("Vui lòng chọn đầu sách cần sửa!");
            KiemTra(s);
            dal.Sua(s);
        }

        public void Xoa(int maSach)
        {
            if (maSach <= 0) throw new Exception("Vui lòng chọn đầu sách cần xóa!");
            try
            {
                dal.Xoa(maSach);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new Exception("Đầu sách đã có trong phiếu mượn, không thể xóa!");
            }
        }

        private void KiemTra(DauSachDTO s)
        {
            s.TenSach = (s.TenSach ?? "").Trim();
            s.TheLoai = (s.TheLoai ?? "").Trim();
            if (s.TenSach == "") throw new LoiNhapLieu("TenSach", "Tên sách không được để trống!");
            if (s.TheLoai == "") throw new LoiNhapLieu("TheLoai", "Vui lòng chọn thể loại!");
            if (s.SoLuongCon < 0) throw new LoiNhapLieu("SoLuongCon", "Số lượng còn phải >= 0!");
        }
    }
}
