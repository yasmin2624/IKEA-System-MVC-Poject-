using IKEA.BLL.DTOS.Department;
using IKEA.BLL.Factories;
using IKEA.DAL.Models.Depratments;
using IKEA.DAL.Presistance.Repositories.Departments;
using IKEA.DAL.Presistance.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Servies.Departments
{
    public class DepartmentService(IunitOfWork unitOfWork) : IDepartmentService
    {
        private readonly IunitOfWork unitOfWork = unitOfWork;

        // Get All Departments
        public IEnumerable<DepartmentsDto> GetAllDepartments()
        {
            var departments = unitOfWork.DepartmentsRepository.GetAll();
            return departments.Select(d => d.ToDepartmentDto());
        }


        // Get Department By Id
        public DepartmentDetailsDto? GetDepartmentDetailsById(int id)
        {
            var department = unitOfWork.DepartmentsRepository.GetById(id);
            return department is null ? null : department.ToDepartmentDetailsDto();
            //if (department is null) return null;
            //else 
            //{
            //    var departmentDetailsDto = new DepartmentDetailsDto
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Code = department.Code,
            //        Description = department.Description,
            //        CreatedBy = department.CreatedBy,
            //        DateOfCreation = department.CreatedOn,
            //        LastModificationBy = department.LastModificationBy,
            //        LastModificationOn = department.LastModificationOn,
            //        IsDeleted = department.IsDeleted
            //    };
            //    return departmentDetailsDto;
            //}
            //-------------------------
            //return department is null ? null : new DepartmentDetailsDto()
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description,
            //    CreatedBy = department.CreatedBy,
            //    DateOfCreation = department.CreatedOn,
            //    LastModificationBy = department.LastModificationBy,
            //    LastModificationOn = department.LastModificationOn,
            //    IsDeleted = department.IsDeleted
            //};




        }

        // Add Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.toEntity();
            unitOfWork.DepartmentsRepository.Add(department);
            return unitOfWork.SaveChanges();
        }

        // Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
             unitOfWork.DepartmentsRepository.Update(departmentDto.toEntity());
            return unitOfWork.SaveChanges();
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = unitOfWork.DepartmentsRepository.GetById(id);
            if (department is null) return false;
            else
            {
                 unitOfWork.DepartmentsRepository.Remove(department);
                return unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

    }
}
