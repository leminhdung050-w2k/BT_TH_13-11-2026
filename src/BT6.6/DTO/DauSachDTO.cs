namespace BT6._6.DTO
{
    // Đầu sách
    public class DauSachDTO
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string TheLoai { get; set; }
        public int SoLuongCon { get; set; }

        public string HienThi
        {
            get { return TenSach + " (còn " + SoLuongCon + ")"; }
        }
    }
}
