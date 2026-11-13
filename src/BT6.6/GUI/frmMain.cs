using System;
using System.Drawing;
using System.Windows.Forms;
using BT6._6.BUS;

namespace BT6._6.GUI
{
    public partial class frmMain : Form
    {
        private readonly BaoCaoBUS baoCaoBUS = new BaoCaoBUS();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CapNhatCanhBao();
        }

        // Cảnh báo số phiếu mượn quá hạn (ExecuteScalar)
        private void CapNhatCanhBao()
        {
            try
            {
                int n = baoCaoBUS.DemQuaHan();
                lblCanhBao.Text = n > 0 ? "Cảnh báo: có " + n + " phiếu mượn đã quá hạn trả!" : "Không có phiếu mượn quá hạn";
                lblCanhBao.ForeColor = n > 0 ? Color.Red : Color.Green;
            }
            catch (Exception ex)
            {
                lblCanhBao.Text = "Không kết nối được CSDL: " + ex.Message;
            }
        }

        private void btnDauSach_Click(object sender, EventArgs e)
        {
            using (frmDauSach f = new frmDauSach()) f.ShowDialog();
        }

        private void btnDocGia_Click(object sender, EventArgs e)
        {
            using (frmDocGia f = new frmDocGia()) f.ShowDialog();
        }

        private void btnMuonTra_Click(object sender, EventArgs e)
        {
            using (frmMuonTra f = new frmMuonTra()) f.ShowDialog();
            CapNhatCanhBao();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            using (frmBaoCao f = new frmBaoCao()) f.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
