using IKEA.BLL.DTOS.Department;
using IKEA.BLL.Servies.Departments;
using IKEA.DAL.Models.Depratments;
using IKEA.PL.ViewsModels.DepartmentViewModels;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment) : Controller
    {
        private readonly IDepartmentService departmentService = departmentService;
        private readonly ILogger<DepartmentController> logger = logger;
        private readonly IWebHostEnvironment environment = environment;

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentService.GetAllDepartments();
            return View(Departments);
        }

        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    { Code = viewModel.Code,
                        Name = viewModel.Name,
                        DateOfCreation=viewModel.CreationDate,
                        Description = viewModel.Description,

                    };

                    int Result = departmentService.AddDepartment(departmentDto);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can Not Be Added !");
                        //return View(departmentDto);
                    }
                }
                catch (Exception ex)
                {
                    //Log Exception
                    //1-Development 
                    if (environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        //return View(departmentDto);
                    }

                    //2-Deployment
                    else
                    {
                        logger.LogError(ex.Message);
                        //return View(departmentDto);
                    }

                }

            }
            return View(viewModel);

        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {

            if (!id.HasValue) return BadRequest();//400
            var department = departmentService.GetDepartmentDetailsById(id.Value);
            if (department is null) return NotFound();//404
            return View(department);


        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = departmentService.GetDepartmentDetailsById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.DateOfCreation ?? DateTime.Now,

            };

            return View(departmentViewModel);
        }

        //---------------
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, DepartmentViewModel ViewModel)
        {
            if (!ModelState.IsValid) return View(ViewModel);
            try
            {
                var UpdatedDepartment = new UpdatedDepartmentDto()
                {
                    Id = id.Value,
                    Code = ViewModel.Code,
                    Name = ViewModel.Name,
                    Description = ViewModel.Description,
                    DateOfCreation = ViewModel.CreationDate
                };

                int Result = departmentService.UpdateDepartment(UpdatedDepartment);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can Not Be Edited !");
                    return View(ViewModel);
                }

            }
            catch (Exception ex)
            {
                //Log Exception
                //1-Development 
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(ViewModel);
                }

                //2-Deployment
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);

                }

            }
            return View(ViewModel);
        }

        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = departmentService.GetDepartmentDetailsById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = departmentService.DeleteDepartment(id);
                if (Deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can Not Be Deleted !");
                   return RedirectToAction(nameof(Index),new {id});
                }
            }
            catch (Exception ex)
            {
                //Log Exception
                //1-Development 
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    var department = departmentService.GetDepartmentDetailsById(id);
                    if (department is null) return NotFound();
                    return RedirectToAction(nameof(Index));
                }
                //2-Deployment
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }

            }
            




        }
        #endregion
    }
}