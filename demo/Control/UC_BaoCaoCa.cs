using demo.DAL;
using demo.BLL.Service;
using System.Data.Entity;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using DW = DocumentFormat.OpenXml.Wordprocessing;
using DrawingFont = System.Drawing.Font;
using PDF = iText.Layout;
using PDFE = iText.Layout.Element;
using iText.Layout;
using iText.Layout.Element;
using DrawingColor = System.Drawing.Color; // Thêm dòng này





namespace demo.Control
{
    public partial class UC_BaoCaoCa : UserControl
    {
        CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();//
        HoaDonService hoaDonService = new HoaDonService();
        DateTime tuNgay = DateTime.Now.AddDays(-7);
        DateTime denNgay = DateTime.Now;

        public UC_BaoCaoCa()
        {
            InitializeComponent();
        }

        private void UC_BaoCaoCa_Load(object sender, EventArgs e)
        {
            // Cấu hình giao diện
            lblDoanhThu.Left = pnlBanner.Width - lblDoanhThu.PreferredWidth - 20;
            flpTopSP.AutoScroll = true;
            flpTopSP.BackColor = DrawingColor.FromArgb(28, 32, 57); // Dùng DrawingColor ở đây

            FormatGridUI();
            dgvHoaDon.CellFormatting += dgvHoaDon_CellFormatting;

            // CHỈ CẦN GỌI DÒNG NÀY LÀ ĐỦ (Nó sẽ tự tính tổng và load bảng)
            RefreshBaoCao(DateTime.Now.AddDays(-7), DateTime.Now);

            var ca = LayCaHienTai();
            HienThiThoiGian(ca);
            FormatOrderGrid_Pro();
        }

        int TinhPhanTram(decimal hienTai, decimal truoc)
        {
            if (truoc == 0)
            {
                if (hienTai > 0) return 100;
                return 0;
            }

            return (int)((hienTai - truoc) * 100 / truoc);
        }

        void SetMau(Label lbl, int value)
        {
            if (value > 0)
                lbl.ForeColor = System.Drawing.Color.Green;
            else if (value < 0)
                lbl.ForeColor = System.Drawing.Color.Red;
            else
                lbl.ForeColor = System.Drawing.Color.Gray;
        }
        // homnay/tuan/thang
        // Thay thế hàm LoadData cũ bằng hàm này
        void RefreshBaoCao(DateTime fromDate, DateTime toDate)
        {
            // Giả sử lấy mã nhân viên là 1 (hoặc lấy từ UserSession.UserId nếu bạn đã có)
            int maNV = UserSession.MaNhanVien;

            // 1. Cập nhật các con số thống kê (Sử dụng Service để đồng bộ logic)
            int tongDon = hoaDonService.GetTongSoDon(maNV, fromDate, toDate);
            decimal doanhThu = hoaDonService.GetTongTien(maNV, fromDate, toDate);
            decimal donTB = tongDon == 0 ? 0 : doanhThu / tongDon;

            // Hiển thị lên Label
            lblTongDon_Value.Text = tongDon.ToString();
            lblDoanhThu_Value.Text = doanhThu.ToString("N0") + "đ";
            lblDonTB_Value.Text = donTB.ToString("N0") + "đ";
            lblDoanhThu.Text = String.Format("{0:N0} đ", doanhThu); // Label to trên banner

            // 2. Cập nhật DataGridView
            dgvHoaDon.DataSource = hoaDonService.GetHoaDonChiTiet(maNV, fromDate, toDate);

            // 3. Cập nhật Top Sản Phẩm (Truyền tham số ngày vào)
            LoadTopSanPham(maNV, fromDate, toDate);

            LoadLichSuCa(maNV, fromDate, toDate);
        }

        private void btnHomNay_Click(object sender, EventArgs e)
        {
            // Từ 00:00:00 đến 23:59:59 hôm nay
            RefreshBaoCao(DateTime.Today, DateTime.Today.AddDays(1).AddTicks(-1));
        }

