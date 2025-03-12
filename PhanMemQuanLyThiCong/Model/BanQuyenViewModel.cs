using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class BanQuyenOldViewModel
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int LimitDate { get; set; }
        public int Status { get; set; }
        public string TypeKhoa { get; set; }
        public int ProvinceId { get; set; }
        public int DepartmentId { get; set; }
    }

    public class RegisterOldViewModel
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string province_id { get; set; }
        public string department_id { get; set; }
        public string key_code { get; set; }
        public string serial_hdd { get; set; }
        public string pc_name { get; set; } = Environment.MachineName;
        public string password { get; set; }
    }
    #region BanQuyenKeyInfo
    public class BanQuyenKeyInfo
    {
        public BanQuyenKeyInfo(AppUserViewModel user) 
        {
            FullName = user.FullName;
            UserId = user.Id;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Address = user.Address;
            Company = user.Company;
            Department = user.Department;
            
        }
            
        public BanQuyenKeyInfo(LoginResponse lr)
        {
            FullName = lr.FullName;
            UserId = lr.Id;
            Email = lr.Email;
            PhoneNumber = lr.PhoneNumber;
            Address = lr.Address;
            Company = lr.Company;
            Department = lr.Department;
        }

        public BanQuyenKeyInfo() { }
        public string FullName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Avatar { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string SerialNo { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public int LimitUser { get; set; }
        public int LimitUserExternal { get; set; }
        public string ServerIP { get; set; } = string.Empty;

        public string ServerPort { get; set; } = string.Empty;

        public string ServerName { get; set; } = string.Empty;

        public string UrlAPI { get; set; } = string.Empty;
        public string BaseSeverUrl
        {
            get
            {
                return UrlAPI.Replace("api/", string.Empty);
            }
        }

        public string DatabaseName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public string PassWord { get; set; } = string.Empty;

        public string CategoryCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime DateNow { get; set; }
        public string KeyCode { get; set; }

        public bool IsDateLimit { get; set; } = true;
        public int LimitDate { get; set; }

        public DateTime StartSeverDate { get; set; } = DateTime.MinValue;
        public DateTime EndSeverDate { get; set; } = DateTime.MinValue;

        public bool IsSeverExpired
        {
            get
            {
                return (StartSeverDate > DateTime.Now.Date || EndSeverDate < DateTime.Now.Date);
            }
        }

        public TypeStatus TypeCode { get; set; } = TypeStatus.ALL;
        public KeyStatus Status { get; set; }

        public double ServerQuota { get; set; }
        public int SubscriptionTypeId { get; set; } = 1;

        public string DisplaySubscriptionType
        {
            get
            {
                return ((SubscriptionTypeEnum)SubscriptionTypeId).GetEnumDisplayName();
            }
        }


        //public bool IsSelectedKey
        //{
        //    get
        //    {
        //        return (!string.IsNullOrEmpty(SerialNo));
        //    }
        //}

    }
    #endregion
}
