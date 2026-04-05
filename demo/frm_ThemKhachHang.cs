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
        KHACH_HANG khachHangDangSua = null;
        public FrmThemKhachHang(KHACH_HANG kh)
        {

            InitializeComponent();
            khachHangDangSua = kh;

            txtTen.Text = kh.TenKhachHang;
            txtSDT.Text = kh.SoDienThoai;
            txtDiem.Text = kh.DiemTichLuy.ToString();

            // Nếu là sửa → tắt nút thêm
            button2.Enabled = false;
        }
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (khachHangDangSua == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa!");
                return;
            }

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                var kh = db.KHACH_HANG.Find(khachHangDangSua.MaKhachHang);

                if (kh != null)
                {
                    kh.TenKhachHang = txtTen.Text;
                    kh.SoDienThoai = txtSDT.Text;

                    int diem = 0;
                    int.TryParse(txtDiem.Text, out diem);
                    kh.DiemTichLuy = diem;

                    db.SaveChanges();
                }
            }

            MessageBox.Show("Cập nhật thành công!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
