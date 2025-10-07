using IKEA.DAL.Models.Employees;
using IKEA.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Depratments
{
    public class Department:ModelBase

    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;
        public string? Description { get; set; } 
        public DateOnly CreationDate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();



    }
}
