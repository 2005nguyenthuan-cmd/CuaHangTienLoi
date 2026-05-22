using demo.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{
    public class DashboardService
    {
        private readonly CUA_HANG_TIEN_LOI_Entities db;

        public DashboardService()
        {
            db = new CUA_HANG_TIEN_LOI_Entities();
        }

        public DashboardDTO GetDashboard(DateTime from, DateTime to)
        {
            var data = new DashboardDTO();

            // 1. Doanh thu + đơn hàng
            to = to.AddDays(1);

            var hoaDons = db.HOA_DON
                .Where(x => x.NgayLap >= from && x.NgayLap < to);

            data.TotalRevenue = hoaDons.Sum(x => (decimal?)x.TongTien) ?? 0;
            data.TotalOrders = hoaDons.Count();

            // 2. Lợi nhuận (tạm tính = doanh thu * 20%)
            data.TotalProfit = data.TotalRevenue * 0.2m;

            // 3. Tổng sản phẩm tồn
            data.TotalProducts = db.SAN_PHAM.Sum(x => (int?)x.SoLuongTon) ?? 0;

            // 4. Top sản phẩm
            data.TopProducts = db.CHI_TIET_HOA_DON
                .Where(x => x.HOA_DON.NgayLap >= from && x.HOA_DON.NgayLap < to)
                .GroupBy(x => x.SAN_PHAM.TenSanPham)
                .Select(g => new TopProductDTO
                {
                    TenSanPham = g.Key,
                    SoLuongBan = g.Sum(x => (int?)x.SoLuong) ?? 0,
                    DoanhThu = g.Sum(x => (decimal?)x.ThanhTien) ?? 0
                })
                .OrderByDescending(x => x.SoLuongBan)
                .Take(5)
                .ToList();

            // 5. Tồn thấp
            data.LowStocks = db.SAN_PHAM
                .Where(x => (x.SoLuongTon ?? 0) <= 10)
                .Select(x => new LowStockDTO
                {
                    TenSanPham = x.TenSanPham,
                    SoLuongTon = x.SoLuongTon ?? 0
                })
                .OrderBy(x => x.SoLuongTon)
                .ToList();

            // 6. Sắp hết hạn (30 ngày)
            DateTime now = DateTime.Now;
            DateTime future = now.AddDays(30);

            data.Expiries = db.CHI_TIET_NHAP
                .Where(x => x.HanSuDung != null
                    && (x.SAN_PHAM.SoLuongTon ?? 0) > 0
                    && x.HanSuDung >= now
                    && x.HanSuDung <= future)
                .GroupBy(x => x.SAN_PHAM.TenSanPham)
                .Select(g => new ExpiryDTO
                {
                    TenSanPham = g.Key,
                    HanSuDung = g.Min(x => x.HanSuDung.Value) // lấy hạn gần nhất
                })
                .OrderBy(x => x.HanSuDung)
                .ToList();

            // 7. Doanh thu theo ngày
            data.RevenueByDates = db.HOA_DON
            .Where(x => x.NgayLap >= from && x.NgayLap < to)
            .GroupBy(x => DbFunctions.TruncateTime(x.NgayLap))
            .Select(g => new RevenueByDateDTO
            {
                Ngay = g.Key.Value,
                DoanhThu = g.Sum(x => (decimal?)x.TongTien) ?? 0
            })
            .OrderBy(x => x.Ngay)
            .ToList();

            // 8. Doanh thu theo danh mục
            data.Categories = db.CHI_TIET_HOA_DON
            .Where(x => x.HOA_DON.NgayLap >= from && x.HOA_DON.NgayLap < to)
            .GroupBy(x => x.SAN_PHAM.DANH_MUC.TenDanhMuc)
            .Select(g => new CategoryDTO
            {
                TenDanhMuc = g.Key,
                DoanhThu = g.Sum(x => (decimal?)x.ThanhTien) ?? 0
            })
            .OrderByDescending(x => x.DoanhThu)
            .ToList();

            // 9. Doanh thu kỳ trước
            var days = (to.Date - from.Date).Days;

            var prevFrom = from.AddDays(-days);
            var prevTo = from;

            var prevRevenue = db.HOA_DON
                .Where(x => x.NgayLap >= prevFrom && x.NgayLap < prevTo)
                .Sum(x => (decimal?)x.TongTien) ?? 0;

            data.RevenueLastPeriod = prevRevenue;

            // 10. Tăng trưởng doanh thu
            if (prevRevenue > 0)
            {
                data.GrowthRevenuePercent =
                    (double)((data.TotalRevenue - prevRevenue) / prevRevenue * 100);
            }

            data.TotalCost = db.CHI_TIET_HOA_DON
            .Where(x => x.HOA_DON.NgayLap >= from && x.HOA_DON.NgayLap < to)
            .Sum(x => (decimal?)x.SoLuong * x.SAN_PHAM.GiaBan) ?? 0;

            // 11. Biên lợi nhuận
            if (data.TotalRevenue > 0)
            {
                data.ProfitMargin = (double)(data.TotalProfit / data.TotalRevenue * 100);
            }
            // 12. Insight
            if (data.GrowthRevenuePercent > 0)
            {
                data.Insights.Add($"Doanh thu tăng {data.GrowthRevenuePercent:N1}% so với kỳ trước");
            }
            else
            {
                data.Insights.Add($"Doanh thu giảm {Math.Abs(data.GrowthRevenuePercent):N1}%");
            }

            var topCategory = data.Categories.OrderByDescending(x => x.DoanhThu).FirstOrDefault();
            if (topCategory != null)
            {
                data.Insights.Add($"Danh mục {topCategory.TenDanhMuc} đang bán tốt nhất");
            }

            if (data.LowStocks.Count > 0)
            {
                data.Insights.Add($"Có {data.LowStocks.Count} sản phẩm tồn thấp");
            }

            if (data.Expiries.Count > 0)
            {
                data.Insights.Add($"Có {data.Expiries.Count} sản phẩm sắp hết hạn");
            }

            return data;
        }

    }

    public class DashboardDTO
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalProfit { get; set; }
        public int TotalProducts { get; set; }

        public List<TopProductDTO> TopProducts { get; set; }
        public List<LowStockDTO> LowStocks { get; set; }
        public List<ExpiryDTO> Expiries { get; set; }

        public List<RevenueByDateDTO> RevenueByDates { get; set; }
        public List<CategoryDTO> Categories { get; set; }

        public decimal RevenueLastPeriod { get; set; }
        public double GrowthRevenuePercent { get; set; }

        public decimal TotalCost { get; set; } // giá vốn
        public double ProfitMargin { get; set; }

        // Insight
        public List<string> Insights { get; set; } = new List<string>();
    }
    public class TopProductDTO
    {
        public string TenSanPham { get; set; }
        public int SoLuongBan { get; set; }
        public decimal DoanhThu { get; set; }
    }

    public class LowStockDTO
    {
        public string TenSanPham { get; set; }
        public int SoLuongTon { get; set; }
    }

    public class ExpiryDTO
    {
        public string TenSanPham { get; set; }
        public DateTime HanSuDung { get; set; }
    }

    public class RevenueByDateDTO
    {
        public DateTime Ngay { get; set; }
        public decimal DoanhThu { get; set; }
    }

    public class CategoryDTO
    {
        public string TenDanhMuc { get; set; }
        public decimal DoanhThu { get; set; }
    }

}
