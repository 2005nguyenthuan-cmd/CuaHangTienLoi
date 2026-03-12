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

        }
    }
}
