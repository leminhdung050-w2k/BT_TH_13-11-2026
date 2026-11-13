using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using BT6._7.BUS;
using BT6._7.DTO;

namespace BT6._7.GUI
{
    public partial class frmThanhToan : Form
    {
        private readonly DonThuocBUS bus = new DonThuocBUS();
        private DonThuocDTO donChon;
        private List<ChiTietDonDTO> chiTietChon = new List<ChiTietDonDTO>();

        public frmThanhToan()
        {
            InitializeComponent();
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            cboLoc.SelectedIndex = 0;
        }

        private void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaiDon(0);
        }

        private void TaiDon(int chonMa)
        {
            try
            {
                string loc = cboLoc.SelectedIndex == 0 ? "Chờ lấy thuốc" : cboLoc.SelectedIndex == 1 ? "Đã thanh toán" : null;
                List<DonThuocDTO> ds = bus.LayDanhSach(loc);
                bindingSource1.DataSource = ds;
                dgvDon.Columns["MaDon"].HeaderText = "Mã đơn";
                dgvDon.Columns["MaDon"].FillWeight = 50;
                dgvDon.Columns["MaHoSo"].HeaderText = "Mã HS";
                dgvDon.Columns["MaHoSo"].FillWeight = 50;
                dgvDon.Columns["HoTen"].HeaderText = "Bệnh nhân";
                dgvDon.Columns["NgayKham"].HeaderText = "Ngày khám";
                dgvDon.Columns["NgayKham"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvDon.Columns["ChanDoan"].HeaderText = "Chẩn đoán";
                dgvDon.Columns["ChanDoan"].FillWeight = 150;
                dgvDon.Columns["TienKham"].HeaderText = "Tiền khám";
                dgvDon.Columns["TienKham"].DefaultCellStyle.Format = "N0";
                dgvDon.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvDon.Columns["TrangThai"].HeaderText = "Trạng thái";

                DonThuocDTO chon = ds.Find(x => x.MaDon == chonMa) ?? (ds.Count > 0 ? ds[0] : null);
                if (chon != null) bindingSource1.Position = ds.IndexOf(chon);
                ChonDon(chon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) ChonDon((DonThuocDTO)bindingSource1[e.RowIndex]);
        }

        private void ChonDon(DonThuocDTO d)
        {
            donChon = d;
            chiTietChon = d == null ? new List<ChiTietDonDTO>() : bus.LayChiTiet(d.MaDon);
            bindingSource2.DataSource = chiTietChon;
            dgvChiTiet.Columns["MaThuoc"].Visible = false;
            dgvChiTiet.Columns["TenThuoc"].HeaderText = "Thuốc";
            dgvChiTiet.Columns["TenThuoc"].FillWeight = 140;
            dgvChiTiet.Columns["DonViTinh"].HeaderText = "ĐVT";
            dgvChiTiet.Columns["DonViTinh"].FillWeight = 50;
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTiet.Columns["SoLuong"].FillWeight = 55;
            dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvChiTiet.Columns["LieuDung"].HeaderText = "Liều dùng";
            dgvChiTiet.Columns["LieuDung"].FillWeight = 200;
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            grpChiTiet.Text = d == null ? "Chi tiết đơn thuốc" : "Chi tiết đơn #" + d.MaDon + " - " + d.HoTen + " (" + d.TrangThai + ")";
            decimal tienKham = d == null ? 0 : d.TienKham;
            lblTienKham.Text = "Tiền khám: " + tienKham.ToString("N0") + " đ";
            lblTienThuoc.Text = "Tiền thuốc: " + chiTietChon.Sum(x => x.ThanhTien).ToString("N0") + " đ";
            lblTong.Text = "Tổng chi phí: " + DonThuocBUS.TinhTongTien(tienKham, chiTietChon).ToString("N0") + " đ";
            btnThanhToan.Enabled = d != null && d.TrangThai != "Đã thanh toán";
            btnIn.Enabled = d != null;
        }

        // Thanh toán: Tổng = TienKham + Tổng (SoLuong x DonGia), đơn chuyển sang "Đã thanh toán"
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (donChon == null) return;
            if (MessageBox.Show("Thanh toán đơn #" + donChon.MaDon + " - " + donChon.HoTen + "?\n" + lblTong.Text, "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                bus.ThanhToan(donChon, chiTietChon);
                MessageBox.Show(DonThuocBUS.TaoHoaDon(donChon, chiTietChon), "Hóa đơn viện phí", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDon(donChon.MaDon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // In hóa đơn: PrintDocument + PrintPreviewDialog (Toolbox)
        private void btnIn_Click(object sender, EventArgs e)
        {
            if (donChon == null) return;
            printDocument1.DocumentName = "HoaDon_Don" + donChon.MaDon;
            printPreviewDialog1.Text = "Xem trước hóa đơn đơn thuốc #" + donChon.MaDon;
            printPreviewDialog1.WindowState = FormWindowState.Normal;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (Font tieuDe = new Font("Arial", 16, FontStyle.Bold))
            using (Font chu = new Font("Arial", 11))
            {
                string[] dong = DonThuocBUS.TaoHoaDon(donChon, chiTietChon).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                float y = e.MarginBounds.Top;
                e.Graphics.DrawString(dong[0], tieuDe, Brushes.Black, e.MarginBounds.Left, y);
                y += 45;
                for (int i = 1; i < dong.Length; i++)
                {
                    e.Graphics.DrawString(dong[i], chu, Brushes.Black, e.MarginBounds.Left, y);
                    y += 24;
                }
            }
        }
    }
}
