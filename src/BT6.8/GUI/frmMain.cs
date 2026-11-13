using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BT6._8.BUS;
using BT6._8.DTO;

namespace BT6._8.GUI
{
    public partial class frmMain : Form
    {
        private readonly BanBidaBUS banBUS = new BanBidaBUS();
        private readonly DichVuBUS dichVuBUS = new DichVuBUS();
        private readonly HoaDonBUS hoaDonBUS = new HoaDonBUS();

        private BanBidaDTO banChon;         // bàn đang chọn trên sơ đồ
        private HoaDonDTO hoaDon;           // hóa đơn đang mở của bàn
        private DataTable dtDichVu;         // món đã gọi (ngắt kết nối)
        private List<DichVuDTO> menu = new List<DichVuDTO>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                TaiMenu();
                TaiSoDoBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            HienBan();
            timer1.Start();
        }

        private void TaiMenu()
        {
            object chon = cboDichVu.SelectedValue;
            menu = dichVuBUS.LayDanhSach();
            cboDichVu.DisplayMember = "HienThi";
            cboDichVu.ValueMember = "MaDV";
            cboDichVu.DataSource = menu;
            if (chon != null) cboDichVu.SelectedValue = chon;
        }

        // SƠ ĐỒ BÀN: Button động sinh từ CSDL
        private void TaiSoDoBan()
        {
            List<BanBidaDTO> ds = banBUS.LayDanhSach();
            while (flpBan.Controls.Count > 0) flpBan.Controls[0].Dispose();
            foreach (BanBidaDTO b in ds)
            {
                Button btn = new Button();
                btn.Size = new Size(118, 80);
                btn.Text = b.TenBan + "\n" + b.LoaiBan + "\n" + b.TrangThai;
                btn.Tag = b;
                btn.Font = new Font(Font, FontStyle.Bold);
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = b.TrangThai == "Trống" ? Color.LightGreen : Color.Orange;   // xanh: trống, cam: đang chơi
                if (banChon != null && b.MaBan == banChon.MaBan)
                {
                    btn.FlatAppearance.BorderSize = 4;
                    btn.FlatAppearance.BorderColor = Color.Blue;
                    banChon = b;
                }
                btn.Click += btnBan_Click;
                flpBan.Controls.Add(btn);
            }
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            banChon = (BanBidaDTO)((Button)sender).Tag;
            TaiSoDoBan();
            HienBan();
        }

