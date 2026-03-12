using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using demo.DAL;

namespace demo.Control
{
    public partial class UC_LSHoaDon : UserControl
    {
        CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();
        public UC_LSHoaDon()
        {
            InitializeComponent();
            LoadHoaDon();
        }

        private void LoadHoaDon()
        {
            var data = db.HOA_DON
                .Select(hd => new
                {
                    hd.MaHoaDon,
                    hd.NgayLap,
                    SoLuongSP = db.CHI_TIET_HOA_DON
                        .Where(ct => ct.MaHoaDon == hd.MaHoaDon)
                        .Sum(ct => ct.SoLuong),

                    TongTien = db.CHI_TIET_HOA_DON
                        .Where(ct => ct.MaHoaDon == hd.MaHoaDon)
                        .Sum(ct => ct.ThanhTien)
                })
                .ToList();

            dgvHoaDon.DataSource = data;
        }

        private void UC_LSHoaDon_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
        }
    }
}
