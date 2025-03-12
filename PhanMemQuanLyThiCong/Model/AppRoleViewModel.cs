using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using System;

namespace PhanMemQuanLyThiCong.Model
{
    public class AppRoleViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public AccountType Type { get; set; }
        public int? RoleLevelId { get; set; }

        public string TypeName 
        {
            get
            {
                return Type.GetEnumDisplayName();
            }
        }
    }
}