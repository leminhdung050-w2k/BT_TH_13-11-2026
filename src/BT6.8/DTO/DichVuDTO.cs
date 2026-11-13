namespace BT6._8.DTO
{
    // Menu dịch vụ: đồ ăn, nước uống, thuê gậy
    public class DichVuDTO
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public decimal DonGia { get; set; }
        public int? TonKho { get; set; }             // null: dịch vụ thuê, không trừ kho

        public bool KhongTruKho
        {
            get { return !TonKho.HasValue; }
        }

        public string HienThi
        {
            get { return TenDV + " - " + DonGia.ToString("N0") + " đ" + (TonKho.HasValue ? " (còn " + TonKho + ")" : " (thuê)"); }
        }
    }
}
