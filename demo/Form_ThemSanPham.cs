using demo.DAL;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace demo
{
    public partial class Form_ThemSanPham : Form
    {
        private readonly CUA_HANG_TIEN_LOI_Entities db = new CUA_HANG_TIEN_LOI_Entities();
        private int maSP;
        private string pathImage = string.Empty;

        public Form_ThemSanPham()
        {
            InitializeComponent();
            LoadDanhMuc();
            UpdateImagePreview();
        }

        public Form_ThemSanPham(int id)
            : this()
        {
            maSP = id;
            SAN_PHAM sp = db.SAN_PHAM.Find(id);

            if (sp == null)
            {
                return;
            }

            txtMaSP.Text = sp.MaSanPham.ToString();
            txtTenSP.Text = sp.TenSanPham;
            txtGiaBan.Text = sp.GiaBan.ToString();
            txtTonKho.Text = sp.SoLuongTon.ToString();
            cbDanhMuc.SelectedValue = sp.MaDanhMuc;
            pathImage = NormalizeStoredImageName(sp.HinhAnh);
            btnThem.Text = "C\u1eadp Nh\u1eadt";
            label1.Text = "C\u1eadp Nh\u1eadt S\u1ea3n Ph\u1ea9m";
            Text = "C\u1eadp Nh\u1eadt S\u1ea3n Ph\u1ea9m";
            UpdateImagePreview();
        }

        private void Form_ThemSanPham_Load(object sender, EventArgs e)
        {
        }

        private void LoadDanhMuc()
        {
            cbDanhMuc.DataSource = db.DANH_MUC.ToList();
            cbDanhMuc.DisplayMember = "TenDanhMuc";
            cbDanhMuc.ValueMember = "MaDanhMuc";
        }

        private static string[] GetResourceFolders()
        {
            string runtimeFolder = Path.Combine(Application.StartupPath, "Resources");
            string projectFolder = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Resources"));

            return new[] { runtimeFolder, projectFolder }
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();
        }

        private static string NormalizeStoredImageName(string storedImage)
        {
            if (string.IsNullOrWhiteSpace(storedImage))
            {
                return string.Empty;
            }

            return Path.GetFileName(storedImage.Trim());
        }

        private static void SaveImageToResources(string sourceFilePath, string fileName)
        {
            foreach (string folder in GetResourceFolders())
            {
                Directory.CreateDirectory(folder);
                File.Copy(sourceFilePath, Path.Combine(folder, fileName), true);
            }
        }

        private static string GetStoredImageFileName(string sourceFilePath)
        {
            string extension = Path.GetExtension(sourceFilePath);
            string baseName = Path.GetFileNameWithoutExtension(sourceFilePath);
            char[] invalidChars = Path.GetInvalidFileNameChars();
            string sanitizedBaseName = new string(baseName.Select(ch => invalidChars.Contains(ch) ? '_' : ch).ToArray()).Trim();

            if (string.IsNullOrWhiteSpace(sanitizedBaseName))
            {
                sanitizedBaseName = "san-pham";
            }

            string candidate = sanitizedBaseName + extension;
            int suffix = 1;

            while (GetResourceFolders().Any(folder => File.Exists(Path.Combine(folder, candidate))))
            {
                candidate = sanitizedBaseName + "_" + suffix + extension;
                suffix++;
            }

            return candidate;
        }

        private static string ResolveImagePath(string storedImage)
        {
            if (!string.IsNullOrWhiteSpace(storedImage))
            {
                if (Path.IsPathRooted(storedImage) && File.Exists(storedImage))
                {
                    return storedImage;
                }

                string normalizedName = NormalizeStoredImageName(storedImage);

                foreach (string folder in GetResourceFolders())
                {
                    string candidate = Path.Combine(folder, normalizedName);
                    if (File.Exists(candidate))
                    {
                        return candidate;
                    }
                }
            }

            foreach (string folder in GetResourceFolders())
            {
                string fallback = Path.Combine(folder, "no-image.png");
                if (File.Exists(fallback))
                {
                    return fallback;
                }
            }

            return null;
        }

        private void UpdateImagePreview()
        {
            if (picHinhAnh.Image != null)
            {
                Image oldImage = picHinhAnh.Image;
                picHinhAnh.Image = null;
                oldImage.Dispose();
            }

            string imagePath = ResolveImagePath(pathImage);
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                return;
            }

            using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            using (Image image = Image.FromStream(stream))
            {
                picHinhAnh.Image = new Bitmap(image);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    string fileName = GetStoredImageFileName(ofd.FileName);
                    SaveImageToResources(ofd.FileName, fileName);
                    pathImage = fileName;
                    UpdateImagePreview();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kh\u00f4ng th\u1ec3 l\u01b0u \u1ea3nh s\u1ea3n ph\u1ea9m.\nChi ti\u1ebft: " + ex.Message, "L\u1ed7i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool TryReadFormValues(out decimal giaBan, out int tonKho)
        {
            giaBan = 0;
            tonKho = 0;

            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui l\u00f2ng nh\u1eadp t\u00ean s\u1ea3n ph\u1ea9m.", "Thi\u1ebfu d\u1eef li\u1ec7u", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSP.Focus();
                return false;
            }

            if (!decimal.TryParse(txtGiaBan.Text.Trim(), out giaBan))
            {
                MessageBox.Show("Gi\u00e1 b\u00e1n kh\u00f4ng h\u1ee3p l\u1ec7.", "D\u1eef li\u1ec7u sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return false;
            }

            if (!int.TryParse(txtTonKho.Text.Trim(), out tonKho))
            {
                MessageBox.Show("T\u1ed3n kho kh\u00f4ng h\u1ee3p l\u1ec7.", "D\u1eef li\u1ec7u sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTonKho.Focus();
                return false;
            }

            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            decimal giaBan;
            int tonKho;

            if (!TryReadFormValues(out giaBan, out tonKho))
            {
                return;
            }

            if (maSP == 0)
            {
                SAN_PHAM sp = new SAN_PHAM
                {
                    TenSanPham = txtTenSP.Text.Trim(),
                    MaDanhMuc = Convert.ToInt32(cbDanhMuc.SelectedValue),
                    GiaBan = giaBan,
                    SoLuongTon = tonKho,
                    HinhAnh = pathImage
                };

                db.SAN_PHAM.Add(sp);
                MessageBox.Show("Th\u00eam s\u1ea3n ph\u1ea9m th\u00e0nh c\u00f4ng");
            }
            else
            {
                SAN_PHAM sp = db.SAN_PHAM.Find(maSP);

                if (sp == null)
                {
                    MessageBox.Show("Kh\u00f4ng t\u00ecm th\u1ea5y s\u1ea3n ph\u1ea9m \u0111\u1ec3 c\u1eadp nh\u1eadt.", "L\u1ed7i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sp.TenSanPham = txtTenSP.Text.Trim();
                sp.MaDanhMuc = Convert.ToInt32(cbDanhMuc.SelectedValue);
                sp.GiaBan = giaBan;
                sp.SoLuongTon = tonKho;
                sp.HinhAnh = pathImage;

                MessageBox.Show("C\u1eadp nh\u1eadt s\u1ea3n ph\u1ea9m th\u00e0nh c\u00f4ng");
            }

            db.SaveChanges();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