        private void btnTuan_Click(object sender, EventArgs e)
        {
            RefreshBaoCao(DateTime.Now.AddDays(-7), DateTime.Now);
        }

        private void btnThang_Click(object sender, EventArgs e)
        {
            RefreshBaoCao(DateTime.Now.AddDays(-30), DateTime.Now);
        }
        
        //Bang datagridview bangca

        string LayCa(DateTime ngay)
        {
            int gio = ngay.Hour;

            if (gio >= 6 && gio < 14)
                return "Ca sáng";
            else if (gio >= 14 && gio < 22)
                return "Ca chiều";
            else
                return "Ca đêm";
        }

        //hienthithoigian
        void HienThiThoiGian(CA_LAM_VIEC ca = null)
        {
            if (ca != null)
            {
                lblThoiGian.Text =
                    $"📅 {DateTime.Now:dd/MM/yyyy HH:mm:ss} | " +
                    $"Ca {ca.TenCa} ({ca.GioBatDau:hh\\:mm} - {ca.GioKetThuc:hh\\:mm})";
            }
            else
            {
                lblThoiGian.Text = $"📅 {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            }
        }
        // top san pham
        void LoadTopSanPham(int maNV, DateTime tu, DateTime den)
        {
            var data = hoaDonService.GetTopSanPham(maNV, tu, den);

            flpTopSP.Controls.Clear();
            if (data.Count == 0) return;

            int max = data.Max(x => (int)x.GetType().GetProperty("SoLuong").GetValue(x));
            int stt = 1;

            foreach (var item in data)
            {
                var soLuong = (int)item.GetType().GetProperty("SoLuong").GetValue(item);
                var tenSP = item.GetType().GetProperty("TenSP").GetValue(item).ToString();

                flpTopSP.Controls.Add(
                    TaoItemTopSP_New(stt++, tenSP, soLuong, max)
                );
            }
        }
        Panel TaoItemTopSP_New(int stt, string tenSP, int soLuong, int max)
        {
            Panel p = new Panel();
            p.Width = flpTopSP.Width - 30;
            p.Height = 85; // Tăng nhẹ chiều cao
            p.Margin = new Padding(0, 5, 0, 5);
            p.BackColor = DrawingColor.FromArgb(40, 45, 70);

            // Áp dụng bo góc cho Panel (Dùng Region như bạn làm ở dưới)
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, 15, 15, 180, 90);
            path.AddArc(p.Width - 15, 0, 15, 15, 270, 90);
            path.AddArc(p.Width - 15, p.Height - 15, 15, 15, 0, 90);
            path.AddArc(0, p.Height - 15, 15, 15, 90, 90);
            p.Region = new Region(path);

            // Tên sản phẩm: Viết hoa chữ cái đầu, font chữ hiện đại
            Label lblTen = new Label
            {
                Text = $"{stt}. {tenSP}",
                ForeColor = DrawingColor.White,
                Font = new DrawingFont("Segoe UI Semibold", 10),
                Top = 15,
                Left = 15,
                AutoSize = true
            };

            // Số lượng: Nhấn mạnh bằng màu sắc
            Label lblSL = new Label
            {
                Text = soLuong + " sản phẩm",
                ForeColor = DrawingColor.FromArgb(0, 200, 150),
                Font = new DrawingFont("Segoe UI", 9),
                Top = 38,
                Left = 15,
                AutoSize = true
            };

            // Progress Bar Background (Làm mỏng lại nhìn sẽ sang hơn)
            Panel barBg = new Panel
            {
                Width = p.Width - 30,
                Height = 6,
                Top = 65,
                Left = 15,
                BackColor = DrawingColor.FromArgb(60, 65, 90)
            };

            Panel bar = new Panel
            {
                Height = 6,
                Width = max == 0 ? 0 : (int)((soLuong * 1.0 / max) * barBg.Width),
                BackColor = (stt <= 3) ? DrawingColor.Gold : DrawingColor.DeepSkyBlue
            };
            barBg.Controls.Add(bar);

            p.Controls.AddRange(new System.Windows.Forms.Control[] { lblTen, lblSL, barBg });
            return p;
        }
        void LoadData(DateTime fromDate, DateTime toDate)
{
    int maNhanVien = UserSession.MaNhanVien;

    // Cập nhật các Label (Sử dụng Service thay vì gọi db trực tiếp để đồng bộ)
    int tongDon = hoaDonService.GetTongSoDon(maNhanVien, fromDate, toDate);
    decimal doanhThu = hoaDonService.GetTongTien(maNhanVien, fromDate, toDate);
    decimal donTB = tongDon == 0 ? 0 : doanhThu / tongDon;

    lblTongDon_Value.Text = tongDon.ToString();
    lblDoanhThu_Value.Text = doanhThu.ToString("N0") + "đ";
    lblDonTB_Value.Text = donTB.ToString("N0") + "đ";
    
    // CẬP NHẬT LUÔN CẢ GRIDVIEW TẠI ĐÂY
    dgvHoaDon.DataSource = hoaDonService.GetHoaDonChiTiet(maNhanVien, fromDate, toDate);
}
        // xuất DL

