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
using System.Windows.Forms.DataVisualization.Charting;

namespace demo.Control
{
    public partial class UC_TongQuan : UserControl
    {
        DashboardService dashboard = new DashboardService();
        public UC_TongQuan()
        {
            InitializeComponent();

            LoadCards();
            LoadChartDoanhThu();
            LoadChartDanhMuc();
        }
        void LoadCards()
        {
            flowTop.Controls.Clear();

            flowTop.Controls.Add(CreateCard("Doanh thu hôm nay",
                dashboard.GetDoanhThuHomNay().ToString("N0") + "đ"));

            flowTop.Controls.Add(CreateCard("Đơn hàng hôm nay",
                dashboard.GetDonHangHomNay().ToString()));

            flowTop.Controls.Add(CreateCard("Khách hàng mới",
                dashboard.GetKhachMoi().ToString()));

            flowTop.Controls.Add(CreateCard("Sản phẩm tồn",
                dashboard.GetTongSanPham().ToString()));
        }
        Panel CreateCard(string title, string value)
        {
            Panel p = new Panel();
            p.Width = 250;
            p.Height = 100;
            p.BackColor = Color.White;
            p.Margin = new Padding(10);

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Location = new Point(10, 10);
            lblTitle.AutoSize = true;

            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblValue.Location = new Point(10, 40);
            lblValue.AutoSize = true;

            p.Controls.Add(lblTitle);
            p.Controls.Add(lblValue);

            return p;
        }
        void LoadChartDoanhThu()
        {
            chart1.Series.Clear();

            var s = new Series("Doanh thu");
            s.ChartType = SeriesChartType.Line;
            s.BorderWidth = 3;

            var data = dashboard.GetDoanhThu7Ngay();

            foreach (var item in data)
            {
                s.Points.AddXY(item.Ngay, item.GiaTri);
            }

            chart1.Series.Add(s);
        }
        void LoadChartDanhMuc()
        {
            chart2.Series.Clear();

            var s = new Series("Danh mục");
            s.ChartType = SeriesChartType.Doughnut;

            var data = dashboard.GetDanhMucSanPham();

            foreach (var item in data)
            {
                s.Points.AddXY(item.Ngay, item.GiaTri);
            }

            chart2.Series.Add(s);
        }
    }
}
