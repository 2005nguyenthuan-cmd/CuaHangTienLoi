using demo.BLL.Service;
using demo.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace demo.Control
{
    public partial class UC_SanPham : UserControl
    {
        private bool isBindingDanhMuc;
        private Label lblSubtitle;
        private readonly ProductService productService = new ProductService();

        public UC_SanPham()
        {
            InitializeComponent();
            ConfigureView();
            LoadDanhMucLoc();
            LoadThongKe();
            LoadSanPham();
        }

        private void ConfigureView()
        {
            BackColor = Color.FromArgb(245, 247, 250);

            lblSubtitle = new Label();
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblSubtitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblSubtitle.Text = "Theo doi so luong san pham, ton kho va danh muc du lieu ngay tren mot man hinh.";
            Controls.Add(lblSubtitle);
            lblSubtitle.BringToFront();

            label1.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(17, 24, 39);
            label1.Text = "Quan ly san pham";

            ConfigureStatCard(panelTongSP, label2, label3, pictureBox1, "Tong san pham", Color.FromArgb(219, 234, 254));
            ConfigureStatCard(panel1, label4, label5, pictureBox2, "Con hang", Color.FromArgb(220, 252, 231));
            ConfigureStatCard(panel2, label7, label6, pictureBox3, "Danh muc", Color.FromArgb(254, 243, 199));
            ConfigureStatCard(panel3, label9, label8, pictureBox4, "Tong ton kho", Color.FromArgb(255, 237, 213));

            panel4.BackColor = Color.White;
            panel4.BorderStyle = BorderStyle.FixedSingle;

            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            txtSearch.Multiline = false;

            cbDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDanhMuc.FlatStyle = FlatStyle.Flat;
            cbDanhMuc.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            btnThemSanPham.BackColor = Color.FromArgb(34, 197, 94);
            btnThemSanPham.FlatStyle = FlatStyle.Flat;
            btnThemSanPham.FlatAppearance.BorderSize = 0;
            btnThemSanPham.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnThemSanPham.ForeColor = Color.White;
            btnThemSanPham.Text = "Them san pham";

            label10.Font = new Font("Segoe UI Semibold", 12.5F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(31, 41, 55);
            label10.Text = "Danh sach san pham";

            dgvSanPham.AllowUserToAddRows = false;
            dgvSanPham.AllowUserToDeleteRows = false;
            dgvSanPham.AllowUserToResizeRows = false;
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSanPham.BackgroundColor = Color.White;
            dgvSanPham.BorderStyle = BorderStyle.None;
            dgvSanPham.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSanPham.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSanPham.EnableHeadersVisualStyles = false;
            dgvSanPham.GridColor = Color.FromArgb(229, 231, 235);
            dgvSanPham.RowHeadersVisible = false;
            dgvSanPham.RowTemplate.Height = 38;
            dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSanPham.MultiSelect = false;
            dgvSanPham.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvSanPham.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(75, 85, 99);
            dgvSanPham.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvSanPham.ColumnHeadersHeight = 42;
            dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSanPham.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvSanPham.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dgvSanPham.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dgvSanPham.Columns[0].FillWeight = 70;
            dgvSanPham.Columns[1].FillWeight = 180;
            dgvSanPham.Columns[2].FillWeight = 120;
            dgvSanPham.Columns[3].FillWeight = 110;
            dgvSanPham.Columns[4].FillWeight = 90;
            dgvSanPham.Columns[5].FillWeight = 70;
            dgvSanPham.Columns[6].FillWeight = 70;

            btnEdit.Text = "Sua";
            btnEdit.UseColumnTextForButtonValue = true;
            btnEdit.FlatStyle = FlatStyle.Flat;

            btnDelete.Text = "Xoa";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.FlatStyle = FlatStyle.Flat;

            LayoutView();
            Resize += delegate { LayoutView(); };
        }

        private void LayoutView()
        {
            int left = 24;
            int top = 24;
            int contentWidth = Math.Max(900, Width - 48);
            int gap = 16;
            int cardWidth = (contentWidth - (gap * 3)) / 4;

            label1.Location = new Point(left, top);
            lblSubtitle.Location = new Point(left, top + 52);

            int cardTop = top + 96;
            LayoutStatCard(panelTongSP, left, cardTop, cardWidth);
            LayoutStatCard(panel1, left + cardWidth + gap, cardTop, cardWidth);
            LayoutStatCard(panel2, left + ((cardWidth + gap) * 2), cardTop, cardWidth);
            LayoutStatCard(panel3, left + ((cardWidth + gap) * 3), cardTop, cardWidth);

            panel4.Location = new Point(left, cardTop + 112);
            panel4.Size = new Size(contentWidth, 76);

            txtSearch.Location = new Point(18, 18);
            txtSearch.Size = new Size(Math.Max(280, panel4.Width - 420), 38);

            cbDanhMuc.Location = new Point(panel4.Width - 370, 18);
            cbDanhMuc.Size = new Size(170, 38);

            btnThemSanPham.Location = new Point(panel4.Width - 182, 18);
            btnThemSanPham.Size = new Size(160, 38);

            label10.Location = new Point(left, panel4.Bottom + 22);

            dgvSanPham.Location = new Point(left, label10.Bottom + 10);
            dgvSanPham.Size = new Size(contentWidth, Math.Max(240, Height - dgvSanPham.Location.Y - 24));
        }

        private static void ConfigureStatCard(Panel panel, Label titleLabel, Label valueLabel, PictureBox icon, string title, Color iconBackColor)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;

            titleLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(75, 85, 99);
            titleLabel.Text = title;
            titleLabel.Location = new Point(16, 14);

            valueLabel.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            valueLabel.ForeColor = Color.FromArgb(17, 24, 39);
            valueLabel.Location = new Point(16, 44);

            icon.BackColor = iconBackColor;
            icon.BorderStyle = BorderStyle.None;
            icon.SizeMode = PictureBoxSizeMode.CenterImage;
            icon.Location = new Point(panel.Width - 56, 18);
            icon.Size = new Size(40, 40);
            icon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        private static void LayoutStatCard(Panel panel, int x, int y, int width)
        {
            panel.Location = new Point(x, y);
            panel.Size = new Size(width, 96);
        }

        private void LoadSanPham()
        {
            string keyword = (txtSearch.Text ?? string.Empty).Trim().ToLowerInvariant();
            int maDanhMuc = GetSelectedDanhMucId();

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                IQueryable<SAN_PHAM> query = db.SAN_PHAM
                    .Include(x => x.DANH_MUC)
                    .OrderBy(x => x.TenSanPham);

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query = query.Where(x => x.TenSanPham != null && x.TenSanPham.ToLower().Contains(keyword));
                }

                if (maDanhMuc != 0)
                {
                    query = query.Where(x => x.MaDanhMuc == maDanhMuc);
                }

                BindSanPham(query.ToList());
            }
        }

        private void BindSanPham(List<SAN_PHAM> products)
        {
            dgvSanPham.Rows.Clear();

            foreach (SAN_PHAM sp in products)
            {
                dgvSanPham.Rows.Add(
                    sp.MaSanPham,
                    sp.TenSanPham,
                    sp.DANH_MUC != null ? sp.DANH_MUC.TenDanhMuc : "Chua phan loai",
                    string.Format(CultureInfo.InvariantCulture, "{0:N0} VND", sp.GiaBan),
                    (sp.SoLuongTon ?? 0).ToString("N0", CultureInfo.InvariantCulture)
                );
            }
        }

        private void LoadThongKe()
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                label3.Text = db.SAN_PHAM.Count().ToString("N0", CultureInfo.InvariantCulture);
                label5.Text = db.SAN_PHAM.Count(x => (x.SoLuongTon ?? 0) > 0).ToString("N0", CultureInfo.InvariantCulture);
                label6.Text = db.DANH_MUC.Count().ToString("N0", CultureInfo.InvariantCulture);
                label8.Text = (db.SAN_PHAM.Sum(x => (int?)x.SoLuongTon) ?? 0).ToString("N0", CultureInfo.InvariantCulture);
            }
        }

        private void LoadDanhMucLoc()
        {
            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                List<DANH_MUC> list = db.DANH_MUC
                    .AsNoTracking()
                    .OrderBy(x => x.TenDanhMuc)
                    .ToList();

                list.Insert(0, new DANH_MUC
                {
                    MaDanhMuc = 0,
                    TenDanhMuc = "Tat ca"
                });

                isBindingDanhMuc = true;
                cbDanhMuc.DataSource = list;
                cbDanhMuc.DisplayMember = "TenDanhMuc";
                cbDanhMuc.ValueMember = "MaDanhMuc";
                cbDanhMuc.SelectedIndex = 0;
                isBindingDanhMuc = false;
            }
        }

        private int GetSelectedDanhMucId()
        {
            object selectedValue = cbDanhMuc.SelectedValue;

            if (selectedValue == null)
            {
                return 0;
            }

            DANH_MUC selectedCategory = selectedValue as DANH_MUC;
            if (selectedCategory != null)
            {
                return selectedCategory.MaDanhMuc;
            }

            int parsedValue;
            return int.TryParse(selectedValue.ToString(), out parsedValue)
                ? parsedValue
                : 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int maSP = Convert.ToInt32(dgvSanPham.Rows[e.RowIndex].Cells[0].Value);

            if (dgvSanPham.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                using (Form_ThemSanPham form = new Form_ThemSanPham(maSP))
                {
                    form.ShowDialog(this);
                }

                LoadThongKe();
                LoadSanPham();
                return;
            }

            if (dgvSanPham.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DialogResult confirm = MessageBox.Show(
                    "Ban co chac muon xoa san pham?",
                    "Xac nhan",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                {
                    return;
                }

                try
                {
                    productService.Delete(maSP);
                    LoadThongKe();
                    LoadSanPham();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(GetInnermostMessage(ex), "Khong the xoa san pham", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            using (Form_ThemSanPham form = new Form_ThemSanPham())
            {
                form.ShowDialog(this);
            }

            LoadThongKe();
            LoadSanPham();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadSanPham();
        }

        private void cbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBindingDanhMuc)
            {
                return;
            }

            LoadSanPham();
        }

        private static string GetInnermostMessage(Exception ex)
        {
            Exception current = ex;

            while (current.InnerException != null)
            {
                current = current.InnerException;
            }

            return current.Message;
        }
    }
}
