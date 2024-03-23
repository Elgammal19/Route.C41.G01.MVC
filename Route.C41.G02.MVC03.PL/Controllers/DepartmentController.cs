using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid) // Server side validation
            {
                var count = _departmentRepo.Add(department);
                if(count > 0) 
                    return RedirectToAction("Index");
            }
            return View(department);
        }

    }
}
