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

            if (System.IO.File.Exists(duongDanHinh))
            {
                pic.Image = Image.FromFile(duongDanHinh);
            }
            else
            {
                pic.BackColor = Color.Gray; // Màu dự phòng nếu không tìm thấy hình
            }

            card.Tag = new string[] { ten, gia };

            // 2. Gắn sự kiện Click. 
            // Phải gắn cho cả Panel, Hình ảnh và các chữ. Tránh tình trạng khách bấm trúng cái chữ thì nó không ăn.
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
                // 2. KIỂM TRA MÓN ĐÃ CÓ TRONG GIỎ CHƯA (Dùng vị trí cột 0, 1, 2, 3)
                // =====================================================
                bool daCoTrongGio = false;

                foreach (DataGridViewRow row in dgvDonHang.Rows)
                {
                    // Bỏ qua dòng trống rỗng cuối cùng của DataGridView
                    if (row.IsNewRow) continue;

                    // Cột [0] là Tên món: Kiểm tra xem tên có trùng không
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == tenSP)
                    {
                        daCoTrongGio = true; // Đánh dấu là đã tìm thấy

                        // Cột [1] là Số lượng: Lấy SL hiện tại cộng thêm 1
                        int slHienTai = Convert.ToInt32(row.Cells[1].Value);
                        int slMoi = slHienTai + 1;
                        row.Cells[1].Value = slMoi.ToString();

                        // Lột bỏ chữ 'đ' và dấu chấm của Đơn Giá để tính toán
                        string chuoiGia = giaSP.Replace("đ", "").Replace(".", "").Replace(",", "").Trim();
                        if (decimal.TryParse(chuoiGia, out decimal giaTienGoc))
                        {
                            // Cột [3] là Thành tiền: Cập nhật lại tiền mới
                            decimal thanhTienMoi = giaTienGoc * slMoi;
                            row.Cells[3].Value = thanhTienMoi.ToString("N0") + " đ";
                        }

                        break; // Tìm thấy rồi thì thoát vòng lặp
                    }
                }

                // 3. Nếu quét hết giỏ mà chưa có món này -> Thêm dòng mới tinh
                if (daCoTrongGio == false)
                {
                    dgvDonHang.Rows.Add(tenSP, "1", giaSP, giaSP);
                }

                // 4. Kích hoạt tính lại Tổng Cộng
                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi rồi Kiệt ơi: " + ex.Message, "Báo Lỗi");
            }
        }

        private void LoadSanPhamTuDatabase()
        {
            // 1. Xóa sạch màn hình trước khi nạp đồ mới
            flpProducts.Controls.Clear();

            // 2. Chuỗi kết nối (CỰC QUAN TRỌNG: Bạn sửa lại chỗ Kiet_PC cho đúng với tên Server SQL của bạn nhé)
            string connectionString = @"Data Source=Kiet_PC;Initial Catalog=CUA_HANG_TIEN_LOI;Integrated Security=True";

            // Câu lệnh SQL lấy dữ liệu
            string query = "SELECT TenSanPham, MoTa, GiaBan, SoLuongTon, HinhAnh FROM SAN_PHAM";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // 3. Đọc từng dòng dữ liệu trong bảng SAN_PHAM
                    while (reader.Read())
                    {
                        // Lấy thông tin cơ bản
                        string ten = reader["TenSanPham"].ToString();

                        // Cột MoTa đang chứa text (Nước ngọt, Mì ăn liền...), mình dùng tạm làm danh mục
                        string danhMuc = reader["MoTa"].ToString();

                        string soLuong = reader["SoLuongTon"].ToString();

                        // Ép kiểu Giá Bán và format cho đẹp (ví dụ: 10000 -> 10.000)
                        decimal gia = Convert.ToDecimal(reader["GiaBan"]);
                        string giaHienThi = gia.ToString("N0") + " đ";

                        // Xử lý cột Hình Ảnh (Đề phòng database đang bị NULL)
                        string duongDanHinh = "";
                        if (reader["HinhAnh"] != DBNull.Value)
                        {
                            duongDanHinh = reader["HinhAnh"].ToString();
                        }

                        // 4. Bỏ dữ liệu vào cái "Khuôn đúc" đã tạo lúc nãy
                        Panel theSP = TaoTheSanPham_DarkTheme(ten, danhMuc, giaHienThi, soLuong, duongDanHinh);

                        // 5. Quăng thẻ lên mâm
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
            decimal tongTien = 0;

            // Duyệt qua từng dòng trong bảng dgvDonHang của bạn
            foreach (DataGridViewRow row in dgvDonHang.Rows)
            {
                // Kiểm tra xem dòng đó có dữ liệu không (tránh dòng trống dưới cùng)
                if (!row.IsNewRow && row.Cells[3].Value != null)
                {
                    // Lấy chuỗi tiền ở cột số 3 (cột Thành tiền)
                    string chuoiTien = row.Cells[3].Value.ToString();

                    // Dọn dẹp rác: Xóa chữ " đ", xóa dấu chấm để thành số nguyên chất (VD: "25.000 đ" -> "25000")
                    chuoiTien = chuoiTien.Replace(" đ", "").Replace(".", "").Replace(",", "").Trim();

                    // Ép sang kiểu số và cộng dồn vào tổng
                    if (decimal.TryParse(chuoiTien, out decimal tienCuaMonNay))
                    {
                        tongTien += tienCuaMonNay;
                    }
                }
            }

            // Gắn kết quả lên 2 cái Label Tạm tính & Tổng cộng
            // LƯU Ý: Chỗ này bạn click vào 2 cái chữ màu vàng trên giao diện xem Name nó là gì thì thay vào chữ lblTamTinh và lblTongCong nhé!
            lbl_TamTinh.Text = tongTien.ToString("N0") + " đ";
            lbl_Sum.Text = tongTien.ToString("N0") + " đ";
        }

        private void HienThiSanPhamTheoMoTa(string tuKhoa)
        {
            // Xóa sạch mâm cũ để bày đồ mới lên
            flpProducts.Controls.Clear();

            // Nhớ sửa lại Data Source cho đúng tên máy của bạn nhé (Kiet_PC)
            string connectionString = @"Data Source=Kiet_PC;Initial Catalog=CUA_HANG_TIEN_LOI;Integrated Security=True";

            // Câu lệnh SQL lọc theo Mô Tả (Tìm gần đúng chứa từ khóa)
            string query = "SELECT TenSanPham, MoTa, GiaBan, SoLuongTon, HinhAnh FROM SAN_PHAM WHERE MoTa LIKE @tuKhoa";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Dùng %tuKhoa% để tìm được cả những chữ chứa từ khóa ở giữa. 
                    // Truyền N phía trước để hỗ trợ tìm tiếng Việt có dấu.
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

                        // Gọi khuôn đúc thẻ
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
        
            // Lấy chữ người dùng đang gõ
            string tuKhoa = txtTimKiem.Text.Trim();

            // Truyền chữ đó vào hàm tìm kiếm để nó gọi Database
            HienThiSanPhamTheoMoTa(tuKhoa);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // 1. CHẶN LỖI: Kiểm tra xem giỏ hàng có đang trống không?
            // Nếu bảng dgvDonHang không có dòng nào (hoặc chỉ có 1 dòng trắng mặc định) thì báo lỗi
            if (dgvDonHang.Rows.Count == 0 || (dgvDonHang.Rows.Count == 1 && dgvDonHang.Rows[0].IsNewRow))
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng chọn món trước khi thanh toán.", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại, không làm các bước dưới nữa
            }

            // 2. XÁC NHẬN: Lấy số tiền ở ô Tổng Cộng ra để hỏi lại thu ngân cho chắc ăn
            string soTienCanThu = lbl_Sum.Text;
            DialogResult xacNhan = MessageBox.Show("Thu của khách: " + soTienCanThu + "\n\nBạn có chắc chắn muốn thanh toán đơn hàng này?", "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan == DialogResult.Yes)
            {
                try
                {
                    // =========================================================================
                    // LƯU Ý CHO ĐỒ ÁN: CHỖ NÀY SAU NÀY SẼ VIẾT CODE LƯU VÀO DATABASE (HOA_DON)
                    // Tạm thời mình cho hiển thị thành công trước nhé.
                    // =========================================================================

                    // 3. THÔNG BÁO THÀNH CÔNG
                    MessageBox.Show("Thanh toán thành công! Đã in hóa đơn.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. DỌN DẸP BÀN LÀM VIỆC ĐỂ ĐÓN KHÁCH MỚI
                    // Quét sạch mâm giỏ hàng
                    dgvDonHang.Rows.Clear();

                    // Trả các con số tiền về lại số 0 tròn trĩnh
                    // (Nhớ thay đúng tên các Label của bạn nha)
                    lbl_TamTinh.Text = "0 đ";
                    //lblGiamGia.Text = "-0 đ";
                    lbl_Sum.Text = "0 đ";

                    // Xóa trắng ô mã giảm giá luôn (Nếu bạn có đặt tên ô đó là txtMaGiamGia)
                    // txtMaGiamGia.Text = ""; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
