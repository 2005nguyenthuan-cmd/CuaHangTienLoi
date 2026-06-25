using demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo.BLL.Service
{
    public class EmployeeListItem
    {
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string VaiTro { get; set; }
        public string TrangThai { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        public string MaNhanVienHienThi
        {
            get { return string.Format("NV{0:D3}", MaNhanVien); }
        }
    }

    public class EmployeeLookupItem
    {
        public int? Id { get; set; }
        public string Ten { get; set; }
    }

    public class EmployeeEditItem
    {
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string TenDangNhap { get; set; }
        public int? MaVaiTro { get; set; }
        public int? MaCa { get; set; }
    }

    public class EmployeeSaveModel
    {
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? MaVaiTro { get; set; }
        public int? MaCa { get; set; }
        public bool CapNhatCaLam { get; set; }
    }

    public class EmployeeDashboardSummary
    {
        public int TongNhanVien { get; set; }
        public int SoTaiKhoanDangHoatDong { get; set; }
        public int SoNhanVienCoEmail { get; set; }
    }

    public class EmployeeService
    {
        public List<EmployeeListItem> GetAllForList(string keyword = null)
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                IQueryable<NHAN_VIEN> query = db.NHAN_VIEN;

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string normalizedKeyword = keyword.Trim();

                    query = query.Where(x =>
                        (x.TenNhanVien != null && x.TenNhanVien.Contains(normalizedKeyword)) ||
                        (x.SoDienThoai != null && x.SoDienThoai.Contains(normalizedKeyword)) ||
                        (x.TenDangNhap != null && x.TenDangNhap.Contains(normalizedKeyword)) ||
                        (x.Email != null && x.Email.Contains(normalizedKeyword)));
                }

                return query
                    .OrderBy(x => x.TenNhanVien)
                    .Select(x => new EmployeeListItem
                    {
                        MaNhanVien = x.MaNhanVien,
                        TenNhanVien = x.TenNhanVien,
                        VaiTro = x.VAI_TRO != null ? x.VAI_TRO.TenVaiTro : "Chua phan quyen",
                        TrangThai = string.IsNullOrEmpty(x.TenDangNhap) ? "Chua co tai khoan" : "Dang hoat dong",
                        SoDienThoai = x.SoDienThoai,
                        Email = x.Email
                    })
                    .ToList();
            }
        }

        public EmployeeDashboardSummary GetDashboardSummary()
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                return new EmployeeDashboardSummary
                {
                    TongNhanVien = db.NHAN_VIEN.Count(),
                    SoTaiKhoanDangHoatDong = db.NHAN_VIEN.Count(x => !string.IsNullOrEmpty(x.TenDangNhap)),
                    SoNhanVienCoEmail = db.NHAN_VIEN.Count(x => !string.IsNullOrEmpty(x.Email))
                };
            }
        }

        public EmployeeEditItem GetById(int maNhanVien)
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                var employee = db.NHAN_VIEN
                    .Where(x => x.MaNhanVien == maNhanVien)
                    .Select(x => new EmployeeEditItem
                    {
                        MaNhanVien = x.MaNhanVien,
                        TenNhanVien = x.TenNhanVien,
                        SoDienThoai = x.SoDienThoai,
                        Email = x.Email,
                        TenDangNhap = x.TenDangNhap,
                        MaVaiTro = x.MaVaiTro,
                        MaCa = x.MaCa
                    })
                    .FirstOrDefault();

                if (employee == null)
                {
                    throw new InvalidOperationException("Nhan vien khong ton tai.");
                }

                return employee;
            }
        }

        public List<EmployeeLookupItem> GetRoleOptions()
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                var result = db.VAI_TRO
                    .OrderBy(x => x.TenVaiTro)
                    .Select(x => new EmployeeLookupItem
                    {
                        Id = x.MaVaiTro,
                        Ten = x.TenVaiTro
                    })
                    .ToList();

                result.Insert(0, new EmployeeLookupItem
                {
                    Id = null,
                    Ten = "Chua phan quyen"
                });

                return result;
            }
        }

        public List<EmployeeLookupItem> GetShiftOptions()
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                var result = db.CA_LAM_VIEC
                    .OrderBy(x => x.TenCa)
                    .Select(x => new EmployeeLookupItem
                    {
                        Id = x.MaCa,
                        Ten = x.TenCa
                    })
                    .ToList();

                result.Insert(0, new EmployeeLookupItem
                {
                    Id = null,
                    Ten = "Chua xep ca"
                });

                return result;
            }
        }

        public int Add(EmployeeSaveModel model)
        {
            Validate(model, true);

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                string normalizedUsername = NormalizeRequired(model.TenDangNhap, "Ten dang nhap");

                if (db.NHAN_VIEN.Any(x => x.TenDangNhap == normalizedUsername))
                {
                    throw new InvalidOperationException("Ten dang nhap da ton tai.");
                }

                NHAN_VIEN employee = new NHAN_VIEN
                {
                    TenNhanVien = NormalizeRequired(model.TenNhanVien, "Ten nhan vien"),
                    SoDienThoai = NormalizeRequired(model.SoDienThoai, "So dien thoai"),
                    Email = NormalizeOptional(model.Email),
                    TenDangNhap = normalizedUsername,
                    MatKhau = NormalizeRequired(model.MatKhau, "Mat khau"),
                    MaVaiTro = model.MaVaiTro,
                    MaCa = model.CapNhatCaLam ? model.MaCa : null
                };

                db.NHAN_VIEN.Add(employee);
                db.SaveChanges();

                return employee.MaNhanVien;
            }
        }

        public void Update(EmployeeSaveModel model, bool updatePassword)
        {
            Validate(model, false);

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                NHAN_VIEN employee = db.NHAN_VIEN.FirstOrDefault(x => x.MaNhanVien == model.MaNhanVien);

                if (employee == null)
                {
                    throw new InvalidOperationException("Nhan vien khong ton tai.");
                }

                string normalizedUsername = NormalizeRequired(model.TenDangNhap, "Ten dang nhap");

                bool duplicatedUsername = db.NHAN_VIEN.Any(x =>
                    x.MaNhanVien != model.MaNhanVien &&
                    x.TenDangNhap == normalizedUsername);

                if (duplicatedUsername)
                {
                    throw new InvalidOperationException("Ten dang nhap da ton tai.");
                }

                employee.TenNhanVien = NormalizeRequired(model.TenNhanVien, "Ten nhan vien");
                employee.SoDienThoai = NormalizeRequired(model.SoDienThoai, "So dien thoai");
                employee.Email = NormalizeOptional(model.Email);
                employee.TenDangNhap = normalizedUsername;
                employee.MaVaiTro = model.MaVaiTro;

                if (model.CapNhatCaLam)
                {
                    employee.MaCa = model.MaCa;
                }

                if (updatePassword)
                {
                    employee.MatKhau = NormalizeRequired(model.MatKhau, "Mat khau");
                }

                db.SaveChanges();
            }
        }

        public void Delete(int maNhanVien)
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                NHAN_VIEN employee = db.NHAN_VIEN.FirstOrDefault(x => x.MaNhanVien == maNhanVien);

                if (employee == null)
                {
                    throw new InvalidOperationException("Nhan vien khong ton tai.");
                }

                if (UserSession.MaNhanVien == maNhanVien)
                {
                    throw new InvalidOperationException("Khong the xoa tai khoan dang dang nhap.");
                }

                List<HOA_DON> invoices = db.HOA_DON
                    .Where(x => x.MaNhanVien == maNhanVien)
                    .ToList();

                foreach (HOA_DON invoice in invoices)
                {
                    invoice.MaNhanVien = null;
                }

                List<PHIEU_NHAP> importReceipts = db.PHIEU_NHAP
                    .Where(x => x.MaNhanVien == maNhanVien)
                    .ToList();

                foreach (PHIEU_NHAP receipt in importReceipts)
                {
                    receipt.MaNhanVien = null;
                }

                List<LICH_LAM_VIEC> updatedSchedules = db.LICH_LAM_VIEC
                    .Where(x => x.NguoiCapNhat == maNhanVien)
                    .ToList();

                foreach (LICH_LAM_VIEC schedule in updatedSchedules)
                {
                    schedule.NguoiCapNhat = null;
                }

                List<int> scheduleIds = db.LICH_LAM_VIEC
                    .Where(x => x.MaNhanVien == maNhanVien)
                    .Select(x => x.MaLich)
                    .ToList();

                if (scheduleIds.Count > 0)
                {
                    List<LICH_LAM_CONG_VIEC> assignments = db.LICH_LAM_CONG_VIEC
                        .Where(x => scheduleIds.Contains(x.MaLich))
                        .ToList();

                    if (assignments.Count > 0)
                    {
                        db.LICH_LAM_CONG_VIEC.RemoveRange(assignments);
                    }

                    List<LICH_LAM_VIEC> schedules = db.LICH_LAM_VIEC
                        .Where(x => scheduleIds.Contains(x.MaLich))
                        .ToList();

                    if (schedules.Count > 0)
                    {
                        db.LICH_LAM_VIEC.RemoveRange(schedules);
                    }
                }

                db.NHAN_VIEN.Remove(employee);
                db.SaveChanges();
            }
        }

        private static void Validate(EmployeeSaveModel model, bool isCreate)
        {
            if (model == null)
            {
                throw new InvalidOperationException("Du lieu nhan vien khong hop le.");
            }

            NormalizeRequired(model.TenNhanVien, "Ten nhan vien");
            NormalizeRequired(model.SoDienThoai, "So dien thoai");
            NormalizeRequired(model.TenDangNhap, "Ten dang nhap");

            if (isCreate)
            {
                NormalizeRequired(model.MatKhau, "Mat khau");
            }
            else if (!string.IsNullOrWhiteSpace(model.MatKhau))
            {
                NormalizeRequired(model.MatKhau, "Mat khau");
            }
        }

        private static string NormalizeRequired(string value, string fieldName)
        {
            string normalized = NormalizeOptional(value);

            if (string.IsNullOrEmpty(normalized))
            {
                throw new InvalidOperationException(fieldName + " khong duoc de trong.");
            }

            return normalized;
        }

        private static string NormalizeOptional(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return value.Trim();
        }
    }
}
