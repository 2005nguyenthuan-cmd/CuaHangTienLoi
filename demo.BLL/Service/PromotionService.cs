using demo.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

namespace demo.BLL.Service
{
    public class PromotionListItem
    {
        public int MaKhuyenMai { get; set; }
        public string MaKhuyenMaiCode { get; set; }
        public int PhanTramGiam { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string TrangThai { get; set; }

        public string ThoiGianApDung
        {
            get
            {
                string ngayBatDau = NgayBatDau.HasValue
                    ? NgayBatDau.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                    : "--";
                string ngayKetThuc = NgayKetThuc.HasValue
                    ? NgayKetThuc.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                    : "--";

                return ngayBatDau + " - " + ngayKetThuc;
            }
        }
    }

    public class PromotionEditItem
    {
        public int MaKhuyenMai { get; set; }
        public string MaKhuyenMaiCode { get; set; }
        public int PhanTramGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }

    public class PromotionSaveModel
    {
        public int MaKhuyenMai { get; set; }
        public string MaKhuyenMaiCode { get; set; }
        public int PhanTramGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }

    public class PosPromotionLookupItem
    {
        public int MaKhuyenMai { get; set; }
        public string MaKhuyenMaiCode { get; set; }
        public int PhanTramGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }

    public class PosPromotionApplyResult : PosPromotionLookupItem
    {
        public decimal TongTienTamTinh { get; set; }
        public decimal SoTienGiam { get; set; }
        public decimal TongTienSauGiam { get; set; }
    }

    public class PromotionService
    {
        public const string TrangThaiSapHieuLuc = "Sap hieu luc";
        public const string TrangThaiDangHieuLuc = "Dang hieu luc";
        public const string TrangThaiHetHieuLuc = "Het hieu luc";

        public List<PromotionListItem> GetAll(string keyword = null)
        {
            DateTime today = DateTime.Today;

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                List<KHUYEN_MAI> promotions = db.KHUYEN_MAI
                    .AsNoTracking()
                    .OrderByDescending(x => x.NgayBatDau)
                    .ThenBy(x => x.TenKhuyenMai)
                    .ToList();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string normalizedKeyword = keyword.Trim();

                    promotions = promotions
                        .Where(x =>
                            x.MaKhuyenMai.ToString(CultureInfo.InvariantCulture).Contains(normalizedKeyword) ||
                            NormalizePromotionCodeForDisplay(x.TenKhuyenMai)
                                .IndexOf(normalizedKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();
                }

                return promotions
                    .Select(x => new PromotionListItem
                    {
                        MaKhuyenMai = x.MaKhuyenMai,
                        MaKhuyenMaiCode = NormalizePromotionCodeForDisplay(x.TenKhuyenMai),
                        PhanTramGiam = x.PhanTramGiam ?? 0,
                        NgayBatDau = x.NgayBatDau,
                        NgayKetThuc = x.NgayKetThuc,
                        TrangThai = BuildStatus(today, x.NgayBatDau, x.NgayKetThuc)
                    })
                    .ToList();
            }
        }

        public PromotionEditItem GetById(int maKhuyenMai)
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                KHUYEN_MAI promotion = db.KHUYEN_MAI
                    .AsNoTracking()
                    .FirstOrDefault(x => x.MaKhuyenMai == maKhuyenMai);

                if (promotion == null)
                {
                    throw new InvalidOperationException("Khuyen mai khong ton tai.");
                }

                return new PromotionEditItem
                {
                    MaKhuyenMai = promotion.MaKhuyenMai,
                    MaKhuyenMaiCode = NormalizePromotionCodeForDisplay(promotion.TenKhuyenMai),
                    PhanTramGiam = promotion.PhanTramGiam ?? 0,
                    NgayBatDau = promotion.NgayBatDau ?? DateTime.Today,
                    NgayKetThuc = promotion.NgayKetThuc ?? DateTime.Today
                };
            }
        }

