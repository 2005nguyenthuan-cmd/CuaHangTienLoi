using demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{
    public class SupplierService
    {
        private readonly CUA_HANG_TIEN_LOI_Entities db;

        public SupplierService()
        {
            db = new CUA_HANG_TIEN_LOI_Entities();
        }

        public NHA_CUNG_CAP GetById(int id)
        {
            return db.NHA_CUNG_CAP.Find(id);
        }

        public List<NHA_CUNG_CAP> GetAll()
        {
            return db.NHA_CUNG_CAP.ToList();
        }
        public void Add(NHA_CUNG_CAP ncc)
        {
            db.NHA_CUNG_CAP.Add(ncc);
            db.SaveChanges();
        }

        public void Update(NHA_CUNG_CAP ncc)
        {
            var existing = db.NHA_CUNG_CAP.Find(ncc.MaNCC);

            if (existing != null)
            {
                existing.TenNCC = ncc.TenNCC;
                existing.SoDienThoai = ncc.SoDienThoai;
                existing.DiaChi = ncc.DiaChi;

                db.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var ncc = db.NHA_CUNG_CAP.Find(id);

            if (ncc != null)
            {
                db.NHA_CUNG_CAP.Remove(ncc);
                db.SaveChanges();
            }
        }

    }
}
