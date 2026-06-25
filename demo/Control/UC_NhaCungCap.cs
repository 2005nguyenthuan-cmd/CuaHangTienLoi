using demo.BLL.Service;
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
    public partial class UC_NhaCungCap : UserControl
    {
        private readonly SupplierService SupplierService;
        public UC_NhaCungCap()
        {
            InitializeComponent();
            SupplierService = new SupplierService();
            flowSuppliers.Padding = new Padding(10);
            LoadSuppliers();
        }



        private void LoadSuppliers()
        {

            flowSuppliers.Controls.Clear();
            var list = SupplierService.GetAll();

            foreach (var ncc in list)
            {
                UC_SupplierCard card = new UC_SupplierCard();
                card.SetData(ncc);

                card.ReloadData += () =>
                {
                    LoadSuppliers();
                };
                flowSuppliers.Controls.Add(card);
            }
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            Form_NhaCungCap f = new Form_NhaCungCap();

            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadSuppliers();
            }
        }
    }
}
