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
    public partial class UC_NhanVienCard : UserControl
    {
        public UC_NhanVienCard()
        {
            InitializeComponent();
        }
        public void SetData(string ten, string vaiTro, string trangThai, string caLam, string sdt, string ngay)
        {
            lblTen.Text = ten;
            lblVaiTro.Text = vaiTro;
            lblTrangThai.Text = trangThai;
            lblCaLam.Text = caLam;
            lblSDT.Text = sdt;
            lblNgay.Text = ngay;
            pictureBox1.Image = SystemIcons.Information.ToBitmap();
            lblVaiTro.ForeColor = string.Equals(vaiTro, "Quản Lý", StringComparison.OrdinalIgnoreCase)
                ? Color.FromArgb(37, 99, 235)
                : Color.FromArgb(14, 116, 144);
            lblTrangThai.ForeColor = trangThai.IndexOf("Đang", StringComparison.OrdinalIgnoreCase) >= 0
                ? Color.FromArgb(34, 139, 34)
                : Color.FromArgb(220, 38, 38);
        }
        private void UC_NhanVienCard_Load(object sender, EventArgs e)
        {
           
        }
    }
}
