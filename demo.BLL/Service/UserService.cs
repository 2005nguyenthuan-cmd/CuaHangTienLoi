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
            string normalizedUsername = username == null ? null : username.Trim();
            string normalizedPassword = password == null ? null : password.Trim();

            if (string.IsNullOrEmpty(normalizedUsername) || string.IsNullOrEmpty(normalizedPassword))
            {
                return null;
            }

            var user = db.NHAN_VIEN.FirstOrDefault(x => x.TenDangNhap == normalizedUsername);

            if (user == null || !PasswordHasher.VerifyPassword(normalizedPassword, user.MatKhau))
            {
                return null;
            }

            if (!PasswordHasher.IsHashedPassword(user.MatKhau))
            {
                user.MatKhau = PasswordHasher.HashPassword(normalizedPassword);
                db.SaveChanges();
            }

            return user;
        }
    }
}
