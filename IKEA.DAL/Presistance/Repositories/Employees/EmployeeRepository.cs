using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Employees
{
    public class EmployeeRepository(ApplicationDbContext dbContext) : GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
        #region Employee Without Genaric Repository 
        //public IEnumerable<Employee> GetAll(bool withTracking = false)
        //{
        //    if (withTracking)
        //        return dbContext.Employees.ToList();
        //    else
        //        return dbContext.Employees.AsNoTracking().ToList();
        //}

        //public Employee? GetById(int id)
        //    => dbContext.Employees.Find(id);

        //public int Add(Employee employee)
        //{
        //    dbContext.Employees.Add(employee);
        //    return dbContext.SaveChanges();
        //}

        //public int Update(Employee employee)
        //{
        //    dbContext.Employees.Update(employee);
        //    return dbContext.SaveChanges();
        //}

        //public int Remove(Employee employee)
        //{
        //    dbContext.Employees.Remove(employee);
        //    return dbContext.SaveChanges();
        //} 
        #endregion
      

   
    }
}
