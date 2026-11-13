using System;
using System.Drawing;
using System.Windows.Forms;
using BT6._6.BUS;
using BT6._6.DTO;

namespace BT6._6.GUI
{
    public partial class frmDocGia : Form
    {
        private readonly DocGiaBUS bus = new DocGiaBUS();
        private int maChon = -1;

        public frmDocGia()
        {
            InitializeComponent();
        }

        private void frmDocGia_Load(object sender, EventArgs e)
        {
            TaiDuLieu();
            LamMoi();
        }

        private void TaiDuLieu()
        {
            try
            {
                bindingSource1.DataSource = bus.LayDanhSach();
                dgvDocGia.Columns["MaDocGia"].HeaderText = "Mã ĐG";
                dgvDocGia.Columns["MaDocGia"].FillWeight = 50;
                dgvDocGia.Columns["HoTen"].HeaderText = "Họ tên";
                dgvDocGia.Columns["HoTen"].FillWeight = 150;
                dgvDocGia.Columns["Email"].FillWeight = 170;
                dgvDocGia.Columns["NgayHetHanThe"].HeaderText = "Hết hạn thẻ";
                dgvDocGia.Columns["NgayHetHanThe"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvDocGia.Columns["TinhTrangThe"].HeaderText = "Tình trạng thẻ";
                dgvDocGia.Columns["HienThi"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thẻ hết hạn tô đỏ
        private void dgvDocGia_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DocGiaDTO dg = dgvDocGia.Rows[e.RowIndex].DataBoundItem as DocGiaDTO;
            if (dg != null && dg.TinhTrangThe == "Hết hạn") e.CellStyle.ForeColor = Color.Red;
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DocGiaDTO dg = (DocGiaDTO)bindingSource1[e.RowIndex];
            maChon = dg.MaDocGia;
            txtHoTen.Text = dg.HoTen;
            txtEmail.Text = dg.Email;
            dtpHetHan.Value = dg.NgayHetHanThe ?? DateTime.Today;
            errorProvider1.Clear();
        }

        private DocGiaDTO DocForm()
        {
            return new DocGiaDTO
            {
                MaDocGia = maChon,
                HoTen = txtHoTen.Text,
                Email = txtEmail.Text,
                NgayHetHanThe = dtpHetHan.Value.Date
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            XuLy(() => bus.Them(DocForm()), "Thêm độc giả thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            XuLy(() => bus.Sua(DocForm()), "Cập nhật độc giả thành công!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maChon > 0 && MessageBox.Show("Xóa độc giả \"" + txtHoTen.Text + "\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            XuLy(() => bus.Xoa(maChon), "Xóa độc giả thành công!");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            maChon = -1;
            txtHoTen.Clear();
            txtEmail.Clear();
            dtpHetHan.Value = DateTime.Today.AddYears(1);    // thẻ mới mặc định có hạn 1 năm
            errorProvider1.Clear();
            txtHoTen.Focus();
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
                Control o = ex.TenTruong == "HoTen" ? txtHoTen : ex.TenTruong == "Email" ? (Control)txtEmail : dtpHetHan;
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
