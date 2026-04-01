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
        DashboardService dashboardService = new DashboardService();
        public UC_TongQuan()
        {
            InitializeComponent();
            
        }

        private void UC_TongQuan_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(245, 246, 250);
            dgvTopProducts.BorderStyle = BorderStyle.None;
            dgvTopProducts.EnableHeadersVisualStyles = false;
            dgvTopProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLowStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExpiry.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTopProducts.ReadOnly = true;
            dgvLowStock.ReadOnly = true;
            dgvExpiry.ReadOnly = true;

            LoadDashboard();
        }

        
        private void LoadDashboard()
        {
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date.AddDays(1).AddTicks(-1);

            var data = dashboardService.GetDashboard(from, to);

            // KPI
            cardRevenue.SetTitle("Doanh thu");
            cardRevenue.SetValue(data.TotalRevenue.ToString("N0"));
            cardOrders.SetTitle("Đơn hàng");
            cardOrders.SetValue(data.TotalOrders.ToString());
            cardProfit.SetTitle("Lợi nhuận");
            cardProfit.SetValue(data.TotalProfit.ToString("N0"));
            cardStock.SetTitle("Tồn kho");
            cardStock.SetValue(data.TotalProducts.ToString());



            // GRID
            dgvTopProducts.DataSource = data.TopProducts;
            dgvLowStock.DataSource = data.LowStocks;
            dgvExpiry.DataSource = data.Expiries;

            //Chart
            RenderChart(data.TopProducts);


            Highlight();
        }
        private void Highlight()
        {
            foreach (DataGridViewRow row in dgvLowStock.Rows)
            {
                if (row.Cells["SoLuongTon"].Value != null)
                {
                    int ton = Convert.ToInt32(row.Cells["SoLuongTon"].Value);
                    if (ton <= 10)
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }

            foreach (DataGridViewRow row in dgvExpiry.Rows)
            {
                if (row.Cells["HanSuDung"].Value != null)
                {
                    DateTime hsd = Convert.ToDateTime(row.Cells["HanSuDung"].Value);
                    int days = (hsd - DateTime.Now).Days;

                    if (days <= 3)
                        row.DefaultCellStyle.BackColor = Color.LightCoral; // đỏ
                    else if (days <= 7)
                        row.DefaultCellStyle.BackColor = Color.Khaki; // vàng
                }
            }
        }
        private void LoadChart()
        {
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date.AddDays(1).AddTicks(-1);

            var data = dashboardService.GetDashboard(from, to);

            chartRevenue.Series.Clear();

            var series = chartRevenue.Series.Add("Doanh thu");
            series.ChartType = SeriesChartType.Column;

            series.IsValueShownAsLabel = true; // hiện số
            series.LabelFormat = "N0"; // format tiền

            foreach (var item in data.TopProducts)
            {
                series.Points.AddXY(item.TenSanPham, item.DoanhThu);
            }
        }
        private void RenderChart(List<TopProductDTO> list)
        {
            chartRevenue.Series.Clear();

            var series = chartRevenue.Series.Add("Doanh thu");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "N0";

            foreach (var item in list)
            {
                series.Points.AddXY(item.TenSanPham, item.DoanhThu);
            }
        }
        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {
            LoadDashboard();
        }
    }
}
