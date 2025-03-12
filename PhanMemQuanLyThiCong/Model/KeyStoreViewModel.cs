 using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class KeyStoreViewModel
    {
        public string SerialNo { get; set; }

        public string KeyCode { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? ServerId { get; set; }

        public TypeStatus TypeCode { get; set; } = TypeStatus.KHOACUNG;
        public TypeOrder TypeOrder { get; set; } = TypeOrder.Unworn;
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public int? ProductCategoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public string EndDateKy { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartDateKy { get; set; } = null;
        public bool IsDateLimit { get; set; } = false;
        public string TypeOfLock { get; set; }
        public string CategoryCode { get; set; }
        public string ProductName { get; set; }
        public string SeverName { get; set; }
        public string DatabaseName { get; set; }
        public int LimitUser { set; get; }
        public Status Status { get; set; } = Status.InActive;
        public string StatusKy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string CustumerName { set; get; }
        public string CustumerEmail { set; get; }
        public string CustumerPhoneNumber { set; get; }

        public bool IsUsing
        {
            get
            {
                return SerialNo == BaseFrom.BanQuyenKeyInfo.SerialNo;
            }
        }


        public virtual ProductCategoryViewModel ProductCategory { get; set; }

        public virtual ServerInfoViewModel ServerInfo { get; set; }

        public virtual List<PermissionKeyStoreViewModel> Permissions { get; set; }
    }
}

