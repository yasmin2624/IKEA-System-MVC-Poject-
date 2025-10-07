using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.GeneralInterfaces
{
    public interface IGenericRepository<TEntity> where TEntity : ModelBase
    {
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        TEntity? GetById(int id);
        void Add(TEntity TEntity);
        void Update(TEntity TEntity);
        void Remove(TEntity TEntity);
    }
}
