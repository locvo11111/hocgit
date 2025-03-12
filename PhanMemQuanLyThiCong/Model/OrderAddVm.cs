using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class OrderAddVm
    {
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustumerName { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustumerAddress { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustumerEmail { set; get; }

        [Required]
        [MaxLength(50)]
        public string CustumerPhoneNumber { set; get; }

        public string CustumerProvince { set; get; }
        public string CustumerMessage { set; get; }
        public string BillCode { get; set; }
        public string Company { get; set; }

        public string Department { get; set; }
        public string Position { get; set; }
        public string TaxCode { get; set; }
        public int ProductCategoryId { get; set; }
        public string PcName { get; set; }
        public string Password { get; set; }
    }
}
