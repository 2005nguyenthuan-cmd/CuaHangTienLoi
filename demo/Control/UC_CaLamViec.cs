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
    public partial class UC_CaLamViec : UserControl
    {
        public UC_CaLamViec()
        {
            InitializeComponent();
            this.Load += UC_CaLamViec_Load;
        }
        private void UC_CaLamViec_Load(object sender, EventArgs e)
        {
            CaLamViecService sv = new CaLamViecService();
            dgvCaLamViec.DataSource = sv.GetAll();
        }
        private void dgvCaLamViec_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvCaLamViec.Columns[e.ColumnIndex].Name == "TrangThai")
            {
                if (e.Value.ToString().Contains("Đang"))
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Gray;
                }
            }
        }
    }

}
