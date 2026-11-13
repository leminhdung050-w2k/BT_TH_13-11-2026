namespace BT6._8.GUI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpBan = new System.Windows.Forms.GroupBox();
            this.flpBan = new System.Windows.Forms.FlowLayoutPanel();
            this.lblChuThich = new System.Windows.Forms.Label();
            this.grpHoaDon = new System.Windows.Forms.GroupBox();
            this.lblBan = new System.Windows.Forms.Label();
            this.lblGioBatDau = new System.Windows.Forms.Label();
            this.lblThoiGian = new System.Windows.Forms.Label();
            this.btnMoBan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDichVu = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnGoiMon = new System.Windows.Forms.Button();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.lblGoiY = new System.Windows.Forms.Label();
            this.lblTienGio = new System.Windows.Forms.Label();
            this.lblTienDV = new System.Windows.Forms.Label();
            this.lblTong = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnQuanLyDV = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grpBan.SuspendLayout();
            this.grpHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBan
            // 
            this.grpBan.Controls.Add(this.flpBan);
            this.grpBan.Controls.Add(this.lblChuThich);
            this.grpBan.Location = new System.Drawing.Point(12, 12);
            this.grpBan.Name = "grpBan";
            this.grpBan.Size = new System.Drawing.Size(520, 625);
            this.grpBan.TabIndex = 0;
            this.grpBan.TabStop = false;
            this.grpBan.Text = "Sơ đồ bàn bida";
            // 
            // flpBan
            // 
            this.flpBan.AutoScroll = true;
            this.flpBan.Location = new System.Drawing.Point(6, 22);
            this.flpBan.Name = "flpBan";
            this.flpBan.Size = new System.Drawing.Size(508, 560);
            this.flpBan.TabIndex = 1;
            // 
            // lblChuThich
            // 
            this.lblChuThich.AutoSize = true;
            this.lblChuThich.Location = new System.Drawing.Point(100, 595);
            this.lblChuThich.Name = "lblChuThich";
            this.lblChuThich.TabIndex = 2;
            this.lblChuThich.Text = "Màu xanh: bàn Trống          Màu cam: bàn Đang chơi";
            // 
            // grpHoaDon
            // 
            this.grpHoaDon.Controls.Add(this.lblBan);
            this.grpHoaDon.Controls.Add(this.lblGioBatDau);
            this.grpHoaDon.Controls.Add(this.lblThoiGian);
            this.grpHoaDon.Controls.Add(this.btnMoBan);
            this.grpHoaDon.Controls.Add(this.label1);
            this.grpHoaDon.Controls.Add(this.cboDichVu);
            this.grpHoaDon.Controls.Add(this.label2);
            this.grpHoaDon.Controls.Add(this.numSoLuong);
            this.grpHoaDon.Controls.Add(this.btnGoiMon);
            this.grpHoaDon.Controls.Add(this.dgvDichVu);
            this.grpHoaDon.Controls.Add(this.lblGoiY);
            this.grpHoaDon.Controls.Add(this.lblTienGio);
            this.grpHoaDon.Controls.Add(this.lblTienDV);
            this.grpHoaDon.Controls.Add(this.lblTong);
            this.grpHoaDon.Controls.Add(this.btnThanhToan);
            this.grpHoaDon.Controls.Add(this.btnQuanLyDV);
            this.grpHoaDon.Location = new System.Drawing.Point(545, 12);
            this.grpHoaDon.Name = "grpHoaDon";
            this.grpHoaDon.Size = new System.Drawing.Size(593, 625);
            this.grpHoaDon.TabIndex = 3;
            this.grpHoaDon.TabStop = false;
            this.grpHoaDon.Text = "Thông tin bàn && hóa đơn";
            // 
            // lblBan
            // 
            this.lblBan.AutoSize = true;
            this.lblBan.Location = new System.Drawing.Point(15, 30);
            this.lblBan.Name = "lblBan";
            this.lblBan.TabIndex = 4;
            this.lblBan.Text = "Chọn 1 bàn trên sơ đồ";
            this.lblBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblGioBatDau
            // 
            this.lblGioBatDau.AutoSize = true;
            this.lblGioBatDau.Location = new System.Drawing.Point(15, 62);
            this.lblGioBatDau.Name = "lblGioBatDau";
            this.lblGioBatDau.TabIndex = 5;
            this.lblGioBatDau.Text = "";
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.AutoSize = true;
            this.lblThoiGian.Location = new System.Drawing.Point(15, 87);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.TabIndex = 6;
            this.lblThoiGian.Text = "";
            this.lblThoiGian.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnMoBan
            // 
            this.btnMoBan.Location = new System.Drawing.Point(440, 25);
            this.btnMoBan.Name = "btnMoBan";
            this.btnMoBan.Size = new System.Drawing.Size(135, 45);
            this.btnMoBan.TabIndex = 7;
            this.btnMoBan.Text = "Mở bàn";
            this.btnMoBan.UseVisualStyleBackColor = true;
            this.btnMoBan.Click += new System.EventHandler(this.btnMoBan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 125);
            this.label1.Name = "label1";
            this.label1.TabIndex = 8;
            this.label1.Text = "Dịch vụ";
            // 
            // cboDichVu
            // 
            this.cboDichVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDichVu.FormattingEnabled = true;
            this.cboDichVu.Location = new System.Drawing.Point(75, 121);
            this.cboDichVu.Name = "cboDichVu";
            this.cboDichVu.Size = new System.Drawing.Size(300, 24);
            this.cboDichVu.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(390, 125);
            this.label2.Name = "label2";
            this.label2.TabIndex = 10;
            this.label2.Text = "SL";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(415, 122);
            this.numSoLuong.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(55, 22);
            this.numSoLuong.TabIndex = 11;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnGoiMon
            // 
            this.btnGoiMon.Location = new System.Drawing.Point(485, 116);
            this.btnGoiMon.Name = "btnGoiMon";
            this.btnGoiMon.Size = new System.Drawing.Size(90, 32);
            this.btnGoiMon.TabIndex = 12;
            this.btnGoiMon.Text = "Gọi thêm";
            this.btnGoiMon.UseVisualStyleBackColor = true;
            this.btnGoiMon.Click += new System.EventHandler(this.btnGoiMon_Click);
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AllowUserToDeleteRows = false;
            this.dgvDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Location = new System.Drawing.Point(15, 160);
            this.dgvDichVu.MultiSelect = false;
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.ReadOnly = false;
            this.dgvDichVu.RowHeadersVisible = false;
            this.dgvDichVu.RowHeadersWidth = 51;
            this.dgvDichVu.RowTemplate.Height = 24;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(560, 270);
            this.dgvDichVu.TabIndex = 13;
            this.dgvDichVu.DataSource = this.bindingSource1;
            this.dgvDichVu.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDichVu_CellEndEdit);
            this.dgvDichVu.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDichVu_CellValidating);
            // 
            // lblGoiY
            // 
            this.lblGoiY.AutoSize = true;
            this.lblGoiY.Location = new System.Drawing.Point(15, 436);
            this.lblGoiY.Name = "lblGoiY";
            this.lblGoiY.TabIndex = 14;
            this.lblGoiY.Text = "Sửa trực tiếp cột Số lượng trên lưới để tăng / giảm món đã gọi";
            this.lblGoiY.ForeColor = System.Drawing.SystemColors.GrayText;
            // 
            // lblTienGio
            // 
            this.lblTienGio.AutoSize = true;
            this.lblTienGio.Location = new System.Drawing.Point(15, 465);
            this.lblTienGio.Name = "lblTienGio";
            this.lblTienGio.TabIndex = 15;
            this.lblTienGio.Text = "Tiền giờ chơi: 0 đ";
            // 
            // lblTienDV
            // 
            this.lblTienDV.AutoSize = true;
            this.lblTienDV.Location = new System.Drawing.Point(15, 492);
            this.lblTienDV.Name = "lblTienDV";
            this.lblTienDV.TabIndex = 16;
            this.lblTienDV.Text = "Tiền dịch vụ: 0 đ";
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Location = new System.Drawing.Point(15, 525);
            this.lblTong.Name = "lblTong";
            this.lblTong.TabIndex = 17;
            this.lblTong.Text = "Tạm tính: 0 đ";
            this.lblTong.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong.ForeColor = System.Drawing.Color.Red;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(425, 515);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(150, 50);
            this.btnThanhToan.TabIndex = 18;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // btnQuanLyDV
            // 
            this.btnQuanLyDV.Location = new System.Drawing.Point(15, 580);
            this.btnQuanLyDV.Name = "btnQuanLyDV";
            this.btnQuanLyDV.Size = new System.Drawing.Size(190, 32);
            this.btnQuanLyDV.TabIndex = 19;
            this.btnQuanLyDV.Text = "Quản lý menu dịch vụ";
            this.btnQuanLyDV.UseVisualStyleBackColor = true;
            this.btnQuanLyDV.Click += new System.EventHandler(this.btnQuanLyDV_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 650);
            this.Controls.Add(this.grpHoaDon);
            this.Controls.Add(this.grpBan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý bàn bida & dịch vụ giải trí";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpBan.ResumeLayout(false);
            this.grpBan.PerformLayout();
            this.grpHoaDon.ResumeLayout(false);
            this.grpHoaDon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBan;
        private System.Windows.Forms.FlowLayoutPanel flpBan;
        private System.Windows.Forms.Label lblChuThich;
        private System.Windows.Forms.GroupBox grpHoaDon;
        private System.Windows.Forms.Label lblBan;
        private System.Windows.Forms.Label lblGioBatDau;
        private System.Windows.Forms.Label lblThoiGian;
        private System.Windows.Forms.Button btnMoBan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDichVu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Button btnGoiMon;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.Label lblGoiY;
        private System.Windows.Forms.Label lblTienGio;
        private System.Windows.Forms.Label lblTienDV;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnQuanLyDV;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Timer timer1;
    }
}
