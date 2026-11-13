using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BT6._7.BUS;
using BT6._7.DTO;

namespace BT6._7.GUI
{
    public partial class frmKeDon : Form
    {
        private readonly BenhNhanBUS benhNhanBUS = new BenhNhanBUS();
        private readonly ThuocBUS thuocBUS = new ThuocBUS();
        private readonly DonThuocBUS donBUS = new DonThuocBUS();

        private HoSoKhamDTO hoSoChon;
        private readonly List<ChiTietDonDTO> don = new List<ChiTietDonDTO>();

        public frmKeDon()
        {
            InitializeComponent();
        }

        private void frmKeDon_Load(object sender, EventArgs e)
        {
            txtLieuDung.Text = "Sáng 1 viên, Chiều 1 viên sau ăn";
            try
            {
                TaiThuoc();
                TaiHoSo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            HienDon();
        }

        private void TaiThuoc()
        {
            cboThuoc.DisplayMember = "HienThi";
            cboThuoc.DataSource = thuocBUS.LayDanhSach();
        }

        private void TaiHoSo()
        {
            List<HoSoKhamDTO> ds = benhNhanBUS.LayHoSoChoKham();
            bindingSource1.DataSource = ds;
            foreach (DataGridViewColumn c in dgvHoSo.Columns) c.Visible = false;
            dgvHoSo.Columns["MaHoSo"].Visible = true;
            dgvHoSo.Columns["MaHoSo"].HeaderText = "Mã HS";
            dgvHoSo.Columns["MaHoSo"].FillWeight = 45;
            dgvHoSo.Columns["HoTen"].Visible = true;
            dgvHoSo.Columns["HoTen"].HeaderText = "Bệnh nhân";
            dgvHoSo.Columns["NgayKham"].Visible = true;
            dgvHoSo.Columns["NgayKham"].HeaderText = "Giờ tiếp nhận";
            dgvHoSo.Columns["NgayKham"].DefaultCellStyle.Format = "HH:mm dd/MM";
            grpHoSo.Text = "Hồ sơ chờ khám (" + ds.Count + ")";
            ChonHoSo(ds.Count > 0 ? ds[0] : null);
        }

        private void dgvHoSo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) ChonHoSo((HoSoKhamDTO)bindingSource1[e.RowIndex]);
        }

        private void ChonHoSo(HoSoKhamDTO hs)
        {
            hoSoChon = hs;
            errorProvider1.Clear();
            lblBenhNhan.Text = hs == null ? "Chưa chọn bệnh nhân"
                : hs.HoTen + (hs.NamSinh.HasValue ? " - " + (DateTime.Today.Year - hs.NamSinh.Value) + " tuổi" : "");
            txtTrieuChung.Text = hs == null ? "" : hs.TrieuChung;
            txtChanDoan.Clear();
            don.Clear();
            HienDon();
        }

        private decimal TienKham()
        {
            decimal tien;
            string s = txtTienKham.Text.Replace(",", "").Replace(".", "").Trim();
            return decimal.TryParse(s, out tien) ? tien : -1;
        }

        private void HienDon()
        {
            bindingSource2.DataSource = null;
            bindingSource2.DataSource = don;
            dgvDon.Columns["MaThuoc"].Visible = false;
            dgvDon.Columns["TenThuoc"].HeaderText = "Thuốc";
            dgvDon.Columns["TenThuoc"].FillWeight = 140;
            dgvDon.Columns["DonViTinh"].HeaderText = "ĐVT";
            dgvDon.Columns["DonViTinh"].FillWeight = 45;
            dgvDon.Columns["SoLuong"].HeaderText = "SL";
            dgvDon.Columns["SoLuong"].FillWeight = 40;
            dgvDon.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvDon.Columns["DonGia"].FillWeight = 60;
            dgvDon.Columns["LieuDung"].HeaderText = "Liều dùng";
            dgvDon.Columns["LieuDung"].FillWeight = 190;
            dgvDon.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            dgvDon.Columns["ThanhTien"].FillWeight = 70;
            CapNhatTong();
        }

        private void CapNhatTong()
        {
            decimal tienKham = Math.Max(0, TienKham());
            lblTienThuoc.Text = "Tiền thuốc: " + don.Sum(x => x.ThanhTien).ToString("N0") + " đ   +   Tiền khám: " + tienKham.ToString("N0") + " đ";
            lblTong.Text = "Tổng chi phí: " + DonThuocBUS.TinhTongTien(tienKham, don).ToString("N0") + " đ";
        }

        private void txtTienKham_TextChanged(object sender, EventArgs e)
        {
            CapNhatTong();
        }

        private void btnThemThuoc_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            try
            {
                donBUS.ThemThuoc(don, cboThuoc.SelectedItem as ThuocDTO, (int)numSoLuong.Value, txtLieuDung.Text);
                numSoLuong.Value = 1;
                HienDon();
            }
            catch (LoiNhapLieu ex)
            {
                Control o = ex.TenTruong == "LieuDung" ? txtLieuDung : ex.TenTruong == "SoLuong" ? (Control)numSoLuong : cboThuoc;
                errorProvider1.SetError(o, ex.Message);
            }
        }

        private void btnBoThuoc_Click(object sender, EventArgs e)
        {
            if (bindingSource2.Current == null) return;
            don.Remove((ChiTietDonDTO)bindingSource2.Current);
            HienDon();
        }

        // Lưu & Xuất đơn: SqlTransaction tạo DonThuoc, ChiTietDonThuoc, trừ SoLuongTon (thiếu thuốc -> Rollback)
        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            try
            {
                int maDon = donBUS.LuuVaXuatDon(hoSoChon, txtChanDoan.Text, TienKham(), don);
                MessageBox.Show("Đã lưu & xuất đơn thuốc #" + maDon + " cho bệnh nhân " + hoSoChon.HoTen + "\n" + lblTong.Text +
                                "\nTrạng thái: Chờ lấy thuốc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiThuoc();
                TaiHoSo();
            }
            catch (LoiNhapLieu ex)
            {
                Control o = ex.TenTruong == "ChanDoan" ? txtChanDoan
                          : ex.TenTruong == "TienKham" ? txtTienKham
                          : ex.TenTruong == "Don" ? (Control)dgvDon
                          : dgvHoSo;
                errorProvider1.SetError(o, ex.Message);
                o.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TaiThuoc();     // nạp lại tồn kho thực tế
            }
        }
    }
}
