using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.DAL.Models
{
    // Model
    public class Department :ModelBase
    {
        //public int Id { get; set; }

        [Required(ErrorMessage ="Code Is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Display(Name ="Date of Creation")]
        public DateTime DateofCreation { get; set; }

    }
}
