using demo.BLL.Service;
using demo.Control;
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
            panel_Main.Controls.Clear();

            // 2. Lấy cái "kênh" UC_BanHang của Kiệt đã làm sẵn mang ra
            UC_BanHang ucCuaKiet = new UC_BanHang();
            ucCuaKiet.Dock = DockStyle.Fill; // Bắt nó phình to lấp đầy vùng trắng

            // 3. Chiếu lên màn hình (Nhét vào Panel chính)
            panel_Main.Controls.Add(ucCuaKiet);
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
            UC_KhuyenMai uc = new UC_KhuyenMai();
            OpenControl(uc);
        }

        private void btn_ql_Click(object sender, EventArgs e)
        {
            UC_QLNV uc = new UC_QLNV();
            OpenControl(uc);
        }

        private void btn_kh_Click(object sender, EventArgs e)
        {

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


    }
}
