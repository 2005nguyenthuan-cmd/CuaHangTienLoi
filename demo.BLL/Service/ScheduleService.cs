using demo.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;

namespace demo.BLL.Service
{
    public class ScheduleBoardStats
    {
        public int TongNhanVien { get; set; }
        public int DangTrongCa { get; set; }
        public int CaLamHomNay { get; set; }
        public int ViPham { get; set; }
    }

    public class ScheduleDayColumn
    {
        public string Key { get; set; }
        public DateTime Date { get; set; }
        public string HeaderText { get; set; }
    }

    public class ScheduleCellItem
    {
        public int? MaLich { get; set; }
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime NgayLam { get; set; }
        public int? MaCa { get; set; }
        public string ShiftLabel { get; set; }
        public string TimeRangeText { get; set; }
        public string TaskSummary { get; set; }
        public string DisplayText { get; set; }
        public string LoaiLich { get; set; }
        public bool IsRestDay { get; set; }
        public bool IsOvertime { get; set; }
        public bool HasTasks { get; set; }
    }

    public class ScheduleEmployeeRow
    {
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public List<ScheduleCellItem> Cells { get; set; }
    }

    public class WeeklyScheduleBoard
    {
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public List<ScheduleDayColumn> Columns { get; set; }
        public List<ScheduleEmployeeRow> Rows { get; set; }
        public ScheduleBoardStats Stats { get; set; }
    }

    public class ShiftOption
    {
        public int? MaCa { get; set; }
        public string TenCa { get; set; }
        public TimeSpan? GioBatDau { get; set; }
        public TimeSpan? GioKetThuc { get; set; }

        public string HienThi
        {
            get
            {
                if (!GioBatDau.HasValue || !GioKetThuc.HasValue)
                {
                    return TenCa;
                }

                return string.Format(
                    CultureInfo.InvariantCulture,
                    "{0} ({1:hh\\:mm} - {2:hh\\:mm})",
                    TenCa,
                    GioBatDau.Value,
                    GioKetThuc.Value);
            }
        }
    }

    public class WorkTaskOption
    {
        public int MaCongViec { get; set; }
        public string TenCongViec { get; set; }
        public string MoTa { get; set; }
        public bool DangHoatDong { get; set; }
    }

    public class ScheduleEditItem
    {
        public int? MaLich { get; set; }
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime NgayLam { get; set; }
        public string CheDoLich { get; set; }
        public int? MaCa { get; set; }
        public TimeSpan? GioBatDau { get; set; }
        public TimeSpan? GioKetThuc { get; set; }
        public string LyDoDieuChinh { get; set; }
        public string GhiChu { get; set; }
    }

    public class ScheduleEditSaveModel
    {
        public int? MaLich { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayLam { get; set; }
        public string CheDoLich { get; set; }
        public int? MaCa { get; set; }
        public TimeSpan? GioBatDau { get; set; }
        public TimeSpan? GioKetThuc { get; set; }
        public string LyDoDieuChinh { get; set; }
        public string GhiChu { get; set; }
    }

    public class TaskAssignmentEditItem
    {
        public int? MaLich { get; set; }
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime NgayLam { get; set; }
        public string CaHienTai { get; set; }
        public TimeSpan? GioBatDau { get; set; }
        public TimeSpan? GioKetThuc { get; set; }
        public List<int> SelectedTaskIds { get; set; }
        public List<WorkTaskOption> AvailableTasks { get; set; }
    }

    public class TaskAssignmentSaveModel
    {
        public int? MaLich { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayLam { get; set; }
        public List<int> TaskIds { get; set; }
    }

    public class ScheduleService
    {
        public const string CheDoTheoCa = "TheoCa";
        public const string CheDoNghi = "Nghi";
        public const string CheDoTangCa = "TangCa";

        private sealed class DefaultTaskSeed
        {
            public string DisplayName { get; set; }
            public string Description { get; set; }
            public string[] Aliases { get; set; }
        }

