using ClosedXML.Excel;
using System;

namespace demo.BLL.Service
{
    public class ReportService
    {
        public void ExportDashboard(DashboardDTO data, string filePath, DateTime from, DateTime to)
        {
            using (var wb = new XLWorkbook())
            {
                var wsInfo = wb.Worksheets.Add("ThongTin");
                BuildInfoSheet(wsInfo, data, from, to);

                var wsOverview = wb.Worksheets.Add("TongQuan");
                BuildOverviewSheet(wsOverview, data, from, to);

                var wsTopProducts = wb.Worksheets.Add("TopSanPham");
                BuildTopProductsSheet(wsTopProducts, data);

                var wsLowStocks = wb.Worksheets.Add("TonThap");
                BuildLowStocksSheet(wsLowStocks, data);

                var wsExpiry = wb.Worksheets.Add("SapHetHan");
                BuildExpirySheet(wsExpiry, data);

                var wsRevenueByDate = wb.Worksheets.Add("DoanhThuNgay");
                BuildRevenueByDateSheet(wsRevenueByDate, data);

                var wsCategories = wb.Worksheets.Add("DanhMuc");
                BuildCategorySheet(wsCategories, data);

                var wsInsight = wb.Worksheets.Add("Insight");
                BuildInsightSheet(wsInsight, data);

                wb.SaveAs(filePath);
            }
        }

        private static void BuildInfoSheet(IXLWorksheet ws, DashboardDTO data, DateTime from, DateTime to)
        {
            ws.Cell("A1").Value = "BÁO CÁO DASHBOARD";
            ws.Range("A1:D1").Merge().Style.Font.SetBold().Font.SetFontSize(16)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            ws.Cell("A2").Value = "Cửa hàng:";
            ws.Cell("B2").Value = "Mini Store ABC";
            ws.Cell("C2").Value = "Ngày xuất:";
            ws.Cell("D2").Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            ws.Cell("A3").Value = "Từ ngày:";
            ws.Cell("B3").Value = from.ToString("dd/MM/yyyy");
            ws.Cell("C3").Value = "Đến ngày:";
            ws.Cell("D3").Value = to.ToString("dd/MM/yyyy");

            ws.Cell("A5").Value = "Tổng doanh thu";
            ws.Cell("B5").Value = data.TotalRevenue;
            ws.Cell("A6").Value = "Tổng đơn hàng";
            ws.Cell("B6").Value = data.TotalOrders;
            ws.Cell("A7").Value = "Lợi nhuận";
            ws.Cell("B7").Value = data.TotalProfit;
            ws.Cell("A8").Value = "Tổng tồn kho";
            ws.Cell("B8").Value = data.TotalProducts;
            ws.Range("B5:B8").Style.NumberFormat.Format = "#,##0";

            ws.Cell("A10").Value = "Số dòng dữ liệu";
            ws.Cell("A11").Value = "Top sản phẩm";
            ws.Cell("B11").Value = data.TopProducts.Count;
            ws.Cell("A12").Value = "Tồn thấp";
            ws.Cell("B12").Value = data.LowStocks.Count;
            ws.Cell("A13").Value = "Sắp hết hạn";
            ws.Cell("B13").Value = data.Expiries.Count;
            ws.Cell("A14").Value = "Doanh thu theo ngày";
            ws.Cell("B14").Value = data.RevenueByDates.Count;
            ws.Cell("A15").Value = "Danh mục";
            ws.Cell("B15").Value = data.Categories.Count;

            ws.Columns().AdjustToContents();
        }

        private static void BuildOverviewSheet(IXLWorksheet ws, DashboardDTO data, DateTime from, DateTime to)
        {
            ws.Cell(1, 1).Value = "BÁO CÁO TỔNG QUAN";
            ws.Range(1, 1, 1, 4).Merge().Style
                .Font.SetBold().Font.SetFontSize(16)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            ws.Cell(3, 1).Value = "Từ ngày:";
            ws.Cell(3, 2).Value = from.ToString("dd/MM/yyyy");
            ws.Cell(4, 1).Value = "Đến ngày:";
            ws.Cell(4, 2).Value = to.ToString("dd/MM/yyyy");

            ws.Cell(6, 1).Value = "Doanh thu";
            ws.Cell(6, 2).Value = data.TotalRevenue;
            ws.Cell(7, 1).Value = "Đơn hàng";
            ws.Cell(7, 2).Value = data.TotalOrders;
            ws.Cell(8, 1).Value = "Lợi nhuận";
            ws.Cell(8, 2).Value = data.TotalProfit;
            ws.Cell(9, 1).Value = "Tồn kho";
            ws.Cell(9, 2).Value = data.TotalProducts;

            ws.Range(6, 2, 9, 2).Style.NumberFormat.Format = "#,##0";
            ws.Columns().AdjustToContents();
        }

        private static void BuildTopProductsSheet(IXLWorksheet ws, DashboardDTO data)
        {
            ws.Cell(1, 1).Value = "Tên sản phẩm";
            ws.Cell(1, 2).Value = "Số lượng bán";
            ws.Cell(1, 3).Value = "Doanh thu";

            int row = 2;
            foreach (var item in data.TopProducts)
            {
                ws.Cell(row, 1).Value = item.TenSanPham;
                ws.Cell(row, 2).Value = item.SoLuongBan;
                ws.Cell(row, 3).Value = item.DoanhThu;
                row++;
            }

            if (data.TopProducts.Count == 0)
            {
                WriteNoDataMessage(ws, 2, 3);
            }

            StyleHeader(ws, 1, 3);
            ws.Column(3).Style.NumberFormat.Format = "#,##0";
            ws.Columns().AdjustToContents();
        }

