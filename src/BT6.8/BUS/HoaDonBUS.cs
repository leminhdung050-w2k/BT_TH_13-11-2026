using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BT6._8.DAL;
using BT6._8.DTO;

namespace BT6._8.BUS
{
    public class HoaDonBUS
    {
        private readonly HoaDonDAL dal = new HoaDonDAL();

        public const int BLOCK_PHUT = 15;      // làm tròn thời gian chơi theo block 15 phút

        public int MoBan(BanBidaDTO ban)
        {
            if (ban == null) throw new Exception("Vui lòng chọn bàn!");
            if (ban.TrangThai != "Trống") throw new Exception(ban.TenBan + " đang có khách chơi!");
            return dal.MoBan(ban.MaBan);
        }

        public HoaDonDTO LayHoaDonDangMo(int maBan)
        {
            return dal.LayHoaDonDangMo(maBan);
        }

        public DataTable LayDichVuDaGoi(int maHD)
        {
            return dal.LayDichVuDaGoi(maHD);
        }

        // Số giờ = GioKetThuc - GioBatDau, làm tròn lên theo block 15 phút (tối thiểu 1 block)
        public static double TinhSoGio(DateTime gioBatDau, DateTime gioKetThuc)
        {
            double phut = Math.Max(0, (gioKetThuc - gioBatDau).TotalMinutes);
            int soBlock = Math.Max(1, (int)Math.Ceiling(phut / BLOCK_PHUT));
            return soBlock * BLOCK_PHUT / 60.0;
        }

        // TienGioChoi = (Số giờ) x GiaTheoGio
        public static decimal TinhTienGio(decimal giaTheoGio, double soGio)
        {
            return Math.Round(giaTheoGio * (decimal)soGio, 0);
        }

        public static decimal TinhTienDichVu(DataTable dt)
        {
            return dt.Rows.Cast<DataRow>().Where(r => r.RowState != DataRowState.Deleted)
                     .Sum(r => (int)r["SoLuong"] * (decimal)r["DonGia"]);
        }

        public static string ThoiGianChoi(DateTime gioBatDau, DateTime den)
        {
            TimeSpan t = den - gioBatDau;
            return (int)t.TotalHours + " giờ " + t.Minutes + " phút";
        }

        // Gọi món mới / tăng số lượng: sửa trên DataTable (ngắt kết nối) rồi đồng bộ về CSDL
        public void GoiMon(DataTable dt, HoaDonDTO hd, DichVuDTO dv, int soLuong)
        {
            if (hd == null) throw new Exception("Bàn chưa được mở!");
            if (dv == null) throw new Exception("Vui lòng chọn món / dịch vụ!");
            if (soLuong <= 0) throw new Exception("Số lượng phải lớn hơn 0!");

            DataRow r = dt.Rows.Cast<DataRow>().FirstOrDefault(x => x.RowState != DataRowState.Deleted && (int)x["MaDV"] == dv.MaDV);
            int daGoi = r == null ? 0 : (int)r["SoLuong"];
            if (dv.TonKho.HasValue && daGoi + soLuong > dv.TonKho.Value)
                throw new Exception("Không đủ hàng: " + dv.TenDV + " chỉ còn " + dv.TonKho + " (bàn đã gọi " + daGoi + ")!");

            if (r == null)
            {
                r = dt.NewRow();
                r["MaHD"] = hd.MaHD;
                r["MaDV"] = dv.MaDV;
                r["SoLuong"] = soLuong;
                r["DonGia"] = dv.DonGia;
                r["TenDV"] = dv.TenDV;
                dt.Rows.Add(r);
            }
            else
            {
                r["SoLuong"] = daGoi + soLuong;
            }
            dal.LuuDichVu(dt, hd.MaHD);
            dt.AcceptChanges();
        }

        // Lưu số lượng sửa trực tiếp trên lưới (kiểm tra tồn kho rồi adapter.Update)
        public void LuuThayDoi(DataTable dt, HoaDonDTO hd, List<DichVuDTO> menu)
        {
            foreach (DataRow r in dt.Rows)
            {
                if (r.RowState != DataRowState.Modified) continue;
                int sl = (int)r["SoLuong"];
                if (sl <= 0) throw new Exception("Số lượng phải lớn hơn 0!");
                DichVuDTO dv = menu.FirstOrDefault(x => x.MaDV == (int)r["MaDV"]);
                if (dv != null && dv.TonKho.HasValue && sl > dv.TonKho.Value)
                    throw new Exception("Không đủ hàng: " + dv.TenDV + " chỉ còn " + dv.TonKho + "!");
            }
            dal.LuuDichVu(dt, hd.MaHD);
            dt.AcceptChanges();
        }

        // Thanh toán: tính giờ chơi, tiền giờ rồi chạy SqlTransaction (xem HoaDonDAL.ThanhToan)
        public HoaDonDTO ThanhToan(HoaDonDTO hd)
        {
            if (hd == null) throw new Exception("Bàn chưa có hóa đơn để thanh toán!");
            hd.GioKetThuc = DateTime.Now;
            hd.SoGio = TinhSoGio(hd.GioBatDau, hd.GioKetThuc.Value);
            hd.TienGioChoi = TinhTienGio(hd.GiaTheoGio, hd.SoGio);
            dal.ThanhToan(hd);
            return hd;
        }
    }
}
