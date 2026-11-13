using System;

namespace BT6._6.DTO
{
    // Phiếu mượn sách
    public class PhieuMuonDTO
    {
        public int MaPhieu { get; set; }
        public int MaDocGia { get; set; }
        public string HoTen { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime HanTra { get; set; }
        public string TrangThai { get; set; }       // Đang mượn / Đã trả / Quá hạn
        public string SachMuon { get; set; }        // danh sách sách trong phiếu

        // Số ngày quá hạn (0 nếu chưa quá hạn hoặc đã trả)
        public int SoNgayQuaHan
        {
            get { return TrangThai == "Đã trả" ? 0 : Math.Max(0, (DateTime.Today - HanTra.Date).Days); }
        }
    }
}
