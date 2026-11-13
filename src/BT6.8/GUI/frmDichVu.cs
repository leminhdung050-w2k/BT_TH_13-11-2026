using System;
using System.Drawing;
using System.Windows.Forms;
using BT6._8.BUS;
using BT6._8.DTO;

namespace BT6._8.GUI
{
    public partial class frmDichVu : Form
    {
        private readonly DichVuBUS bus = new DichVuBUS();
        private int maChon = -1;

        public frmDichVu()
        {
            InitializeComponent();
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            TaiDuLieu();
            LamMoi();
        }

        private void TaiDuLieu()
        {
            try
            {
                bindingSource1.DataSource = bus.LayDanhSach();
                dgvDichVu.Columns["MaDV"].HeaderText = "Mã";
                dgvDichVu.Columns["MaDV"].FillWeight = 40;
                dgvDichVu.Columns["TenDV"].HeaderText = "Tên món / dịch vụ";
                dgvDichVu.Columns["TenDV"].FillWeight = 200;
                dgvDichVu.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                dgvDichVu.Columns["TonKho"].HeaderText = "Tồn kho";
                dgvDichVu.Columns["TonKho"].DefaultCellStyle.NullValue = "Không trừ kho";
                dgvDichVu.Columns["KhongTruKho"].HeaderText = "Dịch vụ thuê";
                dgvDichVu.Columns["KhongTruKho"].FillWeight = 70;
                dgvDichVu.Columns["HienThi"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tô đỏ món sắp hết (tồn dưới 5)
        private void dgvDichVu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DichVuDTO dv = dgvDichVu.Rows[e.RowIndex].DataBoundItem as DichVuDTO;
            if (dv != null && dv.TonKho.HasValue && dv.TonKho < 5) e.CellStyle.ForeColor = Color.Red;
        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DichVuDTO dv = (DichVuDTO)bindingSource1[e.RowIndex];
            maChon = dv.MaDV;
            txtTenDV.Text = dv.TenDV;
            txtDonGia.Text = dv.DonGia.ToString("0");
            chkThue.Checked = dv.KhongTruKho;
            txtTonKho.Text = dv.TonKho.HasValue ? dv.TonKho.ToString() : "";
            errorProvider1.Clear();
        }

        // Dịch vụ thuê (gậy, găng tay) không quản lý tồn kho
        private void chkThue_CheckedChanged(object sender, EventArgs e)
        {
            txtTonKho.Enabled = !chkThue.Checked;
            if (chkThue.Checked) txtTonKho.Clear();
            else if (txtTonKho.Text == "") txtTonKho.Text = "0";
        }

        private DichVuDTO DocForm()
        {
            decimal gia = 0;
            string s = txtDonGia.Text.Replace(",", "").Replace(".", "").Trim();
            if (s != "" && !decimal.TryParse(s, out gia)) throw new LoiNhapLieu("DonGia", "Đơn giá phải là số!");
            int? ton = null;
            if (!chkThue.Checked)
            {
                int t;
                if (!int.TryParse(txtTonKho.Text.Trim(), out t)) throw new LoiNhapLieu("TonKho", "Tồn kho phải là số nguyên!");
                ton = t;
            }
            return new DichVuDTO { MaDV = maChon, TenDV = txtTenDV.Text, DonGia = gia, TonKho = ton };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            XuLy(() => bus.Them(DocForm()), "Thêm món / dịch vụ thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            XuLy(() => bus.Sua(DocForm()), "Cập nhật món / dịch vụ thành công!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maChon > 0 && MessageBox.Show("Xóa \"" + txtTenDV.Text + "\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            XuLy(() => bus.Xoa(maChon), "Xóa món / dịch vụ thành công!");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            maChon = -1;
            txtTenDV.Clear();
            txtDonGia.Clear();
            chkThue.Checked = false;
            txtTonKho.Text = "0";
            errorProvider1.Clear();
            txtTenDV.Focus();
        }

        private void XuLy(Action thaoTac, string thongBao)
        {
            errorProvider1.Clear();
            try
            {
                thaoTac();
                TaiDuLieu();
                LamMoi();
                MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (LoiNhapLieu ex)
            {
                Control o = ex.TenTruong == "TenDV" ? txtTenDV : ex.TenTruong == "DonGia" ? txtDonGia : txtTonKho;
                errorProvider1.SetError(o, ex.Message);
                o.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
