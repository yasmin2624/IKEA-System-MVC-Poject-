﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOS.Department
{
    public  class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public int LastModificationBy { get; set; }
        public DateTime? LastModificationOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
