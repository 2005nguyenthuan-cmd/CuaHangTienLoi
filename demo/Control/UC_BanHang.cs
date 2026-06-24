using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.IO;

namespace demo.Control
{
    public partial class UC_BanHang : UserControl
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lParam);

        private readonly string strConn;

        public UC_BanHang()
        {
            InitializeComponent();
            strConn = GetProviderConnectionString();
            SendMessage(txtTimKiem.Handle, 0x1501, 1, "Nhập danh mục (VD: Nước ngọt)");
            LoadSanPhamTuDatabase();
            TrangDiemGiaoDien(); // Gọi hàm làm đẹp giao diện
        }

        // =================================================================
        // HÀM LÀM ĐẸP GIAO DIỆN (FLAT DESIGN) BẰNG CODE
        // =================================================================
        private void TrangDiemGiaoDien()
        {
            // 1. Tút lại Bảng Giỏ Hàng (DataGridView)
            if (dgvDonHang != null)
            {
                dgvDonHang.BackgroundColor = Color.White;
                dgvDonHang.BorderStyle = BorderStyle.None;
                dgvDonHang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvDonHang.GridColor = Color.FromArgb(230, 230, 230); // Màu viền xám nhạt
                dgvDonHang.EnableHeadersVisualStyles = false;

                // Tiêu đề bảng
                dgvDonHang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvDonHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(43, 45, 66); // Nền xanh đen mờ
                dgvDonHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvDonHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvDonHang.ColumnHeadersHeight = 40;

                // Các dòng dữ liệu
                dgvDonHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 238, 246); // Xanh nhạt khi nhấp vào
                dgvDonHang.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgvDonHang.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                dgvDonHang.RowTemplate.Height = 35;
                dgvDonHang.RowHeadersVisible = false; // Ẩn cột mũi tên trống bên trái
            }

            // 2. Tút lại FlowLayoutPanel (Vùng chứa sản phẩm)
            if (flpProducts != null)
            {
                flpProducts.BackColor = Color.FromArgb(244, 246, 249); // Màu nền xám cực nhạt cho khu vực sản phẩm
            }
        }

        private void UC_BanHang_Load(object sender, EventArgs e)
        {
        }

        private void pnlCart_Paint(object sender, PaintEventArgs e) { }
        private void pnlTop_Paint(object sender, PaintEventArgs e) { }
        private void flpProducts_Paint(object sender, PaintEventArgs e) { }
        private void txtMaGiamGia_TextChanged(object sender, EventArgs e) { }

        // =================================================================
        // 1. TẠO THẺ SẢN PHẨM (STYLE SÁNG TRỌNG - MODERN WHITE)
        // =================================================================
        private Panel TaoTheSanPham_DarkTheme(string ten, string danhMuc, string gia, string soLuong, string duongDanHinh)
        {
            Panel card = new Panel();
            card.Size = new Size(160, 220); // Tăng chiều cao xíu cho thoáng
            card.BackColor = Color.White; // Đổi thành thẻ màu Trắng
            card.Margin = new Padding(12);
            card.Cursor = Cursors.Hand; // Hiển thị hình bàn tay khi rà chuột vào

            // Vẽ viền xám mỏng cho thẻ sản phẩm
            card.Paint += (s, e) => { ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid); };

            Label lblSL = new Label();
            lblSL.Text = "SL: " + soLuong;
            lblSL.ForeColor = Color.Gray;
            lblSL.Font = new Font("Segoe UI", 8);
            lblSL.Location = new Point(110, 10);
            lblSL.AutoSize = true;

            PictureBox pic = new PictureBox();
            pic.Size = new Size(100, 100); // Tăng ảnh to lên tí
            pic.Location = new Point(30, 25);
            pic.SizeMode = PictureBoxSizeMode.Zoom;

            Label lblTen = new Label();
            lblTen.Text = ten;
            lblTen.ForeColor = Color.Black; // Chữ đen sang trọng
            lblTen.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTen.Location = new Point(5, 135);
            lblTen.Size = new Size(150, 40); // Cho tên 2 dòng nếu dài quá
            lblTen.TextAlign = ContentAlignment.TopCenter;

            Label lblDM = new Label();
            lblDM.Text = danhMuc;
            lblDM.ForeColor = Color.DimGray;
            lblDM.Font = new Font("Segoe UI", 8);
            lblDM.Location = new Point(5, 175);
            lblDM.Size = new Size(150, 15);
            lblDM.TextAlign = ContentAlignment.MiddleCenter;

            Label lblGia = new Label();
            lblGia.Text = gia;
            lblGia.ForeColor = Color.FromArgb(231, 76, 60); // Màu đỏ cam nổi bật cho giá tiền
            lblGia.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblGia.Location = new Point(5, 190);
            lblGia.Size = new Size(150, 25);
            lblGia.TextAlign = ContentAlignment.MiddleCenter;

            card.Controls.Add(lblSL);
            card.Controls.Add(pic);
            card.Controls.Add(lblTen);
            card.Controls.Add(lblDM);
            card.Controls.Add(lblGia);

            LoadProductImage(pic, ten, duongDanHinh);

            card.Tag = new string[] { ten, gia };

            card.Click += SanPham_Click;
            pic.Click += SanPham_Click;
            lblTen.Click += SanPham_Click;
            lblGia.Click += SanPham_Click;
            lblDM.Click += SanPham_Click;
            lblSL.Click += SanPham_Click;

            return card;
        }

        private void LoadSanPhamTuDatabase()
        {
            flpProducts.Controls.Clear();
            string query = "SELECT TenSanPham, MoTa, GiaBan, SoLuongTon, HinhAnh FROM SAN_PHAM";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string ten = reader["TenSanPham"].ToString();
                        string danhMuc = reader["MoTa"].ToString();
                        string soLuong = reader["SoLuongTon"].ToString();
                        decimal gia = Convert.ToDecimal(reader["GiaBan"]);
                        string giaHienThi = gia.ToString("N0") + " đ";

                        string duongDanHinh = reader["HinhAnh"] != DBNull.Value ? reader["HinhAnh"].ToString() : "";

                        Panel theSP = TaoTheSanPham_DarkTheme(ten, danhMuc, giaHienThi, soLuong, duongDanHinh);
                        flpProducts.Controls.Add(theSP);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }

        private void HienThiSanPhamTheoMoTa(string tuKhoa)
        {
            flpProducts.Controls.Clear();
            string query = "SELECT TenSanPham, MoTa, GiaBan, SoLuongTon, HinhAnh FROM SAN_PHAM WHERE MoTa LIKE @tuKhoa";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tuKhoa", "%" + tuKhoa + "%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string ten = reader["TenSanPham"].ToString();
                        string danhMuc = reader["MoTa"].ToString();
                        string soLuong = reader["SoLuongTon"].ToString();
                        decimal gia = Convert.ToDecimal(reader["GiaBan"]);
                        string giaHienThi = gia.ToString("N0") + " đ";

                        string duongDanHinh = reader["HinhAnh"] != DBNull.Value ? reader["HinhAnh"].ToString() : "";

                        Panel theSP = TaoTheSanPham_DarkTheme(ten, danhMuc, giaHienThi, soLuong, duongDanHinh);
                        flpProducts.Controls.Add(theSP);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            HienThiSanPhamTheoMoTa(tuKhoa);
        }

        // =================================================================
        // 2. XỬ LÝ GIỎ HÀNG (THÊM, XÓA, TÍNH TIỀN)
        // =================================================================
        private void SanPham_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Control clickedControl = sender as System.Windows.Forms.Control;
                Panel theSanPham = clickedControl is Panel ? (Panel)clickedControl : (Panel)clickedControl.Parent;

                string[] thongTin = (string[])theSanPham.Tag;
                string tenSP = thongTin[0];
                string giaSP = thongTin[1];

                bool daCoTrongGio = false;

                foreach (DataGridViewRow row in dgvDonHang.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == tenSP)
                    {
                        daCoTrongGio = true;
                        int slHienTai = Convert.ToInt32(row.Cells[1].Value);
                        int slMoi = slHienTai + 1;
                        row.Cells[1].Value = slMoi.ToString();

                        string chuoiGia = giaSP.Replace("đ", "").Replace(".", "").Replace(",", "").Trim();
                        if (decimal.TryParse(chuoiGia, out decimal giaTienGoc))
                        {
                            decimal thanhTienMoi = giaTienGoc * slMoi;
                            row.Cells[3].Value = thanhTienMoi.ToString("N0") + " đ";
                        }
                        break;
                    }
                }

                if (daCoTrongGio == false)
                {
                    dgvDonHang.Rows.Add(tenSP, "1", giaSP, giaSP);
                }

                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi rồi Kiệt ơi: " + ex.Message, "Báo Lỗi");
            }
        }

        private static string GetProviderConnectionString()
        {
            ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings["CUA_HANG_TIEN_LOI_Entities"];
            if (connectionSettings == null || string.IsNullOrWhiteSpace(connectionSettings.ConnectionString))
            {
                throw new InvalidOperationException("Không tìm thấy connection string CUA_HANG_TIEN_LOI_Entities trong cấu hình ứng dụng.");
            }

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder(connectionSettings.ConnectionString);
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(entityBuilder.ProviderConnectionString);
            builder.TrustServerCertificate = true;
            builder.Encrypt = false;

            return builder.ConnectionString;
        }

        private static void LoadProductImage(PictureBox pictureBox, string tenSanPham, string hinhAnh)
        {
            string imagePath = ResolveProductImagePath(tenSanPham, hinhAnh);

            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                pictureBox.BackColor = Color.FromArgb(240, 240, 240);
                return;
            }

            try
            {
                using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                using (Image image = Image.FromStream(stream))
                {
                    pictureBox.Image = new Bitmap(image);
                }
            }
            catch
            {
                pictureBox.BackColor = Color.WhiteSmoke;
            }
        }

        private static string ResolveProductImagePath(string tenSanPham, string hinhAnh)
        {
            string fileName = NormalizeImageFileName(hinhAnh);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = GetDefaultProductImageName(tenSanPham);
            }

            foreach (string folder in GetResourceFolders())
            {
                string candidate = Path.Combine(folder, fileName);
                if (File.Exists(candidate))
                {
                    return candidate;
                }
            }

            foreach (string folder in GetResourceFolders())
            {
                string fallback = Path.Combine(folder, "no-image.png");
                if (File.Exists(fallback))
                {
                    return fallback;
                }
            }

            return null;
        }

        private static string NormalizeImageFileName(string hinhAnh)
        {
            if (string.IsNullOrWhiteSpace(hinhAnh))
            {
                return string.Empty;
            }

            return Path.GetFileName(hinhAnh.Trim());
        }

        private static string[] GetResourceFolders()
        {
            string runtimeFolder = Path.Combine(Application.StartupPath, "Resources");
            string projectFolder = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Resources"));

            return new[] { runtimeFolder, projectFolder }
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();
        }

        private static string GetDefaultProductImageName(string tenSanPham)
        {
            if (string.IsNullOrWhiteSpace(tenSanPham))
            {
                return string.Empty;
            }

            string normalizedName = tenSanPham.Trim().ToLowerInvariant();

            if (normalizedName.Contains("coca"))
            {
                return "product-coca-cola.png";
            }

            if (normalizedName.Contains("pepsi"))
            {
                return "product-pepsi.png";
            }

            if (normalizedName.Contains("sting"))
            {
                return "product-sting.png";
            }

            if (normalizedName.Contains("hảo hảo") || normalizedName.Contains("hao hao"))
            {
                return "product-mi-hao-hao.png";
            }

            if (normalizedName.Contains("vinamilk") || normalizedName.Contains("sữa"))
            {
                return "product-sua-vinamilk.png";
            }

            if (normalizedName.Contains("chocopie"))
            {
                return "product-banh-chocopie.png";
            }

            if (normalizedName.Contains("snack"))
            {
                return "product-snack-khoai-tay.png";
            }

            if (normalizedName.Contains("merino") || normalizedName.Contains("kem"))
            {
                return "product-kem-merino.png";
            }

            if (normalizedName.Contains("nước mắm") || normalizedName.Contains("nuoc mam"))
            {
                return "product-nuoc-mam.png";
            }

            if (normalizedName.Contains("trà xanh") || normalizedName.Contains("tra xanh"))
            {
                return "product-tra-xanh.png";
            }

            return string.Empty;
        }

        private void btnXoaSanPham_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.Rows.Count == 0 || (dgvDonHang.Rows.Count == 1 && dgvDonHang.Rows[0].IsNewRow))
            {
                return;
            }

            if (dgvDonHang.CurrentCell != null)
            {
                int rowIndex = dgvDonHang.CurrentCell.RowIndex;

                if (!dgvDonHang.Rows[rowIndex].IsNewRow)
                {
                    string tenSP = dgvDonHang.Rows[rowIndex].Cells[0].Value != null
                                   ? dgvDonHang.Rows[rowIndex].Cells[0].Value.ToString()
                                   : "sản phẩm này";

                    DialogResult xacNhan = MessageBox.Show($"Bạn có chắc muốn xóa '{tenSP}' khỏi đơn hàng?",
                                                           "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (xacNhan == DialogResult.Yes)
                    {
                        dgvDonHang.Rows.RemoveAt(rowIndex);
                        TinhTongTien();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng click chọn một sản phẩm trong danh sách trước khi xóa!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TinhTongTien()
        {
            decimal tamTinh = 0;

            foreach (DataGridViewRow row in dgvDonHang.Rows)
            {
                if (!row.IsNewRow && row.Cells[3].Value != null)
                {
                    string chuoiTien = row.Cells[3].Value.ToString();
                    chuoiTien = chuoiTien.Replace(" đ", "").Replace(".", "").Replace(",", "").Trim();

                    if (decimal.TryParse(chuoiTien, out decimal tienMonNay))
                    {
                        tamTinh += tienMonNay;
                    }
                }
            }

            decimal phanTramGiam = txtMaGiamGia.Tag != null ? Convert.ToDecimal(txtMaGiamGia.Tag) : 0;
            decimal giamGia = tamTinh * phanTramGiam;
            decimal tongCong = tamTinh - giamGia;

            lbl_TamTinh.Text = tamTinh.ToString("N0") + " đ";
            lblGiamGia.Text = "-" + giamGia.ToString("N0") + " đ";
            lbl_Sum.Text = tongCong.ToString("N0") + " đ";
        }

        // =================================================================
        // 3. XỬ LÝ KHUYẾN MÃI
        // =================================================================
        private void btnApDung_Click(object sender, EventArgs e)
        {
            string maNhap = txtMaGiamGia.Text.Trim();

            if (string.IsNullOrEmpty(maNhap))
            {
                MessageBox.Show("Vui lòng nhập tên khuyến mãi!");
                return;
            }

            decimal phanTram = LayPhanTramGiamTuDB(maNhap);

            if (phanTram > 0)
            {
                MessageBox.Show($"Áp dụng thành công! Giảm {(phanTram * 100):N0}%", "Thông báo");
                txtMaGiamGia.Tag = phanTram;
            }
            else
            {
                MessageBox.Show("Mã khuyến mãi không tồn tại hoặc đã hết hạn!", "Lỗi");
                txtMaGiamGia.Tag = 0m;
            }

            TinhTongTien();
        }

        private decimal LayPhanTramGiamTuDB(string tenMa)
        {
            decimal phanTram = 0;
            string query = "SELECT PhanTramGiam FROM KHUYEN_MAI WHERE TenKhuyenMai = @tenMa AND CAST(GETDATE() AS DATE) BETWEEN NgayBatDau AND NgayKetThuc";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenMa", tenMa);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        phanTram = Convert.ToDecimal(result) / 100;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi check mã: " + ex.Message);
                }
            }
            return phanTram;
        }

        // =================================================================
        // 4. CHỐT ĐƠN & THANH TOÁN 
        // =================================================================
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.Rows.Count == 0 || (dgvDonHang.Rows.Count == 1 && dgvDonHang.Rows[0].IsNewRow))
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng chọn món trước khi thanh toán.", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int tongSoLuongSP = 0;
            foreach (DataGridViewRow row in dgvDonHang.Rows)
            {
                if (!row.IsNewRow && row.Cells[1].Value != null)
                {
                    tongSoLuongSP += Convert.ToInt32(row.Cells[1].Value);
                }
            }
            int diemCongThem = tongSoLuongSP * 3;

            int maKhachHang = 0;
            string sdtNhap = "";
            var txtSDTControl = this.Controls.Find("txtSoDienThoai", true).FirstOrDefault();
            if (txtSDTControl != null) sdtNhap = txtSDTControl.Text.Trim();

            if (!string.IsNullOrEmpty(sdtNhap))
            {
                string tenKhach = "";
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    string query = "SELECT MaKhachHang, TenKhachHang FROM KHACH_HANG WHERE SoDienThoai = @SDT";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SDT", sdtNhap);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                maKhachHang = Convert.ToInt32(reader["MaKhachHang"]);
                                tenKhach = reader["TenKhachHang"].ToString();
                            }
                        }
                    }
                }

                if (maKhachHang > 0)
                {
                    DialogResult hoiTichDiem = MessageBox.Show($"Khách hàng: {tenKhach}\nĐơn này được cộng +{diemCongThem} điểm.\n\nTiếp tục thanh toán?",
                                                                "Khách hàng thành viên", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (hoiTichDiem == DialogResult.No) return;
                }
                else
                {
                    DialogResult hoiDangKy = MessageBox.Show($"Số điện thoại {sdtNhap} chưa đăng ký.\nKhách có muốn đăng ký làm thành viên không?",
                                                             "Đăng ký thành viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (hoiDangKy == DialogResult.Yes)
                    {
                        FrmThemKhachHang fThemKH = new FrmThemKhachHang();
                        if (fThemKH.ShowDialog() == DialogResult.OK)
                        {
                            maKhachHang = fThemKH.MaKhachMoi;
                            MessageBox.Show($"Đăng ký hoàn tất! Đơn này được cộng +{diemCongThem} điểm.", "Thành công");
                        }
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Khách chưa cung cấp SĐT. Bạn có muốn đăng ký thành viên cho khách không?", "Nhắc nhở", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FrmThemKhachHang fThemKH = new FrmThemKhachHang();
                    if (fThemKH.ShowDialog() == DialogResult.OK)
                    {
                        maKhachHang = fThemKH.MaKhachMoi;
                    }
                }
            }

            string soTienCanThu = lbl_Sum.Text;
            string tongTienStr = soTienCanThu.Replace("đ", "").Replace(".", "").Replace(",", "").Trim();
            if (!decimal.TryParse(tongTienStr, out decimal tongTienThucTe))
            {
                MessageBox.Show("Lỗi định dạng số tiền!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form_ThanhToan frmTT = new Form_ThanhToan(tongTienThucTe);

            if (frmTT.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string sqlInsertHD = maKhachHang > 0
                                ? "INSERT INTO HOA_DON (NgayLap, TongTien, MaKhachHang, MaNhanVien) OUTPUT INSERTED.MaHoaDon VALUES (GETDATE(), @TongTien, @MaKH, @MaNV)"
                                : "INSERT INTO HOA_DON (NgayLap, TongTien, MaNhanVien) OUTPUT INSERTED.MaHoaDon VALUES (GETDATE(), @TongTien, @MaNV)";

                            int maHoaDonMoi;

                            using (SqlCommand cmdHD = new SqlCommand(sqlInsertHD, conn, transaction))
                            {
                                cmdHD.Parameters.Add("@TongTien", SqlDbType.Decimal).Value = tongTienThucTe;
                                cmdHD.Parameters.Add("@MaNV", SqlDbType.Int).Value = demo.BLL.Service.UserSession.MaNhanVien;

                                if (maKhachHang > 0) cmdHD.Parameters.Add("@MaKH", SqlDbType.Int).Value = maKhachHang;

                                maHoaDonMoi = Convert.ToInt32(cmdHD.ExecuteScalar());
                            }

                            foreach (DataGridViewRow row in dgvDonHang.Rows)
                            {
                                if (!row.IsNewRow && row.Cells[0].Value != null)
                                {
                                    string tenSP = row.Cells[0].Value.ToString();
                                    int soLuongMua = Convert.ToInt32(row.Cells[1].Value);
                                    string giaStr = row.Cells[2].Value.ToString().Replace("đ", "").Replace(".", "").Replace(",", "").Trim();
                                    decimal donGia = Convert.ToDecimal(giaStr);
                                    decimal thanhTienCT = soLuongMua * donGia;

                                    string sqlInsertChiTiet = @"INSERT INTO CHI_TIET_HOA_DON (MaHoaDon, MaSanPham, SoLuong, DonGia, ThanhTien) 
                                                                VALUES (@MaHD, (SELECT TOP 1 MaSanPham FROM SAN_PHAM WHERE TenSanPham = @TenSP), @SL, @Gia, @ThanhTienCT)";
                                    using (SqlCommand cmdCT = new SqlCommand(sqlInsertChiTiet, conn, transaction))
                                    {
                                        cmdCT.Parameters.Add("@MaHD", SqlDbType.Int).Value = maHoaDonMoi;
                                        cmdCT.Parameters.Add("@TenSP", SqlDbType.NVarChar).Value = tenSP;
                                        cmdCT.Parameters.Add("@SL", SqlDbType.Int).Value = soLuongMua;
                                        cmdCT.Parameters.Add("@Gia", SqlDbType.Decimal).Value = donGia;
                                        cmdCT.Parameters.Add("@ThanhTienCT", SqlDbType.Decimal).Value = thanhTienCT;
                                        cmdCT.ExecuteNonQuery();
                                    }

                                    string sqlUpdateKho = "UPDATE SAN_PHAM SET SoLuongTon = SoLuongTon - @SLMua WHERE TenSanPham = @TenSPKho";
                                    using (SqlCommand cmdKho = new SqlCommand(sqlUpdateKho, conn, transaction))
                                    {
                                        cmdKho.Parameters.Add("@SLMua", SqlDbType.Int).Value = soLuongMua;
                                        cmdKho.Parameters.Add("@TenSPKho", SqlDbType.NVarChar).Value = tenSP;
                                        cmdKho.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (maKhachHang > 0)
                            {
                                string sqlDiem = "UPDATE KHACH_HANG SET DiemTichLuy = ISNULL(DiemTichLuy, 0) + @Diem WHERE MaKhachHang = @MaKH";
                                using (SqlCommand cmdDiem = new SqlCommand(sqlDiem, conn, transaction))
                                {
                                    cmdDiem.Parameters.Add("@Diem", SqlDbType.Int).Value = diemCongThem;
                                    cmdDiem.Parameters.Add("@MaKH", SqlDbType.Int).Value = maKhachHang;
                                    cmdDiem.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show($"Thanh toán thành công!\nMã hóa đơn: {maHoaDonMoi}", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dgvDonHang.Rows.Clear();
                            lbl_TamTinh.Text = "0 đ";
                            lbl_Sum.Text = "0 đ";
                            if (txtSDTControl != null) txtSDTControl.Text = "";

                            var lblGiamGiaHienTai = this.Controls.Find("lblGiamGia", true).FirstOrDefault();
                            if (lblGiamGiaHienTai != null) lblGiamGiaHienTai.Text = "-0 đ";

                            txtMaGiamGia.Text = "";
                            txtMaGiamGia.Tag = 0.0m;

                            LoadSanPhamTuDatabase();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi hệ thống khi lưu hóa đơn.\nChi tiết: " + ex.Message, "Lỗi Nghiêm Trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
