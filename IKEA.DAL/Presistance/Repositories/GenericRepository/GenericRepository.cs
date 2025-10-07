using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositories.GeneralInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.SharedGenaric
{
    public class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity> where TEntity : ModelBase
    {



        public ApplicationDbContext DbContext { get; } = dbContext;

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (typeof(TEntity) == typeof(Employee))
            {

                var employeeQuery = query as IQueryable<Employee>;

                employeeQuery = employeeQuery
                                    .Where(e => !e.IsDeleted)
                                    .Include(e => e.Department);

                return withTracking
                    ? employeeQuery.ToList() as IEnumerable<TEntity>
                    : employeeQuery.AsNoTracking().ToList() as IEnumerable<TEntity>;
            }

            return withTracking ? query.ToList() : query.AsNoTracking().ToList();
        }

        public TEntity? GetById(int id)
        {
            var entity = DbContext.Set<TEntity>().Find(id);

            if (entity is Employee emp && emp.IsDeleted)
                return null;

            return entity;
        }

        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            //return unitOfWork.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
            //return unitOfWork.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            //return unitOfWork.SaveChanges();
        }
    }

  
}
