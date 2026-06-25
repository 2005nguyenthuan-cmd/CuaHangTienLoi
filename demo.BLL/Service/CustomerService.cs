using demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{
    
    public class CustomerService
    {
        private readonly CUA_HANG_TIEN_LOI_Entities db;
        public CustomerService()
        {
            db = new CUA_HANG_TIEN_LOI_Entities();
        }
        public List<KHACH_HANG> GetALL()
        {
            return db.KHACH_HANG.ToList();
        }
        
    }

}
