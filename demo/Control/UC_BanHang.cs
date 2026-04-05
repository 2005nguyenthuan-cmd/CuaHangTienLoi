using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace demo.Control
{
    public partial class UC_BanHang : UserControl
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lParam);
        public UC_BanHang()
        {
            InitializeComponent();
            SendMessage(txtTimKiem.Handle, 0x1501, 1, "Nhập danh mục (VD: Nước ngọt)");
            LoadSanPhamTuDatabase();
        }

        private void UC_BanHang_Load(object sender, EventArgs e)
        {

        }

        private void pnlCart_Paint(object sender, PaintEventArgs e)
        {

        }

        private Panel TaoTheSanPham_DarkTheme(string ten, string danhMuc, string gia, string soLuong, string duongDanHinh)
        {
            Panel card = new Panel();
            card.Size = new Size(160, 200);
            card.BackColor = Color.FromArgb(34, 38, 53); // Màu nền xám xanh của thẻ
            card.Margin = new Padding(10);

            // Label Số lượng (Góc trên phải)
            Label lblSL = new Label();
            lblSL.Text = "SL: " + soLuong;
            lblSL.ForeColor = Color.DarkGray;
            lblSL.Font = new Font("Segoe UI", 8);
            lblSL.Location = new Point(110, 10);
            lblSL.AutoSize = true;

            // Hình ảnh (Giữa)
            PictureBox pic = new PictureBox();
            pic.Size = new Size(80, 80);
            pic.Location = new Point(40, 30);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            // pic.Image = Image.FromFile("duong_dan.jpg"); 

            // Label Tên (Dưới hình)
            Label lblTen = new Label();
            lblTen.Text = ten;
            lblTen.ForeColor = Color.White;
            lblTen.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTen.Location = new Point(5, 120);
            lblTen.Size = new Size(150, 20);
            lblTen.TextAlign = ContentAlignment.MiddleCenter; // Canh giữa chữ

            // Label Danh mục (Dưới tên, chữ nhỏ màu xám)
            Label lblDM = new Label();
            lblDM.Text = danhMuc;
            lblDM.ForeColor = Color.Gray;
            lblDM.Font = new Font("Segoe UI", 8);
            lblDM.Location = new Point(5, 145);
            lblDM.Size = new Size(150, 15);
            lblDM.TextAlign = ContentAlignment.MiddleCenter;

            // Label Giá (Dưới cùng, chữ Vàng)
            Label lblGia = new Label();
            lblGia.Text = gia;
            lblGia.ForeColor = Color.Gold;
            lblGia.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblGia.Location = new Point(5, 170);
            lblGia.Size = new Size(150, 25);
            lblGia.TextAlign = ContentAlignment.MiddleCenter;

            // Gắn vào thẻ
            card.Controls.Add(lblSL);
            card.Controls.Add(pic);
            card.Controls.Add(lblTen);
            card.Controls.Add(lblDM);
            card.Controls.Add(lblGia);

            try
            {
                if (!string.IsNullOrEmpty(duongDanHinh))
                {
                    string path = System.IO.Path.Combine(Application.StartupPath, "Resources", duongDanHinh);

                    if (System.IO.File.Exists(path))
                    {
                        pic.Image = Image.FromFile(path);
                    }
                    else
                    {
                        pic.BackColor = Color.FromArgb(50, 50, 60);
                    }
                }
            }
            catch (Exception ex)
            {
                pic.BackColor = Color.DimGray;
            }

            card.Tag = new string[] { ten, gia };

            // 2. Gắn sự kiện Click. 
            card.Click += SanPham_Click;
            pic.Click += SanPham_Click;
            lblTen.Click += SanPham_Click;
            lblGia.Click += SanPham_Click;
            lblDM.Click += SanPham_Click;
            lblSL.Click += SanPham_Click;
            return card;
        }

        private void SanPham_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy thông tin từ thẻ bị bấm
                System.Windows.Forms.Control clickedControl = sender as System.Windows.Forms.Control;
                Panel theSanPham = clickedControl is Panel ? (Panel)clickedControl : (Panel)clickedControl.Parent;

                string[] thongTin = (string[])theSanPham.Tag;
                string tenSP = thongTin[0];
                string giaSP = thongTin[1];

                // =====================================================
                // 2. KIỂM TRA MÓN ĐÃ CÓ TRONG GIỎ CHƯA
                // =====================================================
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

        private void LoadSanPhamTuDatabase()
        {
            flpProducts.Controls.Clear();

            // ĐÃ SỬA LẠI THÀNH Kiet_PC
            string connectionString = @"Data Source=Kiet_PC;Initial Catalog=CUA_HANG_TIEN_LOI;Integrated Security=True";

            string query = "SELECT TenSanPham, MoTa, GiaBan, SoLuongTon, HinhAnh FROM SAN_PHAM";

            using (SqlConnection conn = new SqlConnection(connectionString))
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

                        string duongDanHinh = "";
                        if (reader["HinhAnh"] != DBNull.Value)
                        {
                            duongDanHinh = reader["HinhAnh"].ToString();
                        }

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

        private void HienThiSanPhamTheoMoTa(string tuKhoa)
        {
            flpProducts.Controls.Clear();

            // ĐÃ SỬA LẠI THÀNH Kiet_PC
            string connectionString = @"Data Source=Kiet_PC;Initial Catalog=CUA_HANG_TIEN_LOI;Integrated Security=True";

            string query = "SELECT TenSanPham, MoTa, GiaBan, SoLuongTon, HinhAnh FROM SAN_PHAM WHERE MoTa LIKE @tuKhoa";

            using (SqlConnection conn = new SqlConnection(connectionString))
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

                        string duongDanHinh = "";
                        if (reader["HinhAnh"] != DBNull.Value)
                        {
                            duongDanHinh = reader["HinhAnh"].ToString();
                        }

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

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flpProducts_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            HienThiSanPhamTheoMoTa(tuKhoa);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.Rows.Count == 0 || (dgvDonHang.Rows.Count == 1 && dgvDonHang.Rows[0].IsNewRow))
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng chọn món trước khi thanh toán.", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string soTienCanThu = lbl_Sum.Text;
            if (MessageBox.Show("Thu của khách: " + soTienCanThu + "\n\nBạn có chắc chắn muốn thanh toán?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string tongTienStr = soTienCanThu.Replace("đ", "").Replace(".", "").Replace(",", "").Trim();
            decimal tongTienThucTe = 0;
            decimal.TryParse(tongTienStr, out tongTienThucTe);

            // ĐÃ SỬA LẠI THÀNH Kiet_PC
            string strConn = @"Data Source=Kiet_PC;Initial Catalog=CUA_HANG_TIEN_LOI;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string sqlInsertHD = "INSERT INTO HOA_DON (NgayLap, TongTien) OUTPUT INSERTED.MaHoaDon VALUES (GETDATE(), @TongTien)";
                    SqlCommand cmdHD = new SqlCommand(sqlInsertHD, conn, transaction);
                    cmdHD.Parameters.AddWithValue("@TongTien", tongTienThucTe);

                    int maHoaDonMoi = Convert.ToInt32(cmdHD.ExecuteScalar());

                    foreach (DataGridViewRow row in dgvDonHang.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[0].Value != null)
                        {
                            string tenSP = row.Cells[0].Value.ToString();
                            int soLuongMua = Convert.ToInt32(row.Cells[1].Value);

                            string giaStr = row.Cells[2].Value.ToString().Replace("đ", "").Replace(".", "").Replace(",", "").Trim();
                            decimal donGia = Convert.ToDecimal(giaStr);

                            decimal thanhTienCT = soLuongMua * donGia;

                            string sqlInsertChiTiet = "INSERT INTO CHI_TIET_HOA_DON (MaHoaDon, MaSanPham, SoLuong, DonGia, ThanhTien) " +
                                                      "VALUES (@MaHD, (SELECT TOP 1 MaSanPham FROM SAN_PHAM WHERE TenSanPham = @TenSP), @SL, @Gia, @ThanhTienCT)";
                            SqlCommand cmdCT = new SqlCommand(sqlInsertChiTiet, conn, transaction);
                            cmdCT.Parameters.AddWithValue("@MaHD", maHoaDonMoi);
                            cmdCT.Parameters.Add("@TenSP", SqlDbType.NVarChar).Value = tenSP;
                            cmdCT.Parameters.AddWithValue("@SL", soLuongMua);
                            cmdCT.Parameters.AddWithValue("@Gia", donGia);
                            cmdCT.Parameters.AddWithValue("@ThanhTienCT", thanhTienCT);
                            cmdCT.ExecuteNonQuery();

                            string sqlUpdateKho = "UPDATE SAN_PHAM SET SoLuongTon = SoLuongTon - @SLMua WHERE TenSanPham = @TenSPKho";
                            SqlCommand cmdKho = new SqlCommand(sqlUpdateKho, conn, transaction);
                            cmdKho.Parameters.AddWithValue("@SLMua", soLuongMua);
                            cmdKho.Parameters.Add("@TenSPKho", SqlDbType.NVarChar).Value = tenSP;
                            cmdKho.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show("Thanh toán thành công! Mã hóa đơn: " + maHoaDonMoi, "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvDonHang.Rows.Clear();
                    lbl_TamTinh.Text = "0 đ";
                    lbl_Sum.Text = "0 đ";

                    if (this.Controls.Find("lbl_GiamGia", true).Length > 0)
                        this.Controls.Find("lbl_GiamGia", true)[0].Text = "-0 đ";

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
            // ĐÃ SỬA LẠI THÀNH Kiet_PC
            string connectionString = @"Data Source=Kiet_PC;Initial Catalog=CUA_HANG_TIEN_LOI;Integrated Security=True";

            string query = "SELECT PhanTramGiam FROM KHUYEN_MAI " +
               "WHERE TenKhuyenMai = @tenMa " +
               "AND CAST(GETDATE() AS DATE) BETWEEN NgayBatDau AND NgayKetThuc";

            using (SqlConnection conn = new SqlConnection(connectionString))
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

        private void txtMaGiamGia_TextChanged(object sender, EventArgs e)
        {

        }
    }
}