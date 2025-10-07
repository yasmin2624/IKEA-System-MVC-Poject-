using IKEA.BLL.DTOS.Department;

namespace IKEA.BLL.Servies.Departments
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentsDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentDetailsById(int id);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
    }
}