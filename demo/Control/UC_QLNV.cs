using demo.BLL.Service;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace demo.Control
{
    public partial class UC_QLNV : UserControl
    {
        private readonly EmployeeService employeeService = new EmployeeService();

        private Label lblHeaderSubtitle;
        private Label lblSummaryHint;

        public UC_QLNV()
        {
            InitializeComponent();
            ConfigureView();
        }

        private void ConfigureView()
        {
            BackColor = Color.FromArgb(245, 247, 250);

            lblHeaderSubtitle = new Label();
            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblHeaderSubtitle.MaximumSize = new Size(560, 0);
            lblHeaderSubtitle.Location = new Point(28, 66);
            lblHeaderSubtitle.Text = "Quan ly thong tin nhan vien, email va tai khoan dang nhap tren cung mot man hinh.";
            panelHeader.Controls.Add(lblHeaderSubtitle);

            panelHeader.Height = 104;
            panelHeader.Padding = new Padding(24, 18, 24, 12);

            label1.Text = "Quan ly nhan vien";
            label1.Location = new Point(24, 16);

            panelStats.Height = 118;
            panelStats.Padding = new Padding(24, 0, 24, 0);
            panelStats.SetColumnSpan(panel1, 4);
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            panel1.Margin = new Padding(3, 3, 3, 12);
            panel1.Padding = new Padding(18, 14, 18, 14);
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;

            label2.Text = "Tong nhan vien";
            label2.Location = new Point(17, 16);
            label3.Location = new Point(17, 42);
            label3.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            pictureBox1.Location = new Point(panel1.Width - 58, 18);
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblSummaryHint = new Label();
            lblSummaryHint.AutoSize = true;
            lblSummaryHint.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblSummaryHint.ForeColor = Color.FromArgb(107, 114, 128);
            lblSummaryHint.MaximumSize = new Size(420, 0);
            lblSummaryHint.Location = new Point(19, 88);
            panel1.Controls.Add(lblSummaryHint);

            panelContent.Padding = new Padding(24, 4, 24, 24);

            button2.Text = "Them nhan vien";
            button2.BackColor = Color.FromArgb(34, 197, 94);
            button2.ForeColor = Color.White;
            button2.FlatAppearance.BorderSize = 0;
            button2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        }

        private void OpenChild(UserControl uc)
        {
            var oldControls = panelContent.Controls.OfType<System.Windows.Forms.Control>().ToList();

            panelContent.SuspendLayout();
            foreach (var control in oldControls)
            {
                panelContent.Controls.Remove(control);
                control.Dispose();
            }

            uc.Dock = DockStyle.Fill;
            uc.Margin = Padding.Empty;
            HookChildEvents(uc);
            panelContent.Controls.Add(uc);
            uc.BringToFront();
            panelContent.ResumeLayout();
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Form_NhanVien form = new Form_NhanVien())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshOverviewStats();
                    btnDanhSach_Click(null, null);
                }
            }
        }

        private void UpdateTabButtonState(Button activeButton)
        {
            foreach (Button button in new[] { btnDanhSach, btnLichLam })
            {
                bool isActive = button == activeButton;

                button.BackColor = isActive
                    ? Color.FromArgb(76, 175, 80)
                    : Color.White;
                button.ForeColor = isActive
                    ? Color.White
                    : Color.FromArgb(55, 65, 81);
                button.FlatAppearance.BorderColor = isActive
                    ? Color.FromArgb(76, 175, 80)
                    : Color.Silver;
            }
        }

        private void UC_QLNV_Load(object sender, EventArgs e)
        {
            RefreshOverviewStats();
            btnDanhSach_Click(null, null);
        }

        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            UC_NhanVienDanhSach uc = new UC_NhanVienDanhSach();
            UpdateTabButtonState(btnDanhSach);
            RefreshOverviewStats();
            OpenChild(uc);
        }

        private void btnLichLam_Click(object sender, EventArgs e)
        {
            UC_LichLamNhanVien uc = new UC_LichLamNhanVien();
            UpdateTabButtonState(btnLichLam);
            RefreshOverviewStats();
            OpenChild(uc);
        }

        private void HookChildEvents(UserControl uc)
        {
            UC_NhanVienDanhSach danhSachControl = uc as UC_NhanVienDanhSach;
            if (danhSachControl != null)
            {
                danhSachControl.DataChanged += ChildControl_DataChanged;
            }

            UC_LichLamNhanVien lichLamControl = uc as UC_LichLamNhanVien;
            if (lichLamControl != null)
            {
                lichLamControl.DataChanged += ChildControl_DataChanged;
            }
        }

        private void ChildControl_DataChanged(object sender, EventArgs e)
        {
            RefreshOverviewStats();
        }

        private void RefreshOverviewStats()
        {
            try
            {
                EmployeeDashboardSummary summary = employeeService.GetDashboardSummary();
                label3.Text = summary.TongNhanVien.ToString();
                lblSummaryHint.Text = string.Format(
                    "{0} tai khoan dang hoat dong • {1} email da cap nhat",
                    summary.SoTaiKhoanDangHoatDong,
                    summary.SoNhanVienCoEmail);
            }
            catch
            {
                label3.Text = "0";
                lblSummaryHint.Text = "Chua tai duoc thong tin nhan vien.";
            }
        }
    }
}
