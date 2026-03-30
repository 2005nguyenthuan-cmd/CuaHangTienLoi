using demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo.BLL.Service
{
    public class InventoryService
    {
        private readonly CUA_HANG_TIEN_LOI_Entities db;

        public InventoryService()
        {
            db = new CUA_HANG_TIEN_LOI_Entities();
        }

        // ===== DASHBOARD =====

        public int GetTongMatHang()
        {
            return db.SAN_PHAM.Count();
        }

        public int GetTonThap()
        {
            return db.SAN_PHAM.Count(x => (x.SoLuongTon ?? 0) <= 10);
        }

        public int GetSapHetHan()
        {
            // Tạm thời chưa có HSD
            return 0;
        }

        public decimal GetGiaTriKho()
        {
            return db.SAN_PHAM
                .Sum(x => (decimal?)(x.SoLuongTon * x.GiaBan)) ?? 0;
        }

        // ===== DANH SÁCH TỒN KHO =====

        public List<InventoryDTO> GetTonKho(string keyword = "", string trangThai = "")
        {
            var query = db.SAN_PHAM.AsQueryable();

            //  tìm kiếm
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.TenSanPham.Contains(keyword));
            }

            var data = query.Select(x => new InventoryDTO
            {
                MaSP = x.MaSanPham,
                TenSP = x.TenSanPham,
                SoLuong = x.SoLuongTon ?? 0,
                TonToiThieu = 10,
                TrangThai = (x.SoLuongTon <= 0) ? "Hết hàng"
                        : (x.SoLuongTon < 10) ? "Tồn thấp"
                        : "Tốt"
            }).ToList();

            //  lọc trạng thái
            if (!string.IsNullOrEmpty(trangThai))
            {
                data = data.Where(x => x.TrangThai == trangThai).ToList();
            }

            return data;
        }


        // ===== NHẬP HÀNG =====

        public void NhapHang(int maNCC, int maNV, List<NhapHangDTO> list)
        {
            var phieu = new PHIEU_NHAP
            {
                NgayNhap = DateTime.Now,
                MaNhanVien = maNV,
                MaNCC = maNCC
            };

            db.PHIEU_NHAP.Add(phieu);
            db.SaveChanges();

            foreach (var item in list)
            {
                db.CHI_TIET_NHAP.Add(new CHI_TIET_NHAP
                {
                    MaPhieuNhap = phieu.MaPhieuNhap,
                    MaSanPham = item.MaSanPham,
                    SoLuong = item.SoLuong,
                    GiaNhap = item.GiaNhap
                });

                var sp = db.SAN_PHAM.Find(item.MaSanPham);
                if (sp != null)
                {
                    sp.SoLuongTon = (sp.SoLuongTon ?? 0) + item.SoLuong;
                }
            }

            db.SaveChanges();
        }
        public List<ProductDTO> SearchProducts(string keyword)
{
    return db.SAN_PHAM
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

        // ===== KIỂM KÊ =====

        public List<KiemKeDTO> GetDataKiemKe()
        {
            return db.SAN_PHAM
                .Select(x => new KiemKeDTO
                {
                    MaSP = x.MaSanPham,
                    TenSP = x.TenSanPham,
                    SoLuongHeThong = x.SoLuongTon ?? 0,
                    SoLuongThucTe = x.SoLuongTon ?? 0,
                    ChenhLech = 0
                }).ToList();
        }

        // Tính chênh lệch
        public List<KiemKeDTO> KiemKe(List<KiemKeDTO> input)
        {
            foreach (var item in input)
            {
                var sp = db.SAN_PHAM.Find(item.MaSP);
                if (sp != null)
                {
                    item.SoLuongHeThong = sp.SoLuongTon ?? 0;
                    item.ChenhLech = item.SoLuongThucTe - item.SoLuongHeThong;
                }
            }
            return input;
        }

        // Lưu kiểm kê (cập nhật tồn kho)
        public void SaveKiemKe(List<KiemKeDTO> input)
        {
            foreach (var item in input)
            {
                var sp = db.SAN_PHAM.Find(item.MaSP);
                if (sp != null)
                {
                    sp.SoLuongTon = item.SoLuongThucTe;
                }
            }
            db.SaveChanges();
        }

        /// ===== CẢNH BÁO TỒN KHO =====

        public List<CanhBaoDTO> GetCanhBao()
        {
            DateTime now = DateTime.Now;
            DateTime canhBaoHSD = now.AddDays(7); // cảnh báo trước 7 ngày

            // lấy từ nhập hàng vì có HSD
            var data = db.CHI_TIET_NHAP
                .Select(x => new
                {
                    x.MaSanPham,
                    TenSP = x.SAN_PHAM.TenSanPham,
                    SoLuong = x.SAN_PHAM.SoLuongTon ?? 0,
                    x.HanSuDung
                })
                .ToList(); //  chuyển về memory trước

            var result = new List<CanhBaoDTO>();

            foreach (var item in data)
            {
                //  tồn thấp
                if (item.SoLuong <= 10)
                {
                    result.Add(new CanhBaoDTO
                    {
                        MaSP = item.MaSanPham,
                        TenSP = item.TenSP,
                        SoLuong = item.SoLuong,
                        HanSuDung = item.HanSuDung,
                        LoaiCanhBao = "Tồn thấp"
                    });
                }

                //  gần hết hạn
                if (item.HanSuDung != null && item.HanSuDung <= canhBaoHSD)
                {
                    result.Add(new CanhBaoDTO
                    {
                        MaSP = item.MaSanPham,
                        TenSP = item.TenSP,
                        SoLuong = item.SoLuong,
                        HanSuDung = item.HanSuDung,
                        LoaiCanhBao = "Sắp hết hạn"
                    });
                }
            }

            return result;
        }
    }

    // ===== DTO =====

    public class InventoryDTO
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public int TonToiThieu { get; set; }
        public string TrangThai { get; set; }
    }

    public class NhapHangDTO
    {
        public int MaSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
    }

    public class KiemKeDTO
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }

        public int SoLuongHeThong { get; set; } // tồn trong DB
        public int SoLuongThucTe { get; set; }  // user nhập

        public int ChenhLech { get; set; }
    }
    public class CanhBaoDTO
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public DateTime? HanSuDung { get; set; }
        public string LoaiCanhBao { get; set; }
    }
}