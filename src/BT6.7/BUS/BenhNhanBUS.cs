using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using BT6._7.DAL;
using BT6._7.DTO;

namespace BT6._7.BUS
{
    public class BenhNhanBUS
    {
        private readonly BenhNhanDAL dal = new BenhNhanDAL();

        // Bỏ khoảng trắng, dấu chấm, gạch ngang trong số điện thoại
        public static string ChuanHoaSDT(string sdt)
        {
            return Regex.Replace(sdt ?? "", @"[\s\.\-]", "");
        }

        public BenhNhanDTO TraCuu(string sdt)
        {
            sdt = ChuanHoaSDT(sdt);
            KiemTraSDT(sdt);
            return dal.TimTheoSDT(sdt);
        }

        // Tiếp nhận: bệnh nhân mới thì thêm mới, bệnh nhân cũ thì cập nhật; sau đó tạo HoSoKham
        public int TiepNhan(BenhNhanDTO bn, string trieuChung)
        {
            bn.SoDienThoai = ChuanHoaSDT(bn.SoDienThoai);
            bn.HoTen = (bn.HoTen ?? "").Trim();
            trieuChung = (trieuChung ?? "").Trim();
            KiemTraSDT(bn.SoDienThoai);
            if (bn.HoTen == "") throw new LoiNhapLieu("HoTen", "Họ tên bệnh nhân không được để trống!");
            if (bn.NamSinh.HasValue && (bn.NamSinh < 1900 || bn.NamSinh > DateTime.Today.Year))
                throw new LoiNhapLieu("NamSinh", "Năm sinh phải từ 1900 đến " + DateTime.Today.Year + "!");
            if (trieuChung == "") throw new LoiNhapLieu("TrieuChung", "Vui lòng nhập triệu chứng khi tiếp nhận!");

            if (bn.MaBN > 0) return dal.CapNhatVaTaoHoSo(bn, trieuChung);
            try
            {
                return dal.ThemMoiVaTaoHoSo(bn, trieuChung);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new LoiNhapLieu("SoDienThoai", "Số điện thoại đã thuộc bệnh nhân khác, hãy bấm Tra cứu!");
            }
        }

        public List<HoSoKhamDTO> LayHoSoChoKham()
        {
            return dal.LayHoSoChoKham();
        }

        public List<HoSoKhamDTO> LayHoSoCuaBN(int maBN)
        {
            return dal.LayHoSoCuaBN(maBN);
        }

        public int DemChoKham()
        {
            return dal.DemChoKham();
        }

        private static void KiemTraSDT(string sdt)
        {
            if (sdt == "") throw new LoiNhapLieu("SoDienThoai", "Vui lòng nhập số điện thoại!");
            if (!Regex.IsMatch(sdt, @"^0\d{9}$")) throw new LoiNhapLieu("SoDienThoai", "Số điện thoại phải gồm 10 chữ số, bắt đầu bằng 0!");
        }
    }
}
