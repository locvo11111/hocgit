using PhanMemQuanLyThiCong.Common.Enums;

namespace PhanMemQuanLyThiCong.Model
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? HomeOrder { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
        public bool? HomeFlag { get; set; }
        public int DisplayOrder { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public Status Status { get; set; }
        public int? DayLimit { get; set; }
    }
}