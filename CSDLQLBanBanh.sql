drop database QLBanBanh
go
CREATE DATABASE QLBanBanh
GO
USE	QLBanBanh
GO
CREATE TABLE NHASANXUAT
(
	MaNSX INT IDENTITY(1,1) NOT NULL,
	TenNSX NVARCHAR(50) NOT NULL,
	DiaChiNSX NVARCHAR(200),
	DienThoaiNSX VARCHAR(11) NOT NULL,
	CONSTRAINT PK_nhasanxuat PRIMARY KEY (MaNSX)
)
GO
CREATE TABLE LOAISP
(
	MaLoaiSP INT IDENTITY(1,1) NOT NULL,
	TenLoaiSP NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_loaisp PRIMARY KEY (MaLoaiSP)
)
GO
CREATE TABLE SANPHAM
(
	MaSP INT IDENTITY(1,1) NOT NULL,
	TenSP NVARCHAR(50) NOT NULL,
	GiaBan DECIMAL(18,0) CHECK (Giaban>=0),
	MoTa NVARCHAR(1000),
	AnhBia VARCHAR(50),
	NgayCapNhat DATETIME2,
	SoLuongTon INT NOT NULL,
	PhanTramKM INT NOT NULL,
	MaNSX INT NOT NULL,
	MaLoaiSP INT NOT NULL,
	CONSTRAINT PK_sanpham PRIMARY KEY (MaSP),
	CONSTRAINT FK_sanpham_nhasanxuat FOREIGN KEY (MaNSX) REFERENCES NHASANXUAT(MaNSX),
	CONSTRAINT FK_sanpham_loaisp FOREIGN KEY (MaLoaiSP) REFERENCES LOAISP(MaLoaiSP)
)
GO

CREATE TABLE KHACHHANG
(
	MaKH INT IDENTITY(1,1) NOT NULL,
	HoTenKH NVARCHAR(50),
	TaiKhoanKH NVARCHAR(50) UNIQUE,
	MatKhauKH VARCHAR(50) NOT NULL,
	CMND VARCHAR(11) NOT NULL,
	EmailKH VARCHAR(100) UNIQUE,
	DiaChiKH NVARCHAR(200),
	DienThoaiKH VARCHAR(11) NOT NULL,
	NgaySinhKH DATETIME2,
	CONSTRAINT PK_khachhang PRIMARY KEY (MaKH) 
)

GO
CREATE TABLE DONDATHANG
(
	MaDDH INT IDENTITY(1,1) NOT NULL,
	NgayDat DATETIME2,
	TinhTrangGiaoHang bit,
	TenNguoiNhan NVARCHAR(50) NOT NULL,
	SdtNguoiNhan VARCHAR(11) NOT NULL,
	DCGiaoHang NVARCHAR(100) NOT NULL,
	NgayGiao DATETIME2,
	MaKH INT NOT NULL,
	CONSTRAINT PK_dondathang PRIMARY KEY (MaDDH),
	CONSTRAINT FK_dondathang_makh FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH),
)
GO
CREATE TABLE CHITIETDDH
(
	MaDDH INT NOT NULL,
	MaSP INT NOT NULL,
	SoLuongBan INT CHECK(SoLuongBan>0),
	DonGia DECIMAL(18,0) CHECK(DonGia>=0),
	CONSTRAINT PK_chitietddh_dondathang_sanpham PRIMARY KEY (MaDDH, MaSP),
	CONSTRAINT FK_chitietddh_dondathang FOREIGN KEY (MaDDH) REFERENCES DONDATHANG(MaDDH),
	CONSTRAINT FK_chitietddh_sanpham FOREIGN KEY (MaSP) REFERENCES SANPHAM(MASP)
)
GO
CREATE TABLE ADMIN
(
	UserAdmin VARCHAR(30) PRIMARY KEY,
	PassAdmin VARCHAR(30) NOT NULL,
	HoTen NVARCHAR(50) NOT NULL,
)
/* KHÁCH HÀNG*/
INSERT INTO KHACHHANG VALUES(N'Võ Phương Duy', 'thientuduy', '123456', '261375504', 'vophuongduy155@gmail.com', N'441/32/18 Điện Biên Phủ, P.25, Q.Bình Thạnh, TP.HCM', '01663471033', Convert(date, '15/05/1994',103))
INSERT INTO KHACHHANG VALUES(N'Lý Kiều Giang', 'gianglee', '123456', '123456789', 'gianglee@gmail.com', N'111 Hàm Nghi, Phường 2, Q.1, TP.HCM', '0979899999', Convert(date, '09/09/1989',103))
INSERT INTO KHACHHANG VALUES(N'Phạm Thanh Thảo', 'thanhthao', '123456', '234123421', 'thanhthao@gmail.com', N'111 Hàm Nghi, Phường 2, Q.1, TP.HCM', '0979899999', Convert(date, '09/09/1989',103))

