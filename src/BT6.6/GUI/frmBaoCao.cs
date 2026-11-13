using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BT6._6.BUS;
using BT6._6.DTO;

namespace BT6._6.GUI
{
    public partial class frmBaoCao : Form
    {
        private readonly BaoCaoBUS bus = new BaoCaoBUS();

        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            try
            {
                // Danh sách quá hạn đọc bằng SqlDataReader
                List<PhieuMuonDTO> quaHan = bus.PhieuQuaHan();
                bindingSource1.DataSource = quaHan;
                dgvQuaHan.Columns["MaPhieu"].HeaderText = "Mã phiếu";
                dgvQuaHan.Columns["MaPhieu"].FillWeight = 55;
                dgvQuaHan.Columns["MaDocGia"].Visible = false;
                dgvQuaHan.Columns["HoTen"].HeaderText = "Độc giả";
                dgvQuaHan.Columns["NgayMuon"].HeaderText = "Ngày mượn";
                dgvQuaHan.Columns["NgayMuon"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvQuaHan.Columns["HanTra"].HeaderText = "Hạn trả";
                dgvQuaHan.Columns["HanTra"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvQuaHan.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvQuaHan.Columns["SachMuon"].HeaderText = "Sách mượn";
                dgvQuaHan.Columns["SachMuon"].FillWeight = 220;
                dgvQuaHan.Columns["SoNgayQuaHan"].HeaderText = "Quá hạn (ngày)";
                lblQuaHan.Text = "Có " + bus.DemQuaHan() + " phiếu mượn quá hạn chưa trả sách";

                // Thống kê lượt mượn theo thể loại (GROUP BY) + tổng (ExecuteScalar)
                bindingSource2.DataSource = bus.ThongKeTheLoai();
                dgvThongKe.Columns["TheLoai"].HeaderText = "Thể loại";
                dgvThongKe.Columns["TheLoai"].FillWeight = 200;
                dgvThongKe.Columns["SoPhieu"].HeaderText = "Số phiếu mượn";
                dgvThongKe.Columns["SoLuotMuon"].HeaderText = "Tổng lượt mượn (cuốn)";
                dgvThongKe.Columns["TyLe"].HeaderText = "Tỷ lệ (%)";
                lblTong.Text = "Tổng số lượt mượn toàn thư viện: " + bus.TongLuotMuon() + " cuốn";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
