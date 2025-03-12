INSERT 
	OR REPLACE INTO Tbl_MTC_ChiTietDinhMuc ( Code, CodeMay, CodeDinhMuc, DonVi, MucTieuThu,GhiChu )
VALUES
	(	@Code,
		@CodeMay,
		@CodeDinhMuc,
		@DonVi,
		@MucTieuThu,
		@GhiChu)