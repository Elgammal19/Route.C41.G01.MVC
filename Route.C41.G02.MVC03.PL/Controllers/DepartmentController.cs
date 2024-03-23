using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Route.C41.G02.MVC03.PL.Controllers
{
    // Inheritance : DepartmentController : Is a Controller
    // Association[Composition] : DepartmentController : Has a DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepo , IWebHostEnvironment env)
        {
            _departmentRepo = departmentRepo;
            _env = env;
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

        public IActionResult Details(int? id ,string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var dept = _departmentRepo.Get(id.Value);

            if (dept is null)
                return NotFound();  // 404 

            return View(ViewName , dept);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //    return BadRequest(); // 400

            //var dept = _departmentRepo.Get(id.Value);

            //if (dept is null)
            //    return NotFound();  // 404 

            return Details(id , "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // Action Fitler - AntiForgeryToken
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if(id != department.Id)
                return BadRequest(); //400
            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _departmentRepo.Update(department);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
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
                return View(department);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Department department)
        {
            _departmentRepo.Delete(department);
            return RedirectToAction("Index");
        }
    }
}
