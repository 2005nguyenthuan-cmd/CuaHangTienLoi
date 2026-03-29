using demo.BLL.Service;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace demo
{
    public class Form_KhuyenMai : Form
    {
        private readonly PromotionService promotionService = new PromotionService();
        private readonly int maKhuyenMai;
        private readonly bool isEditMode;

        private TextBox txtMaKhuyenMai;
        private NumericUpDown nudPhanTramGiam;
        private DateTimePicker dtpNgayBatDau;
        private DateTimePicker dtpNgayKetThuc;
        private Button btnLuu;
        private Button btnHuy;

        public int SavedPromotionId { get; private set; }

        public Form_KhuyenMai()
            : this(0)
        {
        }

        public Form_KhuyenMai(int maKhuyenMai)
        {
            this.maKhuyenMai = maKhuyenMai;
            isEditMode = maKhuyenMai > 0;

            InitializeComponent();

            if (isEditMode)
            {
                LoadPromotionData();
            }
        }

        private void InitializeComponent()
        {
            Text = isEditMode ? "Sua ma khuyen mai" : "Them ma khuyen mai";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(560, 340);
            BackColor = Color.White;

            Label lblTitle = new Label();
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitle.Text = isEditMode ? "Cap nhat ma khuyen mai" : "Tao ma khuyen mai moi";

            Label lblDescription = new Label();
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblDescription.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescription.Margin = new Padding(0, 6, 0, 0);
            lblDescription.Text = "Nhan vien co the nhap ma nay trong POS de giam gia cho toan bo hoa don.";

            TableLayoutPanel formTable = new TableLayoutPanel();
            formTable.ColumnCount = 2;
            formTable.RowCount = 4;
            formTable.Dock = DockStyle.Fill;
            formTable.AutoSize = true;
            formTable.Margin = new Padding(0, 18, 0, 0);
            formTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            formTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            txtMaKhuyenMai = CreateTextBox();
            txtMaKhuyenMai.CharacterCasing = CharacterCasing.Upper;

            nudPhanTramGiam = new NumericUpDown();
            nudPhanTramGiam.Minimum = 1;
            nudPhanTramGiam.Maximum = 100;
            nudPhanTramGiam.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            nudPhanTramGiam.Dock = DockStyle.Left;
            nudPhanTramGiam.Width = 140;
            nudPhanTramGiam.Margin = new Padding(0, 6, 0, 6);

            dtpNgayBatDau = CreateDatePicker();
            dtpNgayKetThuc = CreateDatePicker();

            AddField(formTable, 0, "Ma khuyen mai", txtMaKhuyenMai);
            AddField(formTable, 1, "Phan tram giam", nudPhanTramGiam);
            AddField(formTable, 2, "Ngay bat dau", dtpNgayBatDau);
            AddField(formTable, 3, "Ngay ket thuc", dtpNgayKetThuc);

            Label lblHint = new Label();
            lblHint.AutoSize = true;
            lblHint.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblHint.ForeColor = Color.FromArgb(107, 114, 128);
            lblHint.Margin = new Padding(0, 12, 0, 0);
            lblHint.Text = "Goi y: nhap ma ngan gon de thu ngan de nhap o man hinh thanh toan POS.";

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
            btnLuu.Text = isEditMode ? "Luu thay doi" : "Them ma";
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
            rootLayout.RowCount = 5;
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Padding = new Padding(24);
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            rootLayout.Controls.Add(lblTitle, 0, 0);
            rootLayout.Controls.Add(lblDescription, 0, 1);
            rootLayout.Controls.Add(formTable, 0, 2);
            rootLayout.Controls.Add(lblHint, 0, 3);
            rootLayout.Controls.Add(actionPanel, 0, 4);

            Controls.Add(rootLayout);

            AcceptButton = btnLuu;
            CancelButton = btnHuy;
        }

        private void LoadPromotionData()
        {
            PromotionEditItem promotion = promotionService.GetById(maKhuyenMai);

            txtMaKhuyenMai.Text = promotion.MaKhuyenMaiCode;
            nudPhanTramGiam.Value = promotion.PhanTramGiam;
            dtpNgayBatDau.Value = promotion.NgayBatDau;
            dtpNgayKetThuc.Value = promotion.NgayKetThuc;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            PromotionSaveModel model = new PromotionSaveModel
            {
                MaKhuyenMai = maKhuyenMai,
                MaKhuyenMaiCode = txtMaKhuyenMai.Text,
                PhanTramGiam = (int)nudPhanTramGiam.Value,
                NgayBatDau = dtpNgayBatDau.Value.Date,
                NgayKetThuc = dtpNgayKetThuc.Value.Date
            };

            try
            {
                if (isEditMode)
                {
                    promotionService.Update(model);
                    SavedPromotionId = maKhuyenMai;
                    MessageBox.Show("Cap nhat ma khuyen mai thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SavedPromotionId = promotionService.Add(model);
                    MessageBox.Show("Them ma khuyen mai thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Khong the luu ma khuyen mai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private static TextBox CreateTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            textBox.Dock = DockStyle.Fill;
            textBox.Margin = new Padding(0, 6, 0, 6);
            return textBox;
        }

        private static DateTimePicker CreateDatePicker()
        {
            DateTimePicker picker = new DateTimePicker();
            picker.Format = DateTimePickerFormat.Short;
            picker.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            picker.Dock = DockStyle.Left;
            picker.Width = 180;
            picker.Margin = new Padding(0, 6, 0, 6);
            return picker;
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