        private static void BuildLowStocksSheet(IXLWorksheet ws, DashboardDTO data)
        {
            ws.Cell(1, 1).Value = "Tên sản phẩm";
            ws.Cell(1, 2).Value = "Số lượng tồn";

            int row = 2;
            foreach (var item in data.LowStocks)
            {
                ws.Cell(row, 1).Value = item.TenSanPham;
                ws.Cell(row, 2).Value = item.SoLuongTon;

                if (item.SoLuongTon <= 5)
                {
                    ws.Row(row).Style.Fill.BackgroundColor = XLColor.LightCoral;
                }
                else if (item.SoLuongTon <= 10)
                {
                    ws.Row(row).Style.Fill.BackgroundColor = XLColor.LightPink;
                }
                else if (item.SoLuongTon <= 20)
                {
                    ws.Row(row).Style.Fill.BackgroundColor = XLColor.LightYellow;
                }

                row++;
            }

            if (data.LowStocks.Count == 0)
            {
                WriteNoDataMessage(ws, 2, 2);
            }

            StyleHeader(ws, 1, 2);
            ws.Columns().AdjustToContents();
        }

        private static void BuildExpirySheet(IXLWorksheet ws, DashboardDTO data)
        {
            ws.Cell(1, 1).Value = "Tên sản phẩm";
            ws.Cell(1, 2).Value = "Hạn sử dụng";

            int row = 2;
            foreach (var item in data.Expiries)
            {
                ws.Cell(row, 1).Value = item.TenSanPham;
                ws.Cell(row, 2).Value = item.HanSuDung;

                int days = (item.HanSuDung - DateTime.Now).Days;
                if (days <= 3)
                {
                    ws.Row(row).Style.Fill.BackgroundColor = XLColor.LightCoral;
                }
                else if (days <= 7)
                {
                    ws.Row(row).Style.Fill.BackgroundColor = XLColor.Khaki;
                }
                else if (days <= 30)
                {
                    ws.Row(row).Style.Fill.BackgroundColor = XLColor.LightYellow;
                }

                row++;
            }

            if (data.Expiries.Count == 0)
            {
                WriteNoDataMessage(ws, 2, 2);
            }

            StyleHeader(ws, 1, 2);
            ws.Column(2).Style.DateFormat.Format = "dd/MM/yyyy";
            ws.Columns().AdjustToContents();
        }

        private static void BuildRevenueByDateSheet(IXLWorksheet ws, DashboardDTO data)
        {
            ws.Cell(1, 1).Value = "Ngày";
            ws.Cell(1, 2).Value = "Doanh thu";

            int row = 2;
            foreach (var item in data.RevenueByDates)
            {
                ws.Cell(row, 1).Value = item.Ngay;
                ws.Cell(row, 2).Value = item.DoanhThu;
                row++;
            }

            if (data.RevenueByDates.Count == 0)
            {
                WriteNoDataMessage(ws, 2, 2);
            }

            StyleHeader(ws, 1, 2);
            ws.Column(1).Style.DateFormat.Format = "dd/MM/yyyy";
            ws.Column(2).Style.NumberFormat.Format = "#,##0";
            ws.Columns().AdjustToContents();
        }

        private static void BuildCategorySheet(IXLWorksheet ws, DashboardDTO data)
        {
            ws.Cell(1, 1).Value = "Danh mục";
            ws.Cell(1, 2).Value = "Doanh thu";

            int row = 2;
            foreach (var item in data.Categories)
            {
                ws.Cell(row, 1).Value = item.TenDanhMuc;
                ws.Cell(row, 2).Value = item.DoanhThu;
                row++;
            }

            if (data.Categories.Count == 0)
            {
                WriteNoDataMessage(ws, 2, 2);
            }

            StyleHeader(ws, 1, 2);
            ws.Column(2).Style.NumberFormat.Format = "#,##0";
            ws.Columns().AdjustToContents();
        }

        private static void BuildInsightSheet(IXLWorksheet ws, DashboardDTO data)
        {
            ws.Cell(1, 1).Value = "PHÂN TÍCH & NHẬN ĐỊNH";
            ws.Range("A1:D1").Merge().Style.Font.Bold = true;

            int row = 3;
            foreach (var item in data.Insights)
            {
                ws.Cell(row, 1).Value = "- " + item;
                row++;
            }

            if (data.Insights.Count == 0)
            {
                WriteNoDataMessage(ws, 3, 4);
            }

            ws.Columns().AdjustToContents();
        }

        private static void StyleHeader(IXLWorksheet ws, int row, int lastColumn)
        {
            var header = ws.Range(row, 1, row, lastColumn);
            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = XLColor.LightGray;
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private static void WriteNoDataMessage(IXLWorksheet worksheet, int row, int lastColumn)
        {
            worksheet.Cell(row, 1).Value = "Không có dữ liệu trong khoảng thời gian đã chọn.";
            worksheet.Range(row, 1, row, lastColumn).Merge();
            worksheet.Range(row, 1, row, lastColumn).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Range(row, 1, row, lastColumn).Style.Font.SetItalic();
            worksheet.Range(row, 1, row, lastColumn).Style.Font.FontColor = XLColor.Gray;
        }
    }
}
