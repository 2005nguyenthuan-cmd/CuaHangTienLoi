using demo.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace demo.BLL.Service
{
    public class ProductService
    {
        public void Delete(int maSanPham)
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                SAN_PHAM product = db.SAN_PHAM
                    .Include(x => x.KHUYEN_MAI)
                    .FirstOrDefault(x => x.MaSanPham == maSanPham);

                if (product == null)
                {
                    throw new InvalidOperationException("San pham khong ton tai.");
                }

                List<int> affectedInvoiceIds = db.CHI_TIET_HOA_DON
                    .Where(x => x.MaSanPham == maSanPham)
                    .Select(x => x.MaHoaDon)
                    .Distinct()
                    .ToList();

                List<CHI_TIET_HOA_DON> invoiceDetails = db.CHI_TIET_HOA_DON
                    .Where(x => x.MaSanPham == maSanPham)
                    .ToList();

                if (invoiceDetails.Count > 0)
                {
                    db.CHI_TIET_HOA_DON.RemoveRange(invoiceDetails);
                }

                List<CHI_TIET_NHAP> importDetails = db.CHI_TIET_NHAP
                    .Where(x => x.MaSanPham == maSanPham)
                    .ToList();

                if (importDetails.Count > 0)
                {
                    db.CHI_TIET_NHAP.RemoveRange(importDetails);
                }

                if (product.KHUYEN_MAI.Any())
                {
                    product.KHUYEN_MAI.Clear();
                }

                db.SAN_PHAM.Remove(product);
                db.SaveChanges();

                if (affectedInvoiceIds.Count == 0)
                {
                    return;
                }

                List<HOA_DON> invoices = db.HOA_DON
                    .Where(x => affectedInvoiceIds.Contains(x.MaHoaDon))
                    .ToList();

                foreach (HOA_DON invoice in invoices)
                {
                    invoice.TongTien = db.CHI_TIET_HOA_DON
                        .Where(x => x.MaHoaDon == invoice.MaHoaDon)
                        .Sum(x => (decimal?)x.ThanhTien) ?? 0m;
                }

                db.SaveChanges();
            }
        }
    }
}
