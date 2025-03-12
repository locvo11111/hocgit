using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Services.ThietLapService
{
    public class ThietLapService : IThietLapService
    {
        public List<Provinces> GetAllProvinces()
        {
            //return _unitOfWork.Query<Department>("SELECT_ALL_DEPARTMENT");
        }

        public List<Department> GetDepartments()
        {
            //throw new NotImplementedException();
        }
    }
}
