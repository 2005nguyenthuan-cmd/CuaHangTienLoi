using demo.DAL;
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

using PDF = iText.Layout;
using PDFE = iText.Layout.Element;
using iText.Layout;
using iText.Layout.Element;




namespace demo.Control
{
    public partial class UC_BaoCaoCa : UserControl
    {
        CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();
        public UC_BaoCaoCa()
        {
            InitializeComponent();
        }

        private void UC_BaoCaoCa_Load(object sender, EventArgs e)
        {
            lblDoanhThu.Left = pnlBanner.Width - lblDoanhThu.PreferredWidth - 20;
            lblTong.Left = pnlBanner.Width - lblTong.Width - 20;
            LoadData(DateTime.Now.AddDays(-7), DateTime.Now);
            int maNhanVien = 1;

            //loadlichsuca 
            LoadLichSuCa();
            //top san pham
            LoadTopSanPham();

            // ===== DỮ LIỆU HIỆN TẠI =====
            int tongDon = db.HOA_DON
                .Where(x => x.MaNhanVien == maNhanVien)
                .Count();

            decimal doanhThu = db.HOA_DON
                .Where(x => x.MaNhanVien == maNhanVien)
                .Sum(x => (decimal?)x.TongTien) ?? 0;

            decimal donTB = tongDon == 0 ? 0 : doanhThu / tongDon;

            int traHang = 0;

            // ===== HIỂN THỊ =====
            lblTongDon_Value.Text = tongDon.ToString();
            lblDoanhThu_Value.Text = doanhThu.ToString("N0") + "đ";
            lblDoanhThu.Text = String.Format("{0:N0} đ", doanhThu);
            lblDonTB_Value.Text = donTB.ToString("N0") + "đ";
            lblTraHang_Value.Text = traHang.ToString();

            // ===== TUẦN TRƯỚC =====
            var tuanTruoc2 = DateTime.Now.AddDays(-14);
            var tuanGanNhat = DateTime.Now.AddDays(-7);

            int tongDonTuanTruoc = db.HOA_DON
                .Where(x => x.MaNhanVien == maNhanVien
                    && x.NgayLap >= tuanTruoc2
                    && x.NgayLap < tuanGanNhat)
                .Count();

            decimal doanhThuTuanTruoc = db.HOA_DON
                .Where(x => x.MaNhanVien == maNhanVien
                    && x.NgayLap >= tuanTruoc2
                    && x.NgayLap < tuanGanNhat)
                .Sum(x => (decimal?)x.TongTien) ?? 0;

            decimal donTBTuanTruoc = tongDonTuanTruoc == 0
                ? 0
                : doanhThuTuanTruoc / tongDonTuanTruoc;

            // ===== TÍNH % =====
            int ptTongDon = TinhPhanTram(tongDon, tongDonTuanTruoc);
            int ptDoanhThu = TinhPhanTram(doanhThu, doanhThuTuanTruoc);
            int ptDonTB = TinhPhanTram(donTB, donTBTuanTruoc);
            int ptTraHang = 0;

            // ===== GÁN % =====
            lblTongDon_Percent.Text = ptTongDon + "%";
            lblDoanhThu_Percent.Text = ptDoanhThu + "%";
            lblDonTB_Percent.Text = ptDonTB + "%";
            lblTraHang_Percent.Text = ptTraHang + "%";

            // ===== ĐỔI MÀU =====
            SetMau(lblTongDon_Percent, ptTongDon);
            SetMau(lblDoanhThu_Percent, ptDoanhThu);
            SetMau(lblDonTB_Percent, ptDonTB);
            SetMau(lblTraHang_Percent, ptTraHang);
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
        void LoadData(DateTime fromDate, DateTime toDate)
        {
            int maNhanVien = 1;

            int tongDon = db.HOA_DON
                .Where(x => x.MaNhanVien == maNhanVien
                    && x.NgayLap >= fromDate
                    && x.NgayLap <= toDate)
                .Count();

            decimal doanhThu = db.HOA_DON
                .Where(x => x.MaNhanVien == maNhanVien
                    && x.NgayLap >= fromDate
                    && x.NgayLap <= toDate)
                .Sum(x => (decimal?)x.TongTien) ?? 0;

            decimal donTB = tongDon == 0 ? 0 : doanhThu / tongDon;

            lblTongDon_Value.Text = tongDon.ToString();
            lblDoanhThu_Value.Text = doanhThu.ToString("N0") + "đ";
            lblDonTB_Value.Text = donTB.ToString("N0") + "đ";
        }

        private void btnHomNay_Click(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            LoadData(today, today.AddDays(1));
        }

        private void btnTuan_Click(object sender, EventArgs e)
        {
            LoadData(DateTime.Now.AddDays(-7), DateTime.Now);
        }

        private void btnThang_Click(object sender, EventArgs e)
        {
            LoadData(DateTime.Now.AddDays(-30), DateTime.Now);
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
        void LoadLichSuCa()
        {
            var data = db.HOA_DON
                .ToList() // bắt buộc
                .GroupBy(x => new { x.NgayLap.Value.Date, Ca = LayCa(x.NgayLap.Value) })
                .Select(g => new
                {
                    Ngay = g.Key.Date,
                    Ca = g.Key.Ca,
                    SoDon = g.Count(),
                    DoanhThu = g.Sum(x => x.TongTien),
                    TrangThai = g.Key.Date == DateTime.Today ? "🟢 Đang làm" : "✓ Hoàn tất"
                })
                .OrderByDescending(x => x.Ngay)
                .Take(5)
                .ToList();

            dgvLichSuCa.DataSource = data;
            dgvLichSuCa.Columns["Ngay"].HeaderText = "Ngày";
            dgvLichSuCa.Columns["Ca"].HeaderText = "Ca";
            dgvLichSuCa.Columns["SoDon"].HeaderText = "Số đơn";
            dgvLichSuCa.Columns["DoanhThu"].HeaderText = "Doanh thu";
            dgvLichSuCa.Columns["TrangThai"].HeaderText = "Trạng thái";

            // format ngày
            dgvLichSuCa.Columns["Ngay"].DefaultCellStyle.Format = "dd/MM";

            // format tiền
            dgvLichSuCa.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
        }

        // top san pham
        void LoadTopSanPham()
        {
            var data = db.CHI_TIET_HOA_DON
                .GroupBy(x => x.SAN_PHAM.TenSanPham)
                .Select(g => new
                {
                    TenSP = g.Key,
                    SoLuong = g.Sum(x => (int?)x.SoLuong) ?? 0
                })
                .OrderByDescending(x => x.SoLuong)
                .Take(5)
                .ToList();

            flpTopSP.Controls.Clear();

            int max = data.Max(x => x.SoLuong);

            int stt = 1;

            foreach (var item in data)
            {
                flpTopSP.Controls.Add(
                    TaoItemTopSP(stt++, item.TenSP, item.SoLuong, max)
                );
            }
        }
        Panel TaoItemTopSP(int stt, string tenSP, int soLuong, int max)
        {
            Panel p = new Panel();
            p.Width = 300;
            p.Height = 60;

            // Label tên
            Label lblTen = new Label();
            lblTen.Text = stt + ". " + tenSP;
            lblTen.Top = 5;
            lblTen.Left = 5;
            lblTen.Width = 200;

            // Thanh progress
            Panel barBg = new Panel();
            barBg.Width = 200;
            barBg.Height = 10;
            barBg.Top = 30;
            barBg.Left = 5;
            barBg.BackColor = System.Drawing.Color.LightGray;

            Panel bar = new Panel();
            bar.Height = 10;
            bar.BackColor = System.Drawing.Color.Green;

            int width = (int)((soLuong * 1.0 / max) * barBg.Width);
            bar.Width = width;

            barBg.Controls.Add(bar);

            // Số lượng
            Label lblSL = new Label();
            lblSL.Text = soLuong.ToString();
            lblSL.Top = 25;
            lblSL.Left = 220;

            p.Controls.Add(lblTen);
            p.Controls.Add(barBg);
            p.Controls.Add(lblSL);

            return p;
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
            foreach (DataGridViewRow row in dgvLichSuCa.Rows)
            {
                if (row.Cells[0].Value == null) continue;

                sb.AppendLine($"{row.Cells["Ngay"].Value} | {row.Cells["Ca"].Value} | {row.Cells["SoDon"].Value} | {row.Cells["DoanhThu"].Value}");
            }

            sb.AppendLine();

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

            foreach (DataGridViewRow row in dgvLichSuCa.Rows)
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

    }
}
