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
    public partial class UC_NhanVienDanhSach : UserControl
    {
        public UC_NhanVienDanhSach()
        {
            InitializeComponent();
        }
        public void SetupControls()
        {
            txtSearch.Visible = true;
            btnThem.Visible = true;
            btnSua.Visible = true;
            btnXoa.Visible = true;
        }
        public void LoadDanhSach()
        {
            var oldControls = panelContent.Controls.OfType<System.Windows.Forms.Control>().ToList();

            panelContent.SuspendLayout();
            panelContent.Controls.Clear();
            foreach (var control in oldControls)
            {
                control.Dispose();
            }

            var list = new[]
            {
                new {Ten="Nguyễn Văn A", VaiTro="Quản Lý", TrangThai="Đang hoạt động", Ca="Ca sáng", SDT="0901", Ngay="2024"},
                new {Ten="Nguyễn Văn B", VaiTro="Nhân Viên", TrangThai="Đang hoạt động", Ca="Ca chiều", SDT="0902", Ngay="2024"},
            };

            foreach (var nv in list)
            {
                UC_NhanVienCard card = new UC_NhanVienCard();
                card.SetData(nv.Ten, nv.VaiTro, nv.TrangThai, nv.Ca, nv.SDT, nv.Ngay);
                card.Margin = new Padding(0, 0, 20, 20);
                panelContent.Controls.Add(card);
            }

            panelContent.ResumeLayout();
        }

        private void UC_NhanVienDanhSach_Load(object sender, EventArgs e)
        {
            SetupControls();
            LoadDanhSach();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }
    }
}
