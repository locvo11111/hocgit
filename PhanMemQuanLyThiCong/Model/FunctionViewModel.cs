
using PhanMemQuanLyThiCong.Common.Enums;

namespace PhanMemQuanLyThiCong.Model
{
    public class FunctionViewModel
    {
        public string Id { get; set; }

        public string Name { set; get; }

        public int? FuntionTypeId { get; set; }

        public string Url { set; get; }

        public string ParentId { set; get; }

        public string Icon { get; set; }

        public int DisplayOrder { set; get; }
        public KeyStatus Status { set; get; }
        public virtual FunctionViewModel Parent { get; set; }
        public virtual FunctionTypeViewModel FunctionType { get; set; }
    }
}
