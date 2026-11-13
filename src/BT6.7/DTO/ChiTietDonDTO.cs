namespace BT6._7.DTO
{
    // Một dòng thuốc trong đơn
    public class ChiTietDonDTO
    {
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string LieuDung { get; set; }

        public decimal ThanhTien
        {
            get { return SoLuong * DonGia; }
        }
    }
}
