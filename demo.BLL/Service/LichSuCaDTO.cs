using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{
    public class LichSuCaDTO
    {
        public DateTime Ngay { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public int SoDon { get; set; }
        public decimal DoanhThu { get; set; }
    }
}
