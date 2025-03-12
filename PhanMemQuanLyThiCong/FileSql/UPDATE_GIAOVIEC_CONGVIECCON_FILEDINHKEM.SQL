UPDATE Tbl_GiaoViec_CongViecCon_FileDinhKem SET
	[State] = @State,
	LastChange = @LastChange
WHERE
	Code=@Code
	