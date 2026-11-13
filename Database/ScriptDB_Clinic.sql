-- BÀI 6.7: Hệ thống quản lý phòng khám & kê đơn thuốc
IF DB_ID(N'ClinicDB') IS NULL
    CREATE DATABASE ClinicDB;
GO

USE ClinicDB;
GO

IF OBJECT_ID(N'dbo.ChiTietDonThuoc', N'U') IS NOT NULL DROP TABLE dbo.ChiTietDonThuoc;
IF OBJECT_ID(N'dbo.DonThuoc', N'U') IS NOT NULL DROP TABLE dbo.DonThuoc;
IF OBJECT_ID(N'dbo.Thuoc', N'U') IS NOT NULL DROP TABLE dbo.Thuoc;
IF OBJECT_ID(N'dbo.HoSoKham', N'U') IS NOT NULL DROP TABLE dbo.HoSoKham;
IF OBJECT_ID(N'dbo.BenhNhan', N'U') IS NOT NULL DROP TABLE dbo.BenhNhan;
GO

-- 1. Bệnh nhân
CREATE TABLE dbo.BenhNhan
(
    MaBN        INT IDENTITY(1,1) PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NULL UNIQUE,
    NamSinh     INT NULL
);

-- 2. Hồ sơ tiếp nhận khám
CREATE TABLE dbo.HoSoKham
(
    MaHoSo     INT IDENTITY(1,1) PRIMARY KEY,
    MaBN       INT NOT NULL REFERENCES dbo.BenhNhan(MaBN),
    NgayKham   DATETIME NOT NULL DEFAULT GETDATE(),
    TrieuChung NVARCHAR(MAX) NULL,
    ChanDoan   NVARCHAR(250) NULL
);

-- 3. Danh mục thuốc
CREATE TABLE dbo.Thuoc
(
    MaThuoc    INT IDENTITY(1,1) PRIMARY KEY,
    TenThuoc   NVARCHAR(150) NOT NULL,
    DonViTinh  NVARCHAR(20) NULL,                              -- Viên / Vỉ / Chai
    DonGia     DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (DonGia >= 0),
    SoLuongTon INT NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0)
);

-- 4. Đơn thuốc / Hóa đơn khám
CREATE TABLE dbo.DonThuoc
(
    MaDon     INT IDENTITY(1,1) PRIMARY KEY,
    MaHoSo    INT NOT NULL REFERENCES dbo.HoSoKham(MaHoSo),
    TienKham  DECIMAL(18,2) NOT NULL DEFAULT 0,
    TongTien  DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Chờ lấy thuốc'   -- Chờ lấy thuốc / Đã thanh toán
);

-- 5. Chi tiết thuốc kê đơn
CREATE TABLE dbo.ChiTietDonThuoc
(
    MaDon     INT NOT NULL REFERENCES dbo.DonThuoc(MaDon),
    MaThuoc   INT NOT NULL REFERENCES dbo.Thuoc(MaThuoc),
    SoLuong   INT NOT NULL CHECK (SoLuong > 0),
    LieuDung  NVARCHAR(100) NULL,
    PRIMARY KEY (MaDon, MaThuoc)
);
GO

-- Dữ liệu mẫu
INSERT INTO dbo.BenhNhan (HoTen, SoDienThoai, NamSinh) VALUES
(N'Lê Minh Dũng', '0912345678', 2004),
(N'Trần Thị Mai', '0987654321', 1990),
(N'Phạm Văn Long', '0905111222', 1975),
(N'Nguyễn Thu Hà', '0934555666', 2012);

INSERT INTO dbo.Thuoc (TenThuoc, DonViTinh, DonGia, SoLuongTon) VALUES
(N'Paracetamol 500mg', N'Viên', 1500, 500),
(N'Amoxicillin 500mg', N'Viên', 3000, 200),
(N'Vitamin C 500mg', N'Viên', 1000, 300),
(N'Siro ho Prospan', N'Chai', 85000, 12),
(N'Omeprazol 20mg', N'Viên', 2500, 150),
(N'Berberin', N'Vỉ', 12000, 3),
(N'Nước muối sinh lý', N'Chai', 5000, 40);

INSERT INTO dbo.HoSoKham (MaBN, NgayKham, TrieuChung, ChanDoan) VALUES
(2, DATEADD(DAY, -3, GETDATE()), N'Sốt, đau họng, ho khan 3 ngày', N'Viêm họng cấp'),
(3, DATEADD(HOUR, -5, GETDATE()), N'Đau thượng vị, ợ chua', N'Viêm dạ dày'),
(1, DATEADD(MINUTE, -30, GETDATE()), N'Sốt 38.5 độ, đau đầu, mệt mỏi', NULL),     -- chờ khám
(4, DATEADD(MINUTE, -10, GETDATE()), N'Ho có đờm, sổ mũi', NULL);                  -- chờ khám

INSERT INTO dbo.DonThuoc (MaHoSo, TienKham, TrangThai) VALUES
(1, 100000, N'Đã thanh toán'),
(2, 150000, N'Chờ lấy thuốc');

INSERT INTO dbo.ChiTietDonThuoc (MaDon, MaThuoc, SoLuong, LieuDung) VALUES
(1, 2, 10, N'Sáng 1 viên, Chiều 1 viên sau ăn'), (1, 1, 6, N'Uống 1 viên khi sốt trên 38.5 độ'),
(2, 5, 14, N'Sáng 1 viên trước ăn 30 phút'), (2, 3, 10, N'Sáng 1 viên sau ăn');

UPDATE d SET TongTien = d.TienKham + (SELECT SUM(c.SoLuong * t.DonGia)
                                      FROM dbo.ChiTietDonThuoc c JOIN dbo.Thuoc t ON c.MaThuoc = t.MaThuoc
                                      WHERE c.MaDon = d.MaDon)
FROM dbo.DonThuoc d;
GO

SELECT * FROM dbo.BenhNhan;
SELECT * FROM dbo.HoSoKham;
SELECT * FROM dbo.Thuoc;
SELECT * FROM dbo.DonThuoc;
