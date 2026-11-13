using System;

namespace BT6._8.DTO
{
    // Hóa đơn tính tiền bàn bida
    public class HoaDonDTO
    {
        public int MaHD { get; set; }
        public int MaBan { get; set; }
        public string TenBan { get; set; }
        public decimal GiaTheoGio { get; set; }
        public DateTime GioBatDau { get; set; }
        public DateTime? GioKetThuc { get; set; }
        public double SoGio { get; set; }            // số giờ tính tiền (làm tròn block 15 phút)
        public decimal TienGioChoi { get; set; }
        public decimal TienDichVu { get; set; }
        public bool TrangThai { get; set; }          // false: chưa thanh toán, true: đã thanh toán

        public decimal TongCong
        {
            get { return TienGioChoi + TienDichVu; }
        }
    }
}
