PRAGMA foreign_keys = 0;
DROP Table IF Exists temp.TempTbl;
DROP Table IF Exists temp.TempTblHp;
CREATE TABLE temp.TempTblHp AS
WITH tempVt AS 
(
SELECT vt1.Code AS CodeHp, COALESCE(vt2.Code, vt1.Code) AS Code,
vt1.MaVatLieu, vt1.VatTu, vt1.MaTXHienTruong, vt1.VatTu, vt1.DonVi, vt1.DonGia, vt1.DonGiaThiCong, vt1.LoaiVatTu, 
vt1.CodeNhaThau, vt1.CodeNhaThauPhu, vt1.CodeToDoi, vt1.CodeGiaiDoan, vt1.CodeHangMuc, vt1.CodePhanTuyen, vt1.CodeMuiThiCong, 
vt1.NgayBatDau, vt1.NgayKetThuc AS NgayKetThuc, vt1.KhoiLuongKeHoach, vt1.KhoiLuongHopDong,
vt1.Combine
FROM view_HaoPhiVatTu vt1
JOIN view_CongTacWithHangMucCongTrinh ct
ON vt1.CodeCongTac = ct.Code
--Condition1
LEFT JOIN 
Tbl_TDKH_KHVT_VatTu vt2
ON
COALESCE(vt1.MaVatLieu, '') = COALESCE(vt2.MaVatLieu, '')
AND COALESCE(vt1.VatTu, '') = COALESCE(vt2.VatTu, '')
AND COALESCE(vt1.DonVi, '') = COALESCE(vt2.DonVi, '')
AND COALESCE(vt1.DonGia, '') = COALESCE(vt2.DonGia, '')
AND COALESCE(vt1.LoaiVatTu, '') = COALESCE(vt2.LoaiVatTu, '')
AND COALESCE(vt1.CodeHangMuc, '') = COALESCE(vt2.CodeHangMuc, '')
AND COALESCE(vt1.CodePhanTuyen, '') = COALESCE(vt2.CodePhanTuyen, '')
AND COALESCE(vt1.CodeMuiThiCong, '') = COALESCE(vt2.CodeMuiThiCong, '')
AND COALESCE(vt1.CodeNhaThau, vt1.CodeNhaThauPhu, vt1.CodeToDoi, '') = COALESCE(vt2.CodeNhaThau, vt2.CodeNhaThauPhu, vt2.CodeToDoi, '')
AND vt1.CodeGiaiDoan = vt2.CodeGiaiDoan

)
SELECT  * FROM tempVt;

CREATE TABLE temp.TempTbl AS 
SELECT vt1.Code AS CodeHp, Code,
vt1.MaVatLieu, vt1.VatTu, vt1.MaTXHienTruong, vt1.VatTu, vt1.DonVi, vt1.DonGia, vt1.DonGiaThiCong, vt1.LoaiVatTu, 
vt1.CodeNhaThau, vt1.CodeNhaThauPhu, vt1.CodeToDoi, vt1.CodeGiaiDoan, vt1.CodeHangMuc, vt1.CodePhanTuyen, vt1.CodeMuiThiCong, 
MIN(vt1.NgayBatDau) AS NgayBatDau, MAX(vt1.NgayKetThuc) AS NgayKetThuc, SUM(vt1.KhoiLuongKeHoach) AS KhoiLuongKeHoach, SUM(vt1.KhoiLuongHopDong) AS KhoiLuongHopDong,
vt1.Combine
FROM temp.TempTblHp vt1 GROUP BY vt1.Combine, vt1.CodeNhaThau, vt1.CodeNhaThauPhu, vt1.CodeToDoi;
DELETE FROM Tbl_TDKH_KHVT_VatTu WHERE Code NOT IN (SELECT Code FROM temp.TempTbl);

INSERT OR REPLACE INTO Tbl_TDKH_KHVT_VatTu (Code, MaVatLieu, VatTu, MaTXHienTruong, VatTu, DonVi, DonGia, DonGiaThiCong, LoaiVatTu, 
CodeNhaThau, CodeNhaThauPhu, CodeToDoi, CodeGiaiDoan,CodeHangMuc, CodePhanTuyen, CodeMuiThiCong, 
NgayBatDau, NgayKetThuc, KhoiLuongKeHoach, KhoiLuongHopDong)

SELECT Code, MaVatLieu, VatTu, MaTXHienTruong, VatTu, DonVi, DonGia, DonGiaThiCong, LoaiVatTu, 
CodeNhaThau, CodeNhaThauPhu, CodeToDoi, CodeGiaiDoan, CodeHangMuc, CodePhanTuyen, CodeMuiThiCong, 
NgayBatDau, NgayKetThuc, KhoiLuongKeHoach, KhoiLuongHopDong
FROM temp.TempTbl;



UPDATE Tbl_TDKH_HaoPhiVatTu 
SET CodeVatTu = 
(SELECT Code FROM temp.TempTblHp WHERE CodeHp = Tbl_TDKH_HaoPhiVatTu.Code);

PRAGMA foreign_keys = 1;
