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

        // 1. Doanh thu hôm nay
        public decimal GetDoanhThuHomNay()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return db.HOA_DON
                .Where(x => x.NgayLap >= today && x.NgayLap < tomorrow)
                .Sum(x => (decimal?)x.TongTien) ?? 0;
        }

        //  2. Đơn hàng hôm nay
        public int GetDonHangHomNay()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return db.HOA_DON
                .Count(x => x.NgayLap >= today && x.NgayLap < tomorrow);
        }

        //  3. Khách hàng mới (5 người gần nhất)
        public int GetKhachMoi()
        {
            int maxId = db.KHACH_HANG.Max(x => x.MaKhachHang);

            return db.KHACH_HANG
                .Count(x => x.MaKhachHang >= maxId - 5);
        }

        //  4. Tổng sản phẩm tồn
        public int GetTongSanPham()
        {
            return db.SAN_PHAM.Sum(x => (int?)x.SoLuongTon) ?? 0;
        }

        //  5. Doanh thu 7 ngày (Line chart)
        public List<ChartData> GetDoanhThu7Ngay()
        {
            var today = DateTime.Today;

            var data = Enumerable.Range(0, 7)
                .Select(i => today.AddDays(-i))
                .OrderBy(d => d)
                .Select(d =>
                {
                    var start = d;
                    var end = d.AddDays(1);

                    return new ChartData
                    {
                        Ngay = d.ToString("dd/MM"),
                        GiaTri = db.HOA_DON
                            .Where(x => x.NgayLap >= start && x.NgayLap < end)
                            .Sum(x => (decimal?)x.TongTien) ?? 0
                    };
                }).ToList();

            return data;
        }

        //  6. Phân loại sản phẩm (Pie chart)
        public List<ChartData> GetDanhMucSanPham()
        {
            var data = db.SAN_PHAM
                .GroupBy(x => x.DANH_MUC.TenDanhMuc)
                .Select(g => new ChartData
                {
                    Ngay = g.Key, 
                    GiaTri = g.Count()
                }).ToList();

            return data;
        }
    }

    //  Class dùng cho chart
    public class ChartData
    {
        public string Ngay { get; set; }
        public decimal GiaTri { get; set; }
    }

}
