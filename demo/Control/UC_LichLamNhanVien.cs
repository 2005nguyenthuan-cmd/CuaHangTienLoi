using System;
using System.Drawing;
using System.Windows.Forms;

namespace demo.Control
{
    public partial class UC_LichLamNhanVien : UserControl
    {
        public UC_LichLamNhanVien()
        {
            InitializeComponent();
            BuildSchedule();
            dgvSchedule.ClearSelection();
        }

        private void BuildSchedule()
        {
            dgvSchedule.Columns.Clear();
            dgvSchedule.Rows.Clear();

            AddColumn("NhanVien", "Nh\u00e2n vi\u00ean", 220, 190F);
            AddColumn("T3", "T3 03/03", 120, 90F);
            AddColumn("T4", "T4 04/03", 120, 90F);
            AddColumn("T5", "T5 05/03", 120, 90F);
            AddColumn("T6", "T6 06/03", 120, 90F);
            AddColumn("T7", "T7 07/03", 120, 90F);
            AddColumn("CN", "CN 08/03", 120, 90F);
            AddColumn("T2", "T2 09/03", 120, 90F);

            AddScheduleRow("Hu\u1ef3nh Quang Ki\u1ec7t", "Ca s\u00e1ng", "Ca s\u00e1ng", "Ca s\u00e1ng", "Ca s\u00e1ng", "Ngh\u1ec9", "Ngh\u1ec9", "Ngh\u1ec9");
            AddScheduleRow("Tr\u1ea7n Th\u1ecb Lan", "Ca chi\u1ec1u", "Ca chi\u1ec1u", "Ca chi\u1ec1u", "Ca chi\u1ec1u", "Ngh\u1ec9", "Ngh\u1ec9", "Ngh\u1ec9");
            AddScheduleRow("Nguy\u1ec5n V\u0103n Minh", "Ca s\u00e1ng", "Ca s\u00e1ng", "Ca s\u00e1ng", "Ca s\u00e1ng", "Ca t\u1ed1i", "Ca t\u1ed1i", "Ca t\u1ed1i");
            AddScheduleRow("L\u00ea Th\u1ecb Hoa", "Ca chi\u1ec1u", "Ca chi\u1ec1u", "Ca chi\u1ec1u", "Ca chi\u1ec1u", "Ngh\u1ec9", "Ngh\u1ec9", "Ngh\u1ec9");
            AddScheduleRow("Ph\u1ea1m Qu\u1ed1c B\u1ea3o", "Ca s\u00e1ng", "Ca s\u00e1ng", "Ca s\u00e1ng", "Ca s\u00e1ng", "Ngh\u1ec9", "Ngh\u1ec9", "Ngh\u1ec9");

            dgvSchedule.ClearSelection();
        }

        private void AddColumn(string name, string headerText, int minWidth, float fillWeight)
        {
            var column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                MinimumWidth = minWidth,
                FillWeight = fillWeight,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true
            };

            dgvSchedule.Columns.Add(column);
        }

        private void AddScheduleRow(string employeeName, params string[] shifts)
        {
            object[] values = new object[shifts.Length + 1];
            values[0] = employeeName;

            for (int index = 0; index < shifts.Length; index++)
            {
                values[index + 1] = shifts[index];
            }

            int rowIndex = dgvSchedule.Rows.Add(values);
            DataGridViewRow row = dgvSchedule.Rows[rowIndex];
            row.Height = 52;

            DataGridViewCell employeeCell = row.Cells[0];
            employeeCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            employeeCell.Style.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            employeeCell.Style.ForeColor = Color.FromArgb(31, 41, 55);
            employeeCell.Style.SelectionBackColor = Color.White;
            employeeCell.Style.SelectionForeColor = employeeCell.Style.ForeColor;

            for (int index = 0; index < shifts.Length; index++)
            {
                ApplyShiftCellStyle(row.Cells[index + 1], shifts[index]);
            }
        }

        private void ApplyShiftCellStyle(DataGridViewCell cell, string shiftName)
        {
            Color backColor;
            Color foreColor;

            switch (shiftName)
            {
                case "Ca s\u00e1ng":
                    backColor = Color.FromArgb(232, 245, 233);
                    foreColor = Color.FromArgb(46, 125, 50);
                    break;
                case "Ca chi\u1ec1u":
                    backColor = Color.FromArgb(255, 243, 224);
                    foreColor = Color.FromArgb(245, 124, 0);
                    break;
                case "Ca t\u1ed1i":
                    backColor = Color.FromArgb(227, 242, 253);
                    foreColor = Color.FromArgb(21, 101, 192);
                    break;
                default:
                    backColor = Color.FromArgb(245, 245, 245);
                    foreColor = Color.FromArgb(158, 158, 158);
                    break;
            }

            cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cell.Style.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            cell.Style.BackColor = backColor;
            cell.Style.ForeColor = foreColor;
            cell.Style.SelectionBackColor = backColor;
            cell.Style.SelectionForeColor = foreColor;
        }
    }
}
