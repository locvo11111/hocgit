DELETE FROM TempKhoiLuongVatTu;

WITH JobDateRange AS (
    SELECT ct.NgayBatDau AS Ngay, ct.Code AS CodeVatTu
    FROM Tbl_TDKH_KHVT_VatTu ct
    UNION ALL
    SELECT DATE(Ngay, '+1 day') AS Ngay, ct.Code AS CodeVatTu
    FROM JobDateRange d
    JOIN Tbl_TDKH_KHVT_VatTu ct
    ON d.CodeVatTu = ct.Code
    WHERE Ngay < ct.NgayKetThuc
),
jobDetail AS 
(
SELECT ct.Code AS CodeVatTu, (ct.KhoiLuongKeHoach - TOTAL(hn.KhoiLuongKeHoach))/(julianday(ct.NgayKetThuc) - julianday(ct.NgayBatDau) + 1 - COUNT(hn.Code)) AS AverageKhoiLuongKeHoach
FROM Tbl_TDKH_KHVT_VatTu ct
LEFT JOIN Tbl_TDKH_KHVT_KhoiLuongHangNgay hn
ON ct.Code = hn.CodeVatTu AND hn.KhoiLuongKeHoach IS NOT NULL
GROUP BY ct.Code
)
,
jobAllDate AS
(
SELECT j.Ngay, j.CodeVatTu, 
COALESCE(hn.KhoiLuongKeHoach, ct.AverageKhoiLuongKeHoach) AS KhoiLuongKeHoach, 
hn.KhoiLuongThiCong AS KhoiLuongThiCong, hn.KhoiLuongBoSung AS KhoiLuongBoSung
FROM JobDateRange j
JOIN jobDetail ct
ON j.CodeVatTu = ct.CodeVatTu
LEFT JOIN Tbl_TDKH_KHVT_KhoiLuongHangNgay hn
ON j.CodeVatTu = hn.CodeVatTu AND j.Ngay = hn.Ngay
),

tempTC AS
(
SELECT ct.CodeGiaoThau AS CodeVatTu,Ngay, SUM(hn.KhoiLuongThiCong) AS KhoiLuongThiCong
FROM jobAllDate hn
JOIN view_VatTuKeHoachThiCong ct
ON hn.CodeVatTu = ct.Code AND hn.KhoiLuongThiCong IS NOT NULL AND ct.CodeGiaoThau IS NOT NULL
GROUP BY ct.CodeGiaoThau, Ngay
),

tempKH AS
(
SELECT j.Ngay, j.CodeVatTu, TOTAL(hn.KhoiLuongKeHoach) AS KhoiLuongKeHoach, TOTAL(hn.KhoiLuongThiCong) AS KhoiLuongThiCong, TOTAL(hn.KhoiLuongBoSung) AS KhoiLuongBoSung
FROM JobDateRange j
LEFT JOIN jobAllDate hn
ON j.CodeVatTu = hn.CodeVatTu AND j.Ngay = hn.Ngay
),

abc AS
(SELECT hn.Ngay, hn.CodeVatTu,
COALESCE(tc.KhoiLuongThiCong, hn.KhoiLuongThiCong) AS KhoiLuongThiCong,
hn.KhoiLuongKeHoach, hn.KhoiLuongBoSung
FROM jobAllDate hn
LEFT JOIN  tempTC tc
ON tc.Ngay = hn.Ngay AND tc.CodeVatTu = hn.CodeVatTu
UNION ALL
SELECT tc.Ngay, tc.CodeVatTu, tc.KhoiLuongThiCong, null as KhoiLuongKeHoach, null as KhoiLuongBoSung
FROM tempTC tc
LEFT JOIN  Tbl_TDKH_KHVT_KhoiLuongHangNgay hn
ON tc.Ngay = hn.Ngay AND tc.CodeVatTu = hn.CodeVatTu
WHERE hn.Ngay IS NULL
)

INSERT INTO TempKhoiLuongVatTu
(ParentCode, Ngay, KhoiLuongKeHoach, KhoiLuongBoSung, KhoiLuongThiCong, ThanhTienKeHoach, ThanhTienThiCong, ThanhTienBoSung, LuyKeKhoiLuongKeHoach, LuyKeThanhTienKeHoach, LuyKeKhoiLuongThiCong, LuyKeThanhTienThiCong, LuyKeKhoiLuongBoSung, LuyKeThanhTienBoSung)
SELECT hn.CodeVatTu, hn.Ngay, 
hn.KhoiLuongKeHoach, hn.KhoiLuongBoSung, hn.KhoiLuongThiCong, 
hn.KhoiLuongKeHoach*ct.DonGia,
hn.KhoiLuongThiCong*ct.DonGiaThiCong,
hn.KhoiLuongBoSung*ct.DonGia,
SUM(hn.KhoiLuongKeHoach) OVER (Partition by hn.CodeVatTu order by hn.Ngay) as LuyKeKhoiLuongKeHoach,
SUM(hn.KhoiLuongKeHoach*ct.DonGia) OVER (Partition by hn.CodeVatTu order by hn.Ngay) as LuyKeThanhTienKeHoach,
SUM(hn.KhoiLuongThiCong) OVER (Partition by hn.CodeVatTu order by hn.Ngay) as LuyKeKhoiLuongThiCong,
SUM(hn.KhoiLuongThiCong*ct.DonGia) OVER (Partition by hn.CodeVatTu order by hn.Ngay) as LuyKeThanhTienThiCong,
SUM(hn.KhoiLuongBoSung) OVER (Partition by hn.CodeVatTu order by hn.Ngay) as LuyKeKhoiLuongBoSung,
SUM(hn.KhoiLuongBoSung*ct.DonGia) OVER (Partition by hn.CodeVatTu order by hn.Ngay) as LuyKeThanhTienBoSung
FROM Tbl_TDKH_KHVT_VatTu ct
JOIN abc hn
ON hn.CodeVatTu = ct.Code
ORDER BY hn.Ngay DESC 
