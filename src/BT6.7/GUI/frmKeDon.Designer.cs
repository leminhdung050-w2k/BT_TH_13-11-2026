namespace BT6._7.GUI
{
    partial class frmKeDon
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
            this.grpHoSo = new System.Windows.Forms.GroupBox();
            this.dgvHoSo = new System.Windows.Forms.DataGridView();
            this.lblBenhNhan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTrieuChung = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChanDoan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTienKham = new System.Windows.Forms.TextBox();
            this.grpDon = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboThuoc = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLieuDung = new System.Windows.Forms.TextBox();
            this.btnThemThuoc = new System.Windows.Forms.Button();
            this.dgvDon = new System.Windows.Forms.DataGridView();
            this.btnBoThuoc = new System.Windows.Forms.Button();
            this.lblTienThuoc = new System.Windows.Forms.Label();
            this.lblTong = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.grpHoSo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoSo)).BeginInit();
            this.grpDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // grpHoSo
            // 
            this.grpHoSo.Controls.Add(this.dgvHoSo);
            this.grpHoSo.Controls.Add(this.lblBenhNhan);
            this.grpHoSo.Controls.Add(this.label1);
            this.grpHoSo.Controls.Add(this.txtTrieuChung);
            this.grpHoSo.Controls.Add(this.label2);
            this.grpHoSo.Controls.Add(this.txtChanDoan);
            this.grpHoSo.Controls.Add(this.label3);
            this.grpHoSo.Controls.Add(this.txtTienKham);
            this.grpHoSo.Location = new System.Drawing.Point(12, 12);
            this.grpHoSo.Name = "grpHoSo";
            this.grpHoSo.Size = new System.Drawing.Size(330, 560);
            this.grpHoSo.TabIndex = 0;
            this.grpHoSo.TabStop = false;
            this.grpHoSo.Text = "Hồ sơ chờ khám";
            // 
            // dgvHoSo
            // 
            this.dgvHoSo.AllowUserToAddRows = false;
            this.dgvHoSo.AllowUserToDeleteRows = false;
            this.dgvHoSo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoSo.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoSo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoSo.Location = new System.Drawing.Point(12, 22);
            this.dgvHoSo.MultiSelect = false;
            this.dgvHoSo.Name = "dgvHoSo";
            this.dgvHoSo.ReadOnly = true;
            this.dgvHoSo.RowHeadersVisible = false;
            this.dgvHoSo.RowHeadersWidth = 51;
            this.dgvHoSo.RowTemplate.Height = 24;
            this.dgvHoSo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoSo.Size = new System.Drawing.Size(306, 200);
            this.dgvHoSo.TabIndex = 1;
            this.dgvHoSo.DataSource = this.bindingSource1;
            this.dgvHoSo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoSo_CellClick);
            // 
            // lblBenhNhan
            // 
            this.lblBenhNhan.AutoSize = true;
            this.lblBenhNhan.Location = new System.Drawing.Point(12, 232);
            this.lblBenhNhan.Name = "lblBenhNhan";
            this.lblBenhNhan.TabIndex = 2;
            this.lblBenhNhan.Text = "Chưa chọn bệnh nhân";
            this.lblBenhNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 262);
            this.label1.Name = "label1";
            this.label1.TabIndex = 3;
            this.label1.Text = "Triệu chứng";
            // 
            // txtTrieuChung
            // 
            this.txtTrieuChung.Location = new System.Drawing.Point(12, 282);
            this.txtTrieuChung.Name = "txtTrieuChung";
            this.txtTrieuChung.TabIndex = 4;
            this.txtTrieuChung.ReadOnly = true;
            this.txtTrieuChung.Multiline = true;
            this.txtTrieuChung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTrieuChung.Size = new System.Drawing.Size(306, 75);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 370);
            this.label2.Name = "label2";
            this.label2.TabIndex = 5;
            this.label2.Text = "Chẩn đoán";
            // 
            // txtChanDoan
            // 
            this.txtChanDoan.Location = new System.Drawing.Point(12, 390);
            this.txtChanDoan.Name = "txtChanDoan";
            this.txtChanDoan.TabIndex = 6;
            this.txtChanDoan.MaxLength = 250;
            this.txtChanDoan.Multiline = true;
            this.txtChanDoan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChanDoan.Size = new System.Drawing.Size(306, 60);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 470);
            this.label3.Name = "label3";
            this.label3.TabIndex = 7;
            this.label3.Text = "Tiền khám";
            // 
            // txtTienKham
            // 
            this.txtTienKham.Location = new System.Drawing.Point(100, 467);
            this.txtTienKham.Name = "txtTienKham";
            this.txtTienKham.Size = new System.Drawing.Size(130, 22);
            this.txtTienKham.TabIndex = 8;
            this.txtTienKham.Text = "100000";
            this.txtTienKham.TextChanged += new System.EventHandler(this.txtTienKham_TextChanged);
            // 
            // grpDon
            // 
            this.grpDon.Controls.Add(this.label4);
            this.grpDon.Controls.Add(this.cboThuoc);
            this.grpDon.Controls.Add(this.label5);
            this.grpDon.Controls.Add(this.numSoLuong);
            this.grpDon.Controls.Add(this.label6);
            this.grpDon.Controls.Add(this.txtLieuDung);
            this.grpDon.Controls.Add(this.btnThemThuoc);
            this.grpDon.Controls.Add(this.dgvDon);
            this.grpDon.Controls.Add(this.btnBoThuoc);
            this.grpDon.Controls.Add(this.lblTienThuoc);
            this.grpDon.Controls.Add(this.lblTong);
            this.grpDon.Controls.Add(this.btnLuu);
            this.grpDon.Location = new System.Drawing.Point(355, 12);
            this.grpDon.Name = "grpDon";
            this.grpDon.Size = new System.Drawing.Size(633, 560);
            this.grpDon.TabIndex = 9;
            this.grpDon.TabStop = false;
            this.grpDon.Text = "Kê đơn thuốc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 32);
            this.label4.Name = "label4";
            this.label4.TabIndex = 10;
            this.label4.Text = "Thuốc";
            // 
            // cboThuoc
            // 
            this.cboThuoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThuoc.FormattingEnabled = true;
            this.cboThuoc.Location = new System.Drawing.Point(90, 28);
            this.cboThuoc.Name = "cboThuoc";
            this.cboThuoc.Size = new System.Drawing.Size(400, 24);
            this.cboThuoc.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(505, 32);
            this.label5.Name = "label5";
            this.label5.TabIndex = 12;
            this.label5.Text = "SL";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(535, 29);
            this.numSoLuong.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(80, 22);
            this.numSoLuong.TabIndex = 13;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 70);
            this.label6.Name = "label6";
            this.label6.TabIndex = 14;
            this.label6.Text = "Liều dùng";
            // 
            // txtLieuDung
            // 
            this.txtLieuDung.Location = new System.Drawing.Point(90, 67);
            this.txtLieuDung.Name = "txtLieuDung";
            this.txtLieuDung.Size = new System.Drawing.Size(400, 22);
            this.txtLieuDung.TabIndex = 15;
            this.txtLieuDung.MaxLength = 100;
            // 
            // btnThemThuoc
            // 
            this.btnThemThuoc.Location = new System.Drawing.Point(505, 62);
            this.btnThemThuoc.Name = "btnThemThuoc";
            this.btnThemThuoc.Size = new System.Drawing.Size(110, 32);
            this.btnThemThuoc.TabIndex = 16;
            this.btnThemThuoc.Text = "Thêm thuốc";
            this.btnThemThuoc.UseVisualStyleBackColor = true;
            this.btnThemThuoc.Click += new System.EventHandler(this.btnThemThuoc_Click);
            // 
            // dgvDon
            // 
            this.dgvDon.AllowUserToAddRows = false;
            this.dgvDon.AllowUserToDeleteRows = false;
            this.dgvDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDon.Location = new System.Drawing.Point(12, 105);
            this.dgvDon.MultiSelect = false;
            this.dgvDon.Name = "dgvDon";
            this.dgvDon.ReadOnly = true;
            this.dgvDon.RowHeadersVisible = false;
            this.dgvDon.RowHeadersWidth = 51;
            this.dgvDon.RowTemplate.Height = 24;
            this.dgvDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDon.Size = new System.Drawing.Size(609, 330);
            this.dgvDon.TabIndex = 17;
            this.dgvDon.DataSource = this.bindingSource2;
            // 
            // btnBoThuoc
            // 
            this.btnBoThuoc.Location = new System.Drawing.Point(12, 445);
            this.btnBoThuoc.Name = "btnBoThuoc";
            this.btnBoThuoc.Size = new System.Drawing.Size(110, 32);
            this.btnBoThuoc.TabIndex = 18;
            this.btnBoThuoc.Text = "Bỏ thuốc";
            this.btnBoThuoc.UseVisualStyleBackColor = true;
            this.btnBoThuoc.Click += new System.EventHandler(this.btnBoThuoc_Click);
            // 
            // lblTienThuoc
            // 
            this.lblTienThuoc.AutoSize = true;
            this.lblTienThuoc.Location = new System.Drawing.Point(140, 452);
            this.lblTienThuoc.Name = "lblTienThuoc";
            this.lblTienThuoc.TabIndex = 19;
            this.lblTienThuoc.Text = "";
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Location = new System.Drawing.Point(12, 500);
            this.lblTong.Name = "lblTong";
            this.lblTong.TabIndex = 20;
            this.lblTong.Text = "Tổng chi phí: 0 đ";
            this.lblTong.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong.ForeColor = System.Drawing.Color.Red;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(450, 490);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(170, 50);
            this.btnLuu.TabIndex = 21;
            this.btnLuu.Text = "Lưu && Xuất đơn";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmKeDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 585);
            this.Controls.Add(this.grpDon);
            this.Controls.Add(this.grpHoSo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKeDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Khám bệnh & kê đơn thuốc";
            this.Load += new System.EventHandler(this.frmKeDon_Load);
            this.grpHoSo.ResumeLayout(false);
            this.grpHoSo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoSo)).EndInit();
            this.grpDon.ResumeLayout(false);
            this.grpDon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHoSo;
        private System.Windows.Forms.DataGridView dgvHoSo;
        private System.Windows.Forms.Label lblBenhNhan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTrieuChung;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChanDoan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTienKham;
        private System.Windows.Forms.GroupBox grpDon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboThuoc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLieuDung;
        private System.Windows.Forms.Button btnThemThuoc;
        private System.Windows.Forms.DataGridView dgvDon;
        private System.Windows.Forms.Button btnBoThuoc;
        private System.Windows.Forms.Label lblTienThuoc;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
    }
}
