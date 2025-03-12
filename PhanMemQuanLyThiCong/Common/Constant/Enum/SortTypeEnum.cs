using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant.Enum
{
    public enum SortTypeEnum
    {
        [Display(Name = "Id")]
        ID,
        [Display(Name = "Tên")]
        NAME,
        [Display(Name = "Đơn vị giao")]
        NAME_DELIVERER,
        [Display(Name = "Đơn vị nhận")]
        NAME_RECIPIENT,
        [Display(Name = "Ngày")]
        DATE,
        [Display(Name = "Loại")]
        TYPE,
        [Display(Name = "Giá")]
        PRICE,
        [Display(Name = "Khối lượng")]
        VOLUME,
        [Display(Name = "Thành tiền")]
        TOTALAMOUNT,
        [Display(Name = "Thời gian cập nhật")]
        LASTMODIFIED,
        [Display(Name = "Thời gian khởi tạo")]
        CREATEDON,
        [Display(Name = "Mặc định")]
        SortId
    }

    public enum SortDirectionEnum
    {
        ASCENDING,
        DESCENDING,
    }
}
