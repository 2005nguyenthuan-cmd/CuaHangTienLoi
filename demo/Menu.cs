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
    }
}
