using demo.BLL.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace demo
{
    public class Form_NhanVien : Form
    {
        private readonly EmployeeService employeeService = new EmployeeService();
        private readonly int maNhanVien;
        private readonly bool isEditMode;

        private TextBox txtTenNhanVien;
        private TextBox txtSoDienThoai;
        private TextBox txtEmail;
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private TextBox txtXacNhanMatKhau;
        private ComboBox cboVaiTro;
        private Label lblPasswordHint;
        private Button btnLuu;
        private Button btnHuy;

        public Form_NhanVien()
            : this(0)
        {
        }

        public Form_NhanVien(int maNhanVien)
        {
            this.maNhanVien = maNhanVien;
            isEditMode = maNhanVien > 0;

            InitializeComponent();
            LoadLookupData();

            if (isEditMode)
            {
                LoadEmployeeData();
            }
        }

        private void InitializeComponent()
        {
            Text = isEditMode ? "Sua nhan vien" : "Them nhan vien";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(620, 470);
            BackColor = Color.White;

            Label lblTitle = new Label();
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitle.Text = isEditMode ? "Cap nhat nhan vien" : "Them nhan vien moi";

            Label lblDescription = new Label();
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblDescription.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescription.Margin = new Padding(0, 6, 0, 0);
            lblDescription.Text = "Thong tin nhan vien, email va tai khoan dang nhap duoc luu cung luc. Ca lam se duoc dieu chinh o man hinh lich lam.";

            TableLayoutPanel formTable = new TableLayoutPanel();
            formTable.ColumnCount = 2;
            formTable.RowCount = 8;
            formTable.Dock = DockStyle.Top;
            formTable.AutoSize = true;
            formTable.Margin = new Padding(0, 18, 0, 0);
            formTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            formTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            txtTenNhanVien = CreateTextBox();
            txtSoDienThoai = CreateTextBox();
            txtEmail = CreateTextBox();
            txtTenDangNhap = CreateTextBox();
            txtMatKhau = CreateTextBox();
            txtXacNhanMatKhau = CreateTextBox();
            txtMatKhau.PasswordChar = '*';
            txtXacNhanMatKhau.PasswordChar = '*';

            cboVaiTro = CreateComboBox();

            AddField(formTable, 0, "Ten nhan vien", txtTenNhanVien);
            AddField(formTable, 1, "So dien thoai", txtSoDienThoai);
            AddField(formTable, 2, "Email", txtEmail);
            AddField(formTable, 3, "Vai tro", cboVaiTro);
            AddField(formTable, 4, "Ten dang nhap", txtTenDangNhap);
            AddField(formTable, 5, "Mat khau", txtMatKhau);
            AddField(formTable, 6, "Xac nhan mat khau", txtXacNhanMatKhau);

            lblPasswordHint = new Label();
            lblPasswordHint.AutoSize = true;
            lblPasswordHint.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblPasswordHint.ForeColor = Color.FromArgb(107, 114, 128);
            lblPasswordHint.Margin = new Padding(0, 6, 0, 0);
            lblPasswordHint.Text = isEditMode
                ? "De trong mat khau neu ban khong muon doi."
                : "Thong tin ten dang nhap va mat khau se tao tai khoan cho nhan vien moi.";

            formTable.Controls.Add(new Label(), 0, 7);
            formTable.Controls.Add(lblPasswordHint, 1, 7);

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
            btnLuu.Size = new Size(120, 38);
            btnLuu.Text = isEditMode ? "Cap nhat" : "Them moi";
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
            rootLayout.RowCount = 4;
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Padding = new Padding(24);
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            rootLayout.Controls.Add(lblTitle, 0, 0);
            rootLayout.Controls.Add(lblDescription, 0, 1);
            rootLayout.Controls.Add(formTable, 0, 2);
            rootLayout.Controls.Add(actionPanel, 0, 3);

            Controls.Add(rootLayout);

            AcceptButton = btnLuu;
            CancelButton = btnHuy;
        }

        private void LoadLookupData()
        {
            BindComboBox(cboVaiTro, employeeService.GetRoleOptions());
        }

        private void LoadEmployeeData()
        {
            EmployeeEditItem employee = employeeService.GetById(maNhanVien);

            txtTenNhanVien.Text = employee.TenNhanVien;
            txtSoDienThoai.Text = employee.SoDienThoai;
            txtEmail.Text = employee.Email;
            txtTenDangNhap.Text = employee.TenDangNhap;
            SetSelectedValue(cboVaiTro, employee.MaVaiTro);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool updatePassword = !string.IsNullOrWhiteSpace(txtMatKhau.Text) || !string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text);

            if (!ValidatePassword(updatePassword))
            {
                return;
            }

            EmployeeSaveModel model = new EmployeeSaveModel
            {
                MaNhanVien = maNhanVien,
                TenNhanVien = txtTenNhanVien.Text,
                SoDienThoai = txtSoDienThoai.Text,
                Email = txtEmail.Text,
                TenDangNhap = txtTenDangNhap.Text,
                MatKhau = txtMatKhau.Text,
                MaVaiTro = GetSelectedValue(cboVaiTro),
                MaCa = null,
                CapNhatCaLam = false
            };

            try
            {
                if (isEditMode)
                {
                    employeeService.Update(model, updatePassword);
                    MessageBox.Show("Cap nhat nhan vien thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    employeeService.Add(model);
                    MessageBox.Show("Them nhan vien va tao tai khoan thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Khong the luu nhan vien", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidatePassword(bool updatePassword)
        {
            if (!isEditMode || updatePassword)
            {
                if (string.IsNullOrWhiteSpace(txtMatKhau.Text) || string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text))
                {
                    MessageBox.Show("Vui long nhap day du mat khau va xac nhan mat khau.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!string.Equals(txtMatKhau.Text, txtXacNhanMatKhau.Text, StringComparison.Ordinal))
                {
                    MessageBox.Show("Mat khau xac nhan khong khop.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
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

        private static ComboBox CreateComboBox()
        {
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            comboBox.Dock = DockStyle.Fill;
            comboBox.Margin = new Padding(0, 6, 0, 6);
            return comboBox;
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

        private static void BindComboBox(ComboBox comboBox, List<EmployeeLookupItem> data)
        {
            comboBox.DisplayMember = "Ten";
            comboBox.ValueMember = "Id";
            comboBox.DataSource = data;
        }

        private static void SetSelectedValue(ComboBox comboBox, int? value)
        {
            comboBox.SelectedValue = value;

            if (comboBox.SelectedIndex < 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private static int? GetSelectedValue(ComboBox comboBox)
        {
            object selectedValue = comboBox.SelectedValue;

            if (selectedValue == null)
            {
                return null;
            }

            int parsedValue;
            if (int.TryParse(selectedValue.ToString(), out parsedValue))
            {
                return parsedValue;
            }

            return null;
        }
    }
}
