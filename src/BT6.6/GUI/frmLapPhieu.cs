using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BT6._6.BUS;
using BT6._6.DTO;

namespace BT6._6.GUI
{
    public partial class frmLapPhieu : Form
    {
        private readonly PhieuMuonBUS bus = new PhieuMuonBUS();
        private readonly DocGiaBUS docGiaBUS = new DocGiaBUS();
        private readonly DauSachBUS dauSachBUS = new DauSachBUS();
        private readonly List<ChiTietMuonDTO> dsChon = new List<ChiTietMuonDTO>();

        public int MaPhieuMoi { get; private set; }

        public frmLapPhieu()
        {
            InitializeComponent();
        }

        private void frmLapPhieu_Load(object sender, EventArgs e)
        {
            try
            {
                cboDocGia.DisplayMember = "HienThi";
                cboDocGia.DataSource = docGiaBUS.LayDanhSach();
                cboSach.DisplayMember = "HienThi";
                cboSach.DataSource = dauSachBUS.LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dtpHanTra.Value = DateTime.Today.AddDays(14);    // mặc định mượn 14 ngày
            HienDanhSach();
        }

        // Hiện tình trạng thẻ của độc giả đang chọn
        private void cboDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            DocGiaDTO dg = cboDocGia.SelectedItem as DocGiaDTO;
            errorProvider1.SetError(cboDocGia, "");
            if (dg == null) { lblThe.Text = ""; return; }
            string han = dg.NgayHetHanThe.HasValue ? dg.NgayHetHanThe.Value.ToString("dd/MM/yyyy") : "?";
            lblThe.Text = "Thẻ " + dg.TinhTrangThe.ToLower() + " (đến " + han + ")";
            lblThe.ForeColor = dg.TinhTrangThe == "Còn hạn" ? Color.Green : Color.Red;
        }

        private void HienDanhSach()
        {
            bindingSource1.DataSource = null;
            bindingSource1.DataSource = dsChon;
            dgvChon.Columns["MaSach"].HeaderText = "Mã sách";
            dgvChon.Columns["MaSach"].FillWeight = 50;
            dgvChon.Columns["TenSach"].HeaderText = "Tên sách";
            dgvChon.Columns["TenSach"].FillWeight = 200;
            dgvChon.Columns["TheLoai"].HeaderText = "Thể loại";
            dgvChon.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChon.Columns["SoLuong"].FillWeight = 60;
            grpSach.Text = "Chọn sách mượn (" + dsChon.Count + " đầu sách)";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                bus.ThemSach(dsChon, cboSach.SelectedItem as DauSachDTO, (int)numSoLuong.Value);
                errorProvider1.SetError(dgvChon, "");
                numSoLuong.Value = 1;
                HienDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current == null) return;
            dsChon.Remove((ChiTietMuonDTO)bindingSource1.Current);
            HienDanhSach();
        }

        // Lưu phiếu: kiểm tra hạn thẻ -> SqlTransaction tạo phiếu, chi tiết, trừ SoLuongCon
        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            try
            {
                DocGiaDTO dg = cboDocGia.SelectedItem as DocGiaDTO;
                MaPhieuMoi = bus.LapPhieu(dg, dtpHanTra.Value, dsChon);
                MessageBox.Show("Đã lập phiếu mượn #" + MaPhieuMoi + " cho độc giả " + dg.HoTen +
                                "\nHạn trả: " + dtpHanTra.Value.ToString("dd/MM/yyyy"), "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (LoiNhapLieu ex)
            {
                Control o = ex.TenTruong == "DocGia" ? cboDocGia : ex.TenTruong == "HanTra" ? (Control)dtpHanTra : dgvChon;
                errorProvider1.SetError(o, ex.Message);
                MessageBox.Show(ex.Message, "Không thể lập phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