        private void btnXuatTatCa_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Text file (*.txt)|*.txt|Word (*.docx)|*.docx|PDF (*.pdf)|*.pdf";
            sfd.Title = "Chọn nơi lưu báo cáo";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;

                if (path.EndsWith(".txt"))
                    XuatTXT(path);
                else if (path.EndsWith(".docx"))
                    XuatWord(path);
                else if (path.EndsWith(".pdf"))
                    XuatPDF(path);
            }
        }
        string TaoNoiDungBaoCao()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("===== BÁO CÁO CA =====");
            sb.AppendLine("Ngày: " + DateTime.Now.ToString("dd/MM/yyyy"));
            sb.AppendLine();

            sb.AppendLine("--- THỐNG KÊ ---");
            sb.AppendLine("Tổng đơn: " + lblTongDon_Value.Text);
            sb.AppendLine("Doanh thu: " + lblDoanhThu_Value.Text);
            sb.AppendLine("Đơn TB: " + lblDonTB_Value.Text);
            sb.AppendLine();

            sb.AppendLine("--- LỊCH SỬ CA ---");
            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (row.Cells[0].Value == null) continue;

                // Sửa tên cột cho đúng với Data Source
                string ngay = row.Cells["NgayLap"].Value.ToString(); // Đổi "Ngay" thành "NgayLap"
                string ca = LayCa(DateTime.Parse(ngay)); // Dùng hàm LayCa bạn đã viết
                string soDon = row.Cells["SoLuong"].Value.ToString(); // Đổi "SoDon" thành "SoLuong"
                string doanhThu = row.Cells["TongTien"].Value.ToString();

                sb.AppendLine($"{ngay} | {ca} | {soDon} | {doanhThu}");
            }

            sb.AppendLine();

            sb.AppendLine("--- TOP SẢN PHẨM ---");
            int stt = 1;
            foreach (System.Windows.Forms.Control ctrl in flpTopSP.Controls)
            {
                Panel p = ctrl as Panel;
                if (p == null) continue;

                Label lblTen = p.Controls[0] as Label;
                Label lblSL = p.Controls[1] as Label;

                sb.AppendLine($"{stt}. {lblTen.Text} - {lblSL.Text}");
                stt++;
            }

            return sb.ToString();
        }
        void XuatTXT(string path)
        {
            System.IO.File.WriteAllText(path, TaoNoiDungBaoCao(), Encoding.UTF8);

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true
            });
        }
       

