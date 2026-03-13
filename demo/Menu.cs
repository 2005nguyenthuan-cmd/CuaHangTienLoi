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
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {

        }

        private void btn_pos_Click(object sender, EventArgs e)
        {

        }

        private void btn_sp_Click(object sender, EventArgs e)
        {

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

        }

        private void btn_lshd_Click(object sender, EventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {

        }
    }
}
