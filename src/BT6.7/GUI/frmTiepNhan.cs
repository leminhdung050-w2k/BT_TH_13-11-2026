using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BT6._7.BUS;
using BT6._7.DTO;

namespace BT6._7.GUI
{
    public partial class frmTiepNhan : Form
    {
        private readonly BenhNhanBUS bus = new BenhNhanBUS();
        private BenhNhanDTO bnChon;     // bệnh nhân đã có (tra cứu thấy)

        public frmTiepNhan()
        {
            InitializeComponent();
        }

        private void frmTiepNhan_Load(object sender, EventArgs e)
        {
            TaiChoKham();
            LamMoi();
        }

        private void TaiChoKham()
        {
            try
            {
                bindingSource1.DataSource = bus.LayHoSoChoKham();
                CotHoSo(dgvChoKham);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CotHoSo(DataGridView d)
        {
            d.Columns["MaHoSo"].HeaderText = "Mã HS";
            d.Columns["MaHoSo"].FillWeight = 45;
            d.Columns["MaBN"].Visible = false;
            d.Columns["HoTen"].HeaderText = "Bệnh nhân";
            d.Columns["SoDienThoai"].HeaderText = "Điện thoại";
            d.Columns["NamSinh"].HeaderText = "Năm sinh";
            d.Columns["NamSinh"].FillWeight = 55;
            d.Columns["NgayKham"].HeaderText = "Ngày khám";
            d.Columns["NgayKham"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            d.Columns["TrieuChung"].HeaderText = "Triệu chứng";
            d.Columns["TrieuChung"].FillWeight = 160;
            d.Columns["ChanDoan"].HeaderText = "Chẩn đoán";
            d.Columns["TrangThai"].HeaderText = "Trạng thái";
            d.Columns["HienThi"].Visible = false;
        }

        // Tra cứu bệnh nhân theo số điện thoại (SqlParameter)
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            try
            {
                BenhNhanDTO bn = bus.TraCuu(txtSDT.Text);
                txtSDT.Text = BenhNhanBUS.ChuanHoaSDT(txtSDT.Text);
                bnChon = bn;
                txtTrieuChung.Clear();
                if (bn != null)
                {
                    txtHoTen.Text = bn.HoTen;
                    txtNamSinh.Text = bn.NamSinh.HasValue ? bn.NamSinh.ToString() : "";
                    lblKetQua.ForeColor = Color.Green;
                    lblKetQua.Text = "Bệnh nhân đã có hồ sơ: " + bn.HoTen + " (mã BN " + bn.MaBN + ")";
                    txtTrieuChung.Focus();
                }
                else
                {
                    txtHoTen.Clear(); txtNamSinh.Clear();
                    lblKetQua.ForeColor = Color.OrangeRed;
                    lblKetQua.Text = "Bệnh nhân mới - nhập thông tin rồi bấm \"Lưu & tạo hồ sơ khám\"";
                    txtHoTen.Focus();
                }
                HienLichSu();
            }
            catch (LoiNhapLieu ex)
            {
                errorProvider1.SetError(txtSDT, ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienLichSu()
        {
            bindingSource2.DataSource = bnChon == null ? new List<HoSoKhamDTO>() : bus.LayHoSoCuaBN(bnChon.MaBN);
            CotHoSo(dgvLichSu);
            dgvLichSu.Columns["HoTen"].Visible = false;
            dgvLichSu.Columns["SoDienThoai"].Visible = false;
            dgvLichSu.Columns["NamSinh"].Visible = false;
            dgvLichSu.Columns["TrieuChung"].Visible = false;
            grpLichSu.Text = bnChon == null ? "Lịch sử khám của bệnh nhân" : "Lịch sử khám - " + bnChon.HoTen;
        }

        // Lưu bệnh nhân (mới hoặc cũ) và tạo HoSoKham
        private void btnTiepNhan_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            try
            {
                int nam = 0;
                if (txtNamSinh.Text.Trim() != "" && !int.TryParse(txtNamSinh.Text.Trim(), out nam))
                    throw new LoiNhapLieu("NamSinh", "Năm sinh phải là số!");
                if (bnChon != null && BenhNhanBUS.ChuanHoaSDT(txtSDT.Text) != bnChon.SoDienThoai)
                    bnChon = null;      // đã đổi số điện thoại sau khi tra cứu
                BenhNhanDTO bn = new BenhNhanDTO
                {
                    MaBN = bnChon == null ? 0 : bnChon.MaBN,
                    HoTen = txtHoTen.Text,
                    SoDienThoai = txtSDT.Text,
                    NamSinh = txtNamSinh.Text.Trim() == "" ? (int?)null : nam
                };
                int maHoSo = bus.TiepNhan(bn, txtTrieuChung.Text);
                MessageBox.Show("Đã tiếp nhận bệnh nhân " + bn.HoTen + " - tạo hồ sơ khám #" + maHoSo, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiChoKham();
                LamMoi();
            }
            catch (LoiNhapLieu ex)
            {
                Control o = ex.TenTruong == "SoDienThoai" ? txtSDT
                          : ex.TenTruong == "HoTen" ? txtHoTen
                          : ex.TenTruong == "NamSinh" ? (Control)txtNamSinh
                          : txtTrieuChung;
                errorProvider1.SetError(o, ex.Message);
                o.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            bnChon = null;
            txtSDT.Clear(); txtHoTen.Clear(); txtNamSinh.Clear(); txtTrieuChung.Clear();
            lblKetQua.Text = "";
            errorProvider1.Clear();
            HienLichSu();
            txtSDT.Focus();
        }
    }
}
