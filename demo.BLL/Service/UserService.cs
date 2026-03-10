using demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{

    public class UserService
    {
        private CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();

        public NHAN_VIEN Login(string username, string password)
        {
            var user = db.NHAN_VIEN
                .FirstOrDefault(x => x.TenDangNhap == username && x.MatKhau == password);

            return user;
        }
    }
}
