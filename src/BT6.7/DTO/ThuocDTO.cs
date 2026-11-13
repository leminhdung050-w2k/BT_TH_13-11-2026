namespace BT6._7.DTO
{
    // Danh mục thuốc
    public class ThuocDTO
    {
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuongTon { get; set; }

        public string HienThi
        {
            get { return TenThuoc + " - " + DonGia.ToString("N0") + " đ/" + DonViTinh + " (tồn " + SoLuongTon + ")"; }
        }
    }
}
