using AutoMapper;
using MS.Entities;
using MS.Models.ViewModel;

namespace MS.Models.Automapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel, Role>();

            //CreateMap<User, UserData>()
            //    .ForMember(a => a.Id, t => t.MapFrom(b => b.Id))
            //    .ForMember(a => a.RoleName, t => t.MapFrom(b => b.Role.Name))
            //    .ForMember(a => a.RoleDisplayName, t => t.MapFrom(b => b.Role.DisplayName))
            //    .ForMember(a => a.MainDepartmentId, t => t.MapFrom(b => b.UserDepartments.First(x => x.IsMainDepartment == true).Department.Id))
            //    .ForMember(a => a.MainDepartmentDisplayName, t => t.MapFrom(b => b.UserDepartments.First(x => x.IsMainDepartment == true).Department.GetDisplayName()))
            //    ;
        }
    }
}
