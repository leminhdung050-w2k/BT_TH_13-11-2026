-- BÀI 6.6: Hệ thống quản lý mượn trả sách thư viện
IF DB_ID(N'LibraryDB') IS NULL
    CREATE DATABASE LibraryDB;
GO

USE LibraryDB;
GO

IF OBJECT_ID(N'dbo.ChiTietMuon', N'U') IS NOT NULL DROP TABLE dbo.ChiTietMuon;
IF OBJECT_ID(N'dbo.PhieuMuon', N'U') IS NOT NULL DROP TABLE dbo.PhieuMuon;
IF OBJECT_ID(N'dbo.DauSach', N'U') IS NOT NULL DROP TABLE dbo.DauSach;
IF OBJECT_ID(N'dbo.DocGia', N'U') IS NOT NULL DROP TABLE dbo.DocGia;
GO

-- 1. Độc giả
CREATE TABLE dbo.DocGia
(
    MaDocGia      INT IDENTITY(1,1) PRIMARY KEY,
    HoTen         NVARCHAR(100) NOT NULL,
    Email         VARCHAR(100) NULL UNIQUE,
    NgayHetHanThe DATETIME NULL
);

-- 2. Đầu sách
CREATE TABLE dbo.DauSach
(
    MaSach     INT IDENTITY(1,1) PRIMARY KEY,
    TenSach    NVARCHAR(200) NOT NULL,
    TheLoai    NVARCHAR(50) NULL,                              -- Công nghệ thông tin / Kinh tế / Văn học
    SoLuongCon INT NOT NULL DEFAULT 0 CHECK (SoLuongCon >= 0)
);

-- 3. Phiếu mượn sách
CREATE TABLE dbo.PhieuMuon
(
    MaPhieu   INT IDENTITY(1,1) PRIMARY KEY,
    MaDocGia  INT NOT NULL REFERENCES dbo.DocGia(MaDocGia),
    NgayMuon  DATETIME NOT NULL DEFAULT GETDATE(),
    HanTra    DATETIME NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang mượn'      -- Đang mượn / Đã trả / Quá hạn
);

-- 4. Chi tiết sách mượn
CREATE TABLE dbo.ChiTietMuon
(
    MaPhieu INT NOT NULL REFERENCES dbo.PhieuMuon(MaPhieu),
    MaSach  INT NOT NULL REFERENCES dbo.DauSach(MaSach),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    PRIMARY KEY (MaPhieu, MaSach)
);
GO

-- Dữ liệu mẫu
DECLARE @Nay DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);

INSERT INTO dbo.DocGia (HoTen, Email, NgayHetHanThe) VALUES
(N'Lê Minh Dũng', 'dung.le@thuvien.vn', DATEADD(YEAR, 1, @Nay)),
(N'Trần Thị Mai', 'mai.tran@thuvien.vn', DATEADD(MONTH, 6, @Nay)),
(N'Phạm Quốc Huy', 'huy.pham@thuvien.vn', DATEADD(DAY, -10, @Nay)),     -- thẻ đã hết hạn
(N'Nguyễn Thu Hà', 'ha.nguyen@thuvien.vn', DATEADD(MONTH, 8, @Nay)),
(N'Hoàng Văn Nam', 'nam.hoang@thuvien.vn', DATEADD(MONTH, 3, @Nay));

INSERT INTO dbo.DauSach (TenSach, TheLoai, SoLuongCon) VALUES
(N'Lập trình C# căn bản', N'Công nghệ thông tin', 5),
(N'Cơ sở dữ liệu SQL Server', N'Công nghệ thông tin', 3),
(N'Mạng máy tính', N'Công nghệ thông tin', 4),
(N'Kinh tế vi mô', N'Kinh tế', 6),
(N'Nguyên lý kế toán', N'Kinh tế', 2),
(N'Số đỏ', N'Văn học', 3),
(N'Truyện Kiều', N'Văn học', 1),
(N'Dế mèn phiêu lưu ký', N'Văn học', 4);

INSERT INTO dbo.PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) VALUES
(1, DATEADD(DAY, -20, @Nay), DATEADD(DAY, -6, @Nay), N'Đang mượn'),
(2, DATEADD(DAY, -5, @Nay), DATEADD(DAY, 9, @Nay), N'Đang mượn'),
(4, DATEADD(DAY, -30, @Nay), DATEADD(DAY, -16, @Nay), N'Đã trả'),
(5, DATEADD(DAY, -15, @Nay), DATEADD(DAY, -1, @Nay), N'Đang mượn'),
(1, DATEADD(DAY, -40, @Nay), DATEADD(DAY, -26, @Nay), N'Đã trả'),
(4, DATEADD(DAY, -2, @Nay), DATEADD(DAY, 12, @Nay), N'Đang mượn');

INSERT INTO dbo.ChiTietMuon (MaPhieu, MaSach, SoLuong) VALUES
(1, 1, 1), (1, 2, 1),
(2, 6, 1),
(3, 4, 1), (3, 7, 1),
(4, 5, 1), (4, 3, 1),
(5, 1, 1), (5, 8, 1),
(6, 2, 1);

-- Phiếu chưa trả mà đã quá hạn trả -> "Quá hạn"
UPDATE dbo.PhieuMuon SET TrangThai = N'Quá hạn' WHERE TrangThai = N'Đang mượn' AND HanTra < GETDATE();
GO

SELECT * FROM dbo.DocGia;
SELECT * FROM dbo.DauSach;
SELECT * FROM dbo.PhieuMuon;
