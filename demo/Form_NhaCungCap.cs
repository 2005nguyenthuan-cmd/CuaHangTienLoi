using demo.BLL.Service;
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
    public partial class Form_NhaCungCap : Form
    {
        private readonly SupplierService service = new SupplierService();

        int maNCC = 0;
        string pathImage = "";
        public Form_NhaCungCap()
        {
            InitializeComponent();
        }

        private void btn_anh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pathImage = ofd.FileName;

                picHinhAnh.ImageLocation = pathImage;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            NHA_CUNG_CAP ncc = new NHA_CUNG_CAP();

            ncc.TenNCC = txtTenNCC.Text;
            ncc.SoDienThoai = txtSDT.Text;
            ncc.DiaChi = txtDiaChi.Text;
            ncc.HinhAnh = pathImage;

            service.Add(ncc);

            MessageBox.Show("Thêm thành công");
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            NHA_CUNG_CAP ncc = new NHA_CUNG_CAP();

            ncc.MaNCC = maNCC;
            ncc.TenNCC = txtTenNCC.Text;
            ncc.SoDienThoai = txtSDT.Text;
            ncc.DiaChi = txtDiaChi.Text;
            ncc.HinhAnh = pathImage;

            service.Update(ncc);

            MessageBox.Show("Cập nhật thành công");
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa nhà cung cấp?", "Confirm",
        MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                service.Delete(maNCC);

                MessageBox.Show("Đã xóa");

                this.Close();
            }
        }
        public void SetData(NHA_CUNG_CAP ncc)
        {
            maNCC = ncc.MaNCC;

            txtTenNCC.Text = ncc.TenNCC;
            txtSDT.Text = ncc.SoDienThoai;
            txtDiaChi.Text = ncc.DiaChi;

            pathImage = ncc.HinhAnh;

            picHinhAnh.ImageLocation = pathImage;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