        private static readonly DefaultTaskSeed[] DefaultTasks =
        {
            new DefaultTaskSeed
            {
                DisplayName = "Thu ng\u00e2n",
                Description = "Thuc hien thanh toan va ho tro khach mua hang.",
                Aliases = new[]
                {
                    "Thu ngan"
                }
            },
            new DefaultTaskSeed
            {
                DisplayName = "S\u1eafp x\u1ebfp h\u00e0ng h\u00f3a",
                Description = "Sap xep, bo sung va kiem tra trung bay hang hoa.",
                Aliases = new[]
                {
                    "Sap xep hang hoa"
                }
            },
            new DefaultTaskSeed
            {
                DisplayName = "Ki\u1ec3m k\u00ea kho",
                Description = "Kiem tra ton kho, doi chieu so lieu va cap nhat chenh lech.",
                Aliases = new[]
                {
                    "Kiem ke kho"
                }
            },
            new DefaultTaskSeed
            {
                DisplayName = "V\u1ec7 sinh",
                Description = "Don dep khu vuc ban hang, kho va quay thu ngan.",
                Aliases = new[]
                {
                    "Ve sinh"
                }
            }
        };

        public WeeklyScheduleBoard GetWeeklySchedule(DateTime anchorDate)
        {
            DateTime weekStart = GetWeekStart(anchorDate);
            DateTime weekEnd = weekStart.AddDays(6);

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                EnsureDefaultTaskCatalog(db);
                EnsureWeekSchedules(db, weekStart, weekEnd);

                List<NHAN_VIEN> employees = db.NHAN_VIEN
                    .Include(x => x.CA_LAM_VIEC)
                    .OrderBy(x => x.TenNhanVien)
                    .ToList();

                List<LICH_LAM_VIEC> schedules = db.LICH_LAM_VIEC
                    .Include(x => x.CA_LAM_VIEC)
                    .Include(x => x.LICH_LAM_CONG_VIEC.Select(task => task.CONG_VIEC))
                    .Where(x => x.NgayLam >= weekStart && x.NgayLam <= weekEnd)
                    .ToList();

                List<ScheduleDayColumn> columns = Enumerable.Range(0, 7)
                    .Select(offset =>
                    {
                        DateTime date = weekStart.AddDays(offset);
                        return new ScheduleDayColumn
                        {
                            Key = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
                            Date = date,
                            HeaderText = BuildColumnHeader(date)
                        };
                    })
                    .ToList();

                List<ScheduleEmployeeRow> rows = employees
                    .Select(employee => new ScheduleEmployeeRow
                    {
                        MaNhanVien = employee.MaNhanVien,
                        TenNhanVien = employee.TenNhanVien,
                        Cells = columns
                            .Select(column =>
                            {
                                LICH_LAM_VIEC schedule = schedules.First(x =>
                                    x.MaNhanVien == employee.MaNhanVien &&
                                    x.NgayLam.Date == column.Date.Date);

                                return BuildCell(schedule, employee);
                            })
                            .ToList()
                    })
                    .ToList();

                DateTime today = DateTime.Today;
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                List<LICH_LAM_VIEC> todaySchedules = db.LICH_LAM_VIEC
                    .Include(x => x.CA_LAM_VIEC)
                    .Where(x => DbFunctions.TruncateTime(x.NgayLam) == today)
                    .ToList();

                int caLamHomNay = todaySchedules.Count(x => !IsRestSchedule(x));
                int dangTrongCa = todaySchedules.Count(x => IsScheduleActiveAt(x, currentTime));
                int viPham = todaySchedules.Count(x =>
                    !IsRestSchedule(x) &&
                    (x.LoaiLich == CheDoTangCa || !string.IsNullOrWhiteSpace(x.LyDoDieuChinh)));

                return new WeeklyScheduleBoard
                {
                    WeekStart = weekStart,
                    WeekEnd = weekEnd,
                    Columns = columns,
                    Rows = rows,
                    Stats = new ScheduleBoardStats
                    {
                        TongNhanVien = employees.Count,
                        DangTrongCa = dangTrongCa,
                        CaLamHomNay = caLamHomNay,
                        ViPham = viPham
                    }
                };
            }
        }

