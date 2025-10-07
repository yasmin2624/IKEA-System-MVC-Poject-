﻿using IKEA.DAL.Models.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOS.Employee
{
    public class UpdatedEmployeeDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
        [MinLength(3, ErrorMessage = "Min length should be 5 characters")]
        public string Name { get; set; } = null!;
        [Range(22, 30)]
        public int Age { get; set; }
        [RegularExpression("^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
           ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeTypes EmployeeType { get; set; }

        public int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }

    }
}
