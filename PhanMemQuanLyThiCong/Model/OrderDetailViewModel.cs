using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemQuanLyThiCong.Model
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int OrderId { set; get; }
        public string SerialNoId { set; get; }
        public int Quantity { set; get; } = 1;
        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { set; get; }
        public Guid UserId { set; get; }
        public Guid CustomerId { get; set; }
        public int ProductId { get; set; }
        public TypeCode TypeCode { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public TypeOrder TypeOrder { get; set; }
        public KeyStoreViewModel KeyStore { get; set; }
    }
}