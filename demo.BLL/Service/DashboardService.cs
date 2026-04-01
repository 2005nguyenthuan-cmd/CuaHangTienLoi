using demo.DAL;
using System;
using System.Collections.Generic;
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
                    && x.SoLuongCon > 0
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

}
