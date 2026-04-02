using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{
    public class ReportService
    {
        public void ExportDashboard(DashboardDTO data, string filePath, DateTime from, DateTime to)
        {
            using (var wb = new XLWorkbook())
            {
                // ========================= TT ===========================
                var ws = wb.Worksheets.Add("ThongTin");
                ws.Cell("A2").Value = "Cửa hàng:";
                ws.Cell("B2").Value = "Mini Store ABC";

                ws.Cell("C2").Value = "Ngày xuất:";
                ws.Cell("D2").Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                // =========================
                // SHEET 1: TỔNG QUAN
                // =========================
                var ws1 = wb.Worksheets.Add("TongQuan");

                ws1.Cell(1, 1).Value = "BÁO CÁO TỔNG QUAN";
                ws1.Range(1, 1, 1, 4).Merge().Style
                    .Font.SetBold().Font.SetFontSize(16)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                ws1.Cell(3, 1).Value = "Từ ngày:";
                ws1.Cell(3, 2).Value = from.ToString("dd/MM/yyyy");

                ws1.Cell(4, 1).Value = "Đến ngày:";
                ws1.Cell(4, 2).Value = to.ToString("dd/MM/yyyy");

                ws1.Cell(6, 1).Value = "Doanh thu";
                ws1.Cell(6, 2).Value = data.TotalRevenue;

                ws1.Cell(7, 1).Value = "Đơn hàng";
                ws1.Cell(7, 2).Value = data.TotalOrders;

                ws1.Cell(8, 1).Value = "Lợi nhuận";
                ws1.Cell(8, 2).Value = data.TotalProfit;

                ws1.Cell(9, 1).Value = "Tồn kho";
                ws1.Cell(9, 2).Value = data.TotalProducts;

                ws1.Range(6, 2, 9, 2).Style.NumberFormat.Format = "#,##0";

                ws1.Columns().AdjustToContents();

                // =========================
                // SHEET 2: TOP SẢN PHẨM
                // =========================
                var ws2 = wb.Worksheets.Add("TopSanPham");

                ws2.Cell(1, 1).Value = "Tên sản phẩm";
                ws2.Cell(1, 2).Value = "Số lượng bán";
                ws2.Cell(1, 3).Value = "Doanh thu";

                int row = 2;
                foreach (var item in data.TopProducts)
                {
                    ws2.Cell(row, 1).Value = item.TenSanPham;
                    ws2.Cell(row, 2).Value = item.SoLuongBan;
                    ws2.Cell(row, 3).Value = item.DoanhThu;
                    row++;
                }

                // Style header
                var header2 = ws2.Range(1, 1, 1, 3);
                header2.Style.Font.Bold = true;
                header2.Style.Fill.BackgroundColor = XLColor.LightGray;
                header2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws2.Column(3).Style.NumberFormat.Format = "#,##0";
                ws2.Columns().AdjustToContents();

                // =========================
                // SHEET 3: TỒN THẤP
                // =========================
                var ws3 = wb.Worksheets.Add("TonThap");

                ws3.Cell(1, 1).Value = "Tên sản phẩm";
                ws3.Cell(1, 2).Value = "Số lượng tồn";

                row = 2;
                foreach (var item in data.LowStocks)
                {
                    ws3.Cell(row, 1).Value = item.TenSanPham;
                    ws3.Cell(row, 2).Value = item.SoLuongTon;

                    // Highlight giống UI
                    if (item.SoLuongTon <= 5)
                        ws3.Row(row).Style.Fill.BackgroundColor = XLColor.LightCoral;
                    else if (item.SoLuongTon <= 10)
                        ws3.Row(row).Style.Fill.BackgroundColor = XLColor.LightPink;
                    else if (item.SoLuongTon <= 20)
                        ws3.Row(row).Style.Fill.BackgroundColor = XLColor.LightYellow;

                    row++;
                }

                var header3 = ws3.Range(1, 1, 1, 2);
                header3.Style.Font.Bold = true;
                header3.Style.Fill.BackgroundColor = XLColor.LightGray;

                ws3.Columns().AdjustToContents();

                // =========================
                // SHEET 4: SẮP HẾT HẠN
                // =========================
                var ws4 = wb.Worksheets.Add("SapHetHan");

                ws4.Cell(1, 1).Value = "Tên sản phẩm";
                ws4.Cell(1, 2).Value = "Hạn sử dụng";

                row = 2;
                foreach (var item in data.Expiries)
                {
                    ws4.Cell(row, 1).Value = item.TenSanPham;
                    ws4.Cell(row, 2).Value = item.HanSuDung;

                    int days = (item.HanSuDung - DateTime.Now).Days;

                    if (days <= 3)
                        ws4.Row(row).Style.Fill.BackgroundColor = XLColor.LightCoral;
                    else if (days <= 7)
                        ws4.Row(row).Style.Fill.BackgroundColor = XLColor.Khaki;
                    else if (days <= 30)
                        ws4.Row(row).Style.Fill.BackgroundColor = XLColor.LightYellow;

                    row++;
                }

                ws4.Column(2).Style.DateFormat.Format = "dd/MM/yyyy";

                var header4 = ws4.Range(1, 1, 1, 2);
                header4.Style.Font.Bold = true;
                header4.Style.Fill.BackgroundColor = XLColor.LightGray;

                ws4.Columns().AdjustToContents();

                // =========================
                // SHEET 5: DOANH THU THEO NGÀY
                // =========================
                var ws5 = wb.Worksheets.Add("DoanhThuNgay");

                ws5.Cell(1, 1).Value = "Ngày";
                ws5.Cell(1, 2).Value = "Doanh thu";

                row = 2;
                foreach (var item in data.RevenueByDates)
                {
                    ws5.Cell(row, 1).Value = item.Ngay;
                    ws5.Cell(row, 2).Value = item.DoanhThu;
                    row++;
                }

                ws5.Column(1).Style.DateFormat.Format = "dd/MM/yyyy";
                ws5.Column(2).Style.NumberFormat.Format = "#,##0";

                var header5 = ws5.Range(1, 1, 1, 2);
                header5.Style.Font.Bold = true;
                header5.Style.Fill.BackgroundColor = XLColor.LightGray;

                ws5.Columns().AdjustToContents();

                // =========================
                // SHEET 6: DANH MỤC
                // =========================
                var ws6 = wb.Worksheets.Add("DanhMuc");

                ws6.Cell(1, 1).Value = "Danh mục";
                ws6.Cell(1, 2).Value = "Doanh thu";

                row = 2;
                foreach (var item in data.Categories)
                {
                    ws6.Cell(row, 1).Value = item.TenDanhMuc;
                    ws6.Cell(row, 2).Value = item.DoanhThu;
                    row++;
                }

                ws6.Column(2).Style.NumberFormat.Format = "#,##0";

                var header6 = ws6.Range(1, 1, 1, 2);
                header6.Style.Font.Bold = true;
                header6.Style.Fill.BackgroundColor = XLColor.LightGray;

                ws6.Columns().AdjustToContents();

                // =========================
                // SAVE FILE
                // =========================
                wb.SaveAs(filePath);
                // =========================INSIGHT===========================
                var wsInsight = wb.Worksheets.Add("Insight");

                wsInsight.Cell(1, 1).Value = "PHÂN TÍCH & NHẬN ĐỊNH";
                wsInsight.Range("A1:D1").Merge().Style.Font.Bold = true;

                
                foreach (var item in data.Insights)
                {
                    wsInsight.Cell(row, 1).Value = "- " + item;
                    row++;
                }
            }
        }

    }
}
