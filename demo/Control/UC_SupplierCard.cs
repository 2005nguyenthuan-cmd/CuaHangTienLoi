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
        public UC_SupplierCard()
        {
            InitializeComponent();
            supplierService = new SupplierService();
            this.Margin = new Padding(15);
        }

        public void SetData(NHA_CUNG_CAP ncc)
        {
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
    }
}
