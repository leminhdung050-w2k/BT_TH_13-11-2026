namespace BT6._7.GUI
{
    partial class frmTiepNhan
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
            this.grpTraCuu = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.btnTraCuu = new System.Windows.Forms.Button();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.grpBenhNhan = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNamSinh = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTrieuChung = new System.Windows.Forms.TextBox();
            this.btnTiepNhan = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.grpLichSu = new System.Windows.Forms.GroupBox();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.grpChoKham = new System.Windows.Forms.GroupBox();
            this.dgvChoKham = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.grpTraCuu.SuspendLayout();
            this.grpBenhNhan.SuspendLayout();
            this.grpLichSu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.grpChoKham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChoKham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTraCuu
            // 
            this.grpTraCuu.Controls.Add(this.label1);
            this.grpTraCuu.Controls.Add(this.txtSDT);
            this.grpTraCuu.Controls.Add(this.btnTraCuu);
            this.grpTraCuu.Controls.Add(this.lblKetQua);
            this.grpTraCuu.Location = new System.Drawing.Point(12, 12);
            this.grpTraCuu.Name = "grpTraCuu";
            this.grpTraCuu.Size = new System.Drawing.Size(876, 62);
            this.grpTraCuu.TabIndex = 0;
            this.grpTraCuu.TabStop = false;
            this.grpTraCuu.Text = "Tra cứu bệnh nhân";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.TabIndex = 1;
            this.label1.Text = "Số điện thoại";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(120, 25);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(160, 22);
            this.txtSDT.TabIndex = 2;
            this.txtSDT.MaxLength = 15;
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.Location = new System.Drawing.Point(295, 19);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(100, 32);
            this.btnTraCuu.TabIndex = 3;
            this.btnTraCuu.Text = "Tra cứu";
            this.btnTraCuu.UseVisualStyleBackColor = true;
            this.btnTraCuu.Click += new System.EventHandler(this.btnTraCuu_Click);
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = true;
            this.lblKetQua.Location = new System.Drawing.Point(410, 28);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.TabIndex = 4;
            this.lblKetQua.Text = "";
            this.lblKetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grpBenhNhan
            // 
            this.grpBenhNhan.Controls.Add(this.label2);
            this.grpBenhNhan.Controls.Add(this.txtHoTen);
            this.grpBenhNhan.Controls.Add(this.label3);
            this.grpBenhNhan.Controls.Add(this.txtNamSinh);
            this.grpBenhNhan.Controls.Add(this.label4);
            this.grpBenhNhan.Controls.Add(this.txtTrieuChung);
            this.grpBenhNhan.Controls.Add(this.btnTiepNhan);
            this.grpBenhNhan.Controls.Add(this.btnLamMoi);
            this.grpBenhNhan.Location = new System.Drawing.Point(12, 82);
            this.grpBenhNhan.Name = "grpBenhNhan";
            this.grpBenhNhan.Size = new System.Drawing.Size(430, 300);
            this.grpBenhNhan.TabIndex = 5;
            this.grpBenhNhan.TabStop = false;
            this.grpBenhNhan.Text = "Thông tin bệnh nhân && hồ sơ khám";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 35);
            this.label2.Name = "label2";
            this.label2.TabIndex = 6;
            this.label2.Text = "Họ tên";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(110, 32);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(300, 22);
            this.txtHoTen.TabIndex = 7;
            this.txtHoTen.MaxLength = 100;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 72);
            this.label3.Name = "label3";
            this.label3.TabIndex = 8;
            this.label3.Text = "Năm sinh";
            // 
            // txtNamSinh
            // 
            this.txtNamSinh.Location = new System.Drawing.Point(110, 69);
            this.txtNamSinh.Name = "txtNamSinh";
            this.txtNamSinh.Size = new System.Drawing.Size(80, 22);
            this.txtNamSinh.TabIndex = 9;
            this.txtNamSinh.MaxLength = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 110);
            this.label4.Name = "label4";
            this.label4.TabIndex = 10;
            this.label4.Text = "Triệu chứng";
            // 
            // txtTrieuChung
            // 
            this.txtTrieuChung.Location = new System.Drawing.Point(110, 107);
            this.txtTrieuChung.Name = "txtTrieuChung";
            this.txtTrieuChung.TabIndex = 11;
            this.txtTrieuChung.Multiline = true;
            this.txtTrieuChung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTrieuChung.Size = new System.Drawing.Size(300, 90);
            // 
            // btnTiepNhan
            // 
            this.btnTiepNhan.Location = new System.Drawing.Point(110, 215);
            this.btnTiepNhan.Name = "btnTiepNhan";
            this.btnTiepNhan.Size = new System.Drawing.Size(200, 40);
            this.btnTiepNhan.TabIndex = 12;
            this.btnTiepNhan.Text = "Lưu && tạo hồ sơ khám";
            this.btnTiepNhan.UseVisualStyleBackColor = true;
            this.btnTiepNhan.Click += new System.EventHandler(this.btnTiepNhan_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(320, 215);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(90, 40);
            this.btnLamMoi.TabIndex = 13;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // grpLichSu
            // 
            this.grpLichSu.Controls.Add(this.dgvLichSu);
            this.grpLichSu.Location = new System.Drawing.Point(455, 82);
            this.grpLichSu.Name = "grpLichSu";
            this.grpLichSu.Size = new System.Drawing.Size(433, 300);
            this.grpLichSu.TabIndex = 14;
            this.grpLichSu.TabStop = false;
            this.grpLichSu.Text = "Lịch sử khám của bệnh nhân";
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSu.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Location = new System.Drawing.Point(12, 22);
            this.dgvLichSu.MultiSelect = false;
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.RowHeadersVisible = false;
            this.dgvLichSu.RowHeadersWidth = 51;
            this.dgvLichSu.RowTemplate.Height = 24;
            this.dgvLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSu.Size = new System.Drawing.Size(409, 266);
            this.dgvLichSu.TabIndex = 15;
            this.dgvLichSu.DataSource = this.bindingSource2;
            // 
            // grpChoKham
            // 
            this.grpChoKham.Controls.Add(this.dgvChoKham);
            this.grpChoKham.Location = new System.Drawing.Point(12, 390);
            this.grpChoKham.Name = "grpChoKham";
            this.grpChoKham.Size = new System.Drawing.Size(876, 185);
            this.grpChoKham.TabIndex = 16;
            this.grpChoKham.TabStop = false;
            this.grpChoKham.Text = "Bệnh nhân đang chờ khám";
            // 
            // dgvChoKham
            // 
            this.dgvChoKham.AllowUserToAddRows = false;
            this.dgvChoKham.AllowUserToDeleteRows = false;
            this.dgvChoKham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChoKham.BackgroundColor = System.Drawing.Color.White;
            this.dgvChoKham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChoKham.Location = new System.Drawing.Point(12, 22);
            this.dgvChoKham.MultiSelect = false;
            this.dgvChoKham.Name = "dgvChoKham";
            this.dgvChoKham.ReadOnly = true;
            this.dgvChoKham.RowHeadersVisible = false;
            this.dgvChoKham.RowHeadersWidth = 51;
            this.dgvChoKham.RowTemplate.Height = 24;
            this.dgvChoKham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChoKham.Size = new System.Drawing.Size(852, 151);
            this.dgvChoKham.TabIndex = 17;
            this.dgvChoKham.DataSource = this.bindingSource1;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmTiepNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 588);
            this.Controls.Add(this.grpChoKham);
            this.Controls.Add(this.grpLichSu);
            this.Controls.Add(this.grpBenhNhan);
            this.Controls.Add(this.grpTraCuu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTiepNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tiếp nhận bệnh nhân";
            this.Load += new System.EventHandler(this.frmTiepNhan_Load);
            this.grpTraCuu.ResumeLayout(false);
            this.grpTraCuu.PerformLayout();
            this.grpBenhNhan.ResumeLayout(false);
            this.grpBenhNhan.PerformLayout();
            this.grpLichSu.ResumeLayout(false);
            this.grpLichSu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.grpChoKham.ResumeLayout(false);
            this.grpChoKham.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChoKham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTraCuu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Button btnTraCuu;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.GroupBox grpBenhNhan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNamSinh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTrieuChung;
        private System.Windows.Forms.Button btnTiepNhan;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.GroupBox grpLichSu;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.GroupBox grpChoKham;
        private System.Windows.Forms.DataGridView dgvChoKham;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
    }
}
