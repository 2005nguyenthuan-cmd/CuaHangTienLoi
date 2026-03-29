using demo.BLL.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace demo.Control
{
    public partial class UC_LichLamNhanVien : UserControl
    {
        private readonly ScheduleService scheduleService = new ScheduleService();

        private WeeklyScheduleBoard currentBoard;
        private DateTime currentWeekStart;
        private FlowLayoutPanel headerActionPanel;
        private Button btnChinhSuaLich;
        private Button btnPhanCongCongViec;

        public event EventHandler DataChanged;

        public UC_LichLamNhanVien()
        {
            InitializeComponent();
            InitializeHeaderActions();
            ConfigureScheduleGrid();
            LoadWeek(DateTime.Today);
        }

        private void InitializeHeaderActions()
        {
            panelCardHeader.Height = 108;
            lblWeekTitle.Location = new Point(19, 10);
            lblSubTitle.Location = new Point(21, 50);
            lblSubTitle.MaximumSize = new Size(620, 0);

            headerActionPanel = new FlowLayoutPanel();
            headerActionPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            headerActionPanel.AutoSize = true;
            headerActionPanel.FlowDirection = FlowDirection.LeftToRight;
            headerActionPanel.WrapContents = false;
            headerActionPanel.Location = new Point(panelCardHeader.Width - 372, 16);
            headerActionPanel.Margin = Padding.Empty;
            headerActionPanel.Padding = Padding.Empty;

            btnChinhSuaLich = CreateActionButton("Chinh sua lich", false);
            btnChinhSuaLich.Click += btnChinhSuaLich_Click;

            btnPhanCongCongViec = CreateActionButton("Phan cong cong viec", true);
            btnPhanCongCongViec.Click += btnPhanCongCongViec_Click;

            headerActionPanel.Controls.Add(btnChinhSuaLich);
            headerActionPanel.Controls.Add(btnPhanCongCongViec);
            panelCardHeader.Controls.Add(headerActionPanel);
        }

        private void ConfigureScheduleGrid()
        {
            dgvSchedule.AutoGenerateColumns = false;
            dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSchedule.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvSchedule.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSchedule.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSchedule.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSchedule.DefaultCellStyle.Font = new Font("Segoe UI", 8.75F, FontStyle.Regular);
            dgvSchedule.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvSchedule.DefaultCellStyle.Padding = new Padding(6, 10, 6, 10);
            dgvSchedule.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSchedule.RowTemplate.Height = 94;
            dgvSchedule.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgvSchedule.CellClick += dgvSchedule_CellClick;
            dgvSchedule.CellDoubleClick += dgvSchedule_CellDoubleClick;
            dgvSchedule.SelectionChanged += dgvSchedule_SelectionChanged;
        }

        private void LoadWeek(DateTime anchorDate)
        {
            currentBoard = scheduleService.GetWeeklySchedule(anchorDate);
            currentWeekStart = currentBoard.WeekStart;

            lblWeekTitle.Text = string.Format(
                CultureInfo.InvariantCulture,
                "Lich lam viec - Tuan {0:dd/MM} - {1:dd/MM/yyyy}",
                currentBoard.WeekStart,
                currentBoard.WeekEnd);

            RenderBoard();
            SelectFirstScheduleCell();
            UpdateSelectionMessage();
        }

        private void RenderBoard()
        {
            dgvSchedule.SuspendLayout();
            dgvSchedule.Columns.Clear();
            dgvSchedule.Rows.Clear();

            AddColumn("NhanVien", "Nhan vien", 220, 180F);

            foreach (ScheduleDayColumn dayColumn in currentBoard.Columns)
            {
                AddColumn(dayColumn.Key, dayColumn.HeaderText, 136, 104F);
            }

            foreach (ScheduleEmployeeRow employeeRow in currentBoard.Rows)
            {
                int rowIndex = dgvSchedule.Rows.Add();
                DataGridViewRow gridRow = dgvSchedule.Rows[rowIndex];
                gridRow.Tag = employeeRow;
                gridRow.Height = 94;

                DataGridViewCell employeeCell = gridRow.Cells[0];
                employeeCell.Value = employeeRow.TenNhanVien;
                employeeCell.ToolTipText = employeeRow.TenNhanVien;
                employeeCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                employeeCell.Style.BackColor = Color.White;
                employeeCell.Style.ForeColor = Color.FromArgb(31, 41, 55);
                employeeCell.Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
                employeeCell.Style.Padding = new Padding(12, 0, 8, 0);
                employeeCell.Style.SelectionBackColor = Color.FromArgb(240, 249, 255);
                employeeCell.Style.SelectionForeColor = Color.FromArgb(31, 41, 55);

                for (int cellIndex = 0; cellIndex < employeeRow.Cells.Count; cellIndex++)
                {
                    ScheduleCellItem scheduleCell = employeeRow.Cells[cellIndex];
                    DataGridViewCell gridCell = gridRow.Cells[cellIndex + 1];
                    gridCell.Value = scheduleCell.DisplayText;
                    gridCell.ToolTipText = BuildToolTip(scheduleCell);
                    ApplyScheduleCellStyle(gridCell, scheduleCell);
                }
            }

            dgvSchedule.ResumeLayout();
        }

        private void AddColumn(string name, string headerText, int minimumWidth, float fillWeight)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.MinimumWidth = minimumWidth;
            column.FillWeight = fillWeight;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ReadOnly = true;

            if (string.Equals(name, "NhanVien", StringComparison.Ordinal))
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = minimumWidth;
                column.Frozen = true;
            }
            else
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvSchedule.Columns.Add(column);
        }

        private void ApplyScheduleCellStyle(DataGridViewCell cell, ScheduleCellItem scheduleCell)
        {
            Color backColor;
            Color foreColor;
            Color selectionBackColor;
            string shiftKey = BuildTextKey(scheduleCell.ShiftLabel);

            if (scheduleCell.IsRestDay)
            {
                backColor = Color.FromArgb(245, 245, 245);
                foreColor = Color.FromArgb(156, 163, 175);
                selectionBackColor = Color.FromArgb(229, 231, 235);
            }
            else if (shiftKey.Contains("sang"))
            {
                backColor = scheduleCell.IsOvertime
                    ? Color.FromArgb(220, 252, 231)
                    : Color.FromArgb(232, 245, 233);
                foreColor = scheduleCell.IsOvertime
                    ? Color.FromArgb(21, 128, 61)
                    : Color.FromArgb(46, 125, 50);
                selectionBackColor = scheduleCell.IsOvertime
                    ? Color.FromArgb(187, 247, 208)
                    : Color.FromArgb(200, 230, 201);
            }
            else if (shiftKey.Contains("chieu"))
            {
                backColor = scheduleCell.IsOvertime
                    ? Color.FromArgb(255, 237, 213)
                    : Color.FromArgb(255, 243, 224);
                foreColor = scheduleCell.IsOvertime
                    ? Color.FromArgb(194, 65, 12)
                    : Color.FromArgb(245, 124, 0);
                selectionBackColor = scheduleCell.IsOvertime
                    ? Color.FromArgb(254, 215, 170)
                    : Color.FromArgb(255, 224, 178);
            }
            else
            {
                backColor = scheduleCell.IsOvertime
                    ? Color.FromArgb(219, 234, 254)
                    : Color.FromArgb(227, 242, 253);
                foreColor = scheduleCell.IsOvertime
                    ? Color.FromArgb(30, 64, 175)
                    : Color.FromArgb(21, 101, 192);
                selectionBackColor = scheduleCell.IsOvertime
                    ? Color.FromArgb(191, 219, 254)
                    : Color.FromArgb(187, 222, 251);
            }

            cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cell.Style.BackColor = backColor;
            cell.Style.ForeColor = foreColor;
            cell.Style.SelectionBackColor = selectionBackColor;
            cell.Style.SelectionForeColor = foreColor;
            cell.Style.Padding = new Padding(8, 10, 8, 10);
        }

        private void btnChinhSuaLich_Click(object sender, EventArgs e)
        {
            ScheduleCellItem selectedCell = GetSelectedScheduleCell();
            if (selectedCell == null)
            {
                MessageBox.Show("Vui long chon mot o lich lam cua nhan vien truoc khi chinh sua.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (Form_ChinhSuaLichLam form = new Form_ChinhSuaLichLam(selectedCell.MaNhanVien, selectedCell.NgayLam))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ReloadCurrentWeek(selectedCell.MaNhanVien, selectedCell.NgayLam);
                    OnDataChanged();
                }
            }
        }

        private void btnPhanCongCongViec_Click(object sender, EventArgs e)
        {
            ScheduleCellItem selectedCell = GetSelectedScheduleCell();
            if (selectedCell == null)
            {
                MessageBox.Show("Vui long chon mot o lich lam cua nhan vien truoc khi phan cong cong viec.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (Form_PhanCongCongViec form = new Form_PhanCongCongViec(selectedCell.MaNhanVien, selectedCell.NgayLam))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ReloadCurrentWeek(selectedCell.MaNhanVien, selectedCell.NgayLam);
                    OnDataChanged();
                }
            }
        }

        private void dgvSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 0 && dgvSchedule.Columns.Count > 1)
            {
                dgvSchedule.CurrentCell = dgvSchedule.Rows[e.RowIndex].Cells[1];
            }
        }

        private void dgvSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                btnChinhSuaLich_Click(sender, EventArgs.Empty);
            }
        }

        private void dgvSchedule_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectionMessage();
        }

        private void ReloadCurrentWeek(int maNhanVien, DateTime ngayLam)
        {
            LoadWeek(currentWeekStart);
            SelectScheduleCell(maNhanVien, ngayLam);
        }

        private void SelectFirstScheduleCell()
        {
            if (dgvSchedule.Rows.Count == 0 || dgvSchedule.Columns.Count <= 1)
            {
                return;
            }

            dgvSchedule.CurrentCell = dgvSchedule.Rows[0].Cells[1];
        }

        private void SelectScheduleCell(int maNhanVien, DateTime ngayLam)
        {
            for (int rowIndex = 0; rowIndex < dgvSchedule.Rows.Count; rowIndex++)
            {
                ScheduleEmployeeRow employeeRow = dgvSchedule.Rows[rowIndex].Tag as ScheduleEmployeeRow;
                if (employeeRow == null || employeeRow.MaNhanVien != maNhanVien)
                {
                    continue;
                }

                for (int cellIndex = 0; cellIndex < employeeRow.Cells.Count; cellIndex++)
                {
                    if (employeeRow.Cells[cellIndex].NgayLam.Date == ngayLam.Date)
                    {
                        dgvSchedule.CurrentCell = dgvSchedule.Rows[rowIndex].Cells[cellIndex + 1];
                        return;
                    }
                }
            }
        }

        private ScheduleCellItem GetSelectedScheduleCell()
        {
            if (currentBoard == null || dgvSchedule.CurrentCell == null)
            {
                return null;
            }

            int rowIndex = dgvSchedule.CurrentCell.RowIndex;
            int columnIndex = dgvSchedule.CurrentCell.ColumnIndex;
            if (rowIndex < 0 || columnIndex <= 0)
            {
                return null;
            }

            ScheduleEmployeeRow employeeRow = dgvSchedule.Rows[rowIndex].Tag as ScheduleEmployeeRow;
            if (employeeRow == null)
            {
                return null;
            }

            int scheduleIndex = columnIndex - 1;
            if (scheduleIndex < 0 || scheduleIndex >= employeeRow.Cells.Count)
            {
                return null;
            }

            return employeeRow.Cells[scheduleIndex];
        }

        private void UpdateSelectionMessage()
        {
            ScheduleCellItem selectedCell = GetSelectedScheduleCell();
            if (selectedCell == null)
            {
                lblSubTitle.Text = "Chon o lich de chinh sua lich lam, cho nghi, tang ca hoac phan cong them cong viec cho nhan vien.";
                return;
            }

            lblSubTitle.Text = string.Format(
                CultureInfo.InvariantCulture,
                "Dang chon {0} - {1:dd/MM/yyyy}. Ban co the cho nghi, tang ca hoac phan cong them cong viec ngay tren tuan dang hien thi.",
                selectedCell.TenNhanVien,
                selectedCell.NgayLam);
        }

        private static Button CreateActionButton(string text, bool isPrimary)
        {
            Button button = new Button();
            button.AutoSize = false;
            button.Size = new Size(isPrimary ? 164 : 128, 38);
            button.Margin = new Padding(0, 0, 12, 0);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button.Text = text;
            button.UseVisualStyleBackColor = false;

            if (isPrimary)
            {
                button.BackColor = Color.FromArgb(34, 197, 94);
                button.FlatAppearance.BorderSize = 0;
                button.ForeColor = Color.White;
            }
            else
            {
                button.BackColor = Color.White;
                button.FlatAppearance.BorderColor = Color.Silver;
                button.ForeColor = Color.FromArgb(55, 65, 81);
            }

            return button;
        }

        private static string BuildToolTip(ScheduleCellItem scheduleCell)
        {
            List<string> tooltipLines = new List<string>
            {
                scheduleCell.TenNhanVien,
                scheduleCell.NgayLam.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                scheduleCell.ShiftLabel
            };

            if (!string.IsNullOrWhiteSpace(scheduleCell.TimeRangeText))
            {
                tooltipLines.Add(scheduleCell.TimeRangeText);
            }

            if (!scheduleCell.IsRestDay)
            {
                tooltipLines.Add(string.IsNullOrWhiteSpace(scheduleCell.TaskSummary)
                    ? "Chua phan cong cong viec"
                    : scheduleCell.TaskSummary);
            }

            return string.Join(Environment.NewLine, tooltipLines);
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

        private void OnDataChanged()
        {
            EventHandler handler = DataChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
