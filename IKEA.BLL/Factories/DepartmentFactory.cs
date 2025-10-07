using IKEA.BLL.DTOS.Department;
using IKEA.DAL.Models.Depratments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Factories
{
    public static class DepartmentFactory
    {
        // Get All
        public static DepartmentsDto ToDepartmentDto (this Department department )
        {
          return new DepartmentsDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
        }

        // Get By Id
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                DateOfCreation = department.CreatedOn,
                LastModificationBy = department.LastModificationBy,
                LastModificationOn = department.LastModificationOn,
                IsDeleted = department.IsDeleted
            };
        }

        // Create
        public static Department toEntity (this CreatedDepartmentDto D)
        {
            return new Department
            {
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                CreatedOn = D.DateOfCreation ?? DateTime.Now
            };
        }

        // Update
        public static Department toEntity (this UpdatedDepartmentDto U)
        {
            return new Department
            {
                Id = U.Id,
                Name = U.Name,
                Code = U.Code,
                Description = U.Description,
                CreatedOn = U.DateOfCreation
            };
        }

    }
}
