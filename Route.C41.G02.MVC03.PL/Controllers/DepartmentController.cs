using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;

namespace Route.C41.G02.MVC03.PL.Controllers
{
    // Inheritance : DepartmentController : Is a Controller
    // Association[Composition] : DepartmentController : Has a DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public IActionResult Index()
        {
            var department = _departmentRepo.GetAll();
            return View(department);
        }
    }
}
