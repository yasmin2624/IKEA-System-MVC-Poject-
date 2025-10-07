using IKEA.BLL.DTOS.Employee;
using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Factories
{
    public static class EmployeeFactory
    {// Get All
        public static EmployeesDto ToEmployeesDto(this Employee employee)
        {
            return new EmployeesDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                Gender = employee.Gender,             
                EmployeeType = employee.EmployeeType
            };
        }

        // Get By Id
        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employee employee)
        {
            return new EmployeeDetailsDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate =employee.HiringDate, // DateTime → DateOnly
                Gender = employee.Gender,                     // Enum → String
                EmployeeType = employee.EmployeeType,
                CreatedBy = employee.CreatedBy,
                CreatedOn = employee.CreatedOn,
                LastModificationBy = employee.LastModificationBy,
                LastModificationOn = employee.LastModificationOn,
                //IsDeleted = employee.IsDeleted
            };
        }

        // Create
        public static Employee ToEntity(this CreatedEmployeeDto dto)
        {
            return new Employee
            {
                Name = dto.Name,
                Age = dto.Age,
                Address = dto.Address,
                Salary = dto.Salary,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HiringDate = dto.HiringDate,
                Gender = dto.Gender,
                EmployeeType = dto.EmployeeType,
                IsActive = dto.IsActive,
                CreatedOn = DateTime.Now
            };
        }

        // Update
        public static Employee ToEntity(this UpdatedEmployeeDto dto)
        {
            return new Employee
            {
                Id = dto.Id,
                Name = dto.Name,
                Age = dto.Age,
                Address = dto.Address,
                IsActive = dto.IsActive,
                Salary = dto.Salary,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HiringDate = dto.HiringDate, // DateOnly → DateTime
                Gender = dto.Gender,
                EmployeeType = dto.EmployeeType,
                LastModificationOn = DateTime.Now
            };
        }
    }
}
