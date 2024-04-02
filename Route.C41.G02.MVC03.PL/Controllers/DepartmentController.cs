using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Route.C41.G02.MVC03.PL.Controllers
{
    // Inheritance : DepartmentController : Is a Controller
    // Association[Composition] : DepartmentController : Has a DepartmentRepository
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IUnitOfWork unitOfWork/*IDepartmentRepository departmentRepo*/ , IWebHostEnvironment env)
        {
            //_departmentRepo = departmentRepo;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var department = await _unitOfWork.Repository<Department>().GetAllAsync();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if(ModelState.IsValid) // Server side validation
            {
                 _unitOfWork.Repository<Department>().Add(department);
                var count = await _unitOfWork.Complete();
                if (count > 0)  
                    return RedirectToAction("Index");
            }
            return View(department);
        }

        public async Task<ActionResult> Details(int? id ,string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var dept = await _unitOfWork.Repository<Department>().GetAsync(id.Value);

            if (dept is null)
                return NotFound();  // 404 

            return View(ViewName , dept);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (!id.HasValue)
            //    return BadRequest(); // 400

            //var dept = _departmentRepo.Get(id.Value);

            //if (dept is null)
            //    return NotFound();  // 404 

            return await Details(id , "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // Action Fitler - AntiForgeryToken
        public async  Task<IActionResult> Edit([FromRoute]int id,Department department)
        {
            if(id != department.Id)
                return BadRequest(); //400
            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _unitOfWork.Repository<Department>().Update(department);
                await _unitOfWork.Complete();
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
        public async Task<IActionResult> Delete(int? id)
        {
            
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
            _unitOfWork.Repository<Department>().Delete(department);
            await _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
