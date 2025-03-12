
WITH TempDt AS
(
	
	SELECT ct.Code, CodeDuAn, CodeCongTrinh, ct.CodeHangMuc, CodeNhaThau, 
	CodeNhaThauPhu, CodeToDoi, TenDuAn, TenCongTrinh, TenHangMuc, TenCongTac, AutoDivision, IsAutoSynthetic, ct.NgayBatDau, ct.NgayKetThuc, KhoiLuongKeHoach, ct.DonGia, ct.DonGiaThiCong
FROM Tbl_TDKH_NhomCongTac ct
JOIN view_CongTacWithHangMucCongTrinh p
ON ct.KhoiLuongKeHoach IS NOT NULL AND ct.NgayBatDau IS NOT NULL AND ct.NgayKetThuc IS NOT NULL AND p.CodeNhom = ct.Code
	--Condition
GROUP BY ct.Code
 ),
 dates(DateValue) AS 
(
SELECT * FROM 
(
VALUES --Dates
) 
),
ctacsAll AS 
(
SELECT DateValue, ct.Code,
CASE WHEN AutoDivision = 0 THEN 0 ELSE (ct.KhoiLuongKeHoach - COALESCE(SUM(hn.KhoiLuongKeHoach), 0))/NULLIF(julianday(ct.NgayKetThuc) - julianday(ct.NgayBatDau) + 1 - COUNT(CASE WHEN hn.KhoiLuongKeHoach IS NOT NULL AND hn.Ngay BETWEEN ct.NgayBatDau AND ct.NgayKetThuc THEN hn.Code END), 0) END AS AverageKhoiLuongKeHoach,
CASE WHEN AutoDivision = 0 THEN 0 ELSE  MAX(julianday(MIN(d.DateValue, ct.NgayKetThuc)) - julianday(ct.NgayBatDau) + 1 - COUNT(CASE WHEN hn.Ngay <= DateValue AND hn.KhoiLuongKeHoach IS NOT NULL THEN hn.Ngay END), 0) END AS NumberOfAutoDate,
COALESCE(SUM(CASE WHEN hn.Ngay <= DateValue THEN hn.KhoiLuongKeHoach END), 0) AS LuyKeKhoiLuongKeHoach,
SUM(CASE WHEN DateValue = hn.Ngay THEN hn.KhoiLuongKeHoach END) AS KhoiLuongKeHoach,
SUM(CASE WHEN hn.Ngay <= DateValue THEN hn.KhoiLuongThiCong ELSE 0 END) AS LuyKeKhoiLuongThiCong,
SUM(CASE WHEN DateValue = hn.Ngay THEN hn.KhoiLuongThiCong END) AS KhoiLuongThiCong
FROM TempDt ct 
LEFT JOIN Tbl_TDKH_KhoiLuongCongViecTungNgay hn ON hn.CodeNhom = ct.Code
JOIN dates d ON 1=1
GROUP BY DateValue, ct.Code, AutoDivision, ct.KhoiLuongKeHoach, ct.NgayBatDau, ct.NgayKetThuc
),
ctacs AS
(
SELECT ct.Code, ct.DateValue, ct.AverageKhoiLuongKeHoach, ct.NumberOfAutoDate, 
v.NgayBatDau, v.NgayKetThuc, v.CodeNhaThau, v.CodeNhaThauPhu, v.CodeToDoi,
v.CodeDuAn, v.TenDuAn,
v.CodeCongTrinh, v.TenCongTrinh,
v.CodeHangMuc, v.TenHangMuc, v.DonGia, v.DonGiaThiCong, 
v.AutoDivision, v.IsAutoSynthetic,
ct.KhoiLuongKeHoach, ct.KhoiLuongThiCong,
ct.LuyKeKhoiLuongKeHoach, ct.LuyKeKhoiLuongThiCong
FROM ctacsAll ct
JOIN TempDt v
ON ct.Code = v.Code
)
,
tempTC AS
(
SELECT ct.CodeGiaoThau AS Code,c.DateValue,
SUM(CASE WHEN hn.Ngay = c.DateValue THEN hn.KhoiLuongThiCong END) AS KhoiLuongThiCong, 
SUM(CASE WHEN hn.Ngay = c.DateValue THEN hn.KhoiLuongKeHoach END) AS KhoiLuongKeHoach,
SUM(hn.KhoiLuongThiCong) AS LuyKeKhoiLuongThiCong, 
SUM(hn.KhoiLuongKeHoach) AS LuyKeKhoiLuongKeHoach
FROM ctacs c
JOIN view_CongTacKeHoachThiCong ct
ON (c.CodeNhaThau IS NOT NULL AND c.Code = ct.CodeGiaoThau)
JOIN Tbl_TDKH_KhoiLuongCongViecTungNgay hn
ON ct.Code = hn.CodeNhom AND hn.Ngay <= c.DateValue
GROUP BY ct.CodeGiaoThau, c.DateValue
),
KLS AS
(
SELECT ct.Code AS ParentCode, ct.DateValue AS Ngay, ct.DonGia, ct.DonGiaThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
COALESCE(tc.KhoiLuongThiCong, ct.KhoiLuongThiCong) AS KhoiLuongThiCong, 
COALESCE(tc.LuyKeKhoiLuongThiCong, ct.LuyKeKhoiLuongThiCong) AS LuyKeKhoiLuongThiCong,
CASE WHEN ct.AutoDivision = 0 AND ct.IsAutoSynthetic = 1 THEN tc.KhoiLuongKeHoach 
ELSE CASE WHEN ct.DateValue BETWEEN NgayBatDau AND NgayKetThuc THEN
COALESCE(ct.KhoiLuongKeHoach, ct.AverageKhoiLuongKeHoach) ELSE 0 END END AS KhoiLuongKeHoach,
CASE WHEN ct.AutoDivision = 0 AND ct.IsAutoSynthetic = 1 THEN tc.LuyKeKhoiLuongKeHoach 
ELSE ct.LuyKeKhoiLuongKeHoach + ct.AverageKhoiLuongKeHoach*ct.NumberOfAutoDate END AS LuyKeKhoiLuongKeHoach
FROM ctacs ct
LEFT JOIN tempTC tc ON ct.Code = tc.Code AND ct.DateValue = tc.DateValue
)
SELECT ParentCode, Ngay,
KhoiLuongKeHoach, KhoiLuongThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
LuyKeKhoiLuongKeHoach,
LuyKeKhoiLuongThiCong,
KhoiLuongKeHoach*DonGia AS ThanhTienKeHoach,
KhoiLuongThiCong*DonGia AS ThanhTienThiCong,
LuyKeKhoiLuongKeHoach*DonGia AS LuyKeThanhTienKeHoach,
LuyKeKhoiLuongThiCong*DonGia AS LuyKeThanhTienThiCong
FROM KLS ct