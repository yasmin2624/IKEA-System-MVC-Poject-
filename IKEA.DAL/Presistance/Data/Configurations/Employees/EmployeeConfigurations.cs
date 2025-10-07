using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Data.Configurations.Employees
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
           
            builder.Property(e => e.Id).UseIdentityColumn(1, 1);

            builder.Property(e => e.Name)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.HasCheckConstraint("CK_Employee_Age", "[Age] BETWEEN 24 AND 50");

            
            builder.Property(e => e.Address)
                   .HasColumnType("varchar(150)")
                   .IsRequired();

            builder.Property(e => e.Email)
                   .HasColumnType("varchar(100)")
                   .IsRequired();

            builder.Property(e => e.PhoneNumber)
                   .HasColumnType("varchar(20)");

            //builder.HasCheckConstraint("CK_Employee_Gender", "[Gender] IN ('Male','Female')");

            //builder.HasCheckConstraint("CK_Employee_EmployeeType", "[EmployeeType] IN ('FullTime','PartTime')");

            builder.Property(e => e.Gender)
                .HasConversion((EmpGender) => EmpGender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender));

            builder.Property(e => e.EmployeeType)
                .HasConversion((EmpType) => EmpType.ToString(),
                (type) => (EmployeeTypes)Enum.Parse(typeof(EmployeeTypes), type));

            builder.Property(e => e.Salary)
                   .HasColumnType("decimal(18,2)");

            builder.Property(e => e.HiringDate)
                   .HasColumnType("date");

            builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.LastModificationOn).HasDefaultValueSql("GETDATE()");
        }
    }
}
