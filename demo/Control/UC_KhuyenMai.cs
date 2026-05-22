using demo.BLL.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace demo.Control
{
    public class UC_KhuyenMai : UserControl
    {
        private readonly PromotionService promotionService = new PromotionService();

        private TextBox txtSearch;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvKhuyenMai;
        private Label lblTongSo;
        private Label lblDangDienRa;
        private Label lblSapDienRa;
        private Label lblDaKetThuc;

        public UC_KhuyenMai()
        {
            InitializeComponent();
            LoadDanhSachKhuyenMai();
        }

        private void InitializeComponent()
        {
            BackColor = Color.FromArgb(245, 247, 250);
            Size = new Size(1180, 680);

            Label lblTitle = new Label();
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitle.Text = "Quản lý mã khuyến mãi";

            Label lblDescription = new Label();
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblDescription.ForeColor = Color.FromArgb(107, 114, 128);
            lblDescription.Margin = new Padding(0, 6, 0, 0);
            lblDescription.Text = "Lưu trữ mã giảm giá để nhân viên áp dụng trong màn hình POS khi thanh toán.";

            TableLayoutPanel statsTable = new TableLayoutPanel();
            statsTable.ColumnCount = 4;
            statsTable.Dock = DockStyle.Fill;
            statsTable.Height = 100;
            statsTable.Margin = new Padding(0, 18, 0, 0);
            statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            lblTongSo = CreateStatValueLabel();
            lblDangDienRa = CreateStatValueLabel();
            lblSapDienRa = CreateStatValueLabel();
            lblDaKetThuc = CreateStatValueLabel();

            statsTable.Controls.Add(CreateStatCard("Tổng mã giảm", lblTongSo, Color.FromArgb(37, 99, 235)), 0, 0);
            statsTable.Controls.Add(CreateStatCard("Đang hiệu lực", lblDangDienRa, Color.FromArgb(34, 197, 94)), 1, 0);
            statsTable.Controls.Add(CreateStatCard("Sắp hiệu lực", lblSapDienRa, Color.FromArgb(245, 158, 11)), 2, 0);
            statsTable.Controls.Add(CreateStatCard("Hết hiệu lực", lblDaKetThuc, Color.FromArgb(239, 68, 68)), 3, 0);

            Panel toolbar = new Panel();
            toolbar.Dock = DockStyle.Fill;
            toolbar.Height = 60;
            toolbar.Margin = new Padding(0, 18, 0, 0);
            toolbar.BackColor = Color.White;
            toolbar.Padding = new Padding(16, 12, 16, 12);

            Label lblSearch = new Label();
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(55, 65, 81);
            lblSearch.Location = new Point(16, 18);
            lblSearch.Text = "Tìm mã";

            txtSearch = new TextBox();
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtSearch.Location = new Point(110, 14);
            txtSearch.Size = new Size(260, 30);
            txtSearch.TextChanged += txtSearch_TextChanged;

            FlowLayoutPanel actionPanel = new FlowLayoutPanel();
            actionPanel.Dock = DockStyle.Right;
            actionPanel.FlowDirection = FlowDirection.LeftToRight;
            actionPanel.WrapContents = false;
            actionPanel.AutoSize = true;

            btnThem = CreateActionButton("Thêm mới", Color.FromArgb(34, 197, 94), Color.White);
            btnSua = CreateActionButton("Chỉnh sửa", Color.White, Color.FromArgb(55, 65, 81));
            btnXoa = CreateActionButton("Xóa", Color.White, Color.FromArgb(220, 38, 38));
            btnLamMoi = CreateActionButton("Làm mới", Color.White, Color.FromArgb(37, 99, 235));

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            actionPanel.Controls.Add(btnThem);
            actionPanel.Controls.Add(btnSua);
            actionPanel.Controls.Add(btnXoa);
            actionPanel.Controls.Add(btnLamMoi);

            toolbar.Controls.Add(actionPanel);
            toolbar.Controls.Add(txtSearch);
            toolbar.Controls.Add(lblSearch);

            dgvKhuyenMai = new DataGridView();
            dgvKhuyenMai.Dock = DockStyle.Fill;
            dgvKhuyenMai.BackgroundColor = Color.White;
            dgvKhuyenMai.BorderStyle = BorderStyle.None;
            dgvKhuyenMai.AllowUserToAddRows = false;
            dgvKhuyenMai.AllowUserToDeleteRows = false;
            dgvKhuyenMai.AllowUserToResizeRows = false;
            dgvKhuyenMai.ReadOnly = true;
            dgvKhuyenMai.AutoGenerateColumns = false;
            dgvKhuyenMai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhuyenMai.MultiSelect = false;
            dgvKhuyenMai.RowHeadersVisible = false;
            dgvKhuyenMai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhuyenMai.ColumnHeadersHeight = 40;
            dgvKhuyenMai.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvKhuyenMai.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dgvKhuyenMai.SelectionChanged += dgvKhuyenMai_SelectionChanged;
            dgvKhuyenMai.CellDoubleClick += dgvKhuyenMai_CellDoubleClick;

            dgvKhuyenMai.Columns.Add(CreateTextColumn("MaKhuyenMai", "ID", 80));
            dgvKhuyenMai.Columns.Add(CreateTextColumn("MaKhuyenMaiCode", "Mã khuyến mãi", 220));
            dgvKhuyenMai.Columns.Add(CreateTextColumn("PhanTramGiam", "% giảm", 90));
            dgvKhuyenMai.Columns.Add(CreateTextColumn("ThoiGianApDung", "Thời gian áp dụng", 220));

            Panel gridPanel = new Panel();
            gridPanel.Dock = DockStyle.Fill;
            gridPanel.BackColor = Color.White;
            gridPanel.Padding = new Padding(1);
            gridPanel.Margin = new Padding(0, 16, 0, 0);
            gridPanel.Controls.Add(dgvKhuyenMai);

            TableLayoutPanel rootLayout = new TableLayoutPanel();
            rootLayout.ColumnCount = 1;
            rootLayout.RowCount = 4;
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Padding = new Padding(24, 20, 24, 24);
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Panel titlePanel = new Panel();
            titlePanel.Dock = DockStyle.Fill;
            titlePanel.Height = 66;
            titlePanel.Controls.Add(lblDescription);
            titlePanel.Controls.Add(lblTitle);
            lblTitle.Location = new Point(0, 0);
            lblDescription.Location = new Point(2, 40);

            rootLayout.Controls.Add(titlePanel, 0, 0);
            rootLayout.Controls.Add(statsTable, 0, 1);
            rootLayout.Controls.Add(toolbar, 0, 2);
            rootLayout.Controls.Add(gridPanel, 0, 3);

            Controls.Add(rootLayout);
            UpdateActionButtons();
        }

        private void LoadDanhSachKhuyenMai(int? selectedPromotionId = null)
        {
            int? promotionIdToRestore = selectedPromotionId ?? GetSelectedPromotionId();

            try
            {
                List<PromotionListItem> list = promotionService.GetAll(txtSearch.Text);
                dgvKhuyenMai.DataSource = list;
                UpdateStatistics(list);
                RestoreSelection(promotionIdToRestore);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được danh sách mã khuyến mãi.\n\n" + ex.Message,
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                UpdateActionButtons();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachKhuyenMai();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            OpenPromotionEditor(null);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            OpenSelectedPromotionForEdit();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int? selectedId = GetSelectedPromotionId();
            if (!selectedId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn mã khuyến mãi cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa mã khuyến mãi đã chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                promotionService.Delete(selectedId.Value);
                MessageBox.Show("Đã xóa mã khuyến mãi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachKhuyenMai();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Không thể xóa mã khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadDanhSachKhuyenMai();
        }

        private void dgvKhuyenMai_SelectionChanged(object sender, EventArgs e)
        {
            UpdateActionButtons();
        }

        private void dgvKhuyenMai_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OpenSelectedPromotionForEdit();
            }
        }

        private void OpenSelectedPromotionForEdit()
        {
            int? selectedId = GetSelectedPromotionId();
            if (!selectedId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn mã khuyến mãi cần chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenPromotionEditor(selectedId.Value);
        }

        private void OpenPromotionEditor(int? promotionId)
        {
            using (global::demo.Form_KhuyenMai form = promotionId.HasValue
                ? new global::demo.Form_KhuyenMai(promotionId.Value)
                : new global::demo.Form_KhuyenMai())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDanhSachKhuyenMai(form.SavedPromotionId > 0 ? (int?)form.SavedPromotionId : null);
                }
            }
        }

        private int? GetSelectedPromotionId()
        {
            if (dgvKhuyenMai.SelectedRows.Count > 0)
            {
                PromotionListItem selectedItem = dgvKhuyenMai.SelectedRows[0].DataBoundItem as PromotionListItem;
                if (selectedItem != null)
                {
                    return selectedItem.MaKhuyenMai;
                }
            }

            if (dgvKhuyenMai.CurrentRow != null)
            {
                PromotionListItem currentItem = dgvKhuyenMai.CurrentRow.DataBoundItem as PromotionListItem;
                if (currentItem != null)
                {
                    return currentItem.MaKhuyenMai;
                }
            }

            return null;
        }

        private void RestoreSelection(int? selectedPromotionId)
        {
            dgvKhuyenMai.ClearSelection();

            if (!selectedPromotionId.HasValue)
            {
                return;
            }

            foreach (DataGridViewRow row in dgvKhuyenMai.Rows)
            {
                PromotionListItem item = row.DataBoundItem as PromotionListItem;
                if (item != null && item.MaKhuyenMai == selectedPromotionId.Value)
                {
                    row.Selected = true;
                    if (row.Cells.Count > 0)
                    {
                        dgvKhuyenMai.CurrentCell = row.Cells[0];
                    }
                    break;
                }
            }
        }

        private void UpdateActionButtons()
        {
            bool hasSelection = GetSelectedPromotionId().HasValue;
            btnSua.Enabled = hasSelection;
            btnXoa.Enabled = hasSelection;
        }

        private void UpdateStatistics(List<PromotionListItem> list)
        {
            lblTongSo.Text = list.Count.ToString();
            lblDangDienRa.Text = list.Count(x => x.TrangThai == PromotionService.TrangThaiDangHieuLuc).ToString();
            lblSapDienRa.Text = list.Count(x => x.TrangThai == PromotionService.TrangThaiSapHieuLuc).ToString();
            lblDaKetThuc.Text = list.Count(x => x.TrangThai == PromotionService.TrangThaiHetHieuLuc).ToString();
        }

        private static Label CreateStatValueLabel()
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(17, 24, 39);
            label.Text = "0";
            return label;
        }

        private static Panel CreateStatCard(string title, Label valueLabel, Color accentColor)
        {
            Panel card = new Panel();
            card.Dock = DockStyle.Fill;
            card.BackColor = Color.White;
            card.Margin = new Padding(0, 0, 14, 0);
            card.Padding = new Padding(18, 16, 18, 16);

            Panel accent = new Panel();
            accent.Dock = DockStyle.Left;
            accent.Width = 5;
            accent.BackColor = accentColor;

            Label titleLabel = new Label();
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            titleLabel.ForeColor = Color.FromArgb(107, 114, 128);
            titleLabel.Text = title;
            titleLabel.Location = new Point(22, 18);

            valueLabel.Location = new Point(22, 42);

            card.Controls.Add(accent);
            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);
            return card;
        }

        private static Button CreateActionButton(string text, Color backColor, Color foreColor)
        {
            Button button = new Button();
            button.BackColor = backColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.Silver;
            button.FlatAppearance.BorderSize = backColor == Color.White ? 1 : 0;
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            button.ForeColor = foreColor;
            button.Margin = new Padding(8, 0, 0, 0);
            button.Size = new Size(100, 34);
            button.Text = text;
            button.UseVisualStyleBackColor = false;
            return button;
        }

        private static DataGridViewTextBoxColumn CreateTextColumn(string propertyName, string headerText, int minimumWidth)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = propertyName;
            column.HeaderText = headerText;
            column.MinimumWidth = minimumWidth;
            return column;
        }
    }
}
