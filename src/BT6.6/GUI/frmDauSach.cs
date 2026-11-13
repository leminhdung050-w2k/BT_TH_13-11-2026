using System;
using System.Drawing;
using System.Windows.Forms;
using BT6._6.BUS;
using BT6._6.DTO;

namespace BT6._6.GUI
{
    public partial class frmDauSach : Form
    {
        private readonly DauSachBUS bus = new DauSachBUS();
        private int maChon = -1;

        public frmDauSach()
        {
            InitializeComponent();
        }

        private void frmDauSach_Load(object sender, EventArgs e)
        {
            cboTheLoai.Items.AddRange(DauSachBUS.TheLoai);
            TaiDuLieu();
            LamMoi();
        }

        private void TaiDuLieu()
        {
            try
            {
                bindingSource1.DataSource = bus.LayDanhSach();
                dgvDauSach.Columns["MaSach"].HeaderText = "Mã sách";
                dgvDauSach.Columns["MaSach"].FillWeight = 50;
                dgvDauSach.Columns["TenSach"].HeaderText = "Tên sách";
                dgvDauSach.Columns["TenSach"].FillWeight = 220;
                dgvDauSach.Columns["TheLoai"].HeaderText = "Thể loại";
                dgvDauSach.Columns["TheLoai"].FillWeight = 130;
                dgvDauSach.Columns["SoLuongCon"].HeaderText = "Số lượng còn";
                dgvDauSach.Columns["HienThi"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sách đã hết (SoLuongCon = 0) tô đỏ
        private void dgvDauSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DauSachDTO s = dgvDauSach.Rows[e.RowIndex].DataBoundItem as DauSachDTO;
            if (s != null && s.SoLuongCon == 0) e.CellStyle.ForeColor = Color.Red;
        }

        private void dgvDauSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DauSachDTO s = (DauSachDTO)bindingSource1[e.RowIndex];
            maChon = s.MaSach;
            txtTenSach.Text = s.TenSach;
            cboTheLoai.SelectedItem = s.TheLoai;
            numSoLuong.Value = Math.Min(numSoLuong.Maximum, s.SoLuongCon);
            errorProvider1.Clear();
        }

        private DauSachDTO DocForm()
        {
            return new DauSachDTO
            {
                MaSach = maChon,
                TenSach = txtTenSach.Text,
                TheLoai = cboTheLoai.SelectedItem == null ? "" : cboTheLoai.SelectedItem.ToString(),
                SoLuongCon = (int)numSoLuong.Value
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            XuLy(() => bus.Them(DocForm()), "Thêm đầu sách thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            XuLy(() => bus.Sua(DocForm()), "Cập nhật đầu sách thành công!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maChon > 0 && MessageBox.Show("Xóa sách \"" + txtTenSach.Text + "\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            XuLy(() => bus.Xoa(maChon), "Xóa đầu sách thành công!");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            maChon = -1;
            txtTenSach.Clear();
            cboTheLoai.SelectedIndex = -1;
            numSoLuong.Value = 0;
            errorProvider1.Clear();
            txtTenSach.Focus();
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
                Control o = ex.TenTruong == "TenSach" ? txtTenSach : ex.TenTruong == "TheLoai" ? (Control)cboTheLoai : numSoLuong;
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
