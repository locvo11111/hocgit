using DevExpress.XtraRichEdit.Model;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhanMemQuanLyThiCong.Model
{
    public class KeyInfoViewModel
    {
        public KeyInfoViewModel()
        {
        }
        public Guid UserId { set; get; }

        public string FullName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string SerialNo { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ServerIP { get; set; } = string.Empty;

        public string ServerPort { get; set; } = string.Empty;

        public string ServerName { get; set; } = string.Empty;

        public string UrlAPI { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string PassWord { get; set; } = string.Empty;

        public string CategoryCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime StartSeverDate { get; set; }
        public DateTime EndSeverDate { get; set; }

        public DateTime DateNow { get; set; }

        public bool IsDateLimit { get; set; } = true;
        public TypeStatus TypeCode { get; set; }

        public TypeOrder TypeOrder { get; set; }

        public KeyStatus Status { get; set; }
        public string KeyCode { get; set; }

        public int LimitUser { get; set; }
        public int LimitUserExternal { get; set; }

        public string Token { get; set; }

        public int AccountTypeId { get; set; }
        public double ServerQuota { get; set; }
        public int SubscriptionTypeId { get; set; } = 1;

        public string AccountTypeName
        {
            get
            {
                return ((AccountType)AccountTypeId).GetEnumDisplayName();
            }
        }
        public bool IsUsing
        {
            get
            {
                return SerialNo == BaseFrom.BanQuyenKeyInfo.SerialNo;
            }
        }

        public bool IsSeverExpired
        {
            get
            {
                return (StartSeverDate > DateTime.Now.Date || EndSeverDate < DateTime.Now.Date);
            }
        }

        [JsonIgnore]
        public string ServerDurationInfo
        {
            get
            {
                if (IsSeverExpired)
                    return "Hết hạn";
                else
                    return $"Còn {(EndSeverDate.Date - DateTime.Now.Date).Days + 1} Ngày";
            }
        }

        [JsonIgnore]
        public string DisplayKey
        {
            get
            {
                if (string.IsNullOrEmpty(KeyCode))
                    return SerialNo;
                else return KeyCode;
            }
        }

        [JsonIgnore]
        public string DisplaySubscriptionType
        {
            get
            {
                return ((SubscriptionTypeEnum)SubscriptionTypeId).GetEnumDisplayName();
            }
        }
    }
}
