using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Services.ThietLapService
{
    public interface IThietLapService
    {
        List<Provinces> GetAllProvinces();
        List<Department> GetDepartments();
    }
}