GO
/******* NHA SAN XUAT *******/
INSERT INTO NHASANXUAT VALUES(N'Kinh Đô- KIDO', N' 6/134 Quốc Lộ 13, Phường Hiệp Bình Phước, Quận Thủ Đức, TP. HCM', '0833497497')
INSERT INTO NHASANXUAT VALUES(N' VIETFOODS', N'Số 170, Phố Sài Đồng, Quận Long Biên, Tp. Hà Nội', '0837979797')
INSERT INTO NHASANXUAT VALUES(N'Bibica', N':443 Lý Thường Kiệt, Phường 8, Quận Tân Bình, TPHCM', '0834564567')
INSERT INTO NHASANXUAT VALUES(N'Hải Châu', N'15 Mạc Thị Bưởi, Vĩnh Tuy, Hà Nội', '0836789999')
INSERT INTO NHASANXUAT VALUES(N'(ABC Bakery', N'111 Quang Trung, Phường 13, Q.Gò Vấp, TP.HCM', '0833334444')
INSERT INTO NHASANXUAT VALUES(N'Hải Hà Kotobuki', N'25 Trương Định, Phường Trương Định, Quận Hai Bà Trưng, TP. Hà Nội.', '0833778899')
/******* LOAI SAN PHAM *******/
INSERT INTO LOAISP VALUES(N'Gato-Origato')
INSERT INTO LOAISP VALUES(N'Bánh Baumkuchen')
INSERT INTO LOAISP VALUES(N'Orimochi')
INSERT INTO LOAISP VALUES(N'Bánh mỳ')
INSERT INTO LOAISP VALUES(N'Bánh cắt nhỏ')
INSERT INTO LOAISP VALUES(N'Gato Đặc Biệt')
INSERT INTO LOAISP VALUES(N'Bánh trường học')
INSERT INTO LOAISP VALUES(N'Bánh Bao')
/* SẢN PHẨM*/
/* GATO...*/
INSERT INTO SANPHAM VALUES(N'Gato oto Mcqueen', 280000, N'Bánh ngon bổ rẻ, tuyệt vời','1.png', Convert(date, '20/07/2016',103), 5, 0, 1, 1)
INSERT INTO SANPHAM VALUES(N'Gato Socola Sữa', 270000, N'Bánh ngon bổ rẻ, tuyệt vời','2.jpg', Convert(date, '20/07/2016',103), 5, 0, 2, 1)
INSERT INTO SANPHAM VALUES(N'Gato Tim Tiramitsu', 370000, N'Bánh ngon bổ rẻ, tuyệt vời','3.jpg', Convert(date, '20/07/2016',103), 7, 0, 3, 1)
INSERT INTO SANPHAM VALUES(N'Gato Con Heo', 370000, N'Bánh ngon bổ rẻ, tuyệt vời','4.jpg', Convert(date, '20/07/2016',103), 2, 0, 4, 1)
INSERT INTO SANPHAM VALUES(N'Gato Số 9', 230000, N'Bánh ngon bổ rẻ, tuyệt vời','5.jpg', Convert(date, '20/07/2016',103), 2, 0, 5, 1)
INSERT INTO SANPHAM VALUES(N'Gato Kem Sữa', 240000, N'Bánh ngon bổ rẻ, tuyệt vời','6.jpg', Convert(date, '20/07/2016',103), 2, 0, 6, 1)
/*baumkuchen*/
INSERT INTO SANPHAM VALUES(N'Bánh Baumkuchen', 18000, N'Bánh ngon bổ rẻ, tuyệt vời','7.jpg', Convert(date, '20/07/2016',103), 2, 0, 1, 2)
INSERT INTO SANPHAM VALUES(N'Baumkuchen Quế', 28000, N'Bánh ngon bổ rẻ, tuyệt vời','8.jpg', Convert(date, '20/07/2016',103), 2, 0, 2, 2)
/*Orimochi*/
INSERT INTO SANPHAM VALUES(N'Orimochi chanh leo', 22000, N'Bánh ngon bổ rẻ, tuyệt vời','9.jpg', Convert(date, '20/07/2016',103), 2, 0, 3, 3)
INSERT INTO SANPHAM VALUES(N'Orimochi trà xanh', 24000, N'Bánh ngon bổ rẻ, tuyệt vời','10.jpg', Convert(date, '20/07/2016',103), 2, 0, 4, 3)
INSERT INTO SANPHAM VALUES(N'Orimochi chanh leo', 22000, N'Bánh ngon bổ rẻ, tuyệt vời','11.jpg', Convert(date, '20/07/2016',103), 2, 0, 5, 3)
/*banh mỳ*/
INSERT INTO SANPHAM VALUES(N'Baguette bread', 12000, N'Bánh ngon bổ rẻ, tuyệt vời','12.jpg', Convert(date, '20/07/2016',103), 1, 0, 6, 4)
INSERT INTO SANPHAM VALUES(N'Bread Crisp', 12000, N'Bánh ngon bổ rẻ, tuyệt vời','13.jpg', Convert(date, '20/07/2016',103), 1, 0, 1, 4)
INSERT INTO SANPHAM VALUES(N'Butter Crab bread', 14000, N'Bánh ngon bổ rẻ, tuyệt vời','14.jpg', Convert(date, '20/07/2016',103), 9, 0, 2, 4)
/*bánh cắt nhỏ*/
INSERT INTO SANPHAM VALUES(N'Japanese Cheesecake', 140000, N'Bánh ngon bổ rẻ, tuyệt vời','15.jpg', Convert(date, '20/07/2016',103), 3, 0, 3, 5)
INSERT INTO SANPHAM VALUES(N'Japanese Cheesecake', 14000, N'Bánh ngon bổ rẻ, tuyệt vời','16.jpg', Convert(date, '20/07/2016',103), 3, 0, 4, 5)
INSERT INTO SANPHAM VALUES(N'Bánh Babaroa', 11000, N'Bánh ngon bổ rẻ, tuyệt vời','17.jpg', Convert(date, '20/07/2016',103), 12, 0, 5, 5)
INSERT INTO SANPHAM VALUES(N'Bánh cuốn Plain', 18000, N'Bánh ngon bổ rẻ, tuyệt vời','18.jpg', Convert(date, '20/07/2016',103), 12, 0, 6, 5)
/*gato dac biet*/
INSERT INTO SANPHAM VALUES(N'Gato 2 tầng', 710000, N'Bánh ngon bổ rẻ, tuyệt vời','19.png', Convert(date, '20/07/2016',103), 8, 0, 1, 6)
INSERT INTO SANPHAM VALUES(N'Gato đặc biệt', 750000, N'Bánh ngon bổ rẻ, tuyệt vời','20.png', Convert(date, '20/07/2016',103), 4, 0, 2, 6)
INSERT INTO SANPHAM VALUES(N'Gato có gia đỡ', 810000, N'Bánh ngon bổ rẻ, tuyệt vời','21.png', Convert(date, '20/07/2016',103), 7, 0, 3, 6)
/*bánh trường học*/
INSERT INTO SANPHAM VALUES(N'Bánh Skin TH', 70000, N'Bánh ngon bổ rẻ, tuyệt vời','22.jpg', Convert(date, '20/07/2016',103), 2, 0, 4, 7)
INSERT INTO SANPHAM VALUES(N'Bánh Ekurea TH', 20000, N'Bánh ngon bổ rẻ, tuyệt vời','23.jpg', Convert(date, '20/07/2016',103), 3, 0, 5, 7)
INSERT INTO SANPHAM VALUES(N'Bánh Skin cute', 30000, N'Bánh ngon bổ rẻ, tuyệt vời','24.jpg', Convert(date, '20/07/2016',103), 8, 0, 6, 7)
/*bánh bao*/
INSERT INTO SANPHAM VALUES(N'Bánh bao thập cẩm', 27000, N'Bánh ngon bổ rẻ, tuyệt vời','25.jpg', Convert(date, '20/07/2016',103), 0, 0, 1, 8)
INSERT INTO SANPHAM VALUES(N'Bánh bao xá xíu', 28000, N'Bánh ngon bổ rẻ, tuyệt vời','26.jpg', Convert(date, '20/07/2016',103), 6, 0, 2, 8)
INSERT INTO SANPHAM VALUES(N'Bánh bao chay', 23000, N'Bánh ngon bổ rẻ, tuyệt vời','27.jpg', Convert(date, '20/07/2016',103), 2, 0, 3, 8)
/*admin*/
INSERT INTO ADMIN VALUES('admin', '123456', N'Lý Kiều Giang')
INSERT INTO ADMIN VALUES('thientuduy', '123456', N'Võ Phương Duy')




