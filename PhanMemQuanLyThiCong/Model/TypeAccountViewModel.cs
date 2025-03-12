using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Model
{
    public class TypeAccountViewModel
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Name { get; set; }
    }
}