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
            var list = SupplierService.GetAll();

            flowSuppliers.Controls.Clear();

            foreach (var ncc in list)
            {
                UC_SupplierCard card = new UC_SupplierCard();
                card.SetData(ncc);

                flowSuppliers.Controls.Add(card);
            }
        }
    }
}
