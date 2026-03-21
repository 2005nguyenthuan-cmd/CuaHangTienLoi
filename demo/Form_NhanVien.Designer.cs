namespace demo
{
    partial class Form_NhanVien
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
            this.panel_header = new System.Windows.Forms.Panel();
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.panel_Main = new System.Windows.Forms.Panel();
            this.btn_banhang = new System.Windows.Forms.Button();
            this.btn_calamviec = new System.Windows.Forms.Button();
            this.btn_baocaoca = new System.Windows.Forms.Button();
            this.panel_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_header
            // 
            this.panel_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_header.Location = new System.Drawing.Point(0, 0);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(800, 100);
            this.panel_header.TabIndex = 0;
            // 
            // panel_Menu
            // 
            this.panel_Menu.BackColor = System.Drawing.Color.Lime;
            this.panel_Menu.Controls.Add(this.btn_baocaoca);
            this.panel_Menu.Controls.Add(this.btn_calamviec);
            this.panel_Menu.Controls.Add(this.btn_banhang);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Menu.Location = new System.Drawing.Point(0, 100);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(138, 350);
            this.panel_Menu.TabIndex = 1;
            // 
            // panel_Main
            // 
            this.panel_Main.BackColor = System.Drawing.Color.White;
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Main.Location = new System.Drawing.Point(138, 100);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(662, 350);
            this.panel_Main.TabIndex = 2;
            // 
            // btn_banhang
            // 
            this.btn_banhang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_banhang.Location = new System.Drawing.Point(0, 0);
            this.btn_banhang.Name = "btn_banhang";
            this.btn_banhang.Size = new System.Drawing.Size(138, 45);
            this.btn_banhang.TabIndex = 0;
            this.btn_banhang.Text = "Bán Hàng";
            this.btn_banhang.UseVisualStyleBackColor = true;
            this.btn_banhang.Click += new System.EventHandler(this.btn_banhang_Click);
            // 
            // btn_calamviec
            // 
            this.btn_calamviec.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_calamviec.Location = new System.Drawing.Point(0, 45);
            this.btn_calamviec.Name = "btn_calamviec";
            this.btn_calamviec.Size = new System.Drawing.Size(138, 45);
            this.btn_calamviec.TabIndex = 1;
            this.btn_calamviec.Text = "Ca Làm Việc";
            this.btn_calamviec.UseVisualStyleBackColor = true;
            this.btn_calamviec.Click += new System.EventHandler(this.btn_calamviec_Click);
            // 
            // btn_baocaoca
            // 
            this.btn_baocaoca.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_baocaoca.Location = new System.Drawing.Point(0, 90);
            this.btn_baocaoca.Name = "btn_baocaoca";
            this.btn_baocaoca.Size = new System.Drawing.Size(138, 45);
            this.btn_baocaoca.TabIndex = 2;
            this.btn_baocaoca.Text = "Báo Cáo Ca";
            this.btn_baocaoca.UseVisualStyleBackColor = true;
            this.btn_baocaoca.Click += new System.EventHandler(this.btn_baocaoca_Click);
            // 
            // Form_NhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel_Main);
            this.Controls.Add(this.panel_Menu);
            this.Controls.Add(this.panel_header);
            this.Name = "Form_NhanVien";
            this.Text = "Form_NhanVien";
            this.panel_Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Panel panel_Menu;
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.Button btn_baocaoca;
        private System.Windows.Forms.Button btn_calamviec;
        private System.Windows.Forms.Button btn_banhang;
    }
}