using IKEA.DAL.Models.Depratments;
using IKEA.DAL.Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Departments
{
    public class DepartmentRepository(ApplicationDbContext dbContext) : GenericRepository<Department>(dbContext), IDepartmentRepository
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        // private readonly ApplicationDbContext dbContext;
        //public DepartmentRepository( ApplicationDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}
        //ApplicationDbContext dbContext;

        #region DepartmentRepository Without Genaric
        ////CRUD
        //// 1-Get All
        //public IEnumerable<Department> GetAll(bool WithTracking = false)
        //{
        //    if (WithTracking)
        //    {
        //        return dbContext.Departments.ToList();

        //    }
        //    else
        //    {
        //        return dbContext.Departments.AsNoTracking().ToList();

        //    }
        //}

        //// 2-Get By ID 
        //public Department? GetById(int id/*,ApplicationDbContext dbContext*/)
        //   => dbContext.Departments.Find(id);

        //// 3-Add
        //public int Add(Department department)
        //{
        //    dbContext.Departments.Add(department);
        //    return dbContext.SaveChanges();
        //}


        //// 4-Update
        //public int Update(Department department)
        //{
        //    dbContext.Departments.Update(department);
        //    return dbContext.SaveChanges();
        //}

        //// 5-Delete
        //public int Remove(Department department)
        //{
        //    dbContext.Departments.Remove(department);
        //    return dbContext.SaveChanges();
        //}


        #endregion

     

    }
}
