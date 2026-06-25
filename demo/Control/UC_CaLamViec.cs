using demo.BLL.Service;
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

namespace demo.Control
{
    public partial class UC_CaLamViec : UserControl
    {
        CaLamViecService sv = new CaLamViecService();
        CA_LAM_VIEC caHienTai;
        Timer timer1 = new Timer();
        public UC_CaLamViec()
        {
            InitializeComponent();
            this.Load += UC_CaLamViec_Load;
        }
        private void UC_CaLamViec_Load(object sender, EventArgs e)
        {
            flpDonHang.SizeChanged += (s, ev) =>
            {
                foreach (System.Windows.Forms.Control c in flpDonHang.Controls)
                {
                    c.Width = flpDonHang.ClientSize.Width - 25;
                }
            };
            CaLamViecService sv = new CaLamViecService();
        
            LoadCaHoatDong();
            if (caHienTai != null)
            {
                LoadDonHangTheoCa(caHienTai);
            }
            // nhật ký
            LoadNhatKyThat();

            // Timer chạy mỗi phút
            timer1.Interval = 60000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }
        void LoadCaHoatDong()
        {
            caHienTai = sv.GetCaDangHoatDong();

            if (caHienTai == null)
            {
                lblStatus.Text = "Không có ca hoạt động";
                return;
            }

            DateTime now = DateTime.Now;

            DateTime batDau = DateTime.Today.Add(caHienTai.GioBatDau.Value);
            DateTime ketThuc = DateTime.Today.Add(caHienTai.GioKetThuc.Value);

            // xử lý ca đêm
            if (caHienTai.GioBatDau > caHienTai.GioKetThuc)
            {
                if (now.TimeOfDay < caHienTai.GioKetThuc)
                    batDau = batDau.AddDays(-1);
                else
                    ketThuc = ketThuc.AddDays(1);
            }

            TimeSpan daLam = now - batDau;
            TimeSpan conLai = ketThuc - now;

            lblStatus.Text = $"🟢 {caHienTai.TenCa}";
            lblInfo.Text = $"Bắt đầu lúc {batDau:HH:mm} - " +
                           $"Đã làm: {daLam.Hours} giờ {daLam.Minutes} phút - " +
                           $"Còn lại: {conLai.Hours} giờ {conLai.Minutes} phút";

            lblTimeNow.Text = now.ToString("HH:mm");
            var thongKe = sv.GetThongKeCa(caHienTai);
           
            lblSoDon.Text = thongKe.soDon.ToString();
            lblDoanhThu.Text = thongKe.doanhThu.ToString("N0") + " đ";

            if (thongKe.soDon > 0)
                lblDonTB.Text = (thongKe.doanhThu / thongKe.soDon).ToString("N0") + " đ";
            else
                lblDonTB.Text = "0 đ";

            lblSoSP.Text = thongKe.soSP.ToString();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            LoadCaHoatDong();
        }
        
        private Panel TaoDonHang(string ma, string gio, int sp, decimal tien, string pt)
        {
            Panel p = new Panel();
            p.Height = 50;
            p.Width = flpDonHang.ClientSize.Width - 25; // 🔥 THÊM DÒNG NÀY
            p.BackColor = Color.White;
            p.Margin = new Padding(5);

            // bảng layout
            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.Dock = DockStyle.Fill;
            tbl.ColumnCount = 5;
            tbl.RowCount = 1;

            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            // label
            Label l1 = new Label()
            {
                Text = "#ORD-" + ma,
                ForeColor = Color.RoyalBlue,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label l2 = new Label()
            {
                Text = gio,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label l3 = new Label()
            {
                Text = sp + " SP",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label l4 = new Label()
            {
                Text = tien.ToString("N0") + "đ",
                ForeColor = Color.Green,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };

            Label l5 = new Label()
            {
                Text = pt,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // màu thanh toán
            if (pt == "Tiền mặt") l5.BackColor = Color.LightGreen;
            else if (pt == "QR") l5.BackColor = Color.Orange;
            else l5.BackColor = Color.LightBlue;

            // add vào bảng
            tbl.Controls.Add(l1, 0, 0);
            tbl.Controls.Add(l2, 1, 0);
            tbl.Controls.Add(l3, 2, 0);
            tbl.Controls.Add(l4, 3, 0);
            tbl.Controls.Add(l5, 4, 0);

            p.Controls.Add(tbl);

            return p;
        }
        private string LayGioGia(int maHoaDon)
        {
            int index = maHoaDon % 3;

            if (index == 0) return "08:00";
            if (index == 1) return "15:00";
            return "22:00";
        }
       
        private string LayPhuongThuc(int maHoaDon)
        {
            int x = maHoaDon % 3;
            if (x == 0) return "Tiền mặt";
            if (x == 1) return "QR";
            return "Thẻ";
        }
        private void LoadDonHangTheoCa(CA_LAM_VIEC ca)
        {
            flpDonHang.Controls.Clear();

            var ds = sv.LayHoaDonTheoCa(ca);

            foreach (var hd in ds)
            {
                var p = TaoDonHang(
                 hd.MaHoaDon.ToString(),
                 LayGioGia(hd.MaHoaDon),
                sv.LaySoSP(hd.MaHoaDon),
                  hd.TongTien ?? 0,
                   LayPhuongThuc(hd.MaHoaDon)
                                            );

                p.Width = flpDonHang.ClientSize.Width - 25; // 🔥 QUAN TRỌNG

                flpDonHang.Controls.Add(p);
            }
        }

        // nhật ký
        private void LoadNhatKyThat()
        {
            flpNhatKy.Controls.Clear();

            // 1. bắt đầu ca
            flpNhatKy.Controls.Add(
                TaoItemNhatKy(
                    caHienTai.GioBatDau.Value.ToString(@"hh\:mm"),
                    "Bắt đầu " + caHienTai.TenCa,
                    Color.Green
                )
            );

            // 2. đơn hàng
            var ds = sv.LayHoaDonTheoCa(caHienTai);

            foreach (var hd in ds)
            {
                flpNhatKy.Controls.Add(
                    TaoItemNhatKy(
                        LayGioGia(hd.MaHoaDon),
                        $"Đơn hàng #{hd.MaHoaDon}",
                        Color.Blue
                    )
                );
            }

            // 3. hiện tại
            flpNhatKy.Controls.Add(
                TaoItemNhatKy(
                    DateTime.Now.ToString("HH:mm"),
                    "Hiện tại",
                    Color.Green
                )
            );
        }
        private Panel TaoItemNhatKy(string time, string content, Color color)
        {
            Panel p = new Panel();
            p.Height = 60;
            p.Width = flpNhatKy.ClientSize.Width - 25;
            p.Margin = new Padding(5);
            p.BackColor = Color.White;

            // chấm tròn
            Panel dot = new Panel();
            dot.Width = 12;
            dot.Height = 12;
            dot.BackColor = color;
            dot.Location = new Point(10, 20);

            // giờ
            Label lblTime = new Label();
            lblTime.Text = time;
            lblTime.ForeColor = Color.Gray;
            lblTime.Location = new Point(30, 5);
            lblTime.AutoSize = true;

            // nội dung
            Label lblContent = new Label();
            lblContent.Text = content;
            lblContent.Location = new Point(30, 25);
            lblContent.AutoSize = true;

            p.Controls.Add(dot);
            p.Controls.Add(lblTime);
            p.Controls.Add(lblContent);

            return p;
        }



    }

}
