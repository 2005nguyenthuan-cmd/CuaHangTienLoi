using System;
using System.Drawing;
using System.Windows.Forms;

namespace demo
{
    public partial class Form_ThanhToan : Form
    {
        // Khai báo các công cụ (Control) sẽ xuất hiện trên màn hình
        private Label lblTieuDeTongTien;
        private Label lblTongTien;
        private Label lblTieuDeKhachDua;
        private TextBox txtKhachDua;
        private Label lblTieuDeTienThua;
        private Label lblTienThua;
        private Button btnXacNhan;
        private Button btnHuy;

        // Biến lưu tổng tiền lấy từ giỏ hàng sang
        private decimal tongTienHoaDon = 0;

        // Sửa lại hàm khởi tạo (Constructor) để nhận số tiền từ Form Bán Hàng truyền qua
        public Form_ThanhToan(decimal tongTien)
        {
            InitializeComponent();
            tongTienHoaDon = tongTien;

            // Gọi hàm tự động vẽ giao diện
            VeGiaoDien();

            // Hiển thị số tiền đỏ chót lên màn hình
            lblTongTien.Text = tongTienHoaDon.ToString("N0") + " đ";
        }

        // HÀM TỰ ĐỘNG VẼ GIAO DIỆN BẰNG CODE
        private void VeGiaoDien()
        {
            // 1. Cài đặt khung Form bên ngoài
            this.Text = "Thanh Toán Hóa Đơn";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent; // Nằm chễm chệ giữa màn hình
            this.FormBorderStyle = FormBorderStyle.FixedDialog;  // Không cho kéo giãn
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White; // Nền trắng sạch sẽ

            // 2. Phần TỔNG TIỀN
            lblTieuDeTongTien = new Label();
            lblTieuDeTongTien.Text = "TỔNG TIỀN HÓA ĐƠN:";
            lblTieuDeTongTien.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTieuDeTongTien.Location = new Point(30, 20);
            lblTieuDeTongTien.AutoSize = true;

            lblTongTien = new Label();
            lblTongTien.Text = "0 đ";
            lblTongTien.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Red;
            lblTongTien.Location = new Point(30, 45);
            lblTongTien.Size = new Size(320, 40);
            lblTongTien.TextAlign = ContentAlignment.MiddleRight;

            // 3. Phần TIỀN KHÁCH ĐƯA
            lblTieuDeKhachDua = new Label();
            lblTieuDeKhachDua.Text = "TIỀN KHÁCH ĐƯA:";
            lblTieuDeKhachDua.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblTieuDeKhachDua.Location = new Point(30, 105);
            lblTieuDeKhachDua.AutoSize = true;

            txtKhachDua = new TextBox();
            txtKhachDua.Font = new Font("Segoe UI", 14);
            txtKhachDua.Location = new Point(30, 130);
            txtKhachDua.Size = new Size(320, 32);
            txtKhachDua.TextAlign = HorizontalAlignment.Right;
            txtKhachDua.KeyPress += TxtKhachDua_KeyPress; // Chặn chỉ cho nhập số
            txtKhachDua.TextChanged += TxtKhachDua_TextChanged; // Vừa gõ vừa tính tiền thừa

            // 4. Phần TIỀN THỪA
            lblTieuDeTienThua = new Label();
            lblTieuDeTienThua.Text = "TIỀN THỪA TRẢ KHÁCH:";
            lblTieuDeTienThua.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblTieuDeTienThua.Location = new Point(30, 180);
            lblTieuDeTienThua.AutoSize = true;

            lblTienThua = new Label();
            lblTienThua.Text = "0 đ";
            lblTienThua.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTienThua.ForeColor = Color.MediumBlue;
            lblTienThua.Location = new Point(30, 205);
            lblTienThua.Size = new Size(320, 30);
            lblTienThua.TextAlign = ContentAlignment.MiddleRight;

            // 5. Hai nút XÁC NHẬN và HỦY BỎ
            btnXacNhan = new Button();
            btnXacNhan.Text = "XÁC NHẬN";
            btnXacNhan.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnXacNhan.BackColor = Color.MediumSeaGreen; // Màu xanh lá đẹp mắt
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Location = new Point(200, 250);
            btnXacNhan.Size = new Size(150, 45);
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.Cursor = Cursors.Hand;
            btnXacNhan.Click += BtnXacNhan_Click;

            btnHuy = new Button();
            btnHuy.Text = "HỦY BỎ";
            btnHuy.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnHuy.BackColor = Color.IndianRed; // Màu đỏ hồng
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(30, 250);
            btnHuy.Size = new Size(150, 45);
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.Cursor = Cursors.Hand;
            btnHuy.Click += BtnHuy_Click;

            // 6. Gắn tất cả "đồ chơi" này lên Form
            this.Controls.Add(lblTieuDeTongTien);
            this.Controls.Add(lblTongTien);
            this.Controls.Add(lblTieuDeKhachDua);
            this.Controls.Add(txtKhachDua);
            this.Controls.Add(lblTieuDeTienThua);
            this.Controls.Add(lblTienThua);
            this.Controls.Add(btnXacNhan);
            this.Controls.Add(btnHuy);
        }

        // --- CÁC HÀM XỬ LÝ LOGIC CHÍNH ---

        // Hàm 1: Chặn người dùng gõ chữ, chỉ cho gõ số vào ô tiền
        private void TxtKhachDua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Hàm 2: Tự động tính tiền thừa khi nhân viên đang gõ số
        private void TxtKhachDua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy số tiền gõ vào (xóa dấu chấm phẩy nếu có)
                string tienDuaStr = txtKhachDua.Text.Replace(".", "").Replace(",", "").Trim();

                if (decimal.TryParse(tienDuaStr, out decimal tienKhachDua))
                {
                    decimal tienThua = tienKhachDua - tongTienHoaDon;

                    if (tienThua >= 0)
                    {
                        lblTienThua.Text = tienThua.ToString("N0") + " đ";
                        lblTienThua.ForeColor = Color.MediumBlue; // Tiền dư màu xanh
                    }
                    else
                    {
                        lblTienThua.Text = "Khách đưa thiếu!";
                        lblTienThua.ForeColor = Color.Red; // Thiếu tiền báo đỏ
                    }
                }
                else
                {
                    lblTienThua.Text = "0 đ";
                    lblTienThua.ForeColor = Color.MediumBlue;
                }
            }
            catch { }
        }

        // Hàm 3: Bấm nút XÁC NHẬN
        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            string tienDuaStr = txtKhachDua.Text.Replace(".", "").Replace(",", "").Trim();
            decimal.TryParse(tienDuaStr, out decimal tienKhachDua);

            if (tienKhachDua < tongTienHoaDon)
            {
                MessageBox.Show("Khách đưa chưa đủ tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Chặn, không cho đóng Form
            }

            // Truyền tín hiệu "OK" về cho trang Bán Hàng rồi tự đóng
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Hàm 4: Bấm nút HỦY BỎ
        private void BtnHuy_Click(object sender, EventArgs e)
        {
            // Truyền tín hiệu "Cancel" về cho trang Bán Hàng
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Form_ThanhToan_Load(object sender, EventArgs e)
        {
            // Khi form vừa bật lên, nháy con trỏ chuột sẵn vào ô nhập tiền để gõ luôn cho lẹ
            txtKhachDua.Focus();
        }
    }
}