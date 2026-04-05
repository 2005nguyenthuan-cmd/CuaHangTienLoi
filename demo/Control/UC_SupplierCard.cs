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
using System.IO;

namespace demo.Control
{
    public partial class UC_SupplierCard : UserControl
    {
        private readonly SupplierService supplierService;
        private NHA_CUNG_CAP currentNCC;
        public event Action ReloadData;
        public UC_SupplierCard()
        {
            InitializeComponent();
            supplierService = new SupplierService();
            this.Margin = new Padding(15);
        }

        public void SetData(NHA_CUNG_CAP ncc)
        {
            currentNCC = ncc; // lưu lại

            lblTen.Text = ncc.TenNCC;
            lblSdt.Text = ncc.SoDienThoai;
            lblDiachi.Text = ncc.DiaChi;

            string folder = Path.Combine(Application.StartupPath, "Resources");

            string imgPath = "";

            if (!string.IsNullOrEmpty(ncc.HinhAnh))
            {
                imgPath = Path.Combine(folder, ncc.HinhAnh);
            }

            if (!File.Exists(imgPath))
            {
                imgPath = Path.Combine(folder, "no-image.png");
            }

            using (var img = Image.FromFile(imgPath))
            {
                ptb_ha.Image = new Bitmap(img);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Form_NhaCungCap f = new Form_NhaCungCap();

            f.SetData(currentNCC); // truyền dữ liệu

            if (f.ShowDialog() == DialogResult.OK)
            {
                // reload lại card hoặc form cha
                ReloadData?.Invoke();
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa nhà cung cấp?", "Confirm",
        MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                supplierService.Delete(currentNCC.MaNCC);

                MessageBox.Show("Đã xóa");

                ReloadData?.Invoke();
            }
        }
    }
}
