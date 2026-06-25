namespace demo
{
    partial class Form_Menu
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
            this.lblUser = new System.Windows.Forms.Label();
            this.panel_Main = new System.Windows.Forms.Panel();
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.btn_clv = new System.Windows.Forms.Button();
            this.btn_bcc = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_lshd = new System.Windows.Forms.Button();
            this.btn_price_discount = new System.Windows.Forms.Button();
            this.btn_kh = new System.Windows.Forms.Button();
            this.btn_ql = new System.Windows.Forms.Button();
            this.btn_sp = new System.Windows.Forms.Button();
            this.btn_pos = new System.Windows.Forms.Button();
            this.btn_qlk = new System.Windows.Forms.Button();
            this.btn_dashboard = new System.Windows.Forms.Button();
            this.btn_ncc = new System.Windows.Forms.Button();
            this.panel_header.SuspendLayout();
            this.panel_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_header
            // 
            this.panel_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel_header.Controls.Add(this.lblUser);
            this.panel_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_header.Location = new System.Drawing.Point(0, 0);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(1053, 97);
            this.panel_header.TabIndex = 0;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(44, 52);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(71, 29);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "label";
            // 
            // panel_Main
            // 
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Main.Location = new System.Drawing.Point(181, 97);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(872, 595);
            this.panel_Main.TabIndex = 1;
            // 
            // panel_Menu
            // 
            this.panel_Menu.BackColor = System.Drawing.Color.Green;
            this.panel_Menu.Controls.Add(this.btn_exit);
            this.panel_Menu.Controls.Add(this.btn_clv);
            this.panel_Menu.Controls.Add(this.btn_bcc);
            this.panel_Menu.Controls.Add(this.btn_lshd);
            this.panel_Menu.Controls.Add(this.btn_price_discount);
            this.panel_Menu.Controls.Add(this.btn_kh);
            this.panel_Menu.Controls.Add(this.btn_ql);
            this.panel_Menu.Controls.Add(this.btn_ncc);
            this.panel_Menu.Controls.Add(this.btn_qlk);
            this.panel_Menu.Controls.Add(this.btn_sp);
            this.panel_Menu.Controls.Add(this.btn_pos);
            this.panel_Menu.Controls.Add(this.btn_dashboard);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Menu.Location = new System.Drawing.Point(0, 97);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(181, 595);
            this.panel_Menu.TabIndex = 2;
            // 
            // btn_clv
            // 
            this.btn_clv.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_clv.Location = new System.Drawing.Point(0, 409);
            this.btn_clv.Name = "btn_clv";
            this.btn_clv.Size = new System.Drawing.Size(181, 52);
            this.btn_clv.TabIndex = 12;
            this.btn_clv.Text = "Ca Làm Việc";
            this.btn_clv.UseVisualStyleBackColor = true;
            this.btn_clv.Click += new System.EventHandler(this.btn_clv_Click);
            // 
            // btn_bcc
            // 
            this.btn_bcc.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_bcc.Location = new System.Drawing.Point(0, 358);
            this.btn_bcc.Name = "btn_bcc";
            this.btn_bcc.Size = new System.Drawing.Size(181, 51);
            this.btn_bcc.TabIndex = 11;
            this.btn_bcc.Text = "Báo Cáo Ca";
            this.btn_bcc.UseVisualStyleBackColor = true;
            this.btn_bcc.Click += new System.EventHandler(this.btn_bcc_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_exit.Location = new System.Drawing.Point(0, 461);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(181, 55);
            this.btn_exit.TabIndex = 10;
            this.btn_exit.Text = "Đăng Xuất";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_lshd
            // 
            this.btn_lshd.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_lshd.Location = new System.Drawing.Point(0, 319);
            this.btn_lshd.Name = "btn_lshd";
            this.btn_lshd.Size = new System.Drawing.Size(181, 39);
            this.btn_lshd.TabIndex = 9;
            this.btn_lshd.Text = "Lịch Sử Hóa Đơn";
            this.btn_lshd.UseVisualStyleBackColor = true;
            this.btn_lshd.Click += new System.EventHandler(this.btn_lshd_Click);
            // 
            // btn_price_discount
            // 
            this.btn_price_discount.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_price_discount.Location = new System.Drawing.Point(0, 280);
            this.btn_price_discount.Name = "btn_price_discount";
            this.btn_price_discount.Size = new System.Drawing.Size(181, 39);
            this.btn_price_discount.TabIndex = 5;
            this.btn_price_discount.Text = "Giá _ Khuyến Mãi";
            this.btn_price_discount.UseVisualStyleBackColor = true;
            this.btn_price_discount.Click += new System.EventHandler(this.btn_price_discount_Click);
            // 
            // btn_kh
            // 
            this.btn_kh.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_kh.Location = new System.Drawing.Point(0, 234);
            this.btn_kh.Name = "btn_kh";
            this.btn_kh.Size = new System.Drawing.Size(181, 46);
            this.btn_kh.TabIndex = 7;
            this.btn_kh.Text = "Khách Hàng";
            this.btn_kh.UseVisualStyleBackColor = true;
            this.btn_kh.Click += new System.EventHandler(this.btn_kh_Click);
            // 
            // btn_ql
            // 
            this.btn_ql.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ql.Location = new System.Drawing.Point(0, 195);
            this.btn_ql.Name = "btn_ql";
            this.btn_ql.Size = new System.Drawing.Size(181, 39);
            this.btn_ql.TabIndex = 6;
            this.btn_ql.Text = "Quản Lý Nhân Viên";
            this.btn_ql.UseVisualStyleBackColor = true;
            this.btn_ql.Click += new System.EventHandler(this.btn_ql_Click);
            // 
            // btn_sp
            // 
            this.btn_sp.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_sp.Location = new System.Drawing.Point(0, 78);
            this.btn_sp.Name = "btn_sp";
            this.btn_sp.Size = new System.Drawing.Size(181, 39);
            this.btn_sp.TabIndex = 3;
            this.btn_sp.Text = "Sản Phẩm";
            this.btn_sp.UseVisualStyleBackColor = true;
            this.btn_sp.Click += new System.EventHandler(this.btn_sp_Click);
            // 
            // btn_pos
            // 
            this.btn_pos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_pos.Location = new System.Drawing.Point(0, 39);
            this.btn_pos.Name = "btn_pos";
            this.btn_pos.Size = new System.Drawing.Size(181, 39);
            this.btn_pos.TabIndex = 2;
            this.btn_pos.Text = "Bán Hàng (POS)";
            this.btn_pos.UseVisualStyleBackColor = true;
            this.btn_pos.Click += new System.EventHandler(this.btn_pos_Click);
            // 
            // btn_qlk
            // 
            this.btn_qlk.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_qlk.Location = new System.Drawing.Point(0, 117);
            this.btn_qlk.Name = "btn_qlk";
            this.btn_qlk.Size = new System.Drawing.Size(181, 39);
            this.btn_qlk.TabIndex = 4;
            this.btn_qlk.Text = "Quản Lý Kho";
            this.btn_qlk.UseVisualStyleBackColor = true;
            this.btn_qlk.Click += new System.EventHandler(this.btn_qlk_Click);
            // 
            // btn_dashboard
            // 
            this.btn_dashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_dashboard.Location = new System.Drawing.Point(0, 0);
            this.btn_dashboard.Name = "btn_dashboard";
            this.btn_dashboard.Size = new System.Drawing.Size(181, 39);
            this.btn_dashboard.TabIndex = 1;
            this.btn_dashboard.Text = "Tổng Quan";
            this.btn_dashboard.UseVisualStyleBackColor = true;
            this.btn_dashboard.Click += new System.EventHandler(this.btn_dashboard_Click);
            // 
            // btn_ncc
            // 
            this.btn_ncc.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ncc.Location = new System.Drawing.Point(0, 156);
            this.btn_ncc.Name = "btn_ncc";
            this.btn_ncc.Size = new System.Drawing.Size(181, 39);
            this.btn_ncc.TabIndex = 8;
            this.btn_ncc.Text = "Nhà Cung Cấp";
            this.btn_ncc.UseVisualStyleBackColor = true;
            this.btn_ncc.Click += new System.EventHandler(this.btn_ncc_Click);
            // 
            // Form_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 692);
            this.Controls.Add(this.panel_Main);
            this.Controls.Add(this.panel_Menu);
            this.Controls.Add(this.panel_header);
            this.Name = "Form_Menu";
            this.Text = "CỬA HÀNG TIỆN LỢI";
            this.Load += new System.EventHandler(this.Form_Menu_Load);
            this.panel_header.ResumeLayout(false);
            this.panel_header.PerformLayout();
            this.panel_Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.Panel panel_Menu;
        private System.Windows.Forms.Button btn_price_discount;
        private System.Windows.Forms.Button btn_ql;
        private System.Windows.Forms.Button btn_qlk;
        private System.Windows.Forms.Button btn_sp;
        private System.Windows.Forms.Button btn_pos;
        private System.Windows.Forms.Button btn_dashboard;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btn_lshd;
        private System.Windows.Forms.Button btn_ncc;
        private System.Windows.Forms.Button btn_kh;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_clv;
        private System.Windows.Forms.Button btn_bcc;
    }
}

