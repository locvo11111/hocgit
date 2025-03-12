using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Model
{
    public class AppGroupViewModel
    {
        public int Id { set; get; }

        [StringLength(250)]
        public string Name { set; get; }

        [StringLength(250)]
        public string Description { set; get; }
        public  List<AppUserGroupViewModel> ListUsers { get; set; }

        public  List<AppDuAnGoupViewModel> ListDuAns { get; set; }
        public  List<AppRoleGroupViewModel> ListRoles { get; set; }

    }
}