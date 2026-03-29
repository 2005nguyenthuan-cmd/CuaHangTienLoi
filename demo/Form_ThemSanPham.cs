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

    namespace demo
    {
        public partial class Form_ThemSanPham : Form
        {
            CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();
            int maSP = 0;

            public Form_ThemSanPham()
            {
                InitializeComponent();
                LoadDanhMuc();
            }
            public Form_ThemSanPham(int id)
            {
                InitializeComponent();
                LoadDanhMuc();
                maSP = id;
                var sp = db.SAN_PHAM.Find(id);

                if (sp != null)
                {
                    txtTenSP.Text = sp.TenSanPham;
                    txtGiaBan.Text = sp.GiaBan.ToString();
                    txtTonKho.Text = sp.SoLuongTon.ToString();
                    cbDanhMuc.SelectedValue = sp.MaDanhMuc;
                }
            }

            private void Form_ThemSanPham_Load(object sender, EventArgs e)
            {

            }

            void LoadDanhMuc()
            {
                cbDanhMuc.DataSource = db.DANH_MUC.ToList();
                cbDanhMuc.DisplayMember = "TenDanhMuc";
                cbDanhMuc.ValueMember = "MaDanhMuc";
            }

            private void btnThem_Click(object sender, EventArgs e)
            {
                if (maSP == 0)
                {
                    // THÊM
                    SAN_PHAM sp = new SAN_PHAM();

                    sp.TenSanPham = txtTenSP.Text;
                    sp.MaDanhMuc = (int)cbDanhMuc.SelectedValue;
                    sp.GiaBan = Convert.ToDecimal(txtGiaBan.Text);
                    sp.SoLuongTon = Convert.ToInt32(txtTonKho.Text);

                    db.SAN_PHAM.Add(sp);

                    MessageBox.Show("Thêm sản phẩm thành công");
                }
                else
                {
                    // SỬA
                    var sp = db.SAN_PHAM.Find(maSP);

                    if (sp != null)
                    {
                        sp.TenSanPham = txtTenSP.Text;
                        sp.MaDanhMuc = (int)cbDanhMuc.SelectedValue;
                        sp.GiaBan = Convert.ToDecimal(txtGiaBan.Text);
                        sp.SoLuongTon = Convert.ToInt32(txtTonKho.Text);
                    }

                    MessageBox.Show("Cập nhật sản phẩm thành công");
                }

                db.SaveChanges();
                this.Close();
            }
            private void btnHuy_Click(object sender, EventArgs e)
            {
                this.Close();
            }

        }
    }
