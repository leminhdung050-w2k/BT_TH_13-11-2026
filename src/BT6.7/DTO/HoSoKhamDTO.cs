using System;

namespace BT6._7.DTO
{
    // Hồ sơ tiếp nhận khám (kèm thông tin bệnh nhân)
    public class HoSoKhamDTO
    {
        public int MaHoSo { get; set; }
        public int MaBN { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public int? NamSinh { get; set; }
        public DateTime NgayKham { get; set; }
        public string TrieuChung { get; set; }
        public string ChanDoan { get; set; }
        public string TrangThai { get; set; }       // Chờ khám / Chờ lấy thuốc / Đã thanh toán

        public string HienThi
        {
            get { return "#" + MaHoSo + " - " + HoTen + " (" + NgayKham.ToString("HH:mm dd/MM") + ")"; }
        }
    }
}
