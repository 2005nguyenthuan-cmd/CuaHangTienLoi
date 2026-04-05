using demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo.BLL.Service
{

    public class HoaDonService
    {
        string LayCa(DateTime ngay)
        {
            int gio = ngay.Hour;

            if (gio >= 6 && gio < 14)
                return "Ca sáng";
            else if (gio >= 14 && gio < 22)
                return "Ca chiều";
            else
                return "Ca đêm";
        }
        TimeSpan LayGioBatDau(string ca)
        {
            if (ca == "Ca sáng")
                return new TimeSpan(6, 0, 0);
            if (ca == "Ca chiều")
                return new TimeSpan(14, 0, 0);

            return new TimeSpan(22, 0, 0); // ca đêm
        }
        TimeSpan LayGioKetThuc(string ca)
        {
            if (ca == "Ca sáng")
                return new TimeSpan(14, 0, 0);
            if (ca == "Ca chiều")
                return new TimeSpan(22, 0, 0);

            return new TimeSpan(6, 0, 0); // ca đêm
        }
        CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();

        // 1. Hàm lấy danh sách chi tiết (Để đổ vào DataGridView)
        public List<HoaDonDTO> GetHoaDonChiTiet(int maNhanVien, DateTime tuNgay, DateTime denNgay)
        {
            var list = db.HOA_DON
                .Where(hd => hd.NgayLap >= tuNgay
          && hd.NgayLap <= denNgay)
                .Select(hd => new
                {
                    MaDon = hd.MaHoaDon,
                    DanhSachTen = hd.CHI_TIET_HOA_DON.Select(ct => ct.SAN_PHAM.TenSanPham).Distinct(),
                    // Tính toán tạm thời
                    TmpSoLuong = hd.CHI_TIET_HOA_DON.Sum(ct => (int?)ct.SoLuong) ?? 0,
                    hd.TongTien,
                    hd.NgayLap
                })
                .AsEnumerable()
                .Select(x => new HoaDonDTO // Sử dụng Class cụ thể ở đây
                {
                    MaDon = x.MaDon,
                    TenSanPham = string.Join(", ", x.DanhSachTen),
                    SoLuong = x.TmpSoLuong,
                    TongTien = x.TongTien,
                    NgayLap = x.NgayLap
                })
                .OrderByDescending(x => x.NgayLap)
                .ToList();

            return list; // Trả về List<HoaDonDTO>
        }

        // 2. Hàm lấy tổng tiền
        public decimal GetTongTien(int maNhanVien, DateTime tuNgay, DateTime denNgay)
        {
            return db.HOA_DON
          .Where(x => x.NgayLap >= tuNgay && x.NgayLap <= denNgay)
          .Sum(x => (decimal?)x.TongTien) ?? 0;
        }

        // 3. Hàm lấy tổng số đơn
        public int GetTongSoDon(int maNhanVien, DateTime tuNgay, DateTime denNgay)
        {
            return db.HOA_DON
    .Count(x => x.NgayLap >= tuNgay && x.NgayLap <= denNgay);
        }

        // 4. Hàm lấy Top sản phẩm
        public List<dynamic> GetTopSanPham(int maNhanVien, DateTime tuNgay, DateTime denNgay)
        {
            var data = db.CHI_TIET_HOA_DON
                .Where(ct => ct.HOA_DON.NgayLap >= tuNgay
          && ct.HOA_DON.NgayLap <= denNgay)
                .GroupBy(ct => ct.SAN_PHAM.TenSanPham)
                .Select(g => new
                {
                    TenSP = g.Key,
                    SoLuong = g.Sum(x => (int?)x.SoLuong) ?? 0
                })
                .OrderByDescending(x => x.SoLuong)
                .Take(5)
                .ToList();

            return data.Cast<dynamic>().ToList();
        }

        // Bạn có thể giữ lại hàm cũ nếu muốn, hoặc xóa đi và dùng GetHoaDonChiTiet cho đồng bộ
        public List<HoaDonDTO> GetDanhSachHoaDon(int maNhanVien, DateTime tuNgay, DateTime denNgay)
        {
            return GetHoaDonChiTiet(maNhanVien, tuNgay, denNgay);
        }

        public List<LichSuCaDTO> GetLichSuCa(int maNV, DateTime tu, DateTime den)
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                var data = db.HOA_DON
                    .Where(x => x.NgayLap >= tu && x.NgayLap <= den)
                    .ToList()
                    .Select(x =>
                    {
                        string ca = LayCa(x.NgayLap.Value);

                        return new
                        {
                            Ngay = x.NgayLap.Value.Date,
                            Ca = ca,
                            GioBatDau = LayGioBatDau(ca),
                            GioKetThuc = LayGioKetThuc(ca),
                            TongTien = x.TongTien ?? 0
                        };
                    })
                    .GroupBy(x => new { x.Ngay, x.Ca, x.GioBatDau, x.GioKetThuc })
                    .Select(g => new LichSuCaDTO
                    {
                        Ngay = g.Key.Ngay,
                        TenCa = g.Key.Ca,
                        GioBatDau = g.Key.GioBatDau,
                        GioKetThuc = g.Key.GioKetThuc,
                        SoDon = g.Count(),
                        DoanhThu = g.Sum(x => x.TongTien)
                    })
                    .OrderByDescending(x => x.Ngay)
                    .ThenByDescending(x => x.GioBatDau)
                    .ToList();

                return data;
            }
        }
    }
}