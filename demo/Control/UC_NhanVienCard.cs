using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace demo.Control
{
    public partial class UC_NhanVienCard : UserControl
    {
        private bool isSelected;

        public UC_NhanVienCard()
        {
            InitializeComponent();
            labelNgayTitle.Text = "Ma nhan vien";
            labelCaLamTitle.Text = "Email";
            lblCaLam.AutoSize = false;
            lblCaLam.AutoEllipsis = true;
            lblCaLam.Size = new Size(138, 20);
            lblSDT.AutoSize = false;
            lblSDT.AutoEllipsis = true;
            lblSDT.Size = new Size(138, 20);
            lblTrangThai.AutoSize = false;
            lblTrangThai.AutoEllipsis = true;
            lblTrangThai.Size = new Size(138, 20);
            WireClickEvents(this);
            ApplySelectionState();
        }

        public int EmployeeId { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                ApplySelectionState();
            }
        }

        public void SetData(string ten, string vaiTro, string trangThai, string email, string sdt, string maNhanVienHienThi)
        {
            lblTen.Text = ten;
            lblVaiTro.Text = vaiTro;
            lblTrangThai.Text = trangThai;
            lblCaLam.Text = string.IsNullOrWhiteSpace(email) ? "Chua cap nhat" : email;
            lblSDT.Text = string.IsNullOrWhiteSpace(sdt) ? "Chua cap nhat" : sdt;
            lblNgay.Text = maNhanVienHienThi;

            pictureBox1.Image = SystemIcons.Information.ToBitmap();
            lblVaiTro.ForeColor = BuildTextKey(vaiTro).Contains("quanly")
                ? Color.FromArgb(37, 99, 235)
                : Color.FromArgb(14, 116, 144);
            lblTrangThai.ForeColor = BuildTextKey(trangThai).Contains("danghoatdong")
                ? Color.FromArgb(34, 139, 34)
                : Color.FromArgb(220, 38, 38);
        }

        private void UC_NhanVienCard_Load(object sender, EventArgs e)
        {
        }

        private void WireClickEvents(System.Windows.Forms.Control rootControl)
        {
            foreach (System.Windows.Forms.Control child in rootControl.Controls)
            {
                child.Click += ChildControl_Click;
                WireClickEvents(child);
            }
        }

        private void ChildControl_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void ApplySelectionState()
        {
            panelMain.BackColor = isSelected
                ? Color.FromArgb(239, 246, 255)
                : Color.White;
            panelMain.BorderStyle = BorderStyle.FixedSingle;
            panelMain.Padding = new Padding(0);
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
