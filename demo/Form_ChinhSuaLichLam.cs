using demo.BLL.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace demo
{
    public class Form_ChinhSuaLichLam : Form
    {
        private sealed class ScheduleModeOption
        {
            public string Id { get; set; }
            public string Ten { get; set; }
        }

        private readonly ScheduleService scheduleService = new ScheduleService();
        private readonly int maNhanVien;
        private readonly DateTime ngayLam;

        private ComboBox cboCheDo;
        private ComboBox cboCaLam;
        private DateTimePicker dtpGioBatDau;
        private DateTimePicker dtpGioKetThuc;
        private TextBox txtLyDo;
        private TextBox txtGhiChu;
        private Label lblNhanVienValue;
        private Label lblNgayValue;
        private Label lblHint;
        private Button btnLuu;
        private Button btnHuy;

        private bool isBinding;
        private List<ShiftOption> shiftOptions = new List<ShiftOption>();

        public int SavedScheduleId { get; private set; }

        public Form_ChinhSuaLichLam(int maNhanVien, DateTime ngayLam)
        {
            this.maNhanVien = maNhanVien;
            this.ngayLam = ngayLam.Date;

            InitializeComponent();
            LoadLookupData();
            LoadScheduleData();
        }

        private void InitializeComponent()
        {
            Text = "Chinh sua lich lam";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(680, 500);
            BackColor = Color.White;

            Label lblTitle = new Label();
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitle.Text = "Cap nhat lich lam trong ngay";

            Label lblDescription = new Label();
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblDescription.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescription.Margin = new Padding(0, 6, 0, 0);
            lblDescription.Text = "Ban co the chuyen sang ngay nghi, giu theo ca co san hoac dieu chinh tang ca cho nhan vien.";

            TableLayoutPanel infoTable = CreateTableLayout();
            lblNhanVienValue = CreateValueLabel();
            lblNgayValue = CreateValueLabel();
            AddField(infoTable, 0, "Nhan vien", lblNhanVienValue);
            AddField(infoTable, 1, "Ngay lam", lblNgayValue);

            TableLayoutPanel formTable = CreateTableLayout();

            cboCheDo = CreateComboBox();
            cboCheDo.SelectedIndexChanged += cboCheDo_SelectedIndexChanged;

            cboCaLam = CreateComboBox();
            cboCaLam.SelectedIndexChanged += cboCaLam_SelectedIndexChanged;

            dtpGioBatDau = CreateTimePicker();
            dtpGioKetThuc = CreateTimePicker();

            txtLyDo = CreateMultilineTextBox(66);
            txtGhiChu = CreateMultilineTextBox(90);

            AddField(formTable, 0, "Che do lich", cboCheDo);
            AddField(formTable, 1, "Ca lam", cboCaLam);
            AddField(formTable, 2, "Gio bat dau", dtpGioBatDau);
            AddField(formTable, 3, "Gio ket thuc", dtpGioKetThuc);
            AddField(formTable, 4, "Ly do dieu chinh", txtLyDo);
            AddField(formTable, 5, "Ghi chu", txtGhiChu);

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
            btnLuu.BackColor = Color.FromArgb(34, 197, 94);
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Size = new Size(130, 38);
            btnLuu.Text = "Luu thay doi";
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
            rootLayout.RowCount = 6;
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Padding = new Padding(24);
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            rootLayout.Controls.Add(lblTitle, 0, 0);
            rootLayout.Controls.Add(lblDescription, 0, 1);
            rootLayout.Controls.Add(infoTable, 0, 2);
            rootLayout.Controls.Add(formTable, 0, 3);
            rootLayout.Controls.Add(lblHint, 0, 4);
            rootLayout.Controls.Add(actionPanel, 0, 5);

            Controls.Add(rootLayout);

            AcceptButton = btnLuu;
            CancelButton = btnHuy;
        }

        private void LoadLookupData()
        {
            isBinding = true;

            cboCheDo.DisplayMember = "Ten";
            cboCheDo.ValueMember = "Id";
            cboCheDo.DataSource = new List<ScheduleModeOption>
            {
                new ScheduleModeOption
                {
                    Id = ScheduleService.CheDoTheoCa,
                    Ten = "Theo ca mac dinh"
                },
                new ScheduleModeOption
                {
                    Id = ScheduleService.CheDoTangCa,
                    Ten = "Tang ca trong ngay"
                },
                new ScheduleModeOption
                {
                    Id = ScheduleService.CheDoNghi,
                    Ten = "Cho nghi ngay nay"
                }
            };

            shiftOptions = scheduleService.GetShiftOptions();
            cboCaLam.DisplayMember = "HienThi";
            cboCaLam.ValueMember = "MaCa";
            cboCaLam.DataSource = shiftOptions;

            isBinding = false;
        }

        private void LoadScheduleData()
        {
            ScheduleEditItem item = scheduleService.GetScheduleEditItem(maNhanVien, ngayLam);

            isBinding = true;

            lblNhanVienValue.Text = item.TenNhanVien;
            lblNgayValue.Text = item.NgayLam.ToString("dddd, dd/MM/yyyy", new CultureInfo("vi-VN"));

            cboCheDo.SelectedValue = item.CheDoLich;
            SetSelectedShift(item.MaCa);

            SetTimePickerValue(dtpGioBatDau, item.GioBatDau);
            SetTimePickerValue(dtpGioKetThuc, item.GioKetThuc);

            txtLyDo.Text = item.LyDoDieuChinh ?? string.Empty;
            txtGhiChu.Text = item.GhiChu ?? string.Empty;

            isBinding = false;
            UpdateFormState(syncTimeFromShift: !item.GioBatDau.HasValue || !item.GioKetThuc.HasValue);
        }

        private void cboCheDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBinding)
            {
                return;
            }

            UpdateFormState(syncTimeFromShift: true);
        }

        private void cboCaLam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBinding)
            {
                return;
            }

            UpdateFormState(syncTimeFromShift: true);
        }

        private void UpdateFormState(bool syncTimeFromShift)
        {
            string selectedMode = GetSelectedMode();
            bool isRestMode = selectedMode == ScheduleService.CheDoNghi;
            bool isOvertimeMode = selectedMode == ScheduleService.CheDoTangCa;
            ShiftOption selectedShift = GetSelectedShift();

            cboCaLam.Enabled = !isRestMode;
            dtpGioBatDau.Enabled = !isRestMode && isOvertimeMode;
            dtpGioKetThuc.Enabled = !isRestMode && isOvertimeMode;
            txtLyDo.Enabled = true;
            txtGhiChu.Enabled = true;

            if (!isRestMode && selectedShift != null && (syncTimeFromShift || selectedMode == ScheduleService.CheDoTheoCa))
            {
                SetTimePickerValue(dtpGioBatDau, selectedShift.GioBatDau);
                SetTimePickerValue(dtpGioKetThuc, selectedShift.GioKetThuc);
            }

            if (isRestMode)
            {
                lblHint.Text = "Ngay nghi se xoa phan cong cong viec cua ngay nay. Neu muon giao viec lai, hay doi che do ve theo ca hoac tang ca.";
                lblHint.ForeColor = Color.FromArgb(107, 114, 128);
                return;
            }

            if (isOvertimeMode)
            {
                lblHint.Text = "Ban co the doi gio bat dau hoac gio ket thuc. Neu ngay nay dang hien trong bang tuan, du lieu se duoc refresh ngay sau khi luu.";
                lblHint.ForeColor = Color.FromArgb(107, 114, 128);
                return;
            }

            lblHint.Text = "Che do theo ca se lay gio lam tu ca dang chon. Neu doi sang tang ca, ban co the sua khung gio theo thuc te.";
            lblHint.ForeColor = Color.FromArgb(107, 114, 128);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string selectedMode = GetSelectedMode();
            ShiftOption selectedShift = GetSelectedShift();

            if (selectedMode != ScheduleService.CheDoNghi && selectedShift == null)
            {
                MessageBox.Show("Vui long chon ca lam truoc khi luu.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan? gioBatDau = null;
            TimeSpan? gioKetThuc = null;

            if (selectedMode != ScheduleService.CheDoNghi && selectedShift != null)
            {
                gioBatDau = selectedMode == ScheduleService.CheDoTheoCa
                    ? selectedShift.GioBatDau
                    : (TimeSpan?)dtpGioBatDau.Value.TimeOfDay;
                gioKetThuc = selectedMode == ScheduleService.CheDoTheoCa
                    ? selectedShift.GioKetThuc
                    : (TimeSpan?)dtpGioKetThuc.Value.TimeOfDay;
            }

            ScheduleEditSaveModel model = new ScheduleEditSaveModel
            {
                MaNhanVien = maNhanVien,
                NgayLam = ngayLam,
                CheDoLich = selectedMode,
                MaCa = selectedMode == ScheduleService.CheDoNghi || selectedShift == null
                    ? null
                    : selectedShift.MaCa,
                GioBatDau = gioBatDau,
                GioKetThuc = gioKetThuc,
                LyDoDieuChinh = txtLyDo.Text,
                GhiChu = txtGhiChu.Text
            };

            try
            {
                SavedScheduleId = scheduleService.SaveScheduleEdit(model);
                MessageBox.Show("Cap nhat lich lam thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Khong the luu lich lam", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private string GetSelectedMode()
        {
            object selectedValue = cboCheDo.SelectedValue;
            return selectedValue == null
                ? ScheduleService.CheDoTheoCa
                : selectedValue.ToString();
        }

        private ShiftOption GetSelectedShift()
        {
            return cboCaLam.SelectedItem as ShiftOption;
        }

        private void SetSelectedShift(int? maCa)
        {
            if (!maCa.HasValue)
            {
                if (cboCaLam.Items.Count > 0)
                {
                    cboCaLam.SelectedIndex = 0;
                }

                return;
            }

            ShiftOption selectedShift = shiftOptions.FirstOrDefault(x => x.MaCa == maCa.Value);
            if (selectedShift != null)
            {
                cboCaLam.SelectedItem = selectedShift;
            }
        }

        private static void SetTimePickerValue(DateTimePicker picker, TimeSpan? value)
        {
            TimeSpan safeValue = value ?? TimeSpan.Zero;
            picker.Value = DateTime.Today.Date.Add(safeValue);
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

        private static ComboBox CreateComboBox()
        {
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            comboBox.Dock = DockStyle.Fill;
            comboBox.Margin = new Padding(0, 6, 0, 6);
            return comboBox;
        }

        private static DateTimePicker CreateTimePicker()
        {
            DateTimePicker picker = new DateTimePicker();
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "HH:mm";
            picker.ShowUpDown = true;
            picker.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            picker.Dock = DockStyle.Left;
            picker.Width = 160;
            picker.Margin = new Padding(0, 6, 0, 6);
            return picker;
        }

        private static TextBox CreateMultilineTextBox(int height)
        {
            TextBox textBox = new TextBox();
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Height = height;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Margin = new Padding(0, 6, 0, 6);
            return textBox;
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
