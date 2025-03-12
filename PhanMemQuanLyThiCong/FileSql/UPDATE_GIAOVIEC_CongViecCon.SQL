UPDATE Tbl_GiaoViec_CongViecCon SET
	TrangThai = @TrangThai,
	LastChange = @LastChange,
	FullNameApprove = @FullNameApprove,
	FullNameSend = @FullNameSend,
	UserApproveId = @UserApproveId,
	NgayDuyet = @NgayDuyet,
	NgayGuiDuyet = @NgayGuiDuyet,
	UserSendId = @UserSendId,
	GhiChuDuyet = @GhiChuDuyet
WHERE
	CodeCongViecCon=@CodeCongViecCon
	