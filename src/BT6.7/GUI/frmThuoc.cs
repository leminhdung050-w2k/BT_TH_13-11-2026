using System;
using System.Drawing;
using System.Windows.Forms;
using BT6._7.BUS;
using BT6._7.DTO;

namespace BT6._7.GUI
{
    public partial class frmThuoc : Form
    {
        private readonly ThuocBUS bus = new ThuocBUS();
        private int maChon = -1;

        public frmThuoc()
        {
            InitializeComponent();
        }

        private void frmThuoc_Load(object sender, EventArgs e)
        {
            cboDonVi.Items.AddRange(ThuocBUS.DonViTinh);
            TaiDuLieu();
            LamMoi();
        }

        private void TaiDuLieu()
        {
            try
            {
                bindingSource1.DataSource = bus.LayDanhSach();
                dgvThuoc.Columns["MaThuoc"].HeaderText = "Mã";
                dgvThuoc.Columns["MaThuoc"].FillWeight = 40;
                dgvThuoc.Columns["TenThuoc"].HeaderText = "Tên thuốc";
                dgvThuoc.Columns["TenThuoc"].FillWeight = 200;
                dgvThuoc.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
                dgvThuoc.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvThuoc.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                dgvThuoc.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
                dgvThuoc.Columns["HienThi"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thuốc sắp hết (tồn dưới 10) tô đỏ
        private void dgvThuoc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            ThuocDTO t = dgvThuoc.Rows[e.RowIndex].DataBoundItem as ThuocDTO;
            if (t != null && t.SoLuongTon < 10) e.CellStyle.ForeColor = Color.Red;
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ThuocDTO t = (ThuocDTO)bindingSource1[e.RowIndex];
            maChon = t.MaThuoc;
            txtTenThuoc.Text = t.TenThuoc;
            cboDonVi.SelectedItem = t.DonViTinh;
            txtDonGia.Text = t.DonGia.ToString("0");
            txtTon.Text = t.SoLuongTon.ToString();
            txtTon.ReadOnly = true;         // tồn kho chỉ tăng qua "Nhập kho"
            numNhapThem.Value = 0;
            errorProvider1.Clear();
        }

        private ThuocDTO DocForm()
        {
            decimal gia = 0;
            string s = txtDonGia.Text.Replace(",", "").Replace(".", "").Trim();
            if (s == "" || !decimal.TryParse(s, out gia)) throw new LoiNhapLieu("DonGia", "Đơn giá phải là số!");
            int ton;
            if (!int.TryParse(txtTon.Text.Trim(), out ton)) throw new LoiNhapLieu("SoLuongTon", "Số lượng tồn phải là số nguyên!");
            return new ThuocDTO
            {
                MaThuoc = maChon,
                TenThuoc = txtTenThuoc.Text,
                DonViTinh = cboDonVi.SelectedItem == null ? "" : cboDonVi.SelectedItem.ToString(),
                DonGia = gia,
                SoLuongTon = ton
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (maChon > 0)
            {
                MessageBox.Show("Đang chọn thuốc có sẵn, bấm \"Làm mới\" trước khi thêm thuốc mới!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            XuLy(() => bus.Them(DocForm()), "Thêm thuốc mới thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            XuLy(() => bus.Sua(DocForm()), "Cập nhật thông tin / đơn giá thuốc thành công!");
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            int sl = (int)numNhapThem.Value;
            XuLy(() => bus.NhapThem(maChon, sl), "Đã nhập thêm " + sl + " vào kho!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maChon > 0 && MessageBox.Show("Xóa thuốc \"" + txtTenThuoc.Text + "\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            XuLy(() => bus.Xoa(maChon), "Xóa thuốc thành công!");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            maChon = -1;
            txtTenThuoc.Clear();
            cboDonVi.SelectedIndex = 0;
            txtDonGia.Clear();
            txtTon.Text = "0";
            txtTon.ReadOnly = false;
            numNhapThem.Value = 0;
            errorProvider1.Clear();
            txtTenThuoc.Focus();
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
                Control o = ex.TenTruong == "TenThuoc" ? txtTenThuoc
                          : ex.TenTruong == "DonViTinh" ? cboDonVi
                          : ex.TenTruong == "DonGia" ? txtDonGia
                          : ex.TenTruong == "NhapThem" ? (Control)numNhapThem
                          : txtTon;
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
