using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT6._7.DAL;
using BT6._7.DTO;

namespace BT6._7.BUS
{
    public class DonThuocBUS
    {
        private readonly DonThuocDAL dal = new DonThuocDAL();

        // Thêm thuốc vào đơn đang kê (trùng thuốc thì cộng dồn số lượng)
        public void ThemThuoc(List<ChiTietDonDTO> don, ThuocDTO t, int soLuong, string lieuDung)
        {
            if (t == null) throw new LoiNhapLieu("Thuoc", "Vui lòng chọn thuốc!");
            if (soLuong <= 0) throw new LoiNhapLieu("SoLuong", "Số lượng phải lớn hơn 0!");
            lieuDung = (lieuDung ?? "").Trim();
            if (lieuDung == "") throw new LoiNhapLieu("LieuDung", "Vui lòng nhập liều dùng!");
            ChiTietDonDTO ct = don.FirstOrDefault(x => x.MaThuoc == t.MaThuoc);
            if (ct == null)
                don.Add(new ChiTietDonDTO { MaThuoc = t.MaThuoc, TenThuoc = t.TenThuoc, DonViTinh = t.DonViTinh, SoLuong = soLuong, DonGia = t.DonGia, LieuDung = lieuDung });
            else
            {
                ct.SoLuong += soLuong;
                ct.LieuDung = lieuDung;
            }
        }

        // Tổng chi phí = TienKham + Tổng (SoLuong x DonGia)
        public static decimal TinhTongTien(decimal tienKham, IEnumerable<ChiTietDonDTO> don)
        {
            return tienKham + don.Sum(x => x.ThanhTien);
        }

        public int LuuVaXuatDon(HoSoKhamDTO hs, string chanDoan, decimal tienKham, List<ChiTietDonDTO> don)
        {
            if (hs == null) throw new LoiNhapLieu("HoSo", "Vui lòng chọn hồ sơ bệnh nhân cần kê đơn!");
            chanDoan = (chanDoan ?? "").Trim();
            if (chanDoan == "") throw new LoiNhapLieu("ChanDoan", "Bác sĩ chưa nhập chẩn đoán!");
            if (tienKham < 0) throw new LoiNhapLieu("TienKham", "Tiền khám không hợp lệ!");
            if (don == null || don.Count == 0) throw new LoiNhapLieu("Don", "Đơn thuốc chưa có thuốc nào!");
            return dal.LuuVaXuatDon(hs.MaHoSo, chanDoan, tienKham, TinhTongTien(tienKham, don), don);
        }

        public List<DonThuocDTO> LayDanhSach(string trangThai)
        {
            return dal.LayDanhSach(trangThai);
        }

        public List<ChiTietDonDTO> LayChiTiet(int maDon)
        {
            return dal.LayChiTiet(maDon);
        }

        public void ThanhToan(DonThuocDTO don, List<ChiTietDonDTO> chiTiet)
        {
            if (don == null) throw new Exception("Vui lòng chọn đơn thuốc!");
            if (don.TrangThai == "Đã thanh toán") throw new Exception("Đơn #" + don.MaDon + " đã thanh toán!");
            don.TongTien = TinhTongTien(don.TienKham, chiTiet);
            if (dal.ThanhToan(don.MaDon, don.TongTien) == 0) throw new Exception("Đơn #" + don.MaDon + " đã được thanh toán trước đó!");
            don.TrangThai = "Đã thanh toán";
        }

        // Nội dung hóa đơn để xem / in
        public static string TaoHoaDon(DonThuocDTO don, List<ChiTietDonDTO> chiTiet)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("PHÒNG KHÁM ĐA KHOA - HÓA ĐƠN VIỆN PHÍ");
            sb.AppendLine("Mã đơn: " + don.MaDon + "    Ngày khám: " + don.NgayKham.ToString("dd/MM/yyyy HH:mm"));
            sb.AppendLine("Bệnh nhân: " + don.HoTen);
            sb.AppendLine("Chẩn đoán: " + don.ChanDoan);
            sb.AppendLine(new string('-', 60));
            int i = 1;
            foreach (ChiTietDonDTO ct in chiTiet)
            {
                sb.AppendLine(i++ + ". " + ct.TenThuoc + ": " + ct.SoLuong + " " + ct.DonViTinh + " x " + ct.DonGia.ToString("N0") + " = " + ct.ThanhTien.ToString("N0") + " đ");
                sb.AppendLine("   Liều dùng: " + ct.LieuDung);
            }
            sb.AppendLine(new string('-', 60));
            sb.AppendLine("Tiền khám: " + don.TienKham.ToString("N0") + " đ");
            sb.AppendLine("Tiền thuốc: " + chiTiet.Sum(x => x.ThanhTien).ToString("N0") + " đ");
            sb.AppendLine("TỔNG CHI PHÍ: " + TinhTongTien(don.TienKham, chiTiet).ToString("N0") + " đ");
            sb.AppendLine("Trạng thái: " + don.TrangThai);
            return sb.ToString();
        }
    }
}
