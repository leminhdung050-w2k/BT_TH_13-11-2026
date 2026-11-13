using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BT6._6.BUS;
using BT6._6.DTO;

namespace BT6._6.GUI
{
    public partial class frmMuonTra : Form
    {
        private readonly PhieuMuonBUS bus = new PhieuMuonBUS();
        private readonly DocGiaBUS docGiaBUS = new DocGiaBUS();
        private PhieuMuonDTO phieuChon;
        private bool dangNap;

        public frmMuonTra()
        {
            InitializeComponent();
        }

        private void frmMuonTra_Load(object sender, EventArgs e)
        {
            try
            {
                dangNap = true;
                List<DocGiaDTO> ds = docGiaBUS.LayDanhSach();
                ds.Insert(0, new DocGiaDTO { MaDocGia = 0, HoTen = "Tất cả độc giả" });
                cboDocGia.DisplayMember = "HienThi";
                cboDocGia.ValueMember = "MaDocGia";
                cboDocGia.DataSource = ds;
                cboTrangThai.SelectedIndex = 0;
                dangNap = false;
                TaiPhieu(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!dangNap) TaiPhieu(0);
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!dangNap) TaiPhieu(0);
        }

        // Lọc phiếu mượn theo độc giả và trạng thái
        private void TaiPhieu(int chonMa)
        {
            try
            {
                int maDocGia = cboDocGia.SelectedValue == null ? 0 : (int)cboDocGia.SelectedValue;
                string trangThai = cboTrangThai.SelectedIndex <= 0 ? "" : cboTrangThai.SelectedItem.ToString();
                List<PhieuMuonDTO> ds = bus.LayDanhSach(maDocGia, trangThai);
                bindingSource1.DataSource = ds;
                dgvPhieu.Columns["MaPhieu"].HeaderText = "Mã phiếu";
                dgvPhieu.Columns["MaPhieu"].FillWeight = 55;
                dgvPhieu.Columns["MaDocGia"].Visible = false;
                dgvPhieu.Columns["HoTen"].HeaderText = "Độc giả";
                dgvPhieu.Columns["HoTen"].FillWeight = 110;
                dgvPhieu.Columns["NgayMuon"].HeaderText = "Ngày mượn";
                dgvPhieu.Columns["NgayMuon"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPhieu.Columns["NgayMuon"].FillWeight = 75;
                dgvPhieu.Columns["HanTra"].HeaderText = "Hạn trả";
                dgvPhieu.Columns["HanTra"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPhieu.Columns["HanTra"].FillWeight = 75;
                dgvPhieu.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvPhieu.Columns["TrangThai"].FillWeight = 75;
                dgvPhieu.Columns["SachMuon"].HeaderText = "Sách mượn";
                dgvPhieu.Columns["SachMuon"].FillWeight = 230;
                dgvPhieu.Columns["SoNgayQuaHan"].HeaderText = "Quá hạn (ngày)";
                dgvPhieu.Columns["SoNgayQuaHan"].FillWeight = 65;
                lblSoPhieu.Text = ds.Count + " phiếu";

                PhieuMuonDTO chon = ds.Find(x => x.MaPhieu == chonMa) ?? (ds.Count > 0 ? ds[0] : null);
                if (chon != null) bindingSource1.Position = ds.IndexOf(chon);
                ChonPhieu(chon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Quá hạn: đỏ, đã trả: xám
        private void dgvPhieu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            PhieuMuonDTO pm = dgvPhieu.Rows[e.RowIndex].DataBoundItem as PhieuMuonDTO;
            if (pm == null) return;
            if (pm.TrangThai == "Quá hạn") e.CellStyle.ForeColor = Color.Red;
            else if (pm.TrangThai == "Đã trả") e.CellStyle.ForeColor = Color.Gray;
        }

        private void dgvPhieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) ChonPhieu((PhieuMuonDTO)bindingSource1[e.RowIndex]);
        }

        private void ChonPhieu(PhieuMuonDTO pm)
        {
            phieuChon = pm;
            bindingSource2.DataSource = pm == null ? new List<ChiTietMuonDTO>() : bus.LayChiTiet(pm.MaPhieu);
            dgvChiTiet.Columns["MaSach"].HeaderText = "Mã sách";
            dgvChiTiet.Columns["MaSach"].FillWeight = 50;
            dgvChiTiet.Columns["TenSach"].HeaderText = "Tên sách";
            dgvChiTiet.Columns["TenSach"].FillWeight = 200;
            dgvChiTiet.Columns["TheLoai"].HeaderText = "Thể loại";
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTiet.Columns["SoLuong"].FillWeight = 60;
            grpChiTiet.Text = pm == null ? "Chi tiết phiếu mượn" : "Chi tiết phiếu #" + pm.MaPhieu + " - " + pm.HoTen;
            lblPhieuChon.Text = pm == null ? "" : "Phiếu #" + pm.MaPhieu + ": " + pm.TrangThai;
            btnTraSach.Enabled = pm != null && pm.TrangThai != "Đã trả";
        }

        // Trả sách: chuyển "Đang mượn"/"Quá hạn" -> "Đã trả" và cộng lại SoLuongCon
        private void btnTraSach_Click(object sender, EventArgs e)
        {
            if (phieuChon == null) return;
            if (MessageBox.Show("Xác nhận độc giả " + phieuChon.HoTen + " trả sách của phiếu #" + phieuChon.MaPhieu + "?\n" + phieuChon.SachMuon,
                    "Trả sách", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                int ma = phieuChon.MaPhieu;
                bus.TraSach(phieuChon);
                MessageBox.Show("Đã trả sách phiếu #" + ma + ". Số lượng sách trong thư viện đã được cộng lại.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiPhieu(ma);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            using (frmLapPhieu f = new frmLapPhieu())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    dangNap = true;
                    cboDocGia.SelectedIndex = 0;
                    cboTrangThai.SelectedIndex = 0;
                    dangNap = false;
                    TaiPhieu(f.MaPhieuMoi);
                }
            }
        }
    }
}
