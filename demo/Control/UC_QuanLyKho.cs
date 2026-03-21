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
        public UC_QuanLyKho()
        {
            InitializeComponent();
            service = new InventoryService();
        }

        private void UC_QuanLyKho_Load(object sender, EventArgs e)
        {
            dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadDashboard();
            LoadData();
        }
        private void LoadDashboard()
        {
            lblTongMatHang.Text = service.GetTongMatHang().ToString();
            lblTonThap.Text = service.GetTonThap().ToString();
            lblSapHetHan.Text = service.GetSapHetHan().ToString();
            lblGiaTriKho.Text = service.GetGiaTriKho().ToString("N0");
        }

        private void LoadData()
        {
            dgvTonKho.DataSource = service.GetTonKho();

            dgvTonKho.Columns["MaSP"].HeaderText = "Mã";
            dgvTonKho.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvTonKho.Columns["SoLuong"].HeaderText = "Tồn hiện tại";
            dgvTonKho.Columns["TonToiThieu"].HeaderText = "Tồn tối thiểu";
            dgvTonKho.Columns["TrangThai"].HeaderText = "Trạng thái";
        }
    }
}
