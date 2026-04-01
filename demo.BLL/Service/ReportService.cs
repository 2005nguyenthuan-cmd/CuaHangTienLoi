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
        public void ExportDashboard(DashboardDTO data, string filePath)
        {
            using (var wb = new XLWorkbook())
            {
                // ===== SHEET 1: KPI =====
                var ws1 = wb.Worksheets.Add("TongQuan");

                ws1.Cell(1, 1).Value = "Doanh thu";
                ws1.Cell(1, 2).Value = data.TotalRevenue;

                ws1.Cell(2, 1).Value = "Đơn hàng";
                ws1.Cell(2, 2).Value = data.TotalOrders;

                ws1.Cell(3, 1).Value = "Lợi nhuận";
                ws1.Cell(3, 2).Value = data.TotalProfit;

                ws1.Cell(4, 1).Value = "Tồn kho";
                ws1.Cell(4, 2).Value = data.TotalProducts;

                // ===== SHEET 2: TOP PRODUCT =====
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

                // ===== SHEET 3: LOW STOCK =====
                var ws3 = wb.Worksheets.Add("TonThap");

                ws3.Cell(1, 1).Value = "Tên sản phẩm";
                ws3.Cell(1, 2).Value = "Số lượng tồn";

                row = 2;
                foreach (var item in data.LowStocks)
                {
                    ws3.Cell(row, 1).Value = item.TenSanPham;
                    ws3.Cell(row, 2).Value = item.SoLuongTon;
                    row++;
                }

                // ===== SHEET 4: EXPIRY =====
                var ws4 = wb.Worksheets.Add("SapHetHan");

                ws4.Cell(1, 1).Value = "Tên sản phẩm";
                ws4.Cell(1, 2).Value = "Hạn sử dụng";

                row = 2;
                foreach (var item in data.Expiries)
                {
                    ws4.Cell(row, 1).Value = item.TenSanPham;
                    ws4.Cell(row, 2).Value = item.HanSuDung.ToString("dd/MM/yyyy");
                    row++;
                }

                // SAVE
                wb.SaveAs(filePath);
            }
        }
    }
}
