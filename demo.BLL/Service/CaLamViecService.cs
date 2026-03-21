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


    }
}