        public int Add(PromotionSaveModel model)
        {
            Validate(model);

            string promotionCode = NormalizePromotionCode(model.MaKhuyenMaiCode);

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                EnsurePromotionCodeUnique(db, promotionCode, 0);

                KHUYEN_MAI promotion = new KHUYEN_MAI
                {
                    // Schema hien tai chua co cot ma rieng, nen luu ma POS trong TenKhuyenMai.
                    TenKhuyenMai = promotionCode,
                    PhanTramGiam = model.PhanTramGiam,
                    NgayBatDau = model.NgayBatDau.Date,
                    NgayKetThuc = model.NgayKetThuc.Date
                };

                db.KHUYEN_MAI.Add(promotion);
                db.SaveChanges();

                return promotion.MaKhuyenMai;
            }
        }

        public void Update(PromotionSaveModel model)
        {
            Validate(model);

            string promotionCode = NormalizePromotionCode(model.MaKhuyenMaiCode);

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                KHUYEN_MAI promotion = db.KHUYEN_MAI
                    .Include(x => x.SAN_PHAM)
                    .FirstOrDefault(x => x.MaKhuyenMai == model.MaKhuyenMai);

                if (promotion == null)
                {
                    throw new InvalidOperationException("Khuyen mai khong ton tai.");
                }

                EnsurePromotionCodeUnique(db, promotionCode, model.MaKhuyenMai);

                promotion.TenKhuyenMai = promotionCode;
                promotion.PhanTramGiam = model.PhanTramGiam;
                promotion.NgayBatDau = model.NgayBatDau.Date;
                promotion.NgayKetThuc = model.NgayKetThuc.Date;

                if (promotion.SAN_PHAM.Count > 0)
                {
                    promotion.SAN_PHAM.Clear();
                }

                db.SaveChanges();
            }
        }

        public void Delete(int maKhuyenMai)
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                KHUYEN_MAI promotion = db.KHUYEN_MAI
                    .Include(x => x.SAN_PHAM)
                    .FirstOrDefault(x => x.MaKhuyenMai == maKhuyenMai);

                if (promotion == null)
                {
                    throw new InvalidOperationException("Khuyen mai khong ton tai.");
                }

                if (promotion.SAN_PHAM.Count > 0)
                {
                    promotion.SAN_PHAM.Clear();
                }

                db.KHUYEN_MAI.Remove(promotion);
                db.SaveChanges();
            }
        }

        public List<PosPromotionLookupItem> GetAvailablePromotionsForPos(DateTime? ngayApDung = null)
        {
            DateTime applyDate = (ngayApDung ?? DateTime.Today).Date;

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                return db.KHUYEN_MAI
                    .AsNoTracking()
                    .ToList()
                    .Where(x => IsActiveOnDate(x, applyDate))
                    .OrderByDescending(x => x.PhanTramGiam ?? 0)
                    .ThenBy(x => x.TenKhuyenMai)
                    .Select(MapToPosLookupItem)
                    .ToList();
            }
        }

        // POS co the goi method nay de kiem tra ma va tinh ngay so tien giam cho hoa don hien tai.
        public PosPromotionApplyResult ApplyPromotionCodeForPos(string promotionCode, decimal tongTienTamTinh, DateTime? ngayApDung = null)
        {
            if (tongTienTamTinh < 0)
            {
                throw new InvalidOperationException("Tong tien tam tinh khong hop le.");
            }

            DateTime applyDate = (ngayApDung ?? DateTime.Today).Date;
            string normalizedCode = NormalizePromotionCode(promotionCode);

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                KHUYEN_MAI promotion = db.KHUYEN_MAI
                    .AsNoTracking()
                    .ToList()
                    .FirstOrDefault(x => NormalizePromotionCodeForDisplay(x.TenKhuyenMai) == normalizedCode);

                if (promotion == null)
                {
                    throw new InvalidOperationException("Ma khuyen mai khong ton tai.");
                }

                string status = BuildStatus(applyDate, promotion.NgayBatDau, promotion.NgayKetThuc);
                if (status == TrangThaiSapHieuLuc)
                {
                    throw new InvalidOperationException("Ma khuyen mai chua den ngay ap dung.");
                }

                if (status == TrangThaiHetHieuLuc)
                {
                    throw new InvalidOperationException("Ma khuyen mai da het hieu luc.");
                }

                int phanTramGiam = promotion.PhanTramGiam ?? 0;
                decimal soTienGiam = Math.Round(
                    tongTienTamTinh * phanTramGiam / 100m,
                    2,
                    MidpointRounding.AwayFromZero);

                return new PosPromotionApplyResult
                {
                    MaKhuyenMai = promotion.MaKhuyenMai,
                    MaKhuyenMaiCode = NormalizePromotionCodeForDisplay(promotion.TenKhuyenMai),
                    PhanTramGiam = phanTramGiam,
                    NgayBatDau = promotion.NgayBatDau ?? applyDate,
                    NgayKetThuc = promotion.NgayKetThuc ?? applyDate,
                    TongTienTamTinh = tongTienTamTinh,
                    SoTienGiam = soTienGiam,
                    TongTienSauGiam = Math.Max(0, tongTienTamTinh - soTienGiam)
                };
            }
        }

        private static PosPromotionLookupItem MapToPosLookupItem(KHUYEN_MAI promotion)
        {
            DateTime today = DateTime.Today;

            return new PosPromotionLookupItem
            {
                MaKhuyenMai = promotion.MaKhuyenMai,
                MaKhuyenMaiCode = NormalizePromotionCodeForDisplay(promotion.TenKhuyenMai),
                PhanTramGiam = promotion.PhanTramGiam ?? 0,
                NgayBatDau = promotion.NgayBatDau ?? today,
                NgayKetThuc = promotion.NgayKetThuc ?? today
            };
        }

        private static bool IsActiveOnDate(KHUYEN_MAI promotion, DateTime applyDate)
        {
            return BuildStatus(applyDate, promotion.NgayBatDau, promotion.NgayKetThuc) == TrangThaiDangHieuLuc;
        }

        private static string BuildStatus(DateTime today, DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            DateTime startDate = ngayBatDau.HasValue ? ngayBatDau.Value.Date : today;
            DateTime endDate = ngayKetThuc.HasValue ? ngayKetThuc.Value.Date : today;

            if (today < startDate)
            {
                return TrangThaiSapHieuLuc;
            }

            if (today > endDate)
            {
                return TrangThaiHetHieuLuc;
            }

            return TrangThaiDangHieuLuc;
        }

        private static void Validate(PromotionSaveModel model)
        {
            if (model == null)
            {
                throw new InvalidOperationException("Du lieu khuyen mai khong hop le.");
            }

            NormalizePromotionCode(model.MaKhuyenMaiCode);

            if (model.PhanTramGiam <= 0 || model.PhanTramGiam > 100)
            {
                throw new InvalidOperationException("Phan tram giam phai nam trong khoang 1-100.");
            }

            if (model.NgayKetThuc.Date < model.NgayBatDau.Date)
            {
                throw new InvalidOperationException("Ngay ket thuc phai lon hon hoac bang ngay bat dau.");
            }
        }

        private static void EnsurePromotionCodeUnique(CUA_HANG_TIEN_LOI_Entities db, string promotionCode, int currentPromotionId)
        {
            bool isDuplicated = db.KHUYEN_MAI
                .AsNoTracking()
                .ToList()
                .Any(x =>
                    x.MaKhuyenMai != currentPromotionId &&
                    NormalizePromotionCodeForDisplay(x.TenKhuyenMai) == promotionCode);

            if (isDuplicated)
            {
                throw new InvalidOperationException("Ma khuyen mai da ton tai. Vui long chon ma khac.");
            }
        }

        private static string NormalizePromotionCode(string value)
        {
            string normalized = NormalizePromotionCodeForDisplay(value);

            if (string.IsNullOrEmpty(normalized))
            {
                throw new InvalidOperationException("Ma khuyen mai khong duoc de trong.");
            }

            return normalized;
        }

        private static string NormalizePromotionCodeForDisplay(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : value.Trim().ToUpperInvariant();
        }
    }
}
