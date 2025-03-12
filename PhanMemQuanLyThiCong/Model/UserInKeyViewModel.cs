using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Model
{
    public class UserInKeyViewModel
    {
        //public int Id { get; set; }

        public string SerialNo { get; set; }

        public Guid UserId { get; set; }

        public AccountType AccountTypeId { get; set; }
        public bool HaveInitProjectPermission { get; set; } = false;

    }
}