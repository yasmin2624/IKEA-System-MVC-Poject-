using IKEA.BLL.DTOS.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Servies.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeesDto> GetAllEmployees(bool WithTracking);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int AddEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
