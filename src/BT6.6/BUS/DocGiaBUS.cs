using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using BT6._6.DAL;
using BT6._6.DTO;

namespace BT6._6.BUS
{
    public class DocGiaBUS
    {
        private readonly DocGiaDAL dal = new DocGiaDAL();

        public List<DocGiaDTO> LayDanhSach()
        {
            return dal.LayDanhSach();
        }

        public void Them(DocGiaDTO dg)
        {
            KiemTra(dg);
            Luu(() => dal.Them(dg));
        }

        public void Sua(DocGiaDTO dg)
        {
            if (dg.MaDocGia <= 0) throw new Exception("Vui lòng chọn độc giả cần sửa!");
            KiemTra(dg);
            Luu(() => dal.Sua(dg));
        }

        public void Xoa(int maDocGia)
        {
            if (maDocGia <= 0) throw new Exception("Vui lòng chọn độc giả cần xóa!");
            try
            {
                dal.Xoa(maDocGia);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new Exception("Độc giả đã có phiếu mượn, không thể xóa!");
            }
        }

        // Kiểm tra hạn thẻ trước khi cho lập phiếu mượn mới
        public bool ConHanThe(int maDocGia)
        {
            return dal.ConHanThe(maDocGia);
        }

        private void Luu(Action thaoTac)
        {
            try
            {
                thaoTac();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new LoiNhapLieu("Email", "Email này đã được dùng cho độc giả khác!");
            }
        }

        private void KiemTra(DocGiaDTO dg)
        {
            dg.HoTen = (dg.HoTen ?? "").Trim();
            dg.Email = (dg.Email ?? "").Trim();
            if (dg.HoTen == "") throw new LoiNhapLieu("HoTen", "Họ tên độc giả không được để trống!");
            if (dg.Email != "" && !Regex.IsMatch(dg.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new LoiNhapLieu("Email", "Email không hợp lệ!");
            if (!dg.NgayHetHanThe.HasValue) throw new LoiNhapLieu("NgayHetHanThe", "Vui lòng chọn ngày hết hạn thẻ!");
        }
    }
}
