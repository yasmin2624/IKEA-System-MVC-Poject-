using IKEA.DAL.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewsModels.EmployeeViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
        [MinLength(5, ErrorMessage = "Min length should be 5 characters")]
        public string Name { get; set; } = null!;

        [Range(22, 30)]
        public int Age { get; set; }

        [RegularExpression(@"^\d{1,3}-[A-Za-z0-9\s]{2,20}-[A-Za-z0-9\s]{2,20}-[A-Za-z0-9\s]{2,20}$",
    ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; } = null!;

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeTypes EmployeeType { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name="Department")]
        public int? DepartmentId { get; set; }
        public IEnumerable<SelectListItem>? Departments { get; set; }

        public IFormFile? Image { get; set; }
       

    }
}
