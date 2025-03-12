UPDATE Tbl_MTC_ChiTietNhatTrinh SET 
TenMaythiCong = @TenMaythiCong,
CodeCongTac = @CodeCongTac,
CodeCongTacThuCong = @CodeCongTacThuCong,
GioBatDauSang = @GioBatDauSang,
GioKetThucSang = @GioKetThucSang,
GioBatDauChieu = @GioBatDauChieu,
GioKetThucChieu = @GioKetThucChieu,
GioBatDauToi = @GioBatDauToi,
GioKetThucToi = @GioKetThucToi,
TongGio = @TongGio,
HinhAnh = @HinhAnh,
NgayThang = @NgayThang,
NhienLieuChinh = @NhienLieuChinh,
NhienLieuPhu = @NhienLieuPhu,
GhiChu = @GhiChu 
WHERE
	Code = @Code