namespace demo.Control
{
    partial class UC_QuanLyKho
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTonThap = new System.Windows.Forms.Label();
            this.lblSapHetHan = new System.Windows.Forms.Label();
            this.lblTongMatHang = new System.Windows.Forms.Label();
            this.lblGiaTriKho = new System.Windows.Forms.Label();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.lblGiaTriKho, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTongMatHang, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSapHetHan, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTonThap, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(873, 100);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // lblTonThap
            // 
            this.lblTonThap.AutoSize = true;
            this.lblTonThap.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTonThap.Location = new System.Drawing.Point(221, 0);
            this.lblTonThap.Name = "lblTonThap";
            this.lblTonThap.Size = new System.Drawing.Size(98, 32);
            this.lblTonThap.TabIndex = 2;
            this.lblTonThap.Text = "label3";
            // 
            // lblSapHetHan
            // 
            this.lblSapHetHan.AutoSize = true;
            this.lblSapHetHan.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSapHetHan.Location = new System.Drawing.Point(439, 0);
            this.lblSapHetHan.Name = "lblSapHetHan";
            this.lblSapHetHan.Size = new System.Drawing.Size(98, 32);
            this.lblSapHetHan.TabIndex = 1;
            this.lblSapHetHan.Text = "label2";
            // 
            // lblTongMatHang
            // 
            this.lblTongMatHang.AutoSize = true;
            this.lblTongMatHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongMatHang.Location = new System.Drawing.Point(3, 0);
            this.lblTongMatHang.Name = "lblTongMatHang";
            this.lblTongMatHang.Size = new System.Drawing.Size(98, 32);
            this.lblTongMatHang.TabIndex = 0;
            this.lblTongMatHang.Text = "label1";
            // 
            // lblGiaTriKho
            // 
            this.lblGiaTriKho.AutoSize = true;
            this.lblGiaTriKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaTriKho.Location = new System.Drawing.Point(657, 0);
            this.lblGiaTriKho.Name = "lblGiaTriKho";
            this.lblGiaTriKho.Size = new System.Drawing.Size(98, 32);
            this.lblGiaTriKho.TabIndex = 3;
            this.lblGiaTriKho.Text = "label4";
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTonKho.Location = new System.Drawing.Point(0, 0);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.RowHeadersWidth = 51;
            this.dgvTonKho.RowTemplate.Height = 24;
            this.dgvTonKho.Size = new System.Drawing.Size(873, 210);
            this.dgvTonKho.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvTonKho);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 210);
            this.panel1.TabIndex = 6;
            // 
            // UC_QuanLyKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UC_QuanLyKho";
            this.Size = new System.Drawing.Size(873, 310);
            this.Load += new System.EventHandler(this.UC_QuanLyKho_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblGiaTriKho;
        private System.Windows.Forms.Label lblTongMatHang;
        private System.Windows.Forms.Label lblSapHetHan;
        private System.Windows.Forms.Label lblTonThap;
        private System.Windows.Forms.DataGridView dgvTonKho;
        private System.Windows.Forms.Panel panel1;
    }
}