void XuatWord(string path)
    {
        using (WordprocessingDocument doc =
            WordprocessingDocument.Create(path, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
        {
            MainDocumentPart mainPart = doc.AddMainDocumentPart();

            mainPart.Document = new DW.Document();
            DW.Body body = new DW.Body();

            string content = TaoNoiDungBaoCao();

            foreach (string line in content.Split('\n'))
            {
                DW.Paragraph p = new DW.Paragraph();
                DW.Run r = new DW.Run();
                r.Append(new DW.Text(line));

                p.Append(r);
                body.Append(p);
            }

            // 🔥 QUAN TRỌNG NHẤT
            mainPart.Document.Append(body);
        }

        // mở file
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
        {
            FileName = path,
            UseShellExecute = true
        });
    }
        void XuatPDF(string path)
        {
            try
            {
                // 1. Khởi tạo Writer và Document
                var writer = new iText.Kernel.Pdf.PdfWriter(path);
                var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
                PDF.Document document = new PDF.Document(pdf);

                // 2. CẤU HÌNH FONT TIẾNG VIỆT (Quan trọng nhất)
                // Lấy đường dẫn font Arial từ hệ thống Windows
                string fontPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf");

                // Nếu không tìm thấy Arial, có thể thay bằng font khác hỗ trợ Unicode
                if (!System.IO.File.Exists(fontPath))
                {
                    fontPath = "C:\\Windows\\Fonts\\tahoma.ttf";
                }

                var font = iText.Kernel.Font.PdfFontFactory.CreateFont(fontPath, iText.IO.Font.PdfEncodings.IDENTITY_H);
                document.SetFont(font);

                // 3. Lấy nội dung báo cáo
                string content = TaoNoiDungBaoCao();

                // 4. Xử lý xuống dòng
                // iText không tự hiểu ký tự \n trong 1 Paragraph là xuống dòng kiểu văn bản,
                // nên ta tách chuỗi và add từng dòng một.
                string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        document.Add(new PDFE.Paragraph("\n"));
                    }
                    else
                    {
                        // Set Margin để các dòng sát nhau giống văn bản bình thường
                        document.Add(new PDFE.Paragraph(line).SetMarginBottom(0).SetMarginTop(0));
                    }
                }

                // 5. Đóng document
                document.Close();

                // 6. Mở file sau khi xuất thành công
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = path,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void XuatBaoCaoTXT()
        {
            string path = @"D:\BaoCaoCa.txt";

            StringBuilder sb = new StringBuilder();

            // ===== TIÊU ĐỀ =====
            sb.AppendLine("===== BÁO CÁO CA LÀM VIỆC =====");
            sb.AppendLine("Ngày: " + DateTime.Now.ToString("dd/MM/yyyy"));
            sb.AppendLine();

            // ===== THỐNG KÊ =====
            sb.AppendLine("--- THỐNG KÊ ---");
            sb.AppendLine("Tổng đơn: " + lblTongDon_Value.Text);
            sb.AppendLine("Doanh thu: " + lblDoanhThu_Value.Text);
            sb.AppendLine("Đơn TB: " + lblDonTB_Value.Text);
            sb.AppendLine("Trả hàng: " + lblTraHang_Value.Text);
            sb.AppendLine();

            // ===== LỊCH SỬ CA =====
            sb.AppendLine("--- LỊCH SỬ CA ---");

            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (row.Cells[0].Value == null) continue;

                string ngay = row.Cells["Ngay"].Value.ToString();
                string ca = row.Cells["Ca"].Value.ToString();
                string soDon = row.Cells["SoDon"].Value.ToString();
                string doanhThu = row.Cells["DoanhThu"].Value.ToString();

                sb.AppendLine($"{ngay} | {ca} | {soDon} | {doanhThu}");
            }

            sb.AppendLine();

            // ===== TOP SẢN PHẨM =====
            sb.AppendLine("--- TOP SẢN PHẨM ---");

            int stt = 1;

            foreach (System.Windows.Forms.Control ctrl in flpTopSP.Controls)
            {
                Panel p = ctrl as Panel;
                if (p == null) continue;

                Label lblTen = p.Controls[0] as Label;
                Label lblSL = p.Controls[2] as Label;

                sb.AppendLine($"{stt}. {lblTen.Text} - {lblSL.Text}");
                stt++;
            }

            // ===== GHI FILE =====
            System.IO.File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            // ===== MỞ FILE =====
            System.Diagnostics.Process.Start(path);

            MessageBox.Show("Xuất báo cáo thành công!", "Thông báo");
        }
        void LoadDanhSachDonHang()
        {
            int maNhanVien = UserSession.MaNhanVien;

            var data = hoaDonService.GetDanhSachHoaDon(maNhanVien, DateTime.Now.AddDays(-7), DateTime.Now);

            dgvHoaDon.Columns.Clear(); // 🔥 QUAN TRỌNG

            dgvHoaDon.DataSource = data;
            FormatOrderGrid_Pro();

            // đổi tên cột
            dgvHoaDon.Columns["MaDon"].HeaderText = "Mã đơn";
            dgvHoaDon.Columns["SoLuongSP"].HeaderText = "Số lượng SP";
            dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvHoaDon.Columns["TrangThai"].HeaderText = "Trạng thái";

          

            // format tiền
            dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";

            FormatGridUI();
        }
        void FormatGridUI()
        {
            dgvHoaDon.BorderStyle = BorderStyle.None;
            dgvHoaDon.BackgroundColor = System.Drawing.Color.FromArgb(28, 32, 57);

            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(28, 32, 57);
            dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

            dgvHoaDon.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(28, 32, 57);
            dgvHoaDon.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvHoaDon.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(50, 60, 100);

            dgvHoaDon.RowTemplate.Height = 40;
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void dgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "TongTien")
            {
                e.CellStyle.ForeColor = System.Drawing.Color.LightGreen;
            }

            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "TrangThai")
            {
                e.CellStyle.BackColor = System.Drawing.Color.Green;
                e.CellStyle.ForeColor = System.Drawing.Color.White;
            }
        }
        //giờ làm
        CA_LAM_VIEC LayCaHienTai()
        {
            var now = DateTime.Now.TimeOfDay;

            return db.CA_LAM_VIEC
                .ToList()
                .FirstOrDefault(c =>
                    (c.GioBatDau <= c.GioKetThuc && now >= c.GioBatDau && now <= c.GioKetThuc)
                 || (c.GioBatDau > c.GioKetThuc && (now >= c.GioBatDau || now <= c.GioKetThuc))
                );
        }
        void LoadLichSuCa(int maNV, DateTime tu, DateTime den)
        {
            var data = hoaDonService.GetLichSuCa(maNV, tu, den);
            flpLichSuCa.Controls.Add(TaoHeaderLichSuCa());
            flpLichSuCa.Controls.Clear();

            foreach (var item in data)
            {
                flpLichSuCa.Controls.Add(
                    TaoItemLichSuCa(item)
                );
            }
        }

        Panel TaoHeaderLichSuCa()
        {
            Panel header = new Panel();
            header.Width = flpLichSuCa.Width - 25;
            header.Height = 50; // Tăng chiều cao một chút
            header.BackColor = DrawingColor.Transparent; // Để nền trôi theo FlowLayout
            header.Margin = new Padding(5, 10, 5, 0);

            Label lbl = new Label();
            lbl.Text = "🕘 LỊCH SỬ CA LÀM VIỆC";
            lbl.ForeColor = DrawingColor.FromArgb(140, 150, 180); // Màu xám xanh sang trọng
            lbl.Font = new DrawingFont("Segoe UI", 11, FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Left = 10;
            lbl.Top = 15;

            header.Controls.Add(lbl);
            return header;
        }
        Panel TaoItemLichSuCa(LichSuCaDTO item)
        {
            Panel p = new Panel();
            p.Width = flpLichSuCa.Width - 30;
            p.Height = 100; // Tăng chiều cao để thoáng hơn
            p.BackColor = DrawingColor.FromArgb(40, 45, 70); // Màu Surface nhẹ hơn nền chính
            p.Margin = new Padding(5, 8, 5, 8);

            // --- Hiệu ứng Bo góc ---
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 15;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(p.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(p.Width - radius, p.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, p.Height - radius, radius, radius, 90, 90);
            p.Region = new Region(path);

            // --- Thanh nhấn màu bên trái (Accent Bar) ---
            Panel accentBar = new Panel();
            accentBar.Width = 5;
            accentBar.Height = p.Height;
            accentBar.Dock = DockStyle.Left;
            // Đổi màu theo tên ca
            if (item.TenCa.Contains("sáng")) accentBar.BackColor = DrawingColor.Orange;
            else if (item.TenCa.Contains("chiều")) accentBar.BackColor = DrawingColor.DeepSkyBlue;
            else accentBar.BackColor = DrawingColor.MediumPurple; // Ca đêm

            // --- Thông tin Ca & Ngày ---
            Label lblTitle = new Label
            {
                Text = $"{item.TenCa} • {item.Ngay:dd/MM/yyyy}",
                ForeColor = DrawingColor.White,
                Font = new DrawingFont("Segoe UI Semibold", 11),
                Left = 20,
                Top = 15,
                AutoSize = true
            };

            Label lblTime = new Label
            {
                Text = $"🕒 {item.GioBatDau:hh\\:mm} - {item.GioKetThuc:hh\\:mm}",
                ForeColor = DrawingColor.FromArgb(160, 160, 180),
                Font = new DrawingFont("Segoe UI", 9),
                Left = 20,
                Top = 40,
                AutoSize = true
            };

            // --- Khối thống kê bên trong Card ---
            // Số đơn
            Label lblDonVal = new Label
            {
                Text = item.SoDon.ToString().PadLeft(2, '0'),
                ForeColor = DrawingColor.FromArgb(0, 200, 150),
                Font = new DrawingFont("Segoe UI", 12, FontStyle.Bold),
                Left = 20,
                Top = 65,
                AutoSize = true
            };
            Label lblDonText = new Label
            {
                Text = "đơn hàng",
                ForeColor = DrawingColor.Gray,
                Font = new DrawingFont("Segoe UI", 8),
                Left = 50,
                Top = 70,
                AutoSize = true
            };

            // Doanh thu (Căn lề phải)
            Label lblDTVal = new Label
            {
                Text = $"{item.DoanhThu:N0} đ",
                ForeColor = DrawingColor.Gold,
                Font = new DrawingFont("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                Width = 150,
                Top = 65,
                Left = p.Width - 170
            };

            p.Controls.AddRange(new System.Windows.Forms.Control[] {
        accentBar, lblTitle, lblTime, lblDonVal, lblDonText, lblDTVal
    });

            return p;
        }
        void FormatOrderGrid_Pro()
        {
            // --- 1. Thiết lập chung (Nền và Viền) ---
            dgvHoaDon.BackgroundColor = DrawingColor.FromArgb(28, 32, 57);
            dgvHoaDon.BorderStyle = BorderStyle.None;
            dgvHoaDon.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHoaDon.GridColor = DrawingColor.FromArgb(45, 50, 80);

            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.RowHeadersVisible = false;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.MultiSelect = false;
            dgvHoaDon.AllowUserToResizeRows = false;

            // --- 2. Thiết kế Tiêu đề (Header) ---
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = DrawingColor.FromArgb(28, 32, 57);
            headerStyle.ForeColor = DrawingColor.FromArgb(140, 150, 180);
            headerStyle.Font = new DrawingFont("Segoe UI Semibold", 10);
            headerStyle.SelectionBackColor = DrawingColor.FromArgb(28, 32, 57);
            headerStyle.Padding = new Padding(10, 0, 0, 0);

            dgvHoaDon.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvHoaDon.ColumnHeadersHeight = 45;

            // --- 3. Thiết kế Dòng dữ liệu (Rows) ---
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            rowStyle.BackColor = DrawingColor.FromArgb(28, 32, 57);
            rowStyle.ForeColor = DrawingColor.White;
            rowStyle.Font = new DrawingFont("Segoe UI", 10);
            rowStyle.SelectionBackColor = DrawingColor.FromArgb(50, 60, 100);
            rowStyle.SelectionForeColor = DrawingColor.White;
            rowStyle.Padding = new Padding(10, 0, 0, 0);

            dgvHoaDon.DefaultCellStyle = rowStyle;
            dgvHoaDon.RowTemplate.Height = 45;

            // --- 4. Tự động dãn cột ---
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

      
    }
}
