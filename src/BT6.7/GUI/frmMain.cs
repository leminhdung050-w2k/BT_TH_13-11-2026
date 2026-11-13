using System;
using System.Windows.Forms;
using BT6._7.BUS;

namespace BT6._7.GUI
{
    public partial class frmMain : Form
    {
        private readonly BenhNhanBUS benhNhanBUS = new BenhNhanBUS();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CapNhatThongBao();
        }

        // Số bệnh nhân đang chờ khám (ExecuteScalar)
        private void CapNhatThongBao()
        {
            try
            {
                int n = benhNhanBUS.DemChoKham();
                lblCanhBao.Text = n > 0 ? "Đang có " + n + " bệnh nhân chờ khám" : "Không có bệnh nhân chờ khám";
            }
            catch (Exception ex)
            {
                lblCanhBao.Text = "Không kết nối được CSDL: " + ex.Message;
            }
        }

        private void btnTiepNhan_Click(object sender, EventArgs e)
        {
            using (frmTiepNhan f = new frmTiepNhan()) f.ShowDialog();
            CapNhatThongBao();
        }

        private void btnThuoc_Click(object sender, EventArgs e)
        {
            using (frmThuoc f = new frmThuoc()) f.ShowDialog();
        }

        private void btnKeDon_Click(object sender, EventArgs e)
        {
            using (frmKeDon f = new frmKeDon()) f.ShowDialog();
            CapNhatThongBao();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            using (frmThanhToan f = new frmThanhToan()) f.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
