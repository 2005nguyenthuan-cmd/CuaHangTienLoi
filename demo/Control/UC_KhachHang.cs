using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using demo.DAL;

namespace demo.Control
{
    public partial class UC_KhachHang : UserControl
    {
        CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();
        public UC_KhachHang()
        {
            InitializeComponent();
        }

        private void panelheader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void UC_KhachHang_Load(object sender, EventArgs e)
        {
            LoadTopKhachHang();
        }
        void LoadTopKhachHang()
        {
            var topKhach = db.KHACH_HANG
                             .OrderByDescending(x => x.DiemTichLuy)
                             .Take(3)
                             .ToList();

            dgvKhachHang.DataSource = topKhach;
        }
    }
}
