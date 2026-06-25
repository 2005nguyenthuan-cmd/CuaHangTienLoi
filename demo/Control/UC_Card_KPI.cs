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
    public partial class UC_Card_KPI : UserControl
    {
        public UC_Card_KPI()
        {
            InitializeComponent();
        }

        private void UC_Card_KPI_Load(object sender, EventArgs e)
        {
            pnMain.BackColor = Color.White;
            pnMain.Padding = new Padding(10);

            lblTitle.Font = new Font("Segoe UI", 10);
            lblValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
        }
        public void SetValue(string value)
        {
            lblValue.Text = value;
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }
    }
}
