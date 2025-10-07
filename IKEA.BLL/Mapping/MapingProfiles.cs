using AutoMapper;
using IKEA.BLL.DTOS.Employee;
using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Mapping
{
    public class MapingProfiles:Profile
    {
        public MapingProfiles()
        {
            CreateMap<Employee, EmployeesDto>()
     //.ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType.ToString()))
     //.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
     .ForMember(dest => dest.Department,
               opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));


            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, option => option.MapFrom(src => src.EmployeeType))
                 .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                 .ForMember(dest => dest.ImageName , opt =>opt.MapFrom(src => src.ImageName));
                
            
            //CreateMap<Source, Destination>():
            CreateMap<CreatedEmployeeDto,Employee>();
            CreateMap<UpdatedEmployeeDto, Employee>();

        }
    }
}
