using IKEA.BLL.DTOS.Employee;
using IKEA.BLL.Servies.Departments;
using IKEA.BLL.Servies.Employees;
using IKEA.DAL.Models.Shared.Enums;
using IKEA.PL.ViewsModels.EmployeeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IKEA.PL.Controllers
{
    public class EmployeeController(IEmployeeService employeeService,
                                     ILogger<EmployeeController> logger,
                                     IDepartmentService departmentService,
                                     IWebHostEnvironment environment) : Controller
    {
        private readonly IEmployeeService employeeService = employeeService;
        private readonly ILogger<EmployeeController> logger = logger;
        private readonly IDepartmentService departmentService = departmentService;
        private readonly IWebHostEnvironment environment = environment;

        #region Index
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = employeeService.GetAllEmployees(false);
            if (!string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                employees = employees.Where(e => e.Name.Contains(EmployeeSearchName, StringComparison.OrdinalIgnoreCase));
            }
            return View(employees);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var employee = employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            return View(employee);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var Model = new EmployeeViewModel
            { 
               Departments = departmentService.GetAllDepartments()
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }),
            };

            return View(Model);
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = new CreatedEmployeeDto
                {
                    Name = viewModel.Name,
                    Age = viewModel.Age,
                    Address = viewModel.Address,
                    Email = viewModel.Email,
                    PhoneNumber = viewModel.PhoneNumber,
                    Salary = viewModel.Salary,
                    IsActive = viewModel.IsActive,
                    HiringDate = viewModel.HiringDate,
                    Gender = viewModel.Gender,
                    EmployeeType = viewModel.EmployeeType,
                    DepartmentId = viewModel.DepartmentId,
                    Image = viewModel.Image,
                };
                try
                {
                    int result = employeeService.AddEmployee(dto);
                    if (result > 0)
                    {
                        TempData["Message"] = "Employee created successfully!";
                        TempData["MessageType"] = "success"; 
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError("", "Employee could not be added!");
                    TempData["Message"] = "Employee could not be added!";
                    TempData["MessageType"] = "error";

                    ModelState.AddModelError("", "Employee could not be added!");
                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment())
                        ModelState.AddModelError("", ex.Message);
                    else
                        logger.LogError(ex, "Error while creating employee");
                }
            }

            if (!ModelState.IsValid)
            {
                var errors=ModelState.Values.SelectMany(v => v.Errors).Select(e=> e.ErrorMessage).ToList();
                ViewBag.Errors = errors;
                viewModel.Departments = departmentService.GetAllDepartments()
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name });

            }

            return View(viewModel);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var employee = employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            var vm = new EmployeeViewModel
            {
               
                Name = employee.Name,
                Age = employee.Age ?? 0,
                Address = employee.Address,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,

                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                DepartmentId = employee.DepartmentId,
                //Image = employee.ImageName,

            };
            vm.Departments = departmentService.GetAllDepartments()
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name ,
                    Selected= d.Id == employee.DepartmentId
                    });

            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel vm)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid)
            {
                vm.Departments = departmentService.GetAllDepartments()
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.Name,
                        Selected=d.Id == vm.DepartmentId
                    });

                return View(vm);
            }

            

            try
            {

                var updated = new UpdatedEmployeeDto
                {
                    Id = id.Value,
                    Name = vm.Name,
                    Age = vm.Age,
                    Address = vm.Address,
                    Email = vm.Email,
                    PhoneNumber = vm.PhoneNumber,
                    Salary = vm.Salary,
                    IsActive = vm.IsActive,
                    HiringDate = vm.HiringDate,
                    Gender = vm.Gender,
                    EmployeeType = vm.EmployeeType,
                    DepartmentId = vm.DepartmentId,
                    Image = vm.Image,
                };

                var result = employeeService.UpdateEmployee(updated);
                if (result > 0)
                {
                    TempData["Message"] = "Employee updated successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Employee could not be updated!");
                TempData["Message"] = "Employee could not be updated!";
                TempData["MessageType"] = "error";


                ModelState.AddModelError("", "Employee could not be updated!");
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                    ModelState.AddModelError("", ex.Message);
                else
                    logger.LogError(ex, "Error while editing employee");
            }

            return View(vm);
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var employee = employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();

            try
            {
                bool deleted = employeeService.DeleteEmployee(id);
                if (deleted)
                {
                    TempData["Message"] = "Employee deleted successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Employee could not be deleted!");
                TempData["Message"] = "Employee could not be deleted!";
                TempData["MessageType"] = "error";


                ModelState.AddModelError("", "Employee could not be deleted!");
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                    ModelState.AddModelError("", ex.Message);
                else
                    logger.LogError(ex, "Error while deleting employee");
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}
