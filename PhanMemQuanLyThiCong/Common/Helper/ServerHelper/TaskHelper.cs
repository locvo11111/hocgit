using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper.SeverHelper
{
    public class TaskHelper
    {
        public static async Task<List<TaskRecord>> GetAllTask()
        {
            var res = await CusHttpClient.InstanceCustomer
                .MGetAsync<List<TaskRecord>>(RouteAPI.TASK_GetAll);

            if (!res.MESSAGE_TYPECODE)
                return null;

            return res.Dto;
        }
    }
}
