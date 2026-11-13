using System;
using System.Collections.Generic;
using System.Linq;
using BT6._6.DAL;
using BT6._6.DTO;

namespace BT6._6.BUS
{
    public class PhieuMuonBUS
    {
        private readonly PhieuMuonDAL dal = new PhieuMuonDAL();
        private readonly DocGiaDAL docGiaDAL = new DocGiaDAL();

        public static readonly string[] TrangThai = { "Đang mượn", "Đã trả", "Quá hạn" };

        // Lọc theo độc giả và trạng thái (cập nhật "Quá hạn" trước khi lọc)
        public List<PhieuMuonDTO> LayDanhSach(int maDocGia, string trangThai)
        {
            dal.CapNhatQuaHan();
            return dal.LayDanhSach(maDocGia, trangThai);
        }

        public List<ChiTietMuonDTO> LayChiTiet(int maPhieu)
        {
            return dal.LayChiTiet(maPhieu);
        }

        // Thêm sách vào phiếu đang lập (cộng dồn nếu đã chọn)
        public void ThemSach(List<ChiTietMuonDTO> ds, DauSachDTO sach, int soLuong)
        {
            if (sach == null) throw new Exception("Vui lòng chọn sách!");
            if (soLuong <= 0) throw new Exception("Số lượng phải lớn hơn 0!");
            ChiTietMuonDTO ct = ds.FirstOrDefault(x => x.MaSach == sach.MaSach);
            int daChon = ct == null ? 0 : ct.SoLuong;
            if (daChon + soLuong > sach.SoLuongCon)
                throw new Exception("Sách \"" + sach.TenSach + "\" chỉ còn " + sach.SoLuongCon + " cuốn!");
            if (ct == null) ds.Add(new ChiTietMuonDTO { MaSach = sach.MaSach, TenSach = sach.TenSach, TheLoai = sach.TheLoai, SoLuong = soLuong });
            else ct.SoLuong += soLuong;
        }

        // Lập phiếu: kiểm tra hạn thẻ độc giả (NgayHetHanThe >= GetDate()) rồi mới lưu
        public int LapPhieu(DocGiaDTO dg, DateTime hanTra, List<ChiTietMuonDTO> chiTiet)
        {
            if (dg == null) throw new LoiNhapLieu("DocGia", "Vui lòng chọn độc giả!");
            if (!docGiaDAL.ConHanThe(dg.MaDocGia))
                throw new LoiNhapLieu("DocGia", "Thẻ của độc giả " + dg.HoTen + " đã hết hạn" +
                    (dg.NgayHetHanThe.HasValue ? " (" + dg.NgayHetHanThe.Value.ToString("dd/MM/yyyy") + ")" : "") +
                    ", không thể lập phiếu mượn mới!");
            if (hanTra.Date <= DateTime.Today) throw new LoiNhapLieu("HanTra", "Hạn trả phải sau ngày hôm nay!");
            if (chiTiet == null || chiTiet.Count == 0) throw new LoiNhapLieu("Sach", "Phiếu mượn chưa có sách nào!");
            return dal.LapPhieu(dg.MaDocGia, hanTra.Date, chiTiet);
        }

        public void TraSach(PhieuMuonDTO pm)
        {
            if (pm == null) throw new Exception("Vui lòng chọn phiếu mượn!");
            if (pm.TrangThai == "Đã trả") throw new Exception("Phiếu mượn #" + pm.MaPhieu + " đã trả sách rồi!");
            dal.TraSach(pm.MaPhieu);
        }
    }
}
