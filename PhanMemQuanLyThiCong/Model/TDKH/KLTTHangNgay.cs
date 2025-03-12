using DevExpress.XtraRichEdit.Import.Doc;
using DevExpress.XtraSpreadsheet.Model;
using Microsoft.Owin.Security.Provider;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using StackExchange.Profiling.Internal;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    public class KLTTHangNgay : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public bool Chon { get; set; }
        public string TenCongTac { get; set; }
        public string FileDinhKem { get; set; } = "Xem chi tiết";
        public string NoiDungThucHien { get; set; }
        public string TenDVTH { get; set; }
        public string MaCongTac { get; set; }
        public string LoaiVatTu { get; set; }
        public string CodeCongTrinh { get; set; }
        public string CodeDanhMucCongTac { get; set; }
        public string CodeGoc { get; set; }
        public string CodeNhaCungCap { get; set; }
        public string TenNhaCC { get; set; }
        public string TenCongTrinh { get; set; }
        public string CodeHangMuc { get; set; }
        public string TrangThai { get; set; }
        public string TenHangMuc { get; set; }
        public string TenDuAn { get; set; }

        public string CodeCha { get; set; }
        public string CodeCongViecCon { get; set; }
        //public string CodeCha { get; set; }
        public string CodeDVTH { get; set; }
        public string CodeNhaThau { get; set; }
        public string CodeNhaThauPhu { get; set; }
        public string CodeToDoi { get; set; }
        public string NhaCungCap { get; set; }


        //public List<NhaCungCapHangNgayViewModel> NccsVM { get; set; }
        //public string[] CodesNCC
        //{
        //    get
        //    {
        //        return NccsVM.Select(x => x.Code).ToArray();
        //    }
        //}

        public string CodesNCCString { get; set; }
        


        //public string CodeCha { get; set; }
        public string TenDonViThucHien { get; set; }
        public string MaHieuCongTac { get; set; }
        public string Code { get; set; }
        //public string CodeCongTacTheoGiaiDoan { get; set; }
        public string CodeCongTacTheoGiaiDoan { get; set; }
        public string CodeDauMuc { get; set; }
        //public string CodeCha { get; set; }
        public string ParentCode { get; set; }
        public string CodeHangNgay { get; set; }
        //public string CodeHangNgayTDKH { get; set; }
        public bool? IsSumThiCong { get; set; } = true;
        public string DonVi { get; set; }
        public int? FileCount { get; set; }
        public string FileCountDisp
        {
            get
            {
                if (FileCount.HasValue)
                    return $"{FileCount} Files";
                else return null;
            }
        }
        public double? ProgressTC_KH
        {
            get
            {
                if (!KhoiLuongKeHoach.HasValue || !KhoiLuongThiCong.HasValue)
                    return null;
                else
                {
                    if (!KhoiLuongKeHoach.HasValue || KhoiLuongKeHoach.Value == 0)
                        return null;
                    return (KhoiLuongThiCong.Value - KhoiLuongKeHoach.Value) / KhoiLuongKeHoach.Value;
                }
            }
        }

        public double? GiaTri { get; set; }
        public double? KinhPhiTheoTienDo { get; set; }

        public int MyProperty { get; set; }
        public string LyTrinhCaoDo { get; set; }
        public string GhiChu { get; set; }
        public DateTime? Ngay { get; set; }
        public string NgayString
        {
            get { return Ngay?.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE); }
        }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public bool EnableKeHoach
        {
            get
            {
                return NgayBatDau <= Ngay && NgayKetThuc >= Ngay;
            }
        }
        //public string NgayString {
        //    get {
        //        return Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        //    } 
        //}

        private double? _KhoiLuongKeHoach { get; set; }
        private double? _KhoiLuongBoSung { get; set; }
        private double? _KhoiLuongThiCong { get; set; }
        public double? KhoiLuongKeHoach {
            get
            {
                return _KhoiLuongKeHoach;
            }
            set
            {
                _KhoiLuongKeHoach = value;
                
                if (KLKHTong == 0 || !value.HasValue)
                {
                    TyLeKH = null;
                }
                else
                {
                    TyLeKH = Math.Round(value.Value*100 / KLKHTong);
                }
            }}


        public double? KhoiLuongThiCong
        {
            get
            {
                return _KhoiLuongThiCong;
            }
            set
            {
                _KhoiLuongThiCong = value;

                if (KLKHTong == 0 || !value.HasValue)
                {
                    TyLeTC = null;
                }
                else
                {
                    TyLeTC = Math.Round(value.Value * 100 / KLKHTong);
                }
            }
        }
        public double? KhoiLuongKeHoachGiaoViec { get; set; }
        public double? KhoiLuongKeHoachHangNgay { get; set; }
        //public double? KhoiLuongKeHoach { get; set; }
        public double? DonGiaKeHoach { get; set; }
        public double? ThanhTienKeHoachCustom { get; set; }
        public double? ThanhTienThiCongCustom { get; set; }
        public double? ThanhTienKeHoach
        {
            get
            {
                if (ThanhTienKeHoachCustom.HasValue)
                    return ThanhTienKeHoachCustom;
                else if (KhoiLuongKeHoach.HasValue && DonGiaKeHoach.HasValue)
                    return (long)Math.Round(KhoiLuongKeHoach.Value * DonGiaKeHoach.Value);
                else
                    return null;
            }
        }

        public double? ThanhTienKeHoachSaved { get; set; }
        public double? KinhPhiDuKien { get; set; }

    
        public double? DonGiaThiCong { get; set; }
        public double? KhoiLuongVatLieu { get; set; }
        public double? DonGiaVatLieu { get; set; }
        public double? KhoiLuongHDVatLieu { get; set; }
        public double? DonGiaHDVatLieu { get; set; }
        public double? ThanhTienVatLieu
        {
            get
            {
                if (KhoiLuongVatLieu.HasValue && DonGiaVatLieu.HasValue)
                    return (long)Math.Round(KhoiLuongVatLieu.Value * DonGiaVatLieu.Value);
                else
                    return null;
            }
        }
        public double? ThanhTienHDVatLieu
        {
            get
            {
                if (KhoiLuongHDVatLieu.HasValue && DonGiaHDVatLieu.HasValue)
                    return (long)Math.Round(KhoiLuongHDVatLieu.Value * DonGiaHDVatLieu.Value);
                else
                    return null;
            }
        }
        public double? ThanhTienThiCong
        {
            get
            {
                if (ThanhTienThiCongCustom != 0 && ThanhTienThiCongCustom.HasValue)
                {
                    return ThanhTienThiCongCustom;
                }    
                else if (KhoiLuongThiCong.HasValue && DonGiaThiCong.HasValue)
                    return (long)Math.Round(KhoiLuongThiCong.Value * DonGiaThiCong.Value);
                else return null;
            }
        }
        public long? ThanhTienThiCongSaved { get; set; }

        public double? KhoiLuongHopDong { get; set; }
        public long? ThanhTienHopDong
        {
            get
            {
                if (KhoiLuongHopDong.HasValue && DonGiaKeHoach.HasValue)
                    return (long)Math.Round(KhoiLuongHopDong.Value * DonGiaKeHoach.Value);
                else return null;
            }
        }

        [JsonIgnore]
        public bool IsEditedThiCong { get; set; } = false;

        [JsonIgnore]
        public bool IsEditedKeHoach { get; set; } = false;

        [JsonIgnore]
        public bool IsNewThiCong { get; set; } = false;
        [JsonIgnore]
        public bool IsEdited { get; set; } = false;
        
        [JsonIgnore]
        public bool IsNCCConLai
        {
            get
            {  
                if (ParentCode == null) return false;
                return Code.Contains(ParentCode);
            }
        }

        [JsonIgnore]
        public object DispName
        {
            get
            {
                if (Ngay.HasValue) return Ngay.Value;
                else return TenCongTac;
            }
        }

        public double KLKHTong { get; set; }

        public double? TyLeKH { get;set; }

        public double? TyLeTC { get; set; }

        public bool IsThuCong { get; set; } = false;
    }
}
