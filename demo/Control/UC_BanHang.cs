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
    public partial class UC_BanHang : UserControl
    {
        public UC_BanHang()
        {
            InitializeComponent();
            NapDanhSachSanPham_Demo();
        }

        private void UC_BanHang_Load(object sender, EventArgs e)
        {
            
        }

        private void pnlCart_Paint(object sender, PaintEventArgs e)
        {

        }

        private Panel TaoTheSanPham_DarkTheme(string ten, string danhMuc, string gia, string soLuong)
        {
            Panel card = new Panel();
            card.Size = new Size(160, 200);
            card.BackColor = Color.FromArgb(34, 38, 53); // Màu nền xám xanh của thẻ
            card.Margin = new Padding(10);

            // Label Số lượng (Góc trên phải)
            Label lblSL = new Label();
            lblSL.Text = "SL: " + soLuong;
            lblSL.ForeColor = Color.DarkGray;
            lblSL.Font = new Font("Segoe UI", 8);
            lblSL.Location = new Point(110, 10);
            lblSL.AutoSize = true;

            // Hình ảnh (Giữa)
            PictureBox pic = new PictureBox();
            pic.Size = new Size(80, 80);
            pic.Location = new Point(40, 30);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            // pic.Image = Image.FromFile("duong_dan.jpg"); 

            // Label Tên (Dưới hình)
            Label lblTen = new Label();
            lblTen.Text = ten;
            lblTen.ForeColor = Color.White;
            lblTen.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTen.Location = new Point(5, 120);
            lblTen.Size = new Size(150, 20);
            lblTen.TextAlign = ContentAlignment.MiddleCenter; // Canh giữa chữ

            // Label Danh mục (Dưới tên, chữ nhỏ màu xám)
            Label lblDM = new Label();
            lblDM.Text = danhMuc;
            lblDM.ForeColor = Color.Gray;
            lblDM.Font = new Font("Segoe UI", 8);
            lblDM.Location = new Point(5, 145);
            lblDM.Size = new Size(150, 15);
            lblDM.TextAlign = ContentAlignment.MiddleCenter;

            // Label Giá (Dưới cùng, chữ Vàng)
            Label lblGia = new Label();
            lblGia.Text = gia;
            lblGia.ForeColor = Color.Gold;
            lblGia.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblGia.Location = new Point(5, 170);
            lblGia.Size = new Size(150, 25);
            lblGia.TextAlign = ContentAlignment.MiddleCenter;

            // Gắn vào thẻ
            card.Controls.Add(lblSL);
            card.Controls.Add(pic);
            card.Controls.Add(lblTen);
            card.Controls.Add(lblDM);
            card.Controls.Add(lblGia);

            return card;
        }

        private void NapDanhSachSanPham_Demo()
        {
            // Now this works because everything is in the same class!
            flpProducts.Controls.Clear();
            for (int i = 1; i <= 12; i++)
            {
                Panel theMoi = TaoTheSanPham_DarkTheme("Bánh Oreo " + i, "Bánh kẹo", "25.000 đ", "60");
                flpProducts.Controls.Add(theMoi);
            }
        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
