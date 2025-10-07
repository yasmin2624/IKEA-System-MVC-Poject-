using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.UnitOfWork
{
    public class UnitOfwork : IunitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private readonly Lazy<IEmployeeRepository> employeeRepository;
        private readonly Lazy<IDepartmentRepository> departmentRepository;

        public UnitOfwork(ApplicationDbContext dbContext , IEmployeeRepository employeeRepository ,IDepartmentRepository departmentRepository) 
        {
            this.dbContext = dbContext;
            this.employeeRepository = new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(dbContext) );
            this.departmentRepository = new Lazy<IDepartmentRepository>(()=> new DepartmentRepository(dbContext));
        }

        public IEmployeeRepository EmployeesRepository => employeeRepository.Value;

        public IDepartmentRepository DepartmentsRepository => departmentRepository.Value;

        public int SaveChanges()
        { 
            return dbContext.SaveChanges();
        }
    }
}
