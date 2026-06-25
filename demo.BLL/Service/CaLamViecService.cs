using demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{

    public class CaLamViecService
    {
        CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();
        public List<CaLamViecDTO> GetAll()
        {
            var now = DateTime.Now.TimeOfDay;

            return db.CA_LAM_VIEC
                .ToList()
                .Select(x => new CaLamViecDTO
                {
                    MaCa = x.MaCa,
                    TenCa = x.TenCa,

                    GioBatDau = x.GioBatDau.HasValue
                        ? x.GioBatDau.Value.ToString(@"hh\:mm")
                        : "",

                    GioKetThuc = x.GioKetThuc.HasValue
                        ? x.GioKetThuc.Value.ToString(@"hh\:mm")
                        : "",

                    TrangThai =
                        (x.GioBatDau < x.GioKetThuc)
                        ? (x.GioBatDau <= now && now <= x.GioKetThuc
                            ? "Đang hoạt động"
                            : "Chưa tới / Đã kết thúc")

                        : (now >= x.GioBatDau || now <= x.GioKetThuc
                            ? "Đang hoạt động"
                            : "Chưa tới / Đã kết thúc")
                }).ToList();
        }
        public CA_LAM_VIEC GetCaDangHoatDong()
        {
            var now = DateTime.Now.TimeOfDay;

            return db.CA_LAM_VIEC
                .ToList()
                .FirstOrDefault(x =>
                    (x.GioBatDau < x.GioKetThuc)
                        ? (x.GioBatDau <= now && now <= x.GioKetThuc)
                        : (now >= x.GioBatDau || now <= x.GioKetThuc)
                );
        }
        Random rd = new Random();

        DateTime RandomGio(DateTime ngay, int seed)
        {
            Random r = new Random(seed);

            // 👉 chỉ random từ 6h → 22h (giống giờ hoạt động)
            return ngay.Date
                .AddHours(r.Next(6, 22))
                .AddMinutes(r.Next(0, 60));
        }
        public (int soDon, decimal doanhThu, int soSP) GetThongKeCa(CA_LAM_VIEC ca)
        {
            if (ca == null) return (0, 0, 0);

            var hoaDons = db.HOA_DON.ToList();

            int soDon = 0;
            decimal doanhThu = 0;
            int soSP = 0;

            foreach (var hd in hoaDons)
            {
                if (!hd.NgayLap.HasValue) continue;

                // 👉 tạo giờ giả từ MaHoaDon (ổn định, không random)
                int indexCa = hd.MaHoaDon % 3; // chia 3 ca

                TimeSpan gio;

                if (indexCa == 0)
                    gio = new TimeSpan(8, 0, 0);   // ca sáng
                else if (indexCa == 1)
                    gio = new TimeSpan(15, 0, 0);  // ca chiều
                else
                    gio = new TimeSpan(22, 0, 0);  // ca đêm

                bool trongCa = false;

                if (ca.GioBatDau < ca.GioKetThuc)
                {
                    // ca thường (VD: 6h → 14h)
                    if (gio >= ca.GioBatDau && gio <= ca.GioKetThuc)
                        trongCa = true;
                }
                else
                {
                    // ca đêm (VD: 22h → 6h)
                    if (gio >= ca.GioBatDau || gio <= ca.GioKetThuc)
                        trongCa = true;
                }

                if (trongCa)
                {
                    soDon++;
                    doanhThu += hd.TongTien ?? 0;

                    var ct = db.CHI_TIET_HOA_DON
                                .Where(x => x.MaHoaDon == hd.MaHoaDon)
                                .ToList();

                    foreach (var item in ct)
                    {
                        soSP += item.SoLuong ?? 0;
                    }
                }
            }

            return (soDon, doanhThu, soSP);
        }
        public List<HOA_DON> LayHoaDonTheoCa(CA_LAM_VIEC ca)
        {
            var ds = db.HOA_DON.ToList();
            var result = new List<HOA_DON>();

            foreach (var hd in ds)
            {
                int index = hd.MaHoaDon % 3;

                TimeSpan gio;
                if (index == 0) gio = new TimeSpan(8, 0, 0);
                else if (index == 1) gio = new TimeSpan(15, 0, 0);
                else gio = new TimeSpan(22, 0, 0);

                bool thuocCa;

                if (ca.GioBatDau < ca.GioKetThuc)
                    thuocCa = gio >= ca.GioBatDau && gio <= ca.GioKetThuc;
                else
                    thuocCa = gio >= ca.GioBatDau || gio <= ca.GioKetThuc;

                if (thuocCa)
                    result.Add(hd);
            }

            return result;
        }
        public int LaySoSP(int maHoaDon)
        {
            return db.CHI_TIET_HOA_DON
                .Where(x => x.MaHoaDon == maHoaDon)
                .Sum(x => (int?)x.SoLuong) ?? 0;
        }

    }
}
