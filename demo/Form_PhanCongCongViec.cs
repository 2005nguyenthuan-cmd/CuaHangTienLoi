using demo.BLL.Service;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace demo
{
    public class Form_PhanCongCongViec : Form
    {
        private sealed class TaskListItem
        {
            public int MaCongViec { get; set; }
            public string TenCongViec { get; set; }
            public string MoTa { get; set; }

            public override string ToString()
            {
                return TenCongViec;
            }
        }

        private readonly ScheduleService scheduleService = new ScheduleService();
        private readonly int maNhanVien;
        private readonly DateTime ngayLam;

        private Label lblNhanVienValue;
        private Label lblNgayValue;
        private Label lblCaValue;
        private Label lblGioValue;
        private Label lblMoTaCongViec;
        private Label lblHint;
        private CheckedListBox clbCongViec;
        private Button btnLuu;
        private Button btnHuy;

        public Form_PhanCongCongViec(int maNhanVien, DateTime ngayLam)
        {
            this.maNhanVien = maNhanVien;
            this.ngayLam = ngayLam.Date;

            InitializeComponent();
            LoadAssignmentData();
        }

        private void InitializeComponent()
        {
            Text = "Phan cong cong viec";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(680, 520);
            BackColor = Color.White;

            Label lblTitle = new Label();
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitle.Text = "Phan cong cong viec trong ca";

            Label lblDescription = new Label();
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblDescription.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescription.Margin = new Padding(0, 6, 0, 0);
            lblDescription.Text = "Nhan vien co the dam nhan nhieu cong viec trong cung mot ngay lam viec.";

            TableLayoutPanel infoTable = CreateTableLayout();
            lblNhanVienValue = CreateValueLabel();
            lblNgayValue = CreateValueLabel();
            lblCaValue = CreateValueLabel();
            lblGioValue = CreateValueLabel();
            AddField(infoTable, 0, "Nhan vien", lblNhanVienValue);
            AddField(infoTable, 1, "Ngay lam", lblNgayValue);
            AddField(infoTable, 2, "Ca hien tai", lblCaValue);
            AddField(infoTable, 3, "Khung gio", lblGioValue);

            Label lblTaskTitle = new Label();
            lblTaskTitle.AutoSize = true;
            lblTaskTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTaskTitle.ForeColor = Color.FromArgb(55, 65, 81);
            lblTaskTitle.Margin = new Padding(0, 18, 0, 8);
            lblTaskTitle.Text = "Chon cong viec cho nhan vien";

            clbCongViec = new CheckedListBox();
            clbCongViec.BorderStyle = BorderStyle.FixedSingle;
            clbCongViec.CheckOnClick = true;
            clbCongViec.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            clbCongViec.Height = 180;
            clbCongViec.IntegralHeight = false;
            clbCongViec.Dock = DockStyle.Top;
            clbCongViec.Margin = new Padding(0, 0, 0, 0);
            clbCongViec.SelectedIndexChanged += clbCongViec_SelectedIndexChanged;

            lblMoTaCongViec = new Label();
            lblMoTaCongViec.AutoSize = false;
            lblMoTaCongViec.Dock = DockStyle.Top;
            lblMoTaCongViec.Height = 54;
            lblMoTaCongViec.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblMoTaCongViec.ForeColor = Color.FromArgb(107, 114, 128);
            lblMoTaCongViec.Margin = new Padding(0, 10, 0, 0);

            lblHint = new Label();
            lblHint.AutoSize = true;
            lblHint.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblHint.ForeColor = Color.FromArgb(107, 114, 128);
            lblHint.Margin = new Padding(0, 12, 0, 0);

            FlowLayoutPanel actionPanel = new FlowLayoutPanel();
            actionPanel.Dock = DockStyle.Fill;
            actionPanel.FlowDirection = FlowDirection.RightToLeft;
            actionPanel.WrapContents = false;
            actionPanel.AutoSize = true;
            actionPanel.Margin = new Padding(0, 24, 0, 0);

            btnLuu = new Button();
            btnLuu.BackColor = Color.FromArgb(37, 99, 235);
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Size = new Size(150, 38);
            btnLuu.Text = "Luu phan cong";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;

            btnHuy = new Button();
            btnHuy.BackColor = Color.White;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.FlatAppearance.BorderColor = Color.Silver;
            btnHuy.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            btnHuy.ForeColor = Color.FromArgb(55, 65, 81);
            btnHuy.Size = new Size(100, 38);
            btnHuy.Text = "Huy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;

            actionPanel.Controls.Add(btnLuu);
            actionPanel.Controls.Add(btnHuy);

            TableLayoutPanel rootLayout = new TableLayoutPanel();
            rootLayout.ColumnCount = 1;
            rootLayout.RowCount = 8;
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Padding = new Padding(24);
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            rootLayout.Controls.Add(lblTitle, 0, 0);
            rootLayout.Controls.Add(lblDescription, 0, 1);
            rootLayout.Controls.Add(infoTable, 0, 2);
            rootLayout.Controls.Add(lblTaskTitle, 0, 3);
            rootLayout.Controls.Add(clbCongViec, 0, 4);
            rootLayout.Controls.Add(lblMoTaCongViec, 0, 5);
            rootLayout.Controls.Add(lblHint, 0, 6);
            rootLayout.Controls.Add(actionPanel, 0, 7);

            Controls.Add(rootLayout);

            AcceptButton = btnLuu;
            CancelButton = btnHuy;
        }

        private void LoadAssignmentData()
        {
            TaskAssignmentEditItem item = scheduleService.GetTaskAssignmentEditItem(maNhanVien, ngayLam);

            lblNhanVienValue.Text = item.TenNhanVien;
            lblNgayValue.Text = item.NgayLam.ToString("dddd, dd/MM/yyyy", new CultureInfo("vi-VN"));
            lblCaValue.Text = item.CaHienTai;
            lblGioValue.Text = item.GioBatDau.HasValue && item.GioKetThuc.HasValue
                ? string.Format(CultureInfo.InvariantCulture, "{0:hh\\:mm} - {1:hh\\:mm}", item.GioBatDau.Value, item.GioKetThuc.Value)
                : "Khong co khung gio";

            clbCongViec.Items.Clear();

            foreach (WorkTaskOption task in item.AvailableTasks)
            {
                TaskListItem taskItem = new TaskListItem
                {
                    MaCongViec = task.MaCongViec,
                    TenCongViec = task.TenCongViec,
                    MoTa = task.MoTa
                };

                int itemIndex = clbCongViec.Items.Add(taskItem);
                if (item.SelectedTaskIds.Contains(task.MaCongViec))
                {
                    clbCongViec.SetItemChecked(itemIndex, true);
                }
            }

            bool isRestDay = !item.GioBatDau.HasValue || !item.GioKetThuc.HasValue || string.Equals(item.CaHienTai, "Nghi", StringComparison.OrdinalIgnoreCase);
            if (isRestDay)
            {
                clbCongViec.Enabled = false;
                btnLuu.Enabled = false;
                lblHint.Text = "Ngay nay dang de nghi. Hay chinh sua lich lam truoc, sau do quay lai de phan cong cong viec.";
                lblHint.ForeColor = Color.FromArgb(220, 38, 38);
            }
            else
            {
                lblHint.Text = "Moi cong viec duoc luu theo ca hien tai. Neu ban sua tang ca o man hinh truoc, khung gio tren lich se doi theo ngay sau khi luu.";
                lblHint.ForeColor = Color.FromArgb(107, 114, 128);
            }

            if (clbCongViec.Items.Count > 0)
            {
                clbCongViec.SelectedIndex = 0;
                UpdateTaskDescription();
            }
        }

        private void clbCongViec_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTaskDescription();
        }

        private void UpdateTaskDescription()
        {
            TaskListItem selectedTask = clbCongViec.SelectedItem as TaskListItem;
            lblMoTaCongViec.Text = selectedTask == null || string.IsNullOrWhiteSpace(selectedTask.MoTa)
                ? "Chon mot cong viec de xem mo ta ngan."
                : selectedTask.MoTa;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            TaskAssignmentSaveModel model = new TaskAssignmentSaveModel
            {
                MaNhanVien = maNhanVien,
                NgayLam = ngayLam,
                TaskIds = clbCongViec.CheckedItems
                    .OfType<TaskListItem>()
                    .Select(x => x.MaCongViec)
                    .ToList()
            };

            try
            {
                scheduleService.SaveTaskAssignments(model);
                MessageBox.Show("Cap nhat cong viec thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Khong the luu cong viec", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private static TableLayoutPanel CreateTableLayout()
        {
            TableLayoutPanel table = new TableLayoutPanel();
            table.ColumnCount = 2;
            table.RowCount = 0;
            table.Dock = DockStyle.Top;
            table.AutoSize = true;
            table.Margin = new Padding(0, 18, 0, 0);
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            return table;
        }

        private static Label CreateValueLabel()
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label.ForeColor = Color.FromArgb(17, 24, 39);
            label.Margin = new Padding(0, 10, 0, 0);
            return label;
        }

        private static void AddField(TableLayoutPanel table, int rowIndex, string labelText, System.Windows.Forms.Control inputControl)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(55, 65, 81);
            label.Margin = new Padding(0, 10, 12, 0);
            label.Text = labelText;

            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.Controls.Add(label, 0, rowIndex);
            table.Controls.Add(inputControl, 1, rowIndex);
        }
    }
}
