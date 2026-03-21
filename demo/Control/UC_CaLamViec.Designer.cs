namespace demo.Control
{
    partial class UC_CaLamViec
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCaLamViec = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblTimeNow = new System.Windows.Forms.Label();
            this.lblNowText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaLamViec)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "CA LÀM VIỆC";
            // 
            // dgvCaLamViec
            // 
            this.dgvCaLamViec.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCaLamViec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCaLamViec.Location = new System.Drawing.Point(0, 415);
            this.dgvCaLamViec.Name = "dgvCaLamViec";
            this.dgvCaLamViec.RowHeadersWidth = 51;
            this.dgvCaLamViec.RowTemplate.Height = 24;
            this.dgvCaLamViec.Size = new System.Drawing.Size(1049, 216);
            this.dgvCaLamViec.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblStatus.Location = new System.Drawing.Point(27, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(204, 32);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Ca Hoạt Động";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGreen;
            this.panel1.Controls.Add(this.lblNowText);
            this.panel1.Controls.Add(this.lblTimeNow);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.ForeColor = System.Drawing.Color.DarkGreen;
            this.panel1.Location = new System.Drawing.Point(38, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(986, 112);
            this.panel1.TabIndex = 2;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblInfo.Location = new System.Drawing.Point(47, 68);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(109, 25);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Bắt đầu lúc";
            // 
            // lblTimeNow
            // 
            this.lblTimeNow.AutoSize = true;
            this.lblTimeNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTimeNow.Location = new System.Drawing.Point(875, 9);
            this.lblTimeNow.Name = "lblTimeNow";
            this.lblTimeNow.Size = new System.Drawing.Size(91, 32);
            this.lblTimeNow.TabIndex = 3;
            this.lblTimeNow.Text = "10:32";
            // 
            // lblNowText
            // 
            this.lblNowText.AutoSize = true;
            this.lblNowText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNowText.Location = new System.Drawing.Point(877, 50);
            this.lblNowText.Name = "lblNowText";
            this.lblNowText.Size = new System.Drawing.Size(94, 20);
            this.lblNowText.TabIndex = 4;
            this.lblNowText.Text = "Giờ hiện tại";
            // 
            // UC_CaLamViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvCaLamViec);
            this.Controls.Add(this.label1);
            this.Name = "UC_CaLamViec";
            this.Size = new System.Drawing.Size(1049, 634);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaLamViec)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCaLamViec;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblTimeNow;
        private System.Windows.Forms.Label lblNowText;
    }
}
