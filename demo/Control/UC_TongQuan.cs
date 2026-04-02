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
using System.IO;

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
            string growthText = data.GrowthRevenuePercent >= 0
            ? $"↑ {data.GrowthRevenuePercent:N1}%"
            : $"↓ {Math.Abs(data.GrowthRevenuePercent):N1}%";

            cardRevenue.SetValue($"{data.TotalRevenue:N0}\n{growthText}");

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
            RenderRevenue(data.RevenueByDates);
            RenderCategory(data.Categories);

            LoadChart(data);


            Highlight();
        }
        private void Highlight()
        {
            foreach (DataGridViewRow row in dgvLowStock.Rows)
            {
                if (row.Cells["SoLuongTon"].Value != null)
                {
                    int ton = Convert.ToInt32(row.Cells["SoLuongTon"].Value);
                    if (ton <= 5)
                        row.DefaultCellStyle.BackColor = Color.Coral; // đỏ
                    else if (ton <= 10)
                        row.DefaultCellStyle.BackColor = Color.LightCoral; // đỏ nhạt
                    else
                    if (ton <= 20)
                        row.DefaultCellStyle.BackColor = Color.LightYellow; // vàng
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
                    else if (days <= 30)
                        row.DefaultCellStyle.BackColor = Color.LightYellow; // vàng nhạt
                }
            }
        }
        private void LoadChart(DashboardDTO data)
        {
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date;


            chartRevenueByDate.Series.Clear();

            var series = chartRevenueByDate.Series.Add("Doanh thu");
            series.ChartType = SeriesChartType.Column;

            series.IsValueShownAsLabel = true; // hiện số
            series.LabelFormat = "N0"; // format tiền

            foreach (var item in data.TopProducts)
            {
                series.Points.AddXY(item.TenSanPham, item.DoanhThu);
            }
        }
        private void RenderRevenue(List<RevenueByDateDTO> list)
        {
            chartRevenueByDate.Series.Clear();

            var series = chartRevenueByDate.Series.Add("Doanh thu theo ngày");
            series.ChartType = SeriesChartType.Line;
            series.IsValueShownAsLabel = true;

            foreach (var item in list)
            {
                series.Points.AddXY(item.Ngay.ToString("dd/MM"), item.DoanhThu);
            }
        }
        private void RenderCategory(List<CategoryDTO> list)
        {
            chartCategory.Series.Clear();
            chartCategory.Titles.Clear();

            // Title
            chartCategory.Titles.Add("Doanh thu theo danh mục");

            var series = chartCategory.Series.Add("Danh mục");
            series.ChartType = SeriesChartType.Pie;

            // Hiển thị label %
            series.Label = "#PERCENT";
            series.LegendText = "#VALX";

            // Style
            chartCategory.Palette = ChartColorPalette.BrightPastel;
            chartCategory.BackColor = Color.White;
            chartCategory.BorderlineDashStyle = ChartDashStyle.Solid;
            chartCategory.BorderlineColor = Color.LightGray;

            series.IsValueShownAsLabel = true;

            foreach (var item in list)
            {
                series.Points.AddXY(item.TenDanhMuc, item.DoanhThu);
            }

            // Style đẹp hơn
            chartCategory.Legends[0].Docking = Docking.Right;
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date.AddDays(1).AddTicks(-1);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            sfd.FileName = "BaoCaoDashboard.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var data = dashboardService.GetDashboard(dtFrom.Value, dtTo.Value);

                ReportService reportService = new ReportService();
                reportService.ExportDashboard(data, sfd.FileName, from, to);

                MessageBox.Show("Xuất báo cáo thành công!");
            }
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnToday);
            dtFrom.Value = DateTime.Today;
            dtTo.Value = DateTime.Today;

            LoadDashboard();
        }

        private void btn7Days_Click(object sender, EventArgs e)
        {
            SetActiveButton(btn7Days);
            dtFrom.Value = DateTime.Today.AddDays(-7);
            dtTo.Value = DateTime.Today;

            LoadDashboard();
        }

        private void btn30Days_Click(object sender, EventArgs e)
        {
            SetActiveButton(btn30Days);
            dtFrom.Value = DateTime.Today.AddDays(-30);
            dtTo.Value = DateTime.Today;

            LoadDashboard();
        }
        private void StyleButton(Button btn)
        {
            btn.BackColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.Gray;
            btn.FlatAppearance.BorderSize = 1;
            btn.Height = 30;
            btn.Cursor = Cursors.Hand;
        }
        private void SetActiveButton(Button activeBtn)
        {
            ResetButtonColor(this); // quét toàn bộ UI
            activeBtn.BackColor = Color.LightBlue;
        }
        private void ResetButtonColor(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control c in parent.Controls)
            {
                if (c is Button btn && btn.Tag?.ToString() == "filter")
                {
                    btn.BackColor = Color.White;
                }

                if (c.HasChildren)
                {
                    ResetButtonColor(c); // đi sâu xuống
                }
            }
        }
    }
}
