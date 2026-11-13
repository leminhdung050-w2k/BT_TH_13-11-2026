using System;

namespace BT6._7.DTO
{
    // Đơn thuốc / hóa đơn khám
    public class DonThuocDTO
    {
        public int MaDon { get; set; }
        public int MaHoSo { get; set; }
        public string HoTen { get; set; }
        public DateTime NgayKham { get; set; }
        public string ChanDoan { get; set; }
        public decimal TienKham { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }       // Chờ lấy thuốc / Đã thanh toán
    }
}
