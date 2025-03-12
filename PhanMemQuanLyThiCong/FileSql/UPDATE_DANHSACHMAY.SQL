UPDATE Tbl_MTC_DanhSachMay 
SET Ten = @Ten,
TrangThai = @TrangThai,
GhiChu = @GhiChu,
NgayMuaMay = @NgayMuaMay,
DonVi = @DonVi, 
HaoPhi = @HaoPhi,
CaMayKm = @CaMayKm,
No = @No,
LoaiMay = @LoaiMay
WHERE
	Code = @Code