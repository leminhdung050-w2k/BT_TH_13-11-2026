using System;

namespace BT6._6.DTO
{
    // Độc giả
    public class DocGiaDTO
    {
        public int MaDocGia { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public DateTime? NgayHetHanThe { get; set; }

        // Thẻ còn hạn khi NgayHetHanThe >= hôm nay
        public string TinhTrangThe
        {
            get { return NgayHetHanThe.HasValue && NgayHetHanThe.Value.Date >= DateTime.Today ? "Còn hạn" : "Hết hạn"; }
        }

        public string HienThi
        {
            get { return MaDocGia + " - " + HoTen; }
        }
    }
}
