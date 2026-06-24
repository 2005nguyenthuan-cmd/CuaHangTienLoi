USE [master]
GO
/****** Object:  Database [CUA_HANG_TIEN_LOI]    Script Date: 3/29/2026 3:36:17 PM ******/
IF DB_ID(N'CUA_HANG_TIEN_LOI') IS NULL
BEGIN
    CREATE DATABASE [CUA_HANG_TIEN_LOI];
END
GO
DECLARE @ProductMajorVersion INT = TRY_CAST(SERVERPROPERTY('ProductMajorVersion') AS INT);

IF @ProductMajorVersion >= 16
BEGIN
    ALTER DATABASE [CUA_HANG_TIEN_LOI] SET COMPATIBILITY_LEVEL = 160;
END
ELSE IF @ProductMajorVersion >= 15
BEGIN
    ALTER DATABASE [CUA_HANG_TIEN_LOI] SET COMPATIBILITY_LEVEL = 150;
END
ELSE IF @ProductMajorVersion >= 14
BEGIN
    ALTER DATABASE [CUA_HANG_TIEN_LOI] SET COMPATIBILITY_LEVEL = 140;
END
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CUA_HANG_TIEN_LOI].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET ARITHABORT OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET  MULTI_USER 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET QUERY_STORE = ON
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CUA_HANG_TIEN_LOI]
GO
/****** Object:  Table [dbo].[CA_LAM_VIEC]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CA_LAM_VIEC](
	[MaCa] [int] IDENTITY(1,1) NOT NULL,
	[TenCa] [nvarchar](50) NULL,
	[GioBatDau] [time](7) NULL,
	[GioKetThuc] [time](7) NULL,
	[LoaiCa] [nvarchar](20) NOT NULL,
	[SoGioChuan] [decimal](4, 2) NULL,
	[ChoPhepTangCa] [bit] NOT NULL,
	[MoTa] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHI_TIET_HOA_DON]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHI_TIET_HOA_DON](
	[MaHoaDon] [int] NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[SoLuong] [int] NULL,
	[DonGia] [decimal](10, 2) NULL,
	[ThanhTien] [decimal](12, 2) NULL,
 CONSTRAINT [PK_CHI_TIET_HOA_DON] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC,
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHI_TIET_NHAP]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHI_TIET_NHAP](
	[MaPhieuNhap] [int] NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[SoLuong] [int] NULL,
	[GiaNhap] [decimal](10, 2) NULL,
 CONSTRAINT [PK_CHI_TIET_NHAP] PRIMARY KEY CLUSTERED 
(
	[MaPhieuNhap] ASC,
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CONG_VIEC]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONG_VIEC](
	[MaCongViec] [int] IDENTITY(1,1) NOT NULL,
	[TenCongViec] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](255) NULL,
	[TrangThai] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCongViec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DANH_MUC]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DANH_MUC](
	[MaDanhMuc] [int] IDENTITY(1,1) NOT NULL,
	[TenDanhMuc] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](255) NULL,
 CONSTRAINT [PK__DANH_MUC__B375088794E5DF7C] PRIMARY KEY CLUSTERED 
(
	[MaDanhMuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOA_DON]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOA_DON](
	[MaHoaDon] [int] IDENTITY(1,1) NOT NULL,
	[NgayLap] [datetime] NULL,
	[MaNhanVien] [int] NULL,
	[MaKhachHang] [int] NULL,
	[TongTien] [decimal](12, 2) NULL,
 CONSTRAINT [PK__HOA_DON__835ED13B51454876] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACH_HANG]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACH_HANG](
	[MaKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[TenKhachHang] [nvarchar](100) NULL,
	[SoDienThoai] [varchar](15) NULL,
	[DiemTichLuy] [int] NULL,
 CONSTRAINT [PK__KHACH_HA__88D2F0E53BEF9758] PRIMARY KEY CLUSTERED 
(
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHUYEN_MAI]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHUYEN_MAI](
	[MaKhuyenMai] [int] IDENTITY(1,1) NOT NULL,
	[TenKhuyenMai] [nvarchar](200) NULL,
	[PhanTramGiam] [int] NULL,
	[NgayBatDau] [date] NULL,
	[NgayKetThuc] [date] NULL,
 CONSTRAINT [PK_KHUYENMAI] PRIMARY KEY CLUSTERED 
(
	[MaKhuyenMai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LICH_LAM_CONG_VIEC]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LICH_LAM_CONG_VIEC](
	[MaLichCongViec] [int] IDENTITY(1,1) NOT NULL,
	[MaLich] [int] NOT NULL,
	[MaCongViec] [int] NOT NULL,
	[TuGio] [time](7) NULL,
	[DenGio] [time](7) NULL,
	[UuTien] [int] NOT NULL,
	[GhiChu] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLichCongViec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LICH_LAM_VIEC]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LICH_LAM_VIEC](
	[MaLich] [int] IDENTITY(1,1) NOT NULL,
	[MaNhanVien] [int] NOT NULL,
	[NgayLam] [date] NOT NULL,
	[MaCa] [int] NULL,
	[TrangThaiCa] [nvarchar](20) NOT NULL,
	[LoaiLich] [nvarchar](20) NOT NULL,
	[GioBatDauDuKien] [time](7) NULL,
	[GioKetThucDuKien] [time](7) NULL,
	[GioBatDauThucTe] [time](7) NULL,
	[GioKetThucThucTe] [time](7) NULL,
	[LyDoDieuChinh] [nvarchar](255) NULL,
	[GhiChu] [nvarchar](255) NULL,
	[NguoiCapNhat] [int] NULL,
	[NgayCapNhat] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLich] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHA_CUNG_CAP]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHA_CUNG_CAP](
	[MaNCC] [int] IDENTITY(1,1) NOT NULL,
	[TenNCC] [nvarchar](200) NULL,
	[SoDienThoai] [varchar](15) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[HinhAnh] [nvarchar](255) NULL,
 CONSTRAINT [PK__NHA_CUNG__3A185DEB986B5EB9] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHAN_VIEN]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHAN_VIEN](
	[MaNhanVien] [int] IDENTITY(1,1) NOT NULL,
	[TenNhanVien] [nvarchar](100) NOT NULL,
	[SoDienThoai] [varchar](15) NULL,
	[Email] [varchar](100) NULL,
	[TenDangNhap] [varchar](50) NULL,
	[MatKhau] [varchar](100) NULL,
	[MaVaiTro] [int] NULL,
	[HinhAnh] [nvarchar](255) NULL,
	[MaCa] [int] NULL,
 CONSTRAINT [PK__NHAN_VIE__77B2CA470B1A05DE] PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PHIEU_NHAP]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHIEU_NHAP](
	[MaPhieuNhap] [int] IDENTITY(1,1) NOT NULL,
	[NgayNhap] [datetime] NULL,
	[MaNhanVien] [int] NULL,
	[MaNCC] [int] NULL,
 CONSTRAINT [PK__PHIEU_NH__1470EF3B26766710] PRIMARY KEY CLUSTERED 
(
	[MaPhieuNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SAN_PHAM]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SAN_PHAM](
	[MaSanPham] [int] IDENTITY(1,1) NOT NULL,
	[TenSanPham] [nvarchar](200) NOT NULL,
	[GiaBan] [decimal](10, 2) NOT NULL,
	[SoLuongTon] [int] NULL,
	[MaDanhMuc] [int] NULL,
	[MoTa] [nvarchar](255) NULL,
	[HinhAnh] [nvarchar](255) NULL,
 CONSTRAINT [PK__SAN_PHAM__FAC7442DDD1FFEBE] PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SAN_PHAM_KHUYEN_MAI]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SAN_PHAM_KHUYEN_MAI](
	[MaSanPham] [int] NOT NULL,
	[MaKhuyenMai] [int] NOT NULL,
 CONSTRAINT [PK_SP_KM] PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC,
	[MaKhuyenMai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VAI_TRO]    Script Date: 3/29/2026 3:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VAI_TRO](
	[MaVaiTro] [int] IDENTITY(1,1) NOT NULL,
	[TenVaiTro] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](255) NULL,
 CONSTRAINT [PK__VAI_TRO__C24C41CF28BC3A7D] PRIMARY KEY CLUSTERED 
(
	[MaVaiTro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CA_LAM_VIEC] ON 

INSERT [dbo].[CA_LAM_VIEC] ([MaCa], [TenCa], [GioBatDau], [GioKetThuc], [LoaiCa], [SoGioChuan], [ChoPhepTangCa], [MoTa]) VALUES (1, N'Ca sáng', CAST(N'06:00:00' AS Time), CAST(N'14:00:00' AS Time), N'CaChinh', NULL, 0, NULL)
INSERT [dbo].[CA_LAM_VIEC] ([MaCa], [TenCa], [GioBatDau], [GioKetThuc], [LoaiCa], [SoGioChuan], [ChoPhepTangCa], [MoTa]) VALUES (2, N'Ca chiều', CAST(N'14:00:00' AS Time), CAST(N'22:00:00' AS Time), N'CaChinh', NULL, 0, NULL)
INSERT [dbo].[CA_LAM_VIEC] ([MaCa], [TenCa], [GioBatDau], [GioKetThuc], [LoaiCa], [SoGioChuan], [ChoPhepTangCa], [MoTa]) VALUES (3, N'Ca đêm', CAST(N'22:00:00' AS Time), CAST(N'06:00:00' AS Time), N'CaChinh', NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[CA_LAM_VIEC] OFF
GO
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (1, 1, 2, CAST(10000.00 AS Decimal(10, 2)), CAST(20000.00 AS Decimal(12, 2)))
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (2, 2, 3, CAST(10000.00 AS Decimal(10, 2)), CAST(30000.00 AS Decimal(12, 2)))
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (3, 4, 3, CAST(5000.00 AS Decimal(10, 2)), CAST(15000.00 AS Decimal(12, 2)))
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (4, 6, 2, CAST(15000.00 AS Decimal(10, 2)), CAST(30000.00 AS Decimal(12, 2)))
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (5, 5, 2, CAST(8000.00 AS Decimal(10, 2)), CAST(16000.00 AS Decimal(12, 2)))
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (6, 3, 2, CAST(9000.00 AS Decimal(10, 2)), CAST(18000.00 AS Decimal(12, 2)))
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (7, 7, 2, CAST(12000.00 AS Decimal(10, 2)), CAST(24000.00 AS Decimal(12, 2)))
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (8, 8, 4, CAST(7000.00 AS Decimal(10, 2)), CAST(28000.00 AS Decimal(12, 2)))
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (9, 9, 2, CAST(20000.00 AS Decimal(10, 2)), CAST(40000.00 AS Decimal(12, 2)))
INSERT [dbo].[CHI_TIET_HOA_DON] ([MaHoaDon], [MaSanPham], [SoLuong], [DonGia], [ThanhTien]) VALUES (10, 10, 1, CAST(11000.00 AS Decimal(10, 2)), CAST(11000.00 AS Decimal(12, 2)))
GO
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (1, 1, 20, CAST(7000.00 AS Decimal(10, 2)))
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (2, 2, 25, CAST(7000.00 AS Decimal(10, 2)))
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (3, 3, 30, CAST(6000.00 AS Decimal(10, 2)))
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (4, 4, 40, CAST(3000.00 AS Decimal(10, 2)))
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (5, 5, 50, CAST(6000.00 AS Decimal(10, 2)))
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (6, 6, 20, CAST(10000.00 AS Decimal(10, 2)))
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (7, 7, 25, CAST(8000.00 AS Decimal(10, 2)))
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (8, 8, 30, CAST(5000.00 AS Decimal(10, 2)))
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (9, 9, 15, CAST(15000.00 AS Decimal(10, 2)))
INSERT [dbo].[CHI_TIET_NHAP] ([MaPhieuNhap], [MaSanPham], [SoLuong], [GiaNhap]) VALUES (10, 10, 20, CAST(9000.00 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[DANH_MUC] ON 

INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (1, N'Nước giải khát', N'Các loại nước')
INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (2, N'Bánh kẹo', N'Bánh và kẹo')
INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (3, N'Mì ăn liền', N'Mì gói')
INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (4, N'Sữa', N'Các loại sữa')
INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (5, N'Đồ hộp', N'Thực phẩm đóng hộp')
INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (6, N'Snack', N'Đồ ăn vặt')
INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (7, N'Kem', N'Kem lạnh')
INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (8, N'Gia vị', N'Gia vị nấu ăn')
INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (9, N'Đồ dùng', N'Đồ dùng cá nhân')
INSERT [dbo].[DANH_MUC] ([MaDanhMuc], [TenDanhMuc], [MoTa]) VALUES (10, N'Trà', N'Trà các loại')
SET IDENTITY_INSERT [dbo].[DANH_MUC] OFF
GO
SET IDENTITY_INSERT [dbo].[HOA_DON] ON 

INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (1, CAST(N'2026-03-01T00:00:00.000' AS DateTime), 1, 1, CAST(20000.00 AS Decimal(12, 2)))
INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (2, CAST(N'2026-03-02T00:00:00.000' AS DateTime), 2, 2, CAST(30000.00 AS Decimal(12, 2)))
INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (3, CAST(N'2026-03-03T00:00:00.000' AS DateTime), 3, 3, CAST(15000.00 AS Decimal(12, 2)))
INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (4, CAST(N'2026-03-04T00:00:00.000' AS DateTime), 4, 4, CAST(50000.00 AS Decimal(12, 2)))
INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (5, CAST(N'2026-03-05T00:00:00.000' AS DateTime), 5, 5, CAST(22000.00 AS Decimal(12, 2)))
INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (6, CAST(N'2026-03-06T00:00:00.000' AS DateTime), 6, 6, CAST(18000.00 AS Decimal(12, 2)))
INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (7, CAST(N'2026-03-07T00:00:00.000' AS DateTime), 7, 7, CAST(26000.00 AS Decimal(12, 2)))
INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (8, CAST(N'2026-03-08T00:00:00.000' AS DateTime), 8, 8, CAST(34000.00 AS Decimal(12, 2)))
INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (9, CAST(N'2026-03-09T00:00:00.000' AS DateTime), 9, 9, CAST(41000.00 AS Decimal(12, 2)))
INSERT [dbo].[HOA_DON] ([MaHoaDon], [NgayLap], [MaNhanVien], [MaKhachHang], [TongTien]) VALUES (10, CAST(N'2026-03-10T00:00:00.000' AS DateTime), 10, 10, CAST(12000.00 AS Decimal(12, 2)))
SET IDENTITY_INSERT [dbo].[HOA_DON] OFF
GO
SET IDENTITY_INSERT [dbo].[KHACH_HANG] ON 

INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (1, N'Khách 1', N'0981111111', 10)
INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (2, N'Khách 2', N'0981111112', 20)
INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (3, N'Khách 3', N'0981111113', 30)
INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (4, N'Khách 4', N'0981111114', 40)
INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (5, N'Khách 5', N'0981111115', 50)
INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (6, N'Khách 6', N'0981111116', 60)
INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (7, N'Khách 7', N'0981111117', 70)
INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (8, N'Khách 8', N'0981111118', 80)
INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (9, N'Khách 9', N'0981111119', 90)
INSERT [dbo].[KHACH_HANG] ([MaKhachHang], [TenKhachHang], [SoDienThoai], [DiemTichLuy]) VALUES (10, N'Khách 10', N'0981111120', 100)
SET IDENTITY_INSERT [dbo].[KHACH_HANG] OFF
GO
SET IDENTITY_INSERT [dbo].[KHUYEN_MAI] ON 

INSERT [dbo].[KHUYEN_MAI] ([MaKhuyenMai], [TenKhuyenMai], [PhanTramGiam], [NgayBatDau], [NgayKetThuc]) VALUES (2, N'Sale Xuân', 15, CAST(N'2026-02-01' AS Date), CAST(N'2026-02-28' AS Date))
INSERT [dbo].[KHUYEN_MAI] ([MaKhuyenMai], [TenKhuyenMai], [PhanTramGiam], [NgayBatDau], [NgayKetThuc]) VALUES (3, N'Sale Tháng 3', 5, CAST(N'2026-03-01' AS Date), CAST(N'2026-03-31' AS Date))
INSERT [dbo].[KHUYEN_MAI] ([MaKhuyenMai], [TenKhuyenMai], [PhanTramGiam], [NgayBatDau], [NgayKetThuc]) VALUES (4, N'Sale Lễ', 20, CAST(N'2026-04-10' AS Date), CAST(N'2026-04-20' AS Date))
INSERT [dbo].[KHUYEN_MAI] ([MaKhuyenMai], [TenKhuyenMai], [PhanTramGiam], [NgayBatDau], [NgayKetThuc]) VALUES (5, N'Sale Hè', 10, CAST(N'2026-05-01' AS Date), CAST(N'2026-05-30' AS Date))
INSERT [dbo].[KHUYEN_MAI] ([MaKhuyenMai], [TenKhuyenMai], [PhanTramGiam], [NgayBatDau], [NgayKetThuc]) VALUES (6, N'Sale Quốc Khánh', 25, CAST(N'2026-09-01' AS Date), CAST(N'2026-09-05' AS Date))
INSERT [dbo].[KHUYEN_MAI] ([MaKhuyenMai], [TenKhuyenMai], [PhanTramGiam], [NgayBatDau], [NgayKetThuc]) VALUES (7, N'Sale Trung Thu', 15, CAST(N'2026-09-10' AS Date), CAST(N'2026-09-20' AS Date))
INSERT [dbo].[KHUYEN_MAI] ([MaKhuyenMai], [TenKhuyenMai], [PhanTramGiam], [NgayBatDau], [NgayKetThuc]) VALUES (8, N'Sale Black Friday', 30, CAST(N'2026-11-20' AS Date), CAST(N'2026-11-30' AS Date))
INSERT [dbo].[KHUYEN_MAI] ([MaKhuyenMai], [TenKhuyenMai], [PhanTramGiam], [NgayBatDau], [NgayKetThuc]) VALUES (9, N'Sale Noel', 20, CAST(N'2026-12-20' AS Date), CAST(N'2026-12-25' AS Date))
INSERT [dbo].[KHUYEN_MAI] ([MaKhuyenMai], [TenKhuyenMai], [PhanTramGiam], [NgayBatDau], [NgayKetThuc]) VALUES (10, N'Sale Cuối Năm', 35, CAST(N'2026-12-26' AS Date), CAST(N'2026-12-31' AS Date))
SET IDENTITY_INSERT [dbo].[KHUYEN_MAI] OFF
GO
SET IDENTITY_INSERT [dbo].[NHA_CUNG_CAP] ON 

INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (1, N'Công ty Vinamilk', N'0901111111', N'Hồ Chí Minh', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (2, N'Công ty Pepsi', N'0902222222', N'Hà Nội', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (3, N'Công ty Acecook', N'0903333333', N'Đà Nẵng', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (4, N'Công ty Orion', N'0904444444', N'Hà Nội', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (5, N'Công ty Kinh Đô', N'0905555555', N'Hồ Chí Minh', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (6, N'Công ty Nestle', N'0906666666', N'Hồ Chí Minh', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (7, N'Công ty Masan', N'0907777777', N'Hà Nội', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (8, N'Công ty TH True Milk', N'0908888888', N'Nghệ An', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (9, N'Công ty Unilever', N'0909999999', N'Hồ Chí Minh', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (10, N'Công ty Coca Cola', N'0910000000', N'Hồ Chí Minh', NULL)
INSERT [dbo].[NHA_CUNG_CAP] ([MaNCC], [TenNCC], [SoDienThoai], [DiaChi], [HinhAnh]) VALUES (11, N'NNN', N'123', N'123', N'')
SET IDENTITY_INSERT [dbo].[NHA_CUNG_CAP] OFF
GO
SET IDENTITY_INSERT [dbo].[NHAN_VIEN] ON 

INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (1, N'Nguyễn Văn A', N'0911111111', N'a@gmail.com', N'user1', N'123', 1, NULL, 1)
INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (2, N'Trần Văn B', N'0911111112', N'b@gmail.com', N'user2', N'123', 2, NULL, 2)
INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (3, N'Lê Văn C', N'0911111113', N'c@gmail.com', N'user3', N'123', 2, NULL, 3)
INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (4, N'Phạm Văn D', N'0911111114', N'd@gmail.com', N'user4', N'123', 2, NULL, 1)
INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (5, N'Hoàng Văn E', N'0911111115', N'e@gmail.com', N'user5', N'123', 2, NULL, 2)
INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (6, N'Đỗ Văn F', N'0911111116', N'f@gmail.com', N'user6', N'123', 2, NULL, 3)
INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (7, N'Vũ Văn G', N'0911111117', N'g@gmail.com', N'user7', N'123', 2, NULL, 1)
INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (8, N'Bùi Văn H', N'0911111118', N'h@gmail.com', N'user8', N'123', 2, NULL, 2)
INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (9, N'Phan Văn I', N'0911111119', N'i@gmail.com', N'user9', N'123', 2, NULL, 3)
INSERT [dbo].[NHAN_VIEN] ([MaNhanVien], [TenNhanVien], [SoDienThoai], [Email], [TenDangNhap], [MatKhau], [MaVaiTro], [HinhAnh], [MaCa]) VALUES (10, N'Ngô Văn K', N'0911111120', N'k@gmail.com', N'user10', N'123', 2, NULL, 1)
SET IDENTITY_INSERT [dbo].[NHAN_VIEN] OFF
GO
SET IDENTITY_INSERT [dbo].[PHIEU_NHAP] ON 

INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (1, CAST(N'2026-03-01T00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (2, CAST(N'2026-03-02T00:00:00.000' AS DateTime), 2, 2)
INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (3, CAST(N'2026-03-03T00:00:00.000' AS DateTime), 3, 3)
INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (4, CAST(N'2026-03-04T00:00:00.000' AS DateTime), 4, 4)
INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (5, CAST(N'2026-03-05T00:00:00.000' AS DateTime), 5, 5)
INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (6, CAST(N'2026-03-06T00:00:00.000' AS DateTime), 6, 6)
INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (7, CAST(N'2026-03-07T00:00:00.000' AS DateTime), 7, 7)
INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (8, CAST(N'2026-03-08T00:00:00.000' AS DateTime), 8, 8)
INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (9, CAST(N'2026-03-09T00:00:00.000' AS DateTime), 9, 9)
INSERT [dbo].[PHIEU_NHAP] ([MaPhieuNhap], [NgayNhap], [MaNhanVien], [MaNCC]) VALUES (10, CAST(N'2026-03-10T00:00:00.000' AS DateTime), 10, 10)
SET IDENTITY_INSERT [dbo].[PHIEU_NHAP] OFF
GO
SET IDENTITY_INSERT [dbo].[SAN_PHAM] ON 

INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (1, N'Coca Cola', CAST(10000.00 AS Decimal(10, 2)), 50, 1, N'Nước ngọt', N'product-coca-cola.png')
INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (2, N'Pepsi', CAST(10000.00 AS Decimal(10, 2)), 60, 1, N'Nước ngọt', N'product-pepsi.png')
INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (3, N'Sting', CAST(9000.00 AS Decimal(10, 2)), 70, 1, N'Nước tăng lực', N'product-sting.png')
INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (4, N'Mì Hảo Hảo', CAST(5000.00 AS Decimal(10, 2)), 100, 3, N'Mì ăn liền', N'product-mi-hao-hao.png')
INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (5, N'Sữa Vinamilk', CAST(8000.00 AS Decimal(10, 2)), 80, 4, N'Sữa tươi', N'product-sua-vinamilk.png')
INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (6, N'Bánh Chocopie', CAST(15000.00 AS Decimal(10, 2)), 40, 2, N'Bánh ngọt', N'product-banh-chocopie.png')
INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (7, N'Snack khoai tây', CAST(12000.00 AS Decimal(10, 2)), 55, 6, N'Snack', N'product-snack-khoai-tay.png')
INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (8, N'Kem Merino', CAST(7000.00 AS Decimal(10, 2)), 30, 7, N'Kem', N'product-kem-merino.png')
INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (9, N'Nước mắm', CAST(20000.00 AS Decimal(10, 2)), 20, 8, N'Gia vị', N'product-nuoc-mam.png')
INSERT [dbo].[SAN_PHAM] ([MaSanPham], [TenSanPham], [GiaBan], [SoLuongTon], [MaDanhMuc], [MoTa], [HinhAnh]) VALUES (10, N'Trà xanh', CAST(11000.00 AS Decimal(10, 2)), 45, 10, N'Nước trà', N'product-tra-xanh.png')
SET IDENTITY_INSERT [dbo].[SAN_PHAM] OFF
GO
INSERT [dbo].[SAN_PHAM_KHUYEN_MAI] ([MaSanPham], [MaKhuyenMai]) VALUES (2, 2)
INSERT [dbo].[SAN_PHAM_KHUYEN_MAI] ([MaSanPham], [MaKhuyenMai]) VALUES (3, 3)
INSERT [dbo].[SAN_PHAM_KHUYEN_MAI] ([MaSanPham], [MaKhuyenMai]) VALUES (4, 4)
INSERT [dbo].[SAN_PHAM_KHUYEN_MAI] ([MaSanPham], [MaKhuyenMai]) VALUES (5, 5)
INSERT [dbo].[SAN_PHAM_KHUYEN_MAI] ([MaSanPham], [MaKhuyenMai]) VALUES (6, 6)
INSERT [dbo].[SAN_PHAM_KHUYEN_MAI] ([MaSanPham], [MaKhuyenMai]) VALUES (7, 7)
INSERT [dbo].[SAN_PHAM_KHUYEN_MAI] ([MaSanPham], [MaKhuyenMai]) VALUES (8, 8)
INSERT [dbo].[SAN_PHAM_KHUYEN_MAI] ([MaSanPham], [MaKhuyenMai]) VALUES (9, 9)
INSERT [dbo].[SAN_PHAM_KHUYEN_MAI] ([MaSanPham], [MaKhuyenMai]) VALUES (10, 10)
GO
SET IDENTITY_INSERT [dbo].[VAI_TRO] ON 

INSERT [dbo].[VAI_TRO] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (1, N'Quản lý', N'Quản lý cửa hàng')
INSERT [dbo].[VAI_TRO] ([MaVaiTro], [TenVaiTro], [MoTa]) VALUES (2, N'Nhân viên bán hàng', N'Bán hàng tại quầy')
SET IDENTITY_INSERT [dbo].[VAI_TRO] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__NHAN_VIE__55F68FC0EAFF443C]    Script Date: 3/29/2026 3:36:18 PM ******/
ALTER TABLE [dbo].[NHAN_VIEN] ADD  CONSTRAINT [UQ__NHAN_VIE__55F68FC0EAFF443C] UNIQUE NONCLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CA_LAM_VIEC] ADD  DEFAULT (N'CaChinh') FOR [LoaiCa]
GO
ALTER TABLE [dbo].[CA_LAM_VIEC] ADD  DEFAULT ((0)) FOR [ChoPhepTangCa]
GO
ALTER TABLE [dbo].[CONG_VIEC] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[HOA_DON] ADD  CONSTRAINT [DF__HOA_DON__NgayLap__6B24EA82]  DEFAULT (getdate()) FOR [NgayLap]
GO
ALTER TABLE [dbo].[KHACH_HANG] ADD  CONSTRAINT [DF__KHACH_HAN__DiemT__68487DD7]  DEFAULT ((0)) FOR [DiemTichLuy]
GO
ALTER TABLE [dbo].[LICH_LAM_CONG_VIEC] ADD  DEFAULT ((1)) FOR [UuTien]
GO
ALTER TABLE [dbo].[LICH_LAM_VIEC] ADD  DEFAULT (N'PhanCong') FOR [TrangThaiCa]
GO
ALTER TABLE [dbo].[LICH_LAM_VIEC] ADD  DEFAULT (N'CaChinh') FOR [LoaiLich]
GO
ALTER TABLE [dbo].[LICH_LAM_VIEC] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[PHIEU_NHAP] ADD  CONSTRAINT [DF__PHIEU_NHA__NgayN__75A278F5]  DEFAULT (getdate()) FOR [NgayNhap]
GO
ALTER TABLE [dbo].[SAN_PHAM] ADD  CONSTRAINT [DF__SAN_PHAM__SoLuon__6477ECF3]  DEFAULT ((0)) FOR [SoLuongTon]
GO
ALTER TABLE [dbo].[CHI_TIET_HOA_DON]  WITH CHECK ADD  CONSTRAINT [FK_CHI_TIET_HOA_DON_HOA_DON] FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HOA_DON] ([MaHoaDon])
GO
ALTER TABLE [dbo].[CHI_TIET_HOA_DON] CHECK CONSTRAINT [FK_CHI_TIET_HOA_DON_HOA_DON]
GO
ALTER TABLE [dbo].[CHI_TIET_HOA_DON]  WITH CHECK ADD  CONSTRAINT [FK_CHI_TIET_HOA_DON_SAN_PHAM] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SAN_PHAM] ([MaSanPham])
GO
ALTER TABLE [dbo].[CHI_TIET_HOA_DON] CHECK CONSTRAINT [FK_CHI_TIET_HOA_DON_SAN_PHAM]
GO
ALTER TABLE [dbo].[CHI_TIET_NHAP]  WITH CHECK ADD  CONSTRAINT [FK__CHI_TIET___MaPhi__7A672E12] FOREIGN KEY([MaPhieuNhap])
REFERENCES [dbo].[PHIEU_NHAP] ([MaPhieuNhap])
GO
ALTER TABLE [dbo].[CHI_TIET_NHAP] CHECK CONSTRAINT [FK__CHI_TIET___MaPhi__7A672E12]
GO
ALTER TABLE [dbo].[CHI_TIET_NHAP]  WITH CHECK ADD  CONSTRAINT [FK__CHI_TIET___MaSan__7B5B524B] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SAN_PHAM] ([MaSanPham])
GO
ALTER TABLE [dbo].[CHI_TIET_NHAP] CHECK CONSTRAINT [FK__CHI_TIET___MaSan__7B5B524B]
GO
ALTER TABLE [dbo].[HOA_DON]  WITH CHECK ADD  CONSTRAINT [FK__HOA_DON__MaKhach__6D0D32F4] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KHACH_HANG] ([MaKhachHang])
GO
ALTER TABLE [dbo].[HOA_DON] CHECK CONSTRAINT [FK__HOA_DON__MaKhach__6D0D32F4]
GO
ALTER TABLE [dbo].[HOA_DON]  WITH CHECK ADD  CONSTRAINT [FK__HOA_DON__MaNhanV__6C190EBB] FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NHAN_VIEN] ([MaNhanVien])
GO
ALTER TABLE [dbo].[HOA_DON] CHECK CONSTRAINT [FK__HOA_DON__MaNhanV__6C190EBB]
GO
ALTER TABLE [dbo].[LICH_LAM_CONG_VIEC]  WITH CHECK ADD  CONSTRAINT [FK_LLCV_CV] FOREIGN KEY([MaCongViec])
REFERENCES [dbo].[CONG_VIEC] ([MaCongViec])
GO
ALTER TABLE [dbo].[LICH_LAM_CONG_VIEC] CHECK CONSTRAINT [FK_LLCV_CV]
GO
ALTER TABLE [dbo].[LICH_LAM_CONG_VIEC]  WITH CHECK ADD  CONSTRAINT [FK_LLCV_LICH] FOREIGN KEY([MaLich])
REFERENCES [dbo].[LICH_LAM_VIEC] ([MaLich])
GO
ALTER TABLE [dbo].[LICH_LAM_CONG_VIEC] CHECK CONSTRAINT [FK_LLCV_LICH]
GO
ALTER TABLE [dbo].[LICH_LAM_VIEC]  WITH CHECK ADD  CONSTRAINT [FK_LLV_CA] FOREIGN KEY([MaCa])
REFERENCES [dbo].[CA_LAM_VIEC] ([MaCa])
GO
ALTER TABLE [dbo].[LICH_LAM_VIEC] CHECK CONSTRAINT [FK_LLV_CA]
GO
ALTER TABLE [dbo].[LICH_LAM_VIEC]  WITH CHECK ADD  CONSTRAINT [FK_LLV_NGUOICAPNHAT] FOREIGN KEY([NguoiCapNhat])
REFERENCES [dbo].[NHAN_VIEN] ([MaNhanVien])
GO
ALTER TABLE [dbo].[LICH_LAM_VIEC] CHECK CONSTRAINT [FK_LLV_NGUOICAPNHAT]
GO
ALTER TABLE [dbo].[LICH_LAM_VIEC]  WITH CHECK ADD  CONSTRAINT [FK_LLV_NV] FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NHAN_VIEN] ([MaNhanVien])
GO
ALTER TABLE [dbo].[LICH_LAM_VIEC] CHECK CONSTRAINT [FK_LLV_NV]
GO
ALTER TABLE [dbo].[NHAN_VIEN]  WITH CHECK ADD  CONSTRAINT [FK__NHAN_VIEN__MaVai__5FB337D6] FOREIGN KEY([MaVaiTro])
REFERENCES [dbo].[VAI_TRO] ([MaVaiTro])
GO
ALTER TABLE [dbo].[NHAN_VIEN] CHECK CONSTRAINT [FK__NHAN_VIEN__MaVai__5FB337D6]
GO
ALTER TABLE [dbo].[NHAN_VIEN]  WITH CHECK ADD  CONSTRAINT [FK_NHAN_VIEN_CA_LAM_VIEC] FOREIGN KEY([MaCa])
REFERENCES [dbo].[CA_LAM_VIEC] ([MaCa])
GO
ALTER TABLE [dbo].[NHAN_VIEN] CHECK CONSTRAINT [FK_NHAN_VIEN_CA_LAM_VIEC]
GO
ALTER TABLE [dbo].[PHIEU_NHAP]  WITH CHECK ADD  CONSTRAINT [FK__PHIEU_NHA__MaNCC__778AC167] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NHA_CUNG_CAP] ([MaNCC])
GO
ALTER TABLE [dbo].[PHIEU_NHAP] CHECK CONSTRAINT [FK__PHIEU_NHA__MaNCC__778AC167]
GO
ALTER TABLE [dbo].[PHIEU_NHAP]  WITH CHECK ADD  CONSTRAINT [FK__PHIEU_NHA__MaNha__76969D2E] FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NHAN_VIEN] ([MaNhanVien])
GO
ALTER TABLE [dbo].[PHIEU_NHAP] CHECK CONSTRAINT [FK__PHIEU_NHA__MaNha__76969D2E]
GO
ALTER TABLE [dbo].[SAN_PHAM]  WITH CHECK ADD  CONSTRAINT [FK__SAN_PHAM__MaDanh__656C112C] FOREIGN KEY([MaDanhMuc])
REFERENCES [dbo].[DANH_MUC] ([MaDanhMuc])
GO
ALTER TABLE [dbo].[SAN_PHAM] CHECK CONSTRAINT [FK__SAN_PHAM__MaDanh__656C112C]
GO
ALTER TABLE [dbo].[SAN_PHAM_KHUYEN_MAI]  WITH CHECK ADD  CONSTRAINT [FK_SP_KM_KHUYENMAI] FOREIGN KEY([MaKhuyenMai])
REFERENCES [dbo].[KHUYEN_MAI] ([MaKhuyenMai])
GO
ALTER TABLE [dbo].[SAN_PHAM_KHUYEN_MAI] CHECK CONSTRAINT [FK_SP_KM_KHUYENMAI]
GO
ALTER TABLE [dbo].[SAN_PHAM_KHUYEN_MAI]  WITH CHECK ADD  CONSTRAINT [FK_SP_KM_SANPHAM] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SAN_PHAM] ([MaSanPham])
GO
ALTER TABLE [dbo].[SAN_PHAM_KHUYEN_MAI] CHECK CONSTRAINT [FK_SP_KM_SANPHAM]
GO
USE [master]
GO
ALTER DATABASE [CUA_HANG_TIEN_LOI] SET  READ_WRITE 
GO
