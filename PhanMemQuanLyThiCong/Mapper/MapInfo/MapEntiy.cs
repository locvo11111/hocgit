using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Mapper.MapInfo
{
    public class MapEntiy
    {
        public static void Map()
        {
                //AutoMapper.Mapper.CreateMap<AppUserViewModel, UserViewModel>()
                //.ForMember(des => des.Id, mo => mo.MapFrom(mos => mos.Id.ToString().ToUpper()))
                //.ForMember(des => des.Address, mo => mo.MapFrom(mos => mos.Address))
                //.ForMember(des => des.FullName, mo => mo.MapFrom(mos => mos.FullName))
                //.ForMember(des => des.Avatar, mo => mo.MapFrom(mos => mos.Avatar))
                //.ForMember(des => des.Gender, mo => mo.MapFrom(mos => mos.Gender))
                //.ForMember(des => des.DateOfBirth, mo => mo.MapFrom(mos => mos.DateOfBirth))
                //.ForMember(des => des.Company, mo => mo.MapFrom(mos => mos.Company))
                //.ForMember(des => des.Department, mo => mo.MapFrom(mos => mos.Department))
                //.ForMember(des => des.Position, mo => mo.MapFrom(mos => mos.Position))
                //.ForMember(des => des.TaxCode, mo => mo.MapFrom(mos => mos.TaxCode))
                //.ForMember(des => des.WorkAddress, mo => mo.MapFrom(mos => mos.WorkAddress))
                //.ForMember(des => des.DateCreated, mo => mo.MapFrom(mos => mos.DateCreated))
                //.ForMember(des => des.UserName, mo => mo.MapFrom(mos => mos.UserName))
                //.ForMember(des => des.Email, mo => mo.MapFrom(mos => mos.Email))
                //.ForMember(des => des.PhoneNumber, mo => mo.MapFrom(mos => mos.PhoneNumber));


                AutoMapper.Mapper.CreateMap<UserInKeyViewModel, UserAppViewModel>()
                //.ForMember(des => des.Id, mo => mo.MapFrom(mos => mos.Id))
                .ForMember(des => des.SerialNo, mo => mo.MapFrom(mos => mos.SerialNo))
                //.ForMember(des => des.ServerId, mo => mo.MapFrom(mos => mos.ServerId))
                .ForMember(des => des.UserId, mo => mo.MapFrom(mos => mos.UserId.ToString().ToUpper()));
        }
    }
}
