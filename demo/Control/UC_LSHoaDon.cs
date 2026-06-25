using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using demo.DAL;

namespace demo.Control
{
    public partial class UC_LSHoaDon : UserControl
    {
        CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();

        public UC_LSHoaDon()
        {
            InitializeComponent();
            LoadHoaDon();
            TrangDiemGiaoDien(); // Gọi hàm làm đẹp khi vừa mở Form
        }

        // =================================================================
        // HÀM LÀM ĐẸP GIAO DIỆN BẰNG CODE
        // =================================================================
        private void TrangDiemGiaoDien()
        {
            // 1. Đổi màu nền toàn bộ trang cho sáng sủa
            this.BackColor = Color.FromArgb(244, 246, 249);

            // 2. Tút lại cái bảng DataGridView (Xóa viền, đổi màu Header)
            if (dgvHoaDon != null)
            {
                dgvHoaDon.BackgroundColor = Color.White;
                dgvHoaDon.BorderStyle = BorderStyle.None;
                dgvHoaDon.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvHoaDon.GridColor = Color.FromArgb(230, 230, 230);
                dgvHoaDon.EnableHeadersVisualStyles = false;

                // Tiêu đề bảng
                dgvHoaDon.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(43, 45, 66); // Xanh đen sang trọng
                dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvHoaDon.ColumnHeadersHeight = 40;

                // Các dòng dữ liệu
                dgvHoaDon.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 238, 246); // Xanh nhạt khi nhấp chọn
                dgvHoaDon.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgvHoaDon.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                dgvHoaDon.RowTemplate.Height = 35;
                dgvHoaDon.RowHeadersVisible = false; // Ẩn cái cột mũi tên vô duyên bên trái

                // Tự động kéo dãn các cột cho lấp đầy khoảng trống
                dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // =================================================================
        // HÀM ĐỔI TÊN CỘT VÀ ĐỊNH DẠNG SỐ TIỀN HIỂN THỊ
        // =================================================================
        private void FormatCotDataGridView()
        {
            if (dgvHoaDon.Columns.Count > 0)
            {
                dgvHoaDon.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";

                dgvHoaDon.Columns["NgayLap"].HeaderText = "Ngày Lập";
                dgvHoaDon.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; // Định dạng chuẩn VN

                dgvHoaDon.Columns["SoLuongSP"].HeaderText = "Số Lượng Món";
                dgvHoaDon.Columns["SoLuongSP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng Tiền (VNĐ)";
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0"; // Có dấu chấm ngàn (VD: 100.000)
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.ForeColor = Color.DarkRed; // Đỏ đô cho dễ nhìn tiền
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
        }

        private void LoadHoaDon()
        {
            // Mẹo nhỏ: Thêm (int?) và (decimal?) để lỡ hóa đơn không có chi tiết thì phần mềm không bị văng lỗi (Crash)
            var data = db.HOA_DON
                .Select(hd => new
                {
                    hd.MaHoaDon,
                    hd.NgayLap,
                    SoLuongSP = db.CHI_TIET_HOA_DON
                        .Where(ct => ct.MaHoaDon == hd.MaHoaDon)
                        .Sum(ct => (int?)ct.SoLuong) ?? 0,

                    TongTien = db.CHI_TIET_HOA_DON
                        .Where(ct => ct.MaHoaDon == hd.MaHoaDon)
                        .Sum(ct => (decimal?)ct.ThanhTien) ?? 0
                })
                .OrderByDescending(x => x.NgayLap) // Sắp xếp hóa đơn mới nhất lên đầu
                .ToList();

            dgvHoaDon.DataSource = data;
            FormatCotDataGridView(); // Gọi hàm dịch tiếng Việt
        }

        private void UC_LSHoaDon_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
        }

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtTuNgay.Value.Date; // Lấy mốc 00:00:00
            DateTime denNgay = dtDenNgay.Value.Date.AddDays(1).AddSeconds(-1); // Lấy đến 23:59:59 của ngày kết thúc

            var data = db.HOA_DON
                .Where(hd => hd.NgayLap >= tuNgay && hd.NgayLap <= denNgay)
                .Select(hd => new
                {
                    hd.MaHoaDon,
                    hd.NgayLap,
                    SoLuongSP = db.CHI_TIET_HOA_DON
                        .Where(ct => ct.MaHoaDon == hd.MaHoaDon)
                        .Sum(ct => (int?)ct.SoLuong) ?? 0,

                    TongTien = db.CHI_TIET_HOA_DON
                        .Where(ct => ct.MaHoaDon == hd.MaHoaDon)
                        .Sum(ct => (decimal?)ct.ThanhTien) ?? 0
                })
                .OrderByDescending(x => x.NgayLap)
                .ToList();

            dgvHoaDon.DataSource = data;
            FormatCotDataGridView();
        }

        private void btn_XoaLoc_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            

            dtTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtDenNgay.Value = DateTime.Now;

            LoadHoaDon();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Chặn tiếng "Bíp" khi nhấn Enter

                string keyword = txtSearch.Text.Trim();

                var data = db.HOA_DON
                    .Where(hd => hd.MaHoaDon.ToString().Contains(keyword))
                    .Select(hd => new
                    {
                        hd.MaHoaDon,
                        hd.NgayLap,
                        SoLuongSP = db.CHI_TIET_HOA_DON
                            .Where(ct => ct.MaHoaDon == hd.MaHoaDon)
                            .Sum(ct => (int?)ct.SoLuong) ?? 0,

                        TongTien = db.CHI_TIET_HOA_DON
                            .Where(ct => ct.MaHoaDon == hd.MaHoaDon)
                            .Sum(ct => (decimal?)ct.ThanhTien) ?? 0
                    })
                    .OrderByDescending(x => x.NgayLap)
                    .ToList();

                dgvHoaDon.DataSource = data;
                FormatCotDataGridView();
            }
        }
    }
}