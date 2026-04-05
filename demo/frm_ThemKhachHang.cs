using demo.BLL.Service;
using demo.DAL; // Hoặc thư mục chứa Entity của bạn
using System;
using System.Windows.Forms;

namespace demo
{
    public partial class FrmThemKhachHang : Form
    {
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
    }
}