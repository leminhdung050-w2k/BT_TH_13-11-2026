-- BÀI 6.8: Hệ thống quản lý bàn bida & dịch vụ giải trí
IF DB_ID(N'BilliardsDB') IS NULL
    CREATE DATABASE BilliardsDB;
GO

USE BilliardsDB;
GO

IF OBJECT_ID(N'dbo.ChiTietHoaDon', N'U') IS NOT NULL DROP TABLE dbo.ChiTietHoaDon;
IF OBJECT_ID(N'dbo.HoaDon', N'U') IS NOT NULL DROP TABLE dbo.HoaDon;
IF OBJECT_ID(N'dbo.MenuDichVu', N'U') IS NOT NULL DROP TABLE dbo.MenuDichVu;
IF OBJECT_ID(N'dbo.BanBida', N'U') IS NOT NULL DROP TABLE dbo.BanBida;
GO

-- 1. Bàn bida
CREATE TABLE dbo.BanBida
(
    MaBan      INT IDENTITY(1,1) PRIMARY KEY,
    TenBan     NVARCHAR(50) NOT NULL,
    LoaiBan    NVARCHAR(30) NOT NULL,                     
    GiaTheoGio DECIMAL(18,2) NOT NULL CHECK (GiaTheoGio >= 0),
    TrangThai  NVARCHAR(20) NOT NULL DEFAULT N'Trống'       -- Trống / Đang chơi
);

-- 2. Menu dịch vụ: đồ ăn, nước uống, thuê gậy (TonKho NULL = dịch vụ thuê, không trừ kho)
CREATE TABLE dbo.MenuDichVu
(
    MaDV   INT IDENTITY(1,1) PRIMARY KEY,
    TenDV  NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0),
    TonKho INT NULL CHECK (TonKho >= 0)
);

-- 3. Hóa đơn tính tiền
CREATE TABLE dbo.HoaDon
(
    MaHD        INT IDENTITY(1,1) PRIMARY KEY,
    MaBan       INT NOT NULL REFERENCES dbo.BanBida(MaBan),
    GioBatDau   DATETIME NOT NULL,
    GioKetThuc  DATETIME NULL,
    TienGioChoi DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienDichVu  DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai   BIT NOT NULL DEFAULT 0      -- 0: chưa thanh toán, 1: đã thanh toán
);

-- 4. Chi tiết dịch vụ gọi thêm
CREATE TABLE dbo.ChiTietHoaDon
(
    MaHD    INT NOT NULL REFERENCES dbo.HoaDon(MaHD),
    MaDV    INT NOT NULL REFERENCES dbo.MenuDichVu(MaDV),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia  DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (MaHD, MaDV)
);
GO

-- Dữ liệu mẫu
INSERT INTO dbo.BanBida (TenBan, LoaiBan, GiaTheoGio) VALUES
(N'Bàn Lỗ 01', N'Pool (Lỗ)', 60000), (N'Bàn Lỗ 02', N'Pool (Lỗ)', 60000), (N'Bàn Lỗ 03', N'Pool (Lỗ)', 60000),
(N'Bàn Lỗ 04', N'Pool (Lỗ)', 70000), (N'Bàn Carom 01', N'Carom (Phăng)', 50000), (N'Bàn Carom 02', N'Carom (Phăng)', 50000),
(N'Bàn Carom 03', N'Carom (Phăng)', 55000), (N'Bàn Snooker 01', N'Snooker', 120000), (N'Bàn Snooker 02', N'Snooker', 120000);

INSERT INTO dbo.MenuDichVu (TenDV, DonGia, TonKho) VALUES
(N'Nước tăng lực Sting', 15000, 100),
(N'Nước suối', 10000, 150),
(N'Trà đá', 5000, 200),
(N'Bia Tiger', 25000, 80),
(N'Mì xào bò', 45000, 30),
(N'Khô gà lá chanh', 35000, 2),
(N'Thuê gậy Carbon', 30000, NULL),
(N'Thuê găng tay', 10000, NULL);

-- Bàn Lỗ 02 và Bàn Carom 01 đang có khách chơi
INSERT INTO dbo.HoaDon (MaBan, GioBatDau) VALUES
(2, DATEADD(MINUTE, -95, GETDATE())),
(5, DATEADD(MINUTE, -40, GETDATE()));
INSERT INTO dbo.ChiTietHoaDon (MaHD, MaDV, SoLuong, DonGia) VALUES
(1, 1, 3, 15000), (1, 5, 1, 45000), (1, 7, 1, 30000),
(2, 3, 2, 5000);
UPDATE dbo.BanBida SET TrangThai = N'Đang chơi' WHERE MaBan IN (2, 5);
GO

SELECT * FROM dbo.BanBida;
SELECT * FROM dbo.MenuDichVu;
SELECT * FROM dbo.HoaDon;
