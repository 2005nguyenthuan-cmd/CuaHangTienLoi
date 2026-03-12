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

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtTuNgay.Value;
            DateTime denNgay = dtDenNgay.Value;

            var data = db.HOA_DON
                .Where(hd => hd.NgayLap >= tuNgay && hd.NgayLap <= denNgay)
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

        private void btn_XoaLoc_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cbNhanVien.SelectedIndex = -1;
            cbKhachHang.SelectedIndex = -1;

            dtTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtDenNgay.Value = DateTime.Now;

            LoadHoaDon();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string keyword = txtSearch.Text;

                var data = db.HOA_DON
                    .Where(hd => hd.MaHoaDon.ToString().Contains(keyword))
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
        }
    }
}
