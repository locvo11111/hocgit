UPDATE Tbl_GiaoViec_FileDinhKem SET
	[State] = @State,
	LastChange = @LastChange
WHERE
	Code=@Code
	