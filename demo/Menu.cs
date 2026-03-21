using demo.BLL.Service;
using demo.Control;
using demo.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo
{
    public partial class Form_Menu : Form
    {
        CUA_HANG_TIEN_LOI_Entities CH = new CUA_HANG_TIEN_LOI_Entities();
        public Form_Menu()
        {
            InitializeComponent();
        }

        private void OpenControl(UserControl control)
        {
            panel_Main.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel_Main.Controls.Add(control);
        }

        private void Form_Menu_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Xin chào: " + UserSession.TenNhanVien
                 + " (" + UserSession.VaiTro + ")";
            authority();

        }

        private void authority()
        {
            if(UserSession.MaVaiTro == 1)
            {
                btn_clv.Visible = false;
                btn_bcc.Visible = false;
            }

            if (UserSession.MaVaiTro == 2)
            {
                // Nhân viên
                btn_dashboard.Visible = false;
                btn_sp.Visible = false;
                btn_kh.Visible = false;
                btn_ncc.Visible = false;
                btn_ql.Visible = false;
                btn_qlk.Visible = false;
                btn_lshd.Visible = false;
                btn_price_discount.Visible = false;
            }
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {

        }

        private void btn_pos_Click(object sender, EventArgs e)
        {

        }

        private void btn_sp_Click(object sender, EventArgs e)
        {
            UC_SanPham uc = new UC_SanPham();
            OpenControl(uc);
        }

        private void btn_qlk_Click(object sender, EventArgs e)
        {

        }

        private void btn_price_discount_Click(object sender, EventArgs e)
        {

        }

        private void btn_ql_Click(object sender, EventArgs e)
        {

        }

        private void btn_kh_Click(object sender, EventArgs e)
        {
            UC_KhachHang uc = new UC_KhachHang();
            OpenControl(uc);
        }

        private void btn_ncc_Click(object sender, EventArgs e)
        {
            UC_NhaCungCap uc = new UC_NhaCungCap();
            OpenControl(uc);
        }

        private void btn_lshd_Click(object sender, EventArgs e)
        {
            UC_LSHoaDon uc = new UC_LSHoaDon();
            OpenControl(uc);
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {

        }

        private void btn_bcc_Click(object sender, EventArgs e)
        {

        }

        private void btn_clv_Click(object sender, EventArgs e)
        {
            OpenControl(new UC_CaLamViec());
        }
    }
}
