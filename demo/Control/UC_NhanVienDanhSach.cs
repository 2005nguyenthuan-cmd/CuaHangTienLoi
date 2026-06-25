using demo.BLL.Service;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace demo.Control
{
    public partial class UC_NhanVienDanhSach : UserControl
    {
        private readonly EmployeeService employeeService = new EmployeeService();
        private int? selectedEmployeeId;

        public event EventHandler DataChanged;

        public UC_NhanVienDanhSach()
        {
            InitializeComponent();
            txtSearch.TextChanged += txtSearch_TextChanged;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
        }

        public void SetupControls()
        {
            txtSearch.Visible = true;
            btnThem.Visible = true;
            btnSua.Visible = true;
            btnXoa.Visible = true;
            UpdateActionButtons();
        }

        public void LoadDanhSach(int? employeeIdToSelect = null)
        {
            var oldControls = panelContent.Controls.OfType<System.Windows.Forms.Control>().ToList();

            panelContent.SuspendLayout();
            panelContent.Controls.Clear();
            foreach (var control in oldControls)
            {
                control.Dispose();
            }

            selectedEmployeeId = null;

            try
            {
                var list = employeeService.GetAllForList(txtSearch.Text);

                if (list.Count == 0)
                {
                    Label emptyLabel = new Label();
                    emptyLabel.AutoSize = true;
                    emptyLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                    emptyLabel.ForeColor = Color.FromArgb(107, 114, 128);
                    emptyLabel.Margin = new Padding(0, 8, 0, 0);
                    emptyLabel.Text = "Khong tim thay nhan vien nao.";
                    panelContent.Controls.Add(emptyLabel);
                }
                else
                {
                    foreach (var nv in list)
                    {
                        UC_NhanVienCard card = CreateEmployeeCard(nv);
                        panelContent.Controls.Add(card);

                        if (employeeIdToSelect.HasValue && nv.MaNhanVien == employeeIdToSelect.Value)
                        {
                            SelectEmployeeCard(card);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Khong tai duoc danh sach nhan vien.\n\n" + ex.Message,
                    "Loi ket noi du lieu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                UpdateActionButtons();
                panelContent.ResumeLayout();
            }
        }

        private void UC_NhanVienDanhSach_Load(object sender, EventArgs e)
        {
            SetupControls();
            LoadDanhSach();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (Form_NhanVien form = new Form_NhanVien())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDanhSach();
                    OnDataChanged();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!EnsureEmployeeSelected())
            {
                return;
            }

            using (Form_NhanVien form = new Form_NhanVien(selectedEmployeeId.Value))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDanhSach(selectedEmployeeId);
                    OnDataChanged();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!EnsureEmployeeSelected())
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                "Ban co chac muon xoa nhan vien da chon?",
                "Xac nhan xoa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                employeeService.Delete(selectedEmployeeId.Value);
                MessageBox.Show("Da xoa nhan vien thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                OnDataChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Khong the xoa nhan vien", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private UC_NhanVienCard CreateEmployeeCard(EmployeeListItem nv)
        {
            UC_NhanVienCard card = new UC_NhanVienCard();
            card.EmployeeId = nv.MaNhanVien;
            card.SetData(
                nv.TenNhanVien,
                nv.VaiTro,
                nv.TrangThai,
                nv.Email,
                nv.SoDienThoai,
                nv.MaNhanVienHienThi);
            card.Margin = new Padding(0, 0, 20, 20);
            card.Click += EmployeeCard_Click;
            card.DoubleClick += EmployeeCard_DoubleClick;
            return card;
        }

        private void EmployeeCard_Click(object sender, EventArgs e)
        {
            SelectEmployeeCard(sender as UC_NhanVienCard);
        }

        private void EmployeeCard_DoubleClick(object sender, EventArgs e)
        {
            SelectEmployeeCard(sender as UC_NhanVienCard);
            btnSua_Click(sender, e);
        }

        private void SelectEmployeeCard(UC_NhanVienCard selectedCard)
        {
            if (selectedCard == null)
            {
                return;
            }

            foreach (UC_NhanVienCard card in panelContent.Controls.OfType<UC_NhanVienCard>())
            {
                bool isCurrentCard = card == selectedCard;
                card.IsSelected = isCurrentCard;

                if (isCurrentCard)
                {
                    selectedEmployeeId = card.EmployeeId;
                }
            }

            UpdateActionButtons();
        }

        private bool EnsureEmployeeSelected()
        {
            if (!selectedEmployeeId.HasValue)
            {
                MessageBox.Show("Vui long chon nhan vien truoc khi thao tac.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void UpdateActionButtons()
        {
            bool hasSelection = selectedEmployeeId.HasValue;
            btnSua.Enabled = hasSelection;
            btnXoa.Enabled = hasSelection;
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