        // THÔNG TIN BÀN
        private void HienBan()
        {
            hoaDon = null;
            dtDichVu = null;
            if (banChon != null && banChon.TrangThai == "Đang chơi")
            {
                try
                {
                    hoaDon = hoaDonBUS.LayHoaDonDangMo(banChon.MaBan);
                    if (hoaDon != null) dtDichVu = hoaDonBUS.LayDichVuDaGoi(hoaDon.MaHD);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            lblBan.Text = banChon == null ? "Chọn 1 bàn trên sơ đồ" : banChon.TenBan + " - " + banChon.TrangThai;
            lblGioBatDau.Text = banChon == null ? ""
                : banChon.LoaiBan + ", " + banChon.GiaTheoGio.ToString("N0") + " đ/giờ" +
                  (hoaDon == null ? "" : "   |   Bắt đầu: " + hoaDon.GioBatDau.ToString("HH:mm dd/MM") + " (Mã HĐ: " + hoaDon.MaHD + ")");
            btnMoBan.Enabled = banChon != null && banChon.TrangThai == "Trống";
            btnGoiMon.Enabled = btnThanhToan.Enabled = hoaDon != null;

            bindingSource1.DataSource = dtDichVu;
            if (dtDichVu != null)
            {
                foreach (DataGridViewColumn c in dgvDichVu.Columns) c.ReadOnly = true;
                dgvDichVu.Columns["SoLuong"].ReadOnly = false;          // chỉ cho sửa số lượng
                dgvDichVu.Columns["MaHD"].Visible = false;
                dgvDichVu.Columns["MaDV"].Visible = false;
                dgvDichVu.Columns["TenDV"].HeaderText = "Món / dịch vụ";
                dgvDichVu.Columns["TenDV"].DisplayIndex = 0;
                dgvDichVu.Columns["TenDV"].FillWeight = 200;
                dgvDichVu.Columns["SoLuong"].HeaderText = "Số lượng (sửa được)";
                dgvDichVu.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                dgvDichVu.Columns["ThanhTien"].HeaderText = "Thành tiền";
                dgvDichVu.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }
            CapNhatTien();
        }

        // Tính giờ tự động: thời gian chơi và tiền tạm tính (làm tròn block 15 phút)
        private void CapNhatTien()
        {
            if (hoaDon == null)
            {
                lblThoiGian.Text = "";
                lblTienGio.Text = "Tiền giờ chơi: 0 đ";
                lblTienDV.Text = "Tiền dịch vụ: 0 đ";
                lblTong.Text = "Tạm tính: 0 đ";
                return;
            }
            DateTime bayGio = DateTime.Now;
            double soGio = HoaDonBUS.TinhSoGio(hoaDon.GioBatDau, bayGio);
            decimal tienGio = HoaDonBUS.TinhTienGio(hoaDon.GiaTheoGio, soGio);
            decimal tienDV = dtDichVu == null ? 0 : HoaDonBUS.TinhTienDichVu(dtDichVu);
            lblThoiGian.Text = "Thời gian chơi: " + HoaDonBUS.ThoiGianChoi(hoaDon.GioBatDau, bayGio) + "  (tính " + soGio.ToString("0.00") + " giờ)";
            lblTienGio.Text = "Tiền giờ chơi: " + soGio.ToString("0.00") + " giờ x " + hoaDon.GiaTheoGio.ToString("N0") + " = " + tienGio.ToString("N0") + " đ";
            lblTienDV.Text = "Tiền dịch vụ: " + tienDV.ToString("N0") + " đ";
            lblTong.Text = "Tạm tính: " + (tienGio + tienDV).ToString("N0") + " đ";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CapNhatTien();
        }

        // Mở bàn: tạo HoaDon (GioBatDau = GETDATE()), bàn chuyển "Đang chơi"
        private void btnMoBan_Click(object sender, EventArgs e)
        {
            try
            {
                hoaDonBUS.MoBan(banChon);
                TaiSoDoBan();
                HienBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Gọi thêm món / thuê phụ kiện
        private void btnGoiMon_Click(object sender, EventArgs e)
        {
            try
            {
                hoaDonBUS.GoiMon(dtDichVu, hoaDon, cboDichVu.SelectedItem as DichVuDTO, (int)numSoLuong.Value);
                numSoLuong.Value = 1;
                CapNhatTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                HienBan();
            }
        }

        // Kiểm tra giá trị số lượng gõ trực tiếp trên lưới
        private void dgvDichVu_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvDichVu.Columns[e.ColumnIndex].Name != "SoLuong" || !dgvDichVu.IsCurrentCellInEditMode) return;
            int sl;
            if (!int.TryParse(Convert.ToString(e.FormattedValue), out sl) || sl <= 0)
            {
                e.Cancel = true;
                MessageBox.Show("Số lượng phải là số nguyên lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Sửa xong số lượng -> lưu DataTable về CSDL (SqlDataAdapter.Update)
        private void dgvDichVu_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dtDichVu == null) return;
            bindingSource1.EndEdit();
            if (dtDichVu.GetChanges() == null) return;
            try
            {
                hoaDonBUS.LuuThayDoi(dtDichVu, hoaDon, menu);
                CapNhatTien();
            }
            catch (Exception ex)
            {
                dtDichVu.RejectChanges();
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CapNhatTien();
            }
        }

        // Thanh toán: SqlTransaction cập nhật hóa đơn, trừ tồn kho, trả bàn
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (hoaDon == null) return;
            if (MessageBox.Show("Thanh toán " + banChon.TenBan + "?\n" + lblTong.Text, "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                HoaDonDTO hd = hoaDonBUS.ThanhToan(hoaDon);
                string hoaDonText =
                    hd.TenBan + "  -  Mã HĐ: " + hd.MaHD + "\n" +
                    "Bắt đầu: " + hd.GioBatDau.ToString("HH:mm dd/MM") + "   Kết thúc: " + hd.GioKetThuc.Value.ToString("HH:mm dd/MM") + "\n" +
                    "Thời gian chơi: " + HoaDonBUS.ThoiGianChoi(hd.GioBatDau, hd.GioKetThuc.Value) +
                    " -> làm tròn " + hd.SoGio.ToString("0.00") + " giờ (block 15 phút)\n\n" +
                    "Tiền giờ chơi: " + hd.SoGio.ToString("0.00") + " x " + hd.GiaTheoGio.ToString("N0") + " = " + hd.TienGioChoi.ToString("N0") + " đ\n" +
                    "Tiền dịch vụ: " + hd.TienDichVu.ToString("N0") + " đ\n\n" +
                    "TỔNG THANH TOÁN: " + hd.TongCong.ToString("N0") + " đ";
                MessageBox.Show(hoaDonText, "Hóa đơn thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TaiSoDoBan();
            HienBan();
            TaiMenu();
        }

        private void btnQuanLyDV_Click(object sender, EventArgs e)
        {
            using (frmDichVu f = new frmDichVu()) f.ShowDialog();
            TaiMenu();
            HienBan();
        }
    }
}
