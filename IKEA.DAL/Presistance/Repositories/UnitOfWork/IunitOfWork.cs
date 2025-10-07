using IKEA.DAL.Presistance.Repositories.Departments;
using IKEA.DAL.Presistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.UnitOfWork
{
    public interface IunitOfWork
    {
        public IEmployeeRepository EmployeesRepository { get; }
        public IDepartmentRepository DepartmentsRepository { get; }

        int SaveChanges();  
    }
}
