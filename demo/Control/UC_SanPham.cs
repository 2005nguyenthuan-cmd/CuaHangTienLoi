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
    public partial class UC_SanPham : UserControl
    {
        CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();

        public UC_SanPham()
        {
            InitializeComponent();
            LoadSanPham();
            LoadThongKe();
        }


        void LoadSanPham()
        {
            dgvSanPham.Rows.Clear();

            var list = db.SAN_PHAM.ToList();

            foreach (var sp in list)
            {
                dgvSanPham.Rows.Add(
                    sp.MaSanPham,
                    sp.TenSanPham,
                    sp.MaDanhMuc,
                    sp.GiaBan,
                    sp.SoLuongTon
                );
            }
        }

        void LoadThongKe()
        {
            label3.Text = db.SAN_PHAM.Count().ToString();

            label5.Text = db.SAN_PHAM
                .Count()
                .ToString();

            label6.Text = db.DANH_MUC.Count().ToString();

            label8.Text = db.SAN_PHAM
                .Sum(x => (int?)x.SoLuongTon)
                .ToString();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int maSP = Convert.ToInt32(dgvSanPham.Rows[e.RowIndex].Cells[0].Value);


            if (dgvSanPham.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                Form_ThemSanPham f = new Form_ThemSanPham(maSP);

                f.ShowDialog();

                LoadSanPham();
                LoadThongKe();
            }

            if (dgvSanPham.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                var confirm = MessageBox.Show(
                    "Bạn có chắc muốn xóa sản phẩm?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo
                );

                if (confirm == DialogResult.Yes)
                {
                    var sp = db.SAN_PHAM.Find(maSP);

                    if (sp != null)
                    {
                        db.SAN_PHAM.Remove(sp);
                        db.SaveChanges();

                        LoadSanPham();
                        LoadThongKe();
                    }
                }
            }
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            Form_ThemSanPham f = new Form_ThemSanPham();

            f.ShowDialog();

            LoadSanPham();
            LoadThongKe();
        }
    }
}