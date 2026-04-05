using demo.BLL.Service;
using demo.DAL; // Hoặc thư mục chứa Entity của bạn
using System;
using System.Windows.Forms;

namespace demo
{
    public partial class FrmThemKhachHang : Form
    {

        KHACH_HANG khachHangDangSua = null;
        public FrmThemKhachHang(KHACH_HANG kh)
        {

            InitializeComponent();
            khachHangDangSua = kh;

            txtTen.Text = kh.TenKhachHang;
            txtSDT.Text = kh.SoDienThoai;
            txtDiem.Text = kh.DiemTichLuy.ToString();

            // Nếu là sửa → tắt nút thêm
            button2.Enabled = false;
        }

        public int MaKhachMoi { get; private set; } = 0;


        public FrmThemKhachHang()
        {
            InitializeComponent();

            // Lấy thẳng quyền từ UserSession của bạn để check
            // Lưu ý: Chữ "Quản lý" phải khớp y chang với dữ liệu trong cột TenVaiTro ở SQL
            if (UserSession.VaiTro == "Quản lý")
            {
                // -- QUẢN LÝ --
                txtDiem.Visible = true;
                txtDiem.ReadOnly = false;
            }
            else
            {
                // -- NHÂN VIÊN --
                txtDiem.Visible = false;
                txtDiem.Text = "0"; // Gán ngầm bằng 0 để lúc lưu không bị lỗi
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Họ tên và Số điện thoại!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = new CUA_HANG_TIEN_LOI_Entities())
                {
                    KHACH_HANG kh = new KHACH_HANG();
                    kh.TenKhachHang = txtTen.Text.Trim();
                    kh.SoDienThoai = txtSDT.Text.Trim();

                    // Xử lý an toàn cho trường hợp Quản lý xóa rỗng ô điểm
                    if (string.IsNullOrWhiteSpace(txtDiem.Text)) txtDiem.Text = "0";
                    kh.DiemTichLuy = int.Parse(txtDiem.Text);

                    db.KHACH_HANG.Add(kh);
                    db.SaveChanges();

                    MaKhachMoi = kh.MaKhachHang;
                }

                MessageBox.Show("Thêm khách hàng thành công!", "Hoàn tất");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (khachHangDangSua == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa!");
                return;
            }

            using (var db = new CUA_HANG_TIEN_LOI_Entities())
            {
                var kh = db.KHACH_HANG.Find(khachHangDangSua.MaKhachHang);

                if (kh != null)
                {
                    kh.TenKhachHang = txtTen.Text;
                    kh.SoDienThoai = txtSDT.Text;

                    int diem = 0;
                    int.TryParse(txtDiem.Text, out diem);
                    kh.DiemTichLuy = diem;

                    db.SaveChanges();
                }
            }

            MessageBox.Show("Cập nhật thành công!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}