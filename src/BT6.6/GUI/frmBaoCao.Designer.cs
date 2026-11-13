namespace BT6._6.GUI
{
    partial class frmBaoCao
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
            this.grpQuaHan = new System.Windows.Forms.GroupBox();
            this.dgvQuaHan = new System.Windows.Forms.DataGridView();
            this.lblQuaHan = new System.Windows.Forms.Label();
            this.grpThongKe = new System.Windows.Forms.GroupBox();
            this.dgvThongKe = new System.Windows.Forms.DataGridView();
            this.lblTong = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.grpQuaHan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuaHan)).BeginInit();
            this.grpThongKe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // grpQuaHan
            // 
            this.grpQuaHan.Controls.Add(this.dgvQuaHan);
            this.grpQuaHan.Location = new System.Drawing.Point(12, 12);
            this.grpQuaHan.Name = "grpQuaHan";
            this.grpQuaHan.Size = new System.Drawing.Size(876, 270);
            this.grpQuaHan.TabIndex = 0;
            this.grpQuaHan.TabStop = false;
            this.grpQuaHan.Text = "Phiếu mượn quá hạn (chưa trả sách)";
            // 
            // dgvQuaHan
            // 
            this.dgvQuaHan.AllowUserToAddRows = false;
            this.dgvQuaHan.AllowUserToDeleteRows = false;
            this.dgvQuaHan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuaHan.BackgroundColor = System.Drawing.Color.White;
            this.dgvQuaHan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuaHan.Location = new System.Drawing.Point(12, 22);
            this.dgvQuaHan.MultiSelect = false;
            this.dgvQuaHan.Name = "dgvQuaHan";
            this.dgvQuaHan.ReadOnly = true;
            this.dgvQuaHan.RowHeadersVisible = false;
            this.dgvQuaHan.RowHeadersWidth = 51;
            this.dgvQuaHan.RowTemplate.Height = 24;
            this.dgvQuaHan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuaHan.Size = new System.Drawing.Size(852, 236);
            this.dgvQuaHan.TabIndex = 1;
            this.dgvQuaHan.DataSource = this.bindingSource1;
            // 
            // lblQuaHan
            // 
            this.lblQuaHan.AutoSize = true;
            this.lblQuaHan.Location = new System.Drawing.Point(12, 292);
            this.lblQuaHan.Name = "lblQuaHan";
            this.lblQuaHan.TabIndex = 2;
            this.lblQuaHan.Text = "";
            this.lblQuaHan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuaHan.ForeColor = System.Drawing.Color.Red;
            // 
            // grpThongKe
            // 
            this.grpThongKe.Controls.Add(this.dgvThongKe);
            this.grpThongKe.Location = new System.Drawing.Point(12, 322);
            this.grpThongKe.Name = "grpThongKe";
            this.grpThongKe.Size = new System.Drawing.Size(876, 230);
            this.grpThongKe.TabIndex = 3;
            this.grpThongKe.TabStop = false;
            this.grpThongKe.Text = "Thống kê tổng số lượt mượn theo thể loại sách";
            // 
            // dgvThongKe
            // 
            this.dgvThongKe.AllowUserToAddRows = false;
            this.dgvThongKe.AllowUserToDeleteRows = false;
            this.dgvThongKe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThongKe.BackgroundColor = System.Drawing.Color.White;
            this.dgvThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKe.Location = new System.Drawing.Point(12, 22);
            this.dgvThongKe.MultiSelect = false;
            this.dgvThongKe.Name = "dgvThongKe";
            this.dgvThongKe.ReadOnly = true;
            this.dgvThongKe.RowHeadersVisible = false;
            this.dgvThongKe.RowHeadersWidth = 51;
            this.dgvThongKe.RowTemplate.Height = 24;
            this.dgvThongKe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThongKe.Size = new System.Drawing.Size(852, 196);
            this.dgvThongKe.TabIndex = 4;
            this.dgvThongKe.DataSource = this.bindingSource2;
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Location = new System.Drawing.Point(12, 562);
            this.lblTong.Name = "lblTong";
            this.lblTong.TabIndex = 5;
            this.lblTong.Text = "";
            this.lblTong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 595);
            this.Controls.Add(this.lblTong);
            this.Controls.Add(this.grpThongKe);
            this.Controls.Add(this.lblQuaHan);
            this.Controls.Add(this.grpQuaHan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cảnh báo sách quá hạn & thống kê";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            this.grpQuaHan.ResumeLayout(false);
            this.grpQuaHan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuaHan)).EndInit();
            this.grpThongKe.ResumeLayout(false);
            this.grpThongKe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpQuaHan;
        private System.Windows.Forms.DataGridView dgvQuaHan;
        private System.Windows.Forms.Label lblQuaHan;
        private System.Windows.Forms.GroupBox grpThongKe;
        private System.Windows.Forms.DataGridView dgvThongKe;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
    }
}
