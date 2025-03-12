SELECT
	Code,
	CodeCongViecCha,
	CodeCongViecCon,
	CodeNguoiDung,
	Modified,
	LastChange
FROM
	Tbl_GiaoViec_KeHoach_NguoiThamGia 
WHERE
	CodeCongViecCha = @CodeCongViecCha