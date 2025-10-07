﻿namespace IKEA.PL.ViewsModels.DepartmentViewModels
{
    public class DepartmentViewModel
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
