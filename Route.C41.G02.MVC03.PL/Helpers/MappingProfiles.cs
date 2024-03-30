using AutoMapper;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.C41.G02.MVC03.PL.ViewModels;

namespace Route.C41.G02.DAL.Helpers
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel , Employee>().ReverseMap();
        }
    }
}
