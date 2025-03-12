using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VChatCore.Dto
{
    public class z_ThongTinTongHopDuAnDto
    {
        public string Code { get; set; }
        //public string DuAnCode { get; set; }
        public string TenTongHopDuAn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string LastUpdate { get; set; }
        public string Description { get; set; }

        //public List<z_ThongTinDuAnDto> DuAns { get; set; }
        //public List<z_ThongTinCongTrinhDto> CongTrinhs { get; set; }
        //public List<z_ThongTinDaiDienChuDauTuDto> ChuDauTus { get; set; }
        //public List<z_ThongTinHangMucDto> HangMucs { get; set; }
        //public List<z_ThongTinNganSachDto> NganSachs { get; set; }
        //public List<z_ThongTinNhaCungCapDto> NhaCungCaps { get; set; }
        //public List<z_ThongTinNhaThauDto> NhaThaus { get; set; }
        //public List<z_ThongTinNhaThauPhuDto> NhaThauPhus { get; set; }
        //public List<z_ThongTinToDoiThiCongDto> ToDoiThiCongs { get; set; }
        //public List<UserRoleDto> UserRoles { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
