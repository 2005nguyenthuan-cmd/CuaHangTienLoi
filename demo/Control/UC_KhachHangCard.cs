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
using System.Drawing.Drawing2D;

namespace demo.Control
{
    public partial class UC_KhachHangCard : UserControl
    {
        public UC_KhachHangCard()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Chống nhấp nháy khi vẽ lại
            this.MouseEnter += (s, e) => { isHovered = true; this.Invalidate(); };
            this.MouseLeave += (s, e) => { isHovered = false; this.Invalidate(); };
        }
        // Khai báo biến để kiểm soát trạng thái chuột
        private bool isHovered = false;

        // Đăng ký sự kiện trong Constructor của Control/Form
      
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // --- BẢNG MÀU DỊU NHẸ (PASTEL) ---
            // Màu mặc định: Xanh lơ nhẹ (Soft Azure)
            Color baseColor = Color.FromArgb(235, 245, 255);
            Color borderColor = Color.FromArgb(180, 210, 245);

            // Màu khi di chuột: Xanh Mint hoặc Xanh dương đậm hơn chút
            if (isHovered)
            {
                baseColor = Color.FromArgb(220, 240, 230); // Soft Mint
                borderColor = Color.FromArgb(150, 200, 180);
            }

            int borderRadius = 20;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            GraphicsPath path = GetRoundedPath(rect, borderRadius);

            // 1. Cắt vùng hiển thị
            this.Region = new Region(path);

            // 2. Vẽ nền (Fill)
            using (SolidBrush brush = new SolidBrush(baseColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            // 3. Vẽ viền (Border)
            using (Pen pen = new Pen(borderColor, 2f))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, path);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2f;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
        public void setdata(KHACH_HANG kh)
        {
            lblTen.Text = kh.TenKhachHang;
            lblSDT.Text = kh.SoDienThoai;
            lblDiem.Text =kh.DiemTichLuy.ToString();
        }
    }

}
