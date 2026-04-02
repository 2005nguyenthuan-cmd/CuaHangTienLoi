using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{
    public class HoaDonDTO
    {
        public int MaDon { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal? TongTien { get; set; }
        public DateTime? NgayLap { get; set; }
    }
}
