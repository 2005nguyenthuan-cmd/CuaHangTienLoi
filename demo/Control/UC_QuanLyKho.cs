using demo.BLL.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo.Control
{
    public partial class UC_QuanLyKho : UserControl
    {
        private readonly InventoryService service;
        private readonly ImportService importService;

        //  giỏ nhập
        private List<ChiTietNhapDTO> gioNhap;
        private List<KiemKeDTO> dsKiemKe;

        public UC_QuanLyKho()
        {
            InitializeComponent();
            service = new InventoryService();
            importService = new ImportService();
            gioNhap = new List<ChiTietNhapDTO>();
        }

        private void UC_QuanLyKho_Load(object sender, EventArgs e)
        {
            dgvTonkho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTietNhap.ReadOnly = true;
            LoadDashboard();
            LoadTonKho();

            //  load tab nhập hàng
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSanPham.ReadOnly = true;
            dgvChiTietNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTietNhap.ReadOnly = true;
            LoadNCC();
            LoadSanPham();

            // load tab kiểm kê
             dgvKiemKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadKiemKe();

            // load tab cảnh báo
            dgvCanhBao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCanhBao.ReadOnly = true;
            LoadCanhBao();

        }

        // ================= DASHBOARD =================
        private void LoadDashboard()
        {
            lblTongMatHang.Text = service.GetTongMatHang().ToString();
            lblTonThap.Text = service.GetTonThap().ToString();
            lblSapHetHan.Text = service.GetSapHetHan().ToString();
            lblGiaTriKho.Text = service.GetGiaTriKho().ToString("N0");
        }

        // ================= TỒN KHO =================
        private void LoadTonKho()
        {
            dgvTonkho.DataSource = service.GetTonKho();

            dgvTonkho.Columns["MaSP"].HeaderText = "Mã";
            dgvTonkho.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvTonkho.Columns["SoLuong"].HeaderText = "Tồn hiện tại";
            dgvTonkho.Columns["TonToiThieu"].HeaderText = "Tồn tối thiểu";
            dgvTonkho.Columns["TrangThai"].HeaderText = "Trạng thái";

            HighlightTonKho();
        }
        private void HighlightTonKho()
        {
            foreach (DataGridViewRow row in dgvTonkho.Rows)
            {
                if (row.Cells["TrangThai"].Value == null) continue;

                string trangThai = row.Cells["TrangThai"].Value.ToString();

                if (trangThai == "Tồn thấp")
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                }
            }
        }

        // ================= NHẬP HÀNG =================

        private void LoadNCC()
        {
            cbNhaCungCap.DataSource = importService.GetSuppliers();
            cbNhaCungCap.DisplayMember = "TenNCC";
            cbNhaCungCap.ValueMember = "MaNCC";
        }

        private void LoadSanPham()
        {
            dgvSanPham.DataSource = importService.GetProducts();

            dgvSanPham.Columns["MaSanPham"].HeaderText = "Mã";
            dgvSanPham.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns["GiaBan"].HeaderText = "Giá bán";
            dgvSanPham.Columns["TenDanhMuc"].HeaderText = "Danh mục";
        }

        private void LoadGioNhap()
        {
            dgvChiTietNhap.DataSource = null;
            dgvChiTietNhap.DataSource = gioNhap;
            dgvChiTietNhap.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        //  Thêm vào giỏ
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;

            int maSP = (int)dgvSanPham.CurrentRow.Cells["MaSanPham"].Value;
            string tenSP = dgvSanPham.CurrentRow.Cells["TenSanPham"].Value.ToString();

            int soLuong = (int)numSoLuong.Value;
            decimal giaNhap;

            if (!decimal.TryParse(txtGiaNhap.Text, out giaNhap))
            {
                MessageBox.Show("Giá nhập không hợp lệ!");
                return;
            }

            if (soLuong <= 0 || giaNhap <= 0)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
                return;
            }

            DateTime hsd = dtHSD.Value;

            var exist = gioNhap.FirstOrDefault(x => x.MaSP == maSP);

            if (exist != null)
            {
                exist.SoLuong += soLuong;
            }
            else
            {
                gioNhap.Add(new ChiTietNhapDTO
                {
                    MaSP = maSP,
                    TenSP = tenSP,
                    SoLuong = soLuong,
                    GiaNhap = giaNhap,
                    HanSuDung = hsd
                });
            }

            LoadGioNhap();
        }

        //  Xóa dòng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChiTietNhap.CurrentRow == null) return;

            int index = dgvChiTietNhap.CurrentRow.Index;
            gioNhap.RemoveAt(index);

            LoadGioNhap();
        }

        //  Lưu phiếu nhập
        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (gioNhap.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm!");
                return;
            }

            int maNCC = (int)cbNhaCungCap.SelectedValue;

            int maPhieu = importService.CreatePhieuNhap(maNCC, gioNhap);

            MessageBox.Show("Tạo phiếu thành công! Mã: " + maPhieu);

            gioNhap.Clear();
            LoadGioNhap();

            // reload lại kho + dashboard
            LoadTonKho();
            LoadDashboard();
        }

        //================= KIỂM KÊ =================
        private void LoadKiemKe()
        {
            dsKiemKe = service.GetDataKiemKe();

            dgvKiemKe.DataSource = dsKiemKe;

            dgvKiemKe.Columns["MaSP"].HeaderText = "Mã";
            dgvKiemKe.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvKiemKe.Columns["SoLuongHeThong"].HeaderText = "Hệ thống";
            dgvKiemKe.Columns["SoLuongThucTe"].HeaderText = "Thực tế";
            dgvKiemKe.Columns["ChenhLech"].HeaderText = "Chênh lệch";

            dgvKiemKe.Columns["SoLuongThucTe"].ReadOnly = false;
            HighlightKiemKe();
        }
        private void HighlightKiemKe()
        {
            foreach (DataGridViewRow row in dgvKiemKe.Rows)
            {
                if (row.Cells["ChenhLech"].Value == null) continue;

                int diff = Convert.ToInt32(row.Cells["ChenhLech"].Value);

                if (diff != 0)
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
        }
        private void btnTinh_Click(object sender, EventArgs e)
        {
            dgvKiemKe.EndEdit();

            dsKiemKe = service.KiemKe(dsKiemKe);

            dgvKiemKe.DataSource = null;
            dgvKiemKe.DataSource = dsKiemKe;
        }
        private void btnLuuKiemKe_Click(object sender, EventArgs e)
        {
            service.SaveKiemKe(dsKiemKe);

            MessageBox.Show("Đã cập nhật tồn kho!");

            LoadTonKho();
            LoadDashboard();
        }

        //=================== CẢNH BÁO TỒN KHO =================
        private void LoadCanhBao()
        {
            dgvCanhBao.DataSource = service.GetCanhBao();

            dgvCanhBao.Columns["MaSP"].HeaderText = "Mã";
            dgvCanhBao.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvCanhBao.Columns["SoLuong"].HeaderText = "Tồn";
            dgvCanhBao.Columns["HanSuDung"].HeaderText = "HSD";
            dgvCanhBao.Columns["LoaiCanhBao"].HeaderText = "Cảnh báo";

            dgvCanhBao.Columns["HanSuDung"].DefaultCellStyle.Format = "dd/MM/yyyy";

            HighlightCanhBao();
        }
        private void HighlightCanhBao()
        {
            foreach (DataGridViewRow row in dgvCanhBao.Rows)
            {
                if (row.Cells["LoaiCanhBao"].Value == null) continue;

                string type = row.Cells["LoaiCanhBao"].Value.ToString();

                if (type == "Tồn thấp")
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                }
                else if (type == "Sắp hết hạn")
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
        }
        private void cbLocCanhBao_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = cbLocCanhBao.Text;

            var data = service.GetCanhBao();

            if (filter != "Tất cả")
            {
                data = data.Where(x => x.LoaiCanhBao == filter).ToList();
            }

            dgvCanhBao.DataSource = data;
        }

        private void txtSearchSP_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchSP.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
                LoadSanPham();

            dgvSanPham.DataSource = importService.SearchProducts(keyword);
        }

        private void dgvChiTietNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvChiTietNhap.Rows[e.RowIndex];

            txtSearchSP.Text = row.Cells["TenSP"].Value.ToString();

            numSoLuong.Value = Convert.ToInt32(row.Cells["SoLuong"].Value);

            txtGiaNhap.Text = row.Cells["GiaNhap"].Value.ToString();

            dtHSD.Value = Convert.ToDateTime(row.Cells["HanSuDung"].Value);
        }
    }
}
