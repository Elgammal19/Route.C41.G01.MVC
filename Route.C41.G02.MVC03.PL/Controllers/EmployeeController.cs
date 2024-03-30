using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.MVC03.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route.C41.G02.MVC03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _empRepo;
        //private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        /// Cons Of Generic Repository Design Pattern :
        /// 1. If we have an action in the controller require a specific service but other actions in the controller doesn't require it
        /// 2. After the injection of this service for this action we will save changes after each injection for each service
        ///    [Beacuse controller doesn't communicate directly with DbContext(Controller --> Repository --> DbContext)]
        public EmployeeController(IUnitOfWork unitOfWork,/*IEmployeeRepository employee*//*, IDepartmentRepository department */IWebHostEnvironment env, IMapper mapper)
        {
            //_empRepo = employee;
            _unitOfWork = unitOfWork;
            //_departmentRepo = department;
            _env = env;
            _mapper = mapper;
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

            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(searchInp))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetEmployeesByName(searchInp);

            var MappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag["Departments"] = _departmentRepo.GetAll();
            //ViewBag.UnitOfWork = _unitOfWork;

            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        { 
            if (ModelState.IsValid)
            {
                // Manual Mapping
                ///var MappedEmp = new Employee
                ///{
                ///    Name = employee.Name,
                ///    Age = employee.Age,
                ///    Address = employee.Address,
                ///    Email = employee.Email,
                ///    Gander = employee.Gander,
                ///    HiringDate = employee.HiringDate,
                ///    Department = employee.Department,
                ///    Salary = employee.Salary,
                ///    IsActive = employee.IsActive
                ///};

                // AutoMapper
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employee);

                /*var count =*/ _unitOfWork.EmployeeRepository.Add(MappedEmp);

                // 3. TempData --> Dictionary Type
                // To Transfer Data Between 2 Consecutive Requestrs

                // Update Department
                // _unitOfWork.DepartmentRepository.Update(department

                // Delete Project
                // _unitOfWork.ProjectRepository.Delete(project);

                // Save.Changes();

                var count = _unitOfWork.Complete();

                if (count > 0)
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

            var emp = _unitOfWork.EmployeeRepository.Get(id.Value);
            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);

            if (emp is null)
                return NotFound();

            return View(ViewName , MappedEmp);
        }

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            //ViewBag["Departments"] = _departmentRepo.GetAll();

            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,EmployeeViewModel employee)
        {
            if (id != employee.Id)
                return BadRequest(); //400
            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employee);
                _unitOfWork.EmployeeRepository.Update(MappedEmp);
                _unitOfWork.Complete();

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
        public IActionResult Delete(EmployeeViewModel employee)
        {
            var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employee);
            _unitOfWork.EmployeeRepository.Delete(MappedEmp);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
