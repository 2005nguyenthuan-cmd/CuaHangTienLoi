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

namespace demo.Control
{
    public partial class UC_KhachHangCard : UserControl
    {
        public UC_KhachHangCard()
        {
            InitializeComponent();
        }
        public void setdata(KHACH_HANG kh)
        {
            lblTen.Text = kh.TenKhachHang;
            lblSDT.Text = kh.SoDienThoai;
            lblDiem.Text =kh.DiemTichLuy.ToString();
        }
    }
}
