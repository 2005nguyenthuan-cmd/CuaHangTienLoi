using demo.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.BLL.Service
{
    public class ImportService
    {
        private readonly CUA_HANG_TIEN_LOI_Entities db;

        public ImportService()
        {
            db = new CUA_HANG_TIEN_LOI_Entities();
        }

        // 1. Lấy nhà cung cấp
        public List<NHA_CUNG_CAP> GetSuppliers()
        {
            return db.NHA_CUNG_CAP.AsNoTracking().ToList();
        }

        // 2. Lấy sản phẩm
        public List<ProductDTO> GetProducts()
        {
            return db.SAN_PHAM
                .AsNoTracking()
                .Select(x => new ProductDTO
                {
                    MaSanPham = x.MaSanPham,
                    TenSanPham = x.TenSanPham,
                    GiaBan = x.GiaBan,
                    TenDanhMuc = x.DANH_MUC.TenDanhMuc   // lấy tên thay vì object
                })
                .ToList();
        }

        // 3. Tạo phiếu nhập
        public int CreatePhieuNhap(int maNCC, List<ChiTietNhapDTO> list)
        {
            return CreatePhieuNhap(maNCC, DateTime.Now, list);
        }

        public int CreatePhieuNhap(int maNCC, DateTime ngayNhap, List<ChiTietNhapDTO> list)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                var phieu = new PHIEU_NHAP
                {
                    MaNCC = maNCC,
                    MaNhanVien = UserSession.MaNhanVien > 0 ? (int?)UserSession.MaNhanVien : null,
                    NgayNhap = ngayNhap
                };

                db.PHIEU_NHAP.Add(phieu);
                db.SaveChanges();

                int maPhieu = phieu.MaPhieuNhap;

                foreach (var item in list)
                {
                    var ct = new CHI_TIET_NHAP
                    {
                        MaPhieuNhap = maPhieu,
                        MaSanPham = item.MaSP,
                        SoLuong = item.SoLuong,
                        GiaNhap = item.GiaNhap,
                        HanSuDung = item.HanSuDung
                    };

                    db.CHI_TIET_NHAP.Add(ct);

                    //  UPDATE TỒN KHO
                    var sp = db.SAN_PHAM.Find(item.MaSP);
                    if (sp != null)
                    {
                        sp.SoLuongTon = (sp.SoLuongTon ?? 0) + item.SoLuong;
                    }
                }

                db.SaveChanges();
                transaction.Commit();

                return maPhieu;
            }
        }

        // 4. Lấy tên sản phẩm
        public List<ProductDTO> SearchProducts(string keyword)
        {
            return db.SAN_PHAM
                .AsNoTracking()
                .Where(x => x.TenSanPham.Contains(keyword)
                         || x.MaSanPham.ToString().Contains(keyword))
                .Select(x => new ProductDTO
                {
                    MaSanPham = x.MaSanPham,
                    TenSanPham = x.TenSanPham,
                    GiaBan = x.GiaBan,
                    TenDanhMuc = x.DANH_MUC.TenDanhMuc
                })
                .ToList();
        }

        public List<ImportReceiptDTO> GetImportReceipts()
        {
            return db.PHIEU_NHAP
                .AsNoTracking()
                .Where(x => x.CHI_TIET_NHAP.Any())
                .Select(x => new ImportReceiptDTO
                {
                    MaPhieuNhap = x.MaPhieuNhap,
                    NgayNhap = x.NgayNhap,
                    NhaCungCap = x.NHA_CUNG_CAP.TenNCC,
                    NhanVien = x.NHAN_VIEN.TenNhanVien,
                    SoLuongMatHang = x.CHI_TIET_NHAP.Count(),
                    TongSoLuong = x.CHI_TIET_NHAP.Sum(ct => (int?)ct.SoLuong) ?? 0,
                    TongTien = x.CHI_TIET_NHAP.Sum(ct => (decimal?)ct.SoLuong * ct.GiaNhap) ?? 0
                })
                .OrderByDescending(x => x.NgayNhap)
                .ThenByDescending(x => x.MaPhieuNhap)
                .ToList();
        }

        public List<ImportDetailDTO> GetImportDetails(int maPhieuNhap)
        {
            return db.CHI_TIET_NHAP
                .AsNoTracking()
                .Where(x => x.MaPhieuNhap == maPhieuNhap)
                .Select(x => new ImportDetailDTO
                {
                    MaSanPham = x.MaSanPham,
                    TenSanPham = x.SAN_PHAM.TenSanPham,
                    SoLuong = x.SoLuong ?? 0,
                    GiaNhap = x.GiaNhap ?? 0,
                    HanSuDung = x.HanSuDung,
                    ThanhTien = ((decimal?)(x.SoLuong ?? 0) * x.GiaNhap) ?? 0
                })
                .OrderBy(x => x.TenSanPham)
                .ToList();
        }
    }

    public class ChiTietNhapDTO
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public DateTime HanSuDung { get; set; }
        public decimal ThanhTien => SoLuong * GiaNhap;
    }
    public class ProductDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal? GiaBan { get; set; }
        public string TenDanhMuc { get; set; }
    }

    public class ImportReceiptDTO
    {
        public int MaPhieuNhap { get; set; }
        public DateTime? NgayNhap { get; set; }
        public string NhaCungCap { get; set; }
        public string NhanVien { get; set; }
        public int SoLuongMatHang { get; set; }
        public int TongSoLuong { get; set; }
        public decimal TongTien { get; set; }
    }

    public class ImportDetailDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public DateTime? HanSuDung { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
