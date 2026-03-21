using demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{
    public class InventoryService
    {
        private readonly CUA_HANG_TIEN_LOI_Entities db;

        public InventoryService()
        {
            db = new CUA_HANG_TIEN_LOI_Entities();
        }

        // 1. Tổng mặt hàng
        public int GetTongMatHang()
        {
            return db.SAN_PHAM.Count();
        }

        // 2. Tồn thấp
        public int GetTonThap()
        {
            return db.SAN_PHAM.Count(x => x.SoLuongTon < 10);
        }

        // 3. Sắp hết hạn (demo: bạn chưa có hạn dùng nên tạm để 0)
        public int GetSapHetHan()
        {
            return 0;
        }

        // 4. Giá trị kho
        public decimal GetGiaTriKho()
        {
            return db.SAN_PHAM
                .Sum(x => (decimal?)(x.SoLuongTon * x.GiaBan)) ?? 0;
        }

        // 5. Danh sách tồn kho
        public List<InventoryDTO> GetTonKho()
        {
            return db.SAN_PHAM
                .Select(x => new InventoryDTO
                {
                    MaSP = x.MaSanPham,
                    TenSP = x.TenSanPham,
                    SoLuong = x.SoLuongTon ?? 0,
                    TonToiThieu = 10,
                    TrangThai = (x.SoLuongTon < 10) ? "Tồn thấp" : "Tốt"
                }).ToList();
        }
    }

    public class InventoryDTO
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public int TonToiThieu { get; set; }
        public string TrangThai { get; set; }
    }
}
