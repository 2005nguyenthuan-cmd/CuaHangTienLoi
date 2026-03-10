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
            this.panel_Main = new System.Windows.Forms.Panel();
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.lblUser = new System.Windows.Forms.Label();
            this.btn_dashboard = new System.Windows.Forms.Button();
            this.btn_pos = new System.Windows.Forms.Button();
            this.btn_sp = new System.Windows.Forms.Button();
            this.btn_qlk = new System.Windows.Forms.Button();
            this.btn_price_discount = new System.Windows.Forms.Button();
            this.btn_ql = new System.Windows.Forms.Button();
            this.btn_kh = new System.Windows.Forms.Button();
            this.btn_ncc = new System.Windows.Forms.Button();
            this.btn_lshd = new System.Windows.Forms.Button();
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
            this.panel_header.Size = new System.Drawing.Size(1022, 97);
            this.panel_header.TabIndex = 0;
            // 
            // panel_Main
            // 
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Main.Location = new System.Drawing.Point(0, 97);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(1022, 485);
            this.panel_Main.TabIndex = 1;
            // 
            // panel_Menu
            // 
            this.panel_Menu.BackColor = System.Drawing.Color.Green;
            this.panel_Menu.Controls.Add(this.btn_lshd);
            this.panel_Menu.Controls.Add(this.btn_price_discount);
            this.panel_Menu.Controls.Add(this.btn_kh);
            this.panel_Menu.Controls.Add(this.btn_ql);
            this.panel_Menu.Controls.Add(this.btn_sp);
            this.panel_Menu.Controls.Add(this.btn_pos);
            this.panel_Menu.Controls.Add(this.btn_qlk);
            this.panel_Menu.Controls.Add(this.btn_dashboard);
            this.panel_Menu.Controls.Add(this.btn_ncc);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Menu.Location = new System.Drawing.Point(0, 97);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(200, 485);
            this.panel_Menu.TabIndex = 2;
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
            // btn_dashboard
            // 
            this.btn_dashboard.Location = new System.Drawing.Point(17, 6);
            this.btn_dashboard.Name = "btn_dashboard";
            this.btn_dashboard.Size = new System.Drawing.Size(136, 39);
            this.btn_dashboard.TabIndex = 1;
            this.btn_dashboard.Text = "Tổng Quan";
            this.btn_dashboard.UseVisualStyleBackColor = true;
            // 
            // btn_pos
            // 
            this.btn_pos.Location = new System.Drawing.Point(18, 51);
            this.btn_pos.Name = "btn_pos";
            this.btn_pos.Size = new System.Drawing.Size(136, 39);
            this.btn_pos.TabIndex = 2;
            this.btn_pos.Text = "Bán Hàng (POS)";
            this.btn_pos.UseVisualStyleBackColor = true;
            // 
            // btn_sp
            // 
            this.btn_sp.Location = new System.Drawing.Point(17, 96);
            this.btn_sp.Name = "btn_sp";
            this.btn_sp.Size = new System.Drawing.Size(136, 39);
            this.btn_sp.TabIndex = 3;
            this.btn_sp.Text = "Sản Phẩm";
            this.btn_sp.UseVisualStyleBackColor = true;
            // 
            // btn_qlk
            // 
            this.btn_qlk.Location = new System.Drawing.Point(17, 141);
            this.btn_qlk.Name = "btn_qlk";
            this.btn_qlk.Size = new System.Drawing.Size(136, 39);
            this.btn_qlk.TabIndex = 4;
            this.btn_qlk.Text = "Quản Lý Kho";
            this.btn_qlk.UseVisualStyleBackColor = true;
            // 
            // btn_price_discount
            // 
            this.btn_price_discount.Location = new System.Drawing.Point(18, 186);
            this.btn_price_discount.Name = "btn_price_discount";
            this.btn_price_discount.Size = new System.Drawing.Size(136, 39);
            this.btn_price_discount.TabIndex = 5;
            this.btn_price_discount.Text = "Giá _ Khuyến Mãi";
            this.btn_price_discount.UseVisualStyleBackColor = true;
            // 
            // btn_ql
            // 
            this.btn_ql.Location = new System.Drawing.Point(17, 231);
            this.btn_ql.Name = "btn_ql";
            this.btn_ql.Size = new System.Drawing.Size(136, 39);
            this.btn_ql.TabIndex = 6;
            this.btn_ql.Text = "Quản Lý Nhân Viên";
            this.btn_ql.UseVisualStyleBackColor = true;
            // 
            // btn_kh
            // 
            this.btn_kh.Location = new System.Drawing.Point(17, 276);
            this.btn_kh.Name = "btn_kh";
            this.btn_kh.Size = new System.Drawing.Size(135, 46);
            this.btn_kh.TabIndex = 7;
            this.btn_kh.Text = "Khách Hàng";
            this.btn_kh.UseVisualStyleBackColor = true;
            // 
            // btn_ncc
            // 
            this.btn_ncc.Location = new System.Drawing.Point(17, 328);
            this.btn_ncc.Name = "btn_ncc";
            this.btn_ncc.Size = new System.Drawing.Size(136, 39);
            this.btn_ncc.TabIndex = 8;
            this.btn_ncc.Text = "Nhà Cung Cấp";
            this.btn_ncc.UseVisualStyleBackColor = true;
            // 
            // btn_lshd
            // 
            this.btn_lshd.Location = new System.Drawing.Point(18, 373);
            this.btn_lshd.Name = "btn_lshd";
            this.btn_lshd.Size = new System.Drawing.Size(136, 39);
            this.btn_lshd.TabIndex = 9;
            this.btn_lshd.Text = "Lịch Sử Hóa Đơn";
            this.btn_lshd.UseVisualStyleBackColor = true;
            // 
            // Form_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 582);
            this.Controls.Add(this.panel_Menu);
            this.Controls.Add(this.panel_Main);
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
    }
}

