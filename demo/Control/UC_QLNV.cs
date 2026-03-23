using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo.Control
{
    public partial class UC_QLNV : UserControl
    {
        public UC_QLNV()
        {
            InitializeComponent();
        }
        private void OpenChild(UserControl uc)
        {
            var oldControls = panelContent.Controls.OfType<System.Windows.Forms.Control>().ToList();

            panelContent.SuspendLayout();
            foreach (var control in oldControls)
            {
                panelContent.Controls.Remove(control);
                control.Dispose();
            }

            uc.Dock = DockStyle.Fill;
            uc.Margin = Padding.Empty;
            panelContent.Controls.Add(uc);
            uc.BringToFront();
            panelContent.ResumeLayout();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void UpdateTabButtonState(Button activeButton)
        {
            foreach (var button in new[] { btnDanhSach, btnLichLam })
            {
                bool isActive = button == activeButton;

                button.BackColor = isActive
                    ? Color.FromArgb(76, 175, 80)
                    : Color.White;
                button.ForeColor = isActive
                    ? Color.White
                    : Color.FromArgb(55, 65, 81);
                button.FlatAppearance.BorderColor = isActive
                    ? Color.FromArgb(76, 175, 80)
                    : Color.Silver;
            }
        }

        private void UC_QLNV_Load(object sender, EventArgs e)
        {
            btnDanhSach_Click(null, null);
        }

        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            UC_NhanVienDanhSach uc = new UC_NhanVienDanhSach();
            UpdateTabButtonState(btnDanhSach);
            OpenChild(uc);
        }

        private void btnLichLam_Click(object sender, EventArgs e)
        {
            UC_LichLamNhanVien uc = new UC_LichLamNhanVien();
            UpdateTabButtonState(btnLichLam);
            OpenChild(uc);
        }
    }
}

