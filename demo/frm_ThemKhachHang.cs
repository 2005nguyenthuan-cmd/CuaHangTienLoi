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
    public partial class FrmThemKhachHang : Form
    {
        public FrmThemKhachHang()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                KHACH_HANG kh = new KHACH_HANG();

                kh.TenKhachHang = txtTen.Text;
                kh.SoDienThoai = txtSDT.Text;
                kh.DiemTichLuy = int.Parse(txtDiem.Text);

                db.KHACH_HANG.Add(kh);
                db.SaveChanges();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