        public List<ShiftOption> GetShiftOptions()
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                return db.CA_LAM_VIEC
                    .AsNoTracking()
                    .OrderBy(x => x.TenCa)
                    .Select(x => new ShiftOption
                    {
                        MaCa = x.MaCa,
                        TenCa = x.TenCa,
                        GioBatDau = x.GioBatDau,
                        GioKetThuc = x.GioKetThuc
                    })
                    .ToList();
            }
        }

        public ScheduleEditItem GetScheduleEditItem(int maNhanVien, DateTime ngayLam)
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                LICH_LAM_VIEC schedule = GetOrCreateSchedule(db, maNhanVien, ngayLam.Date, includeTasks: false);
                NHAN_VIEN employee = db.NHAN_VIEN.AsNoTracking().First(x => x.MaNhanVien == maNhanVien);

                return new ScheduleEditItem
                {
                    MaLich = schedule.MaLich,
                    MaNhanVien = maNhanVien,
                    TenNhanVien = employee.TenNhanVien,
                    NgayLam = schedule.NgayLam.Date,
                    CheDoLich = ResolveScheduleMode(schedule),
                    MaCa = schedule.MaCa,
                    GioBatDau = schedule.GioBatDauDuKien,
                    GioKetThuc = schedule.GioKetThucDuKien,
                    LyDoDieuChinh = schedule.LyDoDieuChinh,
                    GhiChu = schedule.GhiChu
                };
            }
        }

        public int SaveScheduleEdit(ScheduleEditSaveModel model)
        {
            ValidateScheduleEdit(model);

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                LICH_LAM_VIEC schedule = db.LICH_LAM_VIEC
                    .Include(x => x.LICH_LAM_CONG_VIEC)
                    .FirstOrDefault(x => x.MaNhanVien == model.MaNhanVien && x.NgayLam == model.NgayLam.Date);

                if (schedule == null)
                {
                    schedule = GetOrCreateSchedule(db, model.MaNhanVien, model.NgayLam.Date, includeTasks: true);
                }

                ApplyScheduleChanges(db, schedule, model);
                RebalanceTaskTimes(schedule);
                db.SaveChanges();

                return schedule.MaLich;
            }
        }

        public TaskAssignmentEditItem GetTaskAssignmentEditItem(int maNhanVien, DateTime ngayLam)
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                EnsureDefaultTaskCatalog(db);

                LICH_LAM_VIEC schedule = GetOrCreateSchedule(db, maNhanVien, ngayLam.Date, includeTasks: true);
                NHAN_VIEN employee = db.NHAN_VIEN.AsNoTracking().First(x => x.MaNhanVien == maNhanVien);

                return new TaskAssignmentEditItem
                {
                    MaLich = schedule.MaLich,
                    MaNhanVien = maNhanVien,
                    TenNhanVien = employee.TenNhanVien,
                    NgayLam = schedule.NgayLam.Date,
                    CaHienTai = BuildShiftLabel(schedule),
                    GioBatDau = schedule.GioBatDauDuKien,
                    GioKetThuc = schedule.GioKetThucDuKien,
                    SelectedTaskIds = schedule.LICH_LAM_CONG_VIEC
                        .OrderBy(x => x.UuTien)
                        .Select(x => x.MaCongViec)
                        .ToList(),
                    AvailableTasks = db.CONG_VIEC
                        .AsNoTracking()
                        .Where(x => x.TrangThai)
                        .OrderBy(x => x.TenCongViec)
                        .Select(x => new WorkTaskOption
                        {
                            MaCongViec = x.MaCongViec,
                            TenCongViec = x.TenCongViec,
                            MoTa = x.MoTa,
                            DangHoatDong = x.TrangThai
                        })
                        .ToList()
                };
            }
        }

        public void SaveTaskAssignments(TaskAssignmentSaveModel model)
        {
            if (model == null)
            {
                throw new InvalidOperationException("Du lieu phan cong cong viec khong hop le.");
            }

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                EnsureDefaultTaskCatalog(db);

                LICH_LAM_VIEC schedule = db.LICH_LAM_VIEC
                    .Include(x => x.LICH_LAM_CONG_VIEC)
                    .FirstOrDefault(x => x.MaNhanVien == model.MaNhanVien && x.NgayLam == model.NgayLam.Date);

                if (schedule == null)
                {
                    schedule = GetOrCreateSchedule(db, model.MaNhanVien, model.NgayLam.Date, includeTasks: true);
                }

                if (IsRestSchedule(schedule))
                {
                    throw new InvalidOperationException("Ngay nay dang de nghi. Hay chinh sua lich lam truoc khi phan cong cong viec.");
                }

                List<int> taskIds = model.TaskIds == null
                    ? new List<int>()
                    : model.TaskIds
                        .Distinct()
                        .ToList();

                ClearTaskAssignments(db, schedule);

                if (taskIds.Any())
                {
                    List<CONG_VIEC> selectedTasks = db.CONG_VIEC
                        .Where(x => taskIds.Contains(x.MaCongViec) && x.TrangThai)
                        .ToList();

                    selectedTasks = selectedTasks
                        .OrderBy(x => taskIds.IndexOf(x.MaCongViec))
                        .ToList();

                    for (int index = 0; index < selectedTasks.Count; index++)
                    {
                        CONG_VIEC task = selectedTasks[index];
                        schedule.LICH_LAM_CONG_VIEC.Add(new LICH_LAM_CONG_VIEC
                        {
                            MaCongViec = task.MaCongViec,
                            UuTien = index + 1,
                            GhiChu = null
                        });
                    }
                }

                ApplyAudit(schedule);
                RebalanceTaskTimes(schedule);
                db.SaveChanges();
            }
        }

        private static void ValidateScheduleEdit(ScheduleEditSaveModel model)
        {
            if (model == null)
            {
                throw new InvalidOperationException("Du lieu lich lam khong hop le.");
            }

            if (model.MaNhanVien <= 0)
            {
                throw new InvalidOperationException("Khong xac dinh duoc nhan vien can cap nhat lich.");
            }

            if (model.CheDoLich != CheDoTheoCa && model.CheDoLich != CheDoNghi && model.CheDoLich != CheDoTangCa)
            {
                throw new InvalidOperationException("Che do lich lam khong hop le.");
            }

            if (model.CheDoLich == CheDoNghi)
            {
                return;
            }

            if (!model.MaCa.HasValue)
            {
                throw new InvalidOperationException("Vui long chon ca lam.");
            }

            if (!model.GioBatDau.HasValue || !model.GioKetThuc.HasValue)
            {
                throw new InvalidOperationException("Vui long chon gio bat dau va gio ket thuc.");
            }

            if (model.GioKetThuc.Value == model.GioBatDau.Value)
            {
                throw new InvalidOperationException("Gio ket thuc khong duoc trung voi gio bat dau.");
            }
        }

        private static void ApplyScheduleChanges(CUA_HANG_TIEN_LOI_Entities db, LICH_LAM_VIEC schedule, ScheduleEditSaveModel model)
        {
            CA_LAM_VIEC shift = model.MaCa.HasValue
                ? db.CA_LAM_VIEC.FirstOrDefault(x => x.MaCa == model.MaCa.Value)
                : null;

            schedule.NgayLam = model.NgayLam.Date;
            schedule.LyDoDieuChinh = NormalizeOptional(model.LyDoDieuChinh);
            schedule.GhiChu = NormalizeOptional(model.GhiChu);

            if (model.CheDoLich == CheDoNghi)
            {
                schedule.MaCa = null;
                schedule.TrangThaiCa = "Nghi";
                schedule.LoaiLich = CheDoNghi;
                schedule.GioBatDauDuKien = null;
                schedule.GioKetThucDuKien = null;

                ClearTaskAssignments(db, schedule);
            }
            else
            {
                if (shift == null)
                {
                    throw new InvalidOperationException("Ca lam khong ton tai.");
                }

                schedule.MaCa = shift.MaCa;
                schedule.TrangThaiCa = shift.TenCa;
                schedule.LoaiLich = model.CheDoLich;
                schedule.GioBatDauDuKien = model.GioBatDau;
                schedule.GioKetThucDuKien = model.GioKetThuc;
            }

            ApplyAudit(schedule);
        }

        private static void ApplyAudit(LICH_LAM_VIEC schedule)
        {
            schedule.NguoiCapNhat = UserSession.MaNhanVien > 0
                ? (int?)UserSession.MaNhanVien
                : schedule.NguoiCapNhat;
            schedule.NgayCapNhat = DateTime.Now;
        }

        private static void RebalanceTaskTimes(LICH_LAM_VIEC schedule)
        {
            List<LICH_LAM_CONG_VIEC> tasks = schedule.LICH_LAM_CONG_VIEC
                .OrderBy(x => x.UuTien)
                .ToList();

            if (!tasks.Any())
            {
                return;
            }

            if (!schedule.GioBatDauDuKien.HasValue ||
                !schedule.GioKetThucDuKien.HasValue)
            {
                foreach (LICH_LAM_CONG_VIEC task in tasks)
                {
                    task.TuGio = null;
                    task.DenGio = null;
                }

                return;
            }

            TimeSpan startTime = schedule.GioBatDauDuKien.Value;
            TimeSpan endTime = schedule.GioKetThucDuKien.Value;
            bool wrapsToNextDay = endTime <= startTime;
            double totalMinutes = wrapsToNextDay
                ? (TimeSpan.FromHours(24) - startTime + endTime).TotalMinutes
                : (endTime - startTime).TotalMinutes;

            if (totalMinutes <= 0)
            {
                foreach (LICH_LAM_CONG_VIEC task in tasks)
                {
                    task.TuGio = null;
                    task.DenGio = null;
                }

                return;
            }

            for (int index = 0; index < tasks.Count; index++)
            {
                int startOffset = (int)Math.Round(index * totalMinutes / tasks.Count, MidpointRounding.AwayFromZero);
                int endOffset = index == tasks.Count - 1
                    ? (int)totalMinutes
                    : (int)Math.Round((index + 1) * totalMinutes / tasks.Count, MidpointRounding.AwayFromZero);

                tasks[index].UuTien = index + 1;
                tasks[index].TuGio = NormalizeClockTime(startTime.Add(TimeSpan.FromMinutes(startOffset)));
                tasks[index].DenGio = NormalizeClockTime(startTime.Add(TimeSpan.FromMinutes(endOffset)));
            }
        }

        private static ScheduleCellItem BuildCell(LICH_LAM_VIEC schedule, NHAN_VIEN employee)
        {
            string taskSummary = BuildTaskSummary(schedule);
            string shiftLabel = BuildShiftLabel(schedule);
            string timeRangeText = BuildTimeRange(schedule);
            bool isRestDay = IsRestSchedule(schedule);

            List<string> displayLines = new List<string>();
            displayLines.Add(shiftLabel);

            if (!isRestDay && !string.IsNullOrEmpty(timeRangeText))
            {
                displayLines.Add(timeRangeText);
            }

            if (!isRestDay)
            {
                displayLines.Add(string.IsNullOrEmpty(taskSummary) ? "Chua phan cong cong viec" : taskSummary);
            }

            return new ScheduleCellItem
            {
                MaLich = schedule.MaLich,
                MaNhanVien = employee.MaNhanVien,
                TenNhanVien = employee.TenNhanVien,
                NgayLam = schedule.NgayLam.Date,
                MaCa = schedule.MaCa,
                ShiftLabel = shiftLabel,
                TimeRangeText = timeRangeText,
                TaskSummary = taskSummary,
                DisplayText = string.Join(Environment.NewLine, displayLines),
                LoaiLich = schedule.LoaiLich,
                IsRestDay = isRestDay,
                IsOvertime = schedule.LoaiLich == CheDoTangCa,
                HasTasks = !string.IsNullOrEmpty(taskSummary)
            };
        }

        private static string BuildShiftLabel(LICH_LAM_VIEC schedule)
        {
            if (IsRestSchedule(schedule))
            {
                return "Nghi";
            }

            string baseLabel = schedule.CA_LAM_VIEC != null && !string.IsNullOrWhiteSpace(schedule.CA_LAM_VIEC.TenCa)
                ? schedule.CA_LAM_VIEC.TenCa
                : (!string.IsNullOrWhiteSpace(schedule.TrangThaiCa) ? schedule.TrangThaiCa : "Ca lam");

            return schedule.LoaiLich == CheDoTangCa
                ? baseLabel + " + tang ca"
                : baseLabel;
        }

        private static string BuildTaskSummary(LICH_LAM_VIEC schedule)
        {
            List<string> taskNames = schedule.LICH_LAM_CONG_VIEC
                .OrderBy(x => x.UuTien)
                .Select(x => x.CONG_VIEC != null ? GetTaskDisplayName(x.CONG_VIEC.TenCongViec) : null)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            return taskNames.Any()
                ? string.Join(", ", taskNames)
                : null;
        }

        private static string BuildTimeRange(LICH_LAM_VIEC schedule)
        {
            if (!schedule.GioBatDauDuKien.HasValue || !schedule.GioKetThucDuKien.HasValue)
            {
                return null;
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                "{0:hh\\:mm} - {1:hh\\:mm}",
                schedule.GioBatDauDuKien.Value,
                schedule.GioKetThucDuKien.Value);
        }

        private static string ResolveScheduleMode(LICH_LAM_VIEC schedule)
        {
            if (IsRestSchedule(schedule))
            {
                return CheDoNghi;
            }

            return schedule.LoaiLich == CheDoTangCa
                ? CheDoTangCa
                : CheDoTheoCa;
        }

        private static bool IsRestSchedule(LICH_LAM_VIEC schedule)
        {
            return schedule == null ||
                string.Equals(schedule.LoaiLich, CheDoNghi, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(schedule.TrangThaiCa, "Nghi", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsScheduleActiveAt(LICH_LAM_VIEC schedule, TimeSpan currentTime)
        {
            if (IsRestSchedule(schedule) ||
                !schedule.GioBatDauDuKien.HasValue ||
                !schedule.GioKetThucDuKien.HasValue)
            {
                return false;
            }

            TimeSpan startTime = schedule.GioBatDauDuKien.Value;
            TimeSpan endTime = schedule.GioKetThucDuKien.Value;

            if (endTime > startTime)
            {
                return currentTime >= startTime && currentTime < endTime;
            }

            return currentTime >= startTime || currentTime < endTime;
        }

        private static void EnsureDefaultTaskCatalog(CUA_HANG_TIEN_LOI_Entities db)
        {
            List<CONG_VIEC> existingTasks = db.CONG_VIEC.ToList();
            bool hasChanges = false;

            foreach (DefaultTaskSeed taskSeed in DefaultTasks)
            {
                HashSet<string> acceptedKeys = new HashSet<string>(
                    taskSeed.Aliases
                        .Concat(new[] { taskSeed.DisplayName })
                        .Select(BuildTextKey));

                CONG_VIEC existingTask = existingTasks
                    .FirstOrDefault(x => acceptedKeys.Contains(BuildTextKey(x.TenCongViec)));

                if (existingTask == null)
                {
                    existingTask = new CONG_VIEC
                    {
                        TenCongViec = taskSeed.DisplayName,
                        MoTa = taskSeed.Description,
                        TrangThai = true
                    };

                    db.CONG_VIEC.Add(existingTask);
                    existingTasks.Add(existingTask);
                    hasChanges = true;
                }
                else
                {
                    if (!string.Equals(existingTask.TenCongViec, taskSeed.DisplayName, StringComparison.Ordinal))
                    {
                        existingTask.TenCongViec = taskSeed.DisplayName;
                        hasChanges = true;
                    }

                    if (string.IsNullOrWhiteSpace(existingTask.MoTa))
                    {
                        existingTask.MoTa = taskSeed.Description;
                        hasChanges = true;
                    }

                    if (!existingTask.TrangThai)
                    {
                        existingTask.TrangThai = true;
                        hasChanges = true;
                    }
                }
            }

            if (hasChanges)
            {
                db.SaveChanges();
            }
        }

        private static void EnsureWeekSchedules(CUA_HANG_TIEN_LOI_Entities db, DateTime weekStart, DateTime weekEnd)
        {
            List<NHAN_VIEN> employees = db.NHAN_VIEN
                .Include(x => x.CA_LAM_VIEC)
                .OrderBy(x => x.TenNhanVien)
                .ToList();

            HashSet<string> existingKeys = new HashSet<string>(
                db.LICH_LAM_VIEC
                    .Where(x => x.NgayLam >= weekStart && x.NgayLam <= weekEnd)
                    .AsEnumerable()
                    .Select(x => BuildScheduleKey(x.MaNhanVien, x.NgayLam))
                    .ToList());

            bool hasChanges = false;

            foreach (NHAN_VIEN employee in employees)
            {
                for (int offset = 0; offset < 7; offset++)
                {
                    DateTime date = weekStart.AddDays(offset).Date;
                    string key = BuildScheduleKey(employee.MaNhanVien, date);
                    if (existingKeys.Contains(key))
                    {
                        continue;
                    }

                    db.LICH_LAM_VIEC.Add(CreateDefaultSchedule(employee, date));
                    existingKeys.Add(key);
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                db.SaveChanges();
            }
        }

        private static LICH_LAM_VIEC GetOrCreateSchedule(CUA_HANG_TIEN_LOI_Entities db, int maNhanVien, DateTime ngayLam, bool includeTasks)
        {
            IQueryable<LICH_LAM_VIEC> query = db.LICH_LAM_VIEC;
            if (includeTasks)
            {
                query = query.Include(x => x.LICH_LAM_CONG_VIEC.Select(task => task.CONG_VIEC));
            }

            LICH_LAM_VIEC schedule = query.FirstOrDefault(x => x.MaNhanVien == maNhanVien && x.NgayLam == ngayLam.Date);
            if (schedule != null)
            {
                return schedule;
            }

            NHAN_VIEN employee = db.NHAN_VIEN
                .Include(x => x.CA_LAM_VIEC)
                .FirstOrDefault(x => x.MaNhanVien == maNhanVien);

            if (employee == null)
            {
                throw new InvalidOperationException("Nhan vien khong ton tai.");
            }

            schedule = CreateDefaultSchedule(employee, ngayLam.Date);
            db.LICH_LAM_VIEC.Add(schedule);
            db.SaveChanges();

            return includeTasks
                ? db.LICH_LAM_VIEC
                    .Include(x => x.LICH_LAM_CONG_VIEC.Select(task => task.CONG_VIEC))
                    .First(x => x.MaLich == schedule.MaLich)
                : schedule;
        }

        private static LICH_LAM_VIEC CreateDefaultSchedule(NHAN_VIEN employee, DateTime date)
        {
            bool hasDefaultShift = employee.MaCa.HasValue && employee.CA_LAM_VIEC != null;
            bool isWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
            bool isWorkingDay = hasDefaultShift && !isWeekend;

            return new LICH_LAM_VIEC
            {
                MaNhanVien = employee.MaNhanVien,
                NgayLam = date.Date,
                MaCa = isWorkingDay ? employee.MaCa : null,
                TrangThaiCa = isWorkingDay ? employee.CA_LAM_VIEC.TenCa : "Nghi",
                LoaiLich = isWorkingDay ? CheDoTheoCa : CheDoNghi,
                GioBatDauDuKien = isWorkingDay ? employee.CA_LAM_VIEC.GioBatDau : null,
                GioKetThucDuKien = isWorkingDay ? employee.CA_LAM_VIEC.GioKetThuc : null,
                GioBatDauThucTe = null,
                GioKetThucThucTe = null,
                LyDoDieuChinh = null,
                GhiChu = null,
                NguoiCapNhat = UserSession.MaNhanVien > 0 ? (int?)UserSession.MaNhanVien : null,
                NgayCapNhat = DateTime.Now
            };
        }

        private static DateTime GetWeekStart(DateTime date)
        {
            int diff = (7 + ((int)date.DayOfWeek - (int)DayOfWeek.Monday)) % 7;
            return date.Date.AddDays(-diff);
        }

        private static string BuildColumnHeader(DateTime date)
        {
            string dayName;

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayName = "T2";
                    break;
                case DayOfWeek.Tuesday:
                    dayName = "T3";
                    break;
                case DayOfWeek.Wednesday:
                    dayName = "T4";
                    break;
                case DayOfWeek.Thursday:
                    dayName = "T5";
                    break;
                case DayOfWeek.Friday:
                    dayName = "T6";
                    break;
                case DayOfWeek.Saturday:
                    dayName = "T7";
                    break;
                default:
                    dayName = "CN";
                    break;
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                "{0} {1:dd/MM}",
                dayName,
                date);
        }

        private static string NormalizeOptional(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : value.Trim();
        }

        private static void ClearTaskAssignments(CUA_HANG_TIEN_LOI_Entities db, LICH_LAM_VIEC schedule)
        {
            List<LICH_LAM_CONG_VIEC> existingAssignments = schedule.LICH_LAM_CONG_VIEC.ToList();
            if (!existingAssignments.Any())
            {
                return;
            }

            db.LICH_LAM_CONG_VIEC.RemoveRange(existingAssignments);

            foreach (LICH_LAM_CONG_VIEC assignment in existingAssignments)
            {
                schedule.LICH_LAM_CONG_VIEC.Remove(assignment);
            }
        }

        private static TimeSpan NormalizeClockTime(TimeSpan value)
        {
            double totalMinutes = value.TotalMinutes % (24 * 60);
            if (totalMinutes < 0)
            {
                totalMinutes += 24 * 60;
            }

            return TimeSpan.FromMinutes(totalMinutes);
        }

        private static string GetTaskDisplayName(string taskName)
        {
            string taskKey = BuildTextKey(taskName);
            DefaultTaskSeed matchedTask = DefaultTasks.FirstOrDefault(seed =>
                seed.Aliases
                    .Concat(new[] { seed.DisplayName })
                    .Select(BuildTextKey)
                    .Contains(taskKey));

            return matchedTask != null
                ? matchedTask.DisplayName
                : taskName;
        }

        private static string BuildScheduleKey(int maNhanVien, DateTime ngayLam)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}_{1:yyyyMMdd}",
                maNhanVien,
                ngayLam.Date);
        }

        private static string BuildTextKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            string normalized = value.Trim().Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder(normalized.Length);

            foreach (char character in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(character);
                if (category == UnicodeCategory.NonSpacingMark)
                {
                    continue;
                }

                if (char.IsLetterOrDigit(character))
                {
                    builder.Append(char.ToLowerInvariant(character));
                }
            }

            return builder.ToString();
        }
    }
}
