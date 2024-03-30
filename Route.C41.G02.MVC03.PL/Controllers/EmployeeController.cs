using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Models;
using System;

namespace Route.C41.G02.MVC03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _empRepo;
        //private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employee/*, IDepartmentRepository department */,IWebHostEnvironment env)
        {
            _empRepo = employee;
            //_departmentRepo = department;
            _env = env;
        }

        //[HttpGet]
        public IActionResult Index(string searchInp)
        {
            // Binding --> 1. Through Model [From , RoutedData , QuerySigment , File]
            //         --> 2. Through View's Dictionary [ViewData , ViewBag] --> Transfer Data from Action to View[OneWay]

            // ViewData
            // Tyep determined in compilation time so it's more faster than ViewBag
            ViewData["Message"] = "Hello ViewData";

            // ViewBag
            // Tyep determined in run time so it's more Slower than ViewData
            ViewBag.Message = "Hello ViewBag";

            if (string.IsNullOrEmpty(searchInp))
            {
                var emp = _empRepo.GetAll();
                return View(emp);
            }
            else
            {
                var emp = _empRepo.GetEmployeesByName(searchInp);
                return View(emp);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag["Departments"] = _departmentRepo.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        { 
            if (ModelState.IsValid)
            {
                // 3. TempData --> Dictionary Type
                // To Transfer Data Between 2 Consecutive Requestrs
                var count = _empRepo.Add(employee);
                if(count > 0)
                     TempData["Message"] = "Department Is Created Successfully"; 
                else
                    TempData["Message"] = "An Error Has Occured , Department Is n't Created ";
                return RedirectToAction("Index");

            }
                return View(employee);

        }

        [HttpGet]
        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var emp = _empRepo.Get(id.Value);

            if(emp is null)
                return NotFound();

            return View(ViewName ,emp);
        }

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            //ViewBag["Departments"] = _departmentRepo.GetAll();

            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Employee employee)
        {
            if (id != employee.Id)
                return BadRequest(); //400
            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                _empRepo.Update(employee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log Exeption
                // Friendly Message
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Has Occured During Updating Department");
                }
                return View(employee);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            _empRepo.Delete(employee);
            return RedirectToAction("Index");
        }
    }
}
