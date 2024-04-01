using Route.C41.G02.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace Route.C41.G02.MVC03.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(22, 30)]
        public int Age { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        //public DateTime CreationDate { get; set; } = DateTime.Now;
        //public bool IsDeleted { get; set; } = false;

        public Gander Gander { get; set; }
        public EmpType EmployeeType { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

        public int? DepartmentId { get; set; } // Foreign Key

        public Department Department { get; set; } //  Navigational Property [One]
    }
}
