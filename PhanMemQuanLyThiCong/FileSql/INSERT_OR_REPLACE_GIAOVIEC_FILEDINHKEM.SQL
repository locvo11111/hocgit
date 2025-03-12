INSERT 
	OR REPLACE INTO Tbl_GiaoViec_FileDinhKem ( Code, CodeCongViecCha, FileDinhKem, [Checksum], Modified, [State],  LastChange)
VALUES
	( @Code,
		@CodeCongViecCha,
		@FileDinhKem,
		@Checksum,
		@Modified,
		@State,
		@LastChange)