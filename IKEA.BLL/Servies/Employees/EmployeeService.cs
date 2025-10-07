
using AutoMapper;
using IKEA.BLL.DTOS.Employee;
using IKEA.BLL.Factories;
using IKEA.BLL.Servies.AttachementsService;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositories.Employees;
using IKEA.DAL.Presistance.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Servies.Employees
{
    public class EmployeeService(IunitOfWork unitOfWork,IAttachementService attachementService, IMapper mapper) : IEmployeeService
    {
        //private readonly IEmployeeRepository employeeRepository = employeeRepository;
        private readonly IunitOfWork unitOfWork = unitOfWork;
        private readonly IAttachementService attachementService = attachementService;
        private readonly IMapper mapper = mapper;

        public IEnumerable<EmployeesDto> GetAllEmployees(bool WithTracking = false)
        {
            var employees = unitOfWork.EmployeesRepository.GetAll(WithTracking);
            // src ==> Employee
            // Dest ==> EmployeeDto
            var EmployeeDto = mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeesDto>>(employees);
            return EmployeeDto;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = unitOfWork.EmployeesRepository.GetById(id);
            //var employeeDetailsDto = mapper.Map<Employee,EmployeeDetailsDto>(employee);
            //return employeeDetailsDto;
            return employee is null ? null : mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }


        public int AddEmployee(CreatedEmployeeDto employeeDto)
        {
            if (employeeDto.Age < 22 || employeeDto.Age > 30)
                throw new ArgumentException("Age must be between 22 and 30.");

            var employee = mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
              
                employee.ImageName = attachementService.Upload(employeeDto.Image, "Images", employee.ImageName);
            }
            unitOfWork.EmployeesRepository.Add(employee);
            return unitOfWork.SaveChanges();
        }


        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = unitOfWork.EmployeesRepository.GetById(employeeDto.Id);
            if (employee is null) return 0;

            mapper.Map(employeeDto, employee);

            if (employeeDto.Image is not null)
            {
                
                employee.ImageName = attachementService.Upload(
                    employeeDto.Image,
                    "Images",
                    employee.ImageName 
                );
            }

            unitOfWork.EmployeesRepository.Update(employee);
            return unitOfWork.SaveChanges();
        }


        public bool DeleteEmployee(int id)
        {
            var employee = unitOfWork.EmployeesRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                unitOfWork.EmployeesRepository.Update(employee);
                   return unitOfWork.SaveChanges() >0 ? true : false;
            }
                

        }

       
    }
}
