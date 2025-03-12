UPDATE Tbl_MTC_NhienLieu 
SET KhoiLuongDaDung = @KhoiLuongDaDung,
KhoiLuongDaNhap = @KhoiLuongDaNhap,
IsHienThi = @IsHienThi
WHERE
	Code = @Code