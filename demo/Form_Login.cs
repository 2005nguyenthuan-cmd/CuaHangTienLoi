using demo.BLL.Service;
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
    public partial class Form_Login : Form
    {
        UserService userService = new UserService();

        public Form_Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';

        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu");
                return;
            }

            var user = userService.Login(username, password);

            if (user != null)
            {
                UserSession.MaNhanVien = user.MaNhanVien;
                UserSession.TenNhanVien = user.TenNhanVien;
                UserSession.TenDangNhap = user.TenDangNhap;
                UserSession.VaiTro = user.VAI_TRO?.TenVaiTro;
                UserSession.MaVaiTro = user.MaVaiTro;

                MessageBox.Show("Đăng nhập thành công!");

                Form_Menu f = new Form_Menu();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0'; // hiện mật khẩu
            }
            else
            {
                txtPassword.PasswordChar = '*'; // ẩn mật khẩu
            }
        }


    }
}
