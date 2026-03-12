namespace demo.Control
{
    partial class UC_SupplierCard
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
            this.lblTen = new System.Windows.Forms.Label();
            this.lblSdt = new System.Windows.Forms.Label();
            this.lblDiachi = new System.Windows.Forms.Label();
            this.ptb_ha = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_ha)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(45, 20);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(44, 16);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "label1";
            // 
            // lblSdt
            // 
            this.lblSdt.AutoSize = true;
            this.lblSdt.Location = new System.Drawing.Point(45, 61);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(44, 16);
            this.lblSdt.TabIndex = 1;
            this.lblSdt.Text = "label2";
            // 
            // lblDiachi
            // 
            this.lblDiachi.AutoSize = true;
            this.lblDiachi.Location = new System.Drawing.Point(45, 106);
            this.lblDiachi.Name = "lblDiachi";
            this.lblDiachi.Size = new System.Drawing.Size(44, 16);
            this.lblDiachi.TabIndex = 2;
            this.lblDiachi.Text = "label3";
            // 
            // ptb_ha
            // 
            this.ptb_ha.Location = new System.Drawing.Point(274, 3);
            this.ptb_ha.Name = "ptb_ha";
            this.ptb_ha.Size = new System.Drawing.Size(203, 214);
            this.ptb_ha.TabIndex = 3;
            this.ptb_ha.TabStop = false;
            // 
            // UC_SupplierCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ptb_ha);
            this.Controls.Add(this.lblDiachi);
            this.Controls.Add(this.lblSdt);
            this.Controls.Add(this.lblTen);
            this.Name = "UC_SupplierCard";
            this.Size = new System.Drawing.Size(480, 220);
            ((System.ComponentModel.ISupportInitialize)(this.ptb_ha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblSdt;
        private System.Windows.Forms.Label lblDiachi;
        private System.Windows.Forms.PictureBox ptb_ha;
    }
}
