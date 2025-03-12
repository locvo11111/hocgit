INSERT 
	OR REPLACE INTO Tbl_MTC_NhienLieuInMay ( Code, CodeMay, CodeNhienLieu, LoaiNhienLieu, MucTieuThu )
VALUES
	( @Code,
		@CodeMay,
		@CodeNhienLieu,
		@LoaiNhienLieu,
		@MucTieuThu )