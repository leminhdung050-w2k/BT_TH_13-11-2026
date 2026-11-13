namespace BT6._8.DTO
{
    // Bàn bida
    public class BanBidaDTO
    {
        public int MaBan { get; set; }
        public string TenBan { get; set; }
        public string LoaiBan { get; set; }          // Pool (Lỗ) / Carom (Phăng) / Snooker
        public decimal GiaTheoGio { get; set; }
        public string TrangThai { get; set; }        // Trống / Đang chơi
    }
}
