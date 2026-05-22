USE [CUA_HANG_TIEN_LOI];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET DATEFORMAT dmy;

BEGIN TRANSACTION;

IF COL_LENGTH('dbo.SAN_PHAM', 'TonToiThieu') IS NULL
BEGIN
    ALTER TABLE dbo.SAN_PHAM
    ADD TonToiThieu INT CONSTRAINT DF_SAN_PHAM_TonToiThieu DEFAULT (10);
END;
GO

IF COL_LENGTH('dbo.SAN_PHAM', 'HanSuDung') IS NULL
BEGIN
    ALTER TABLE dbo.SAN_PHAM
    ADD HanSuDung DATE NULL;
END;
GO

IF COL_LENGTH('dbo.CHI_TIET_NHAP', 'HanSuDung') IS NULL
BEGIN
    ALTER TABLE dbo.CHI_TIET_NHAP
    ADD HanSuDung DATETIME NULL;
END;
GO

IF OBJECT_ID('dbo.KIEM_KE', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.KIEM_KE
    (
        MaKiemKe INT IDENTITY(1,1) NOT NULL,
        NgayKiemKe DATETIME CONSTRAINT DF_KIEM_KE_NgayKiemKe DEFAULT (GETDATE()) NULL,
        MaNhanVien INT NULL,
        GhiChu NVARCHAR(255) NULL,
        CONSTRAINT PK_KIEM_KE PRIMARY KEY CLUSTERED (MaKiemKe),
        CONSTRAINT FK_KIEM_KE_NHAN_VIEN FOREIGN KEY (MaNhanVien) REFERENCES dbo.NHAN_VIEN (MaNhanVien)
    );
END;
GO

IF OBJECT_ID('dbo.CHI_TIET_KIEM_KE', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.CHI_TIET_KIEM_KE
    (
        MaKiemKe INT NOT NULL,
        MaSanPham INT NOT NULL,
        SoLuongHeThong INT NULL,
        SoLuongThucTe INT NULL,
        ChenhLech INT NULL,
        CONSTRAINT PK_CHI_TIET_KIEM_KE PRIMARY KEY CLUSTERED (MaKiemKe, MaSanPham),
        CONSTRAINT FK_CHI_TIET_KIEM_KE_KIEM_KE FOREIGN KEY (MaKiemKe) REFERENCES dbo.KIEM_KE (MaKiemKe),
        CONSTRAINT FK_CHI_TIET_KIEM_KE_SAN_PHAM FOREIGN KEY (MaSanPham) REFERENCES dbo.SAN_PHAM (MaSanPham)
    );
END;
GO

UPDATE dbo.SAN_PHAM
SET TonToiThieu = 10,
    HanSuDung = '31/12/2026'
WHERE MaSanPham = 1;

UPDATE dbo.SAN_PHAM
SET TonToiThieu = 10,
    HanSuDung = '15/06/2026'
WHERE MaSanPham = 2;

UPDATE dbo.SAN_PHAM
SET TonToiThieu = 20,
    HanSuDung = '20/05/2027'
WHERE MaSanPham = 5;

UPDATE dbo.SAN_PHAM
SET TonToiThieu = 5,
    HanSuDung = '01/01/2028'
WHERE MaSanPham = 8;
GO

UPDATE dbo.CHI_TIET_NHAP
SET HanSuDung = '30/12/2026'
WHERE MaSanPham IN (1, 2, 3);

UPDATE dbo.CHI_TIET_NHAP
SET HanSuDung = '15/06/2026'
WHERE MaSanPham IN (4, 5, 6);

UPDATE dbo.CHI_TIET_NHAP
SET HanSuDung = '20/10/2027'
WHERE MaSanPham IN (7, 8, 9, 10);
GO

IF NOT EXISTS
(
    SELECT 1
    FROM dbo.KIEM_KE
    WHERE MaKiemKe = 1
)
BEGIN
    SET IDENTITY_INSERT dbo.KIEM_KE ON;

    INSERT INTO dbo.KIEM_KE (MaKiemKe, NgayKiemKe, MaNhanVien, GhiChu)
    VALUES
        (1, '28/03/2026 09:00:00', 1, N'Kiem ke dinh ky khu vuc nuoc giai khat'),
        (2, '29/03/2026 22:30:00', 2, N'Kiem ke cuoi ca toi - khu vuc do an nhe');

    SET IDENTITY_INSERT dbo.KIEM_KE OFF;
END;

IF NOT EXISTS
(
    SELECT 1
    FROM dbo.CHI_TIET_KIEM_KE
    WHERE MaKiemKe = 1
      AND MaSanPham = 1
)
BEGIN
    INSERT INTO dbo.CHI_TIET_KIEM_KE (MaKiemKe, MaSanPham, SoLuongHeThong, SoLuongThucTe, ChenhLech)
    VALUES
        (1, 1, 50, 48, -2),
        (1, 2, 60, 60, 0),
        (1, 3, 45, 46, 1),
        (2, 4, 100, 98, -2),
        (2, 7, 30, 30, 0);
END;
GO

COMMIT TRANSACTION;
