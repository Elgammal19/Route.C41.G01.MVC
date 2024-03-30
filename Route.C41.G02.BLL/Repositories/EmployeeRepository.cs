using Microsoft.EntityFrameworkCore;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //private readonly ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext): base(dbContext)
        {
            //_dbContext= dbContext;
        }

        //private readonly ApplicationDbContext _dbContext;

        //public EmployeeRepository(ApplicationDbContext dbContext) // Ask CLR to creating an object from "ApplicationDbContext" 
        //{
        //    _dbContext = dbContext;
        //}

        //public int Add(Employee record)
        //{
        //    _dbContext.Employees.Add(record);
        //    return _dbContext.SaveChanges();
        //}

        //public int Update(Employee record)
        //{
        //    _dbContext.Employees.Update(record);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Employee record)
        //{
        //    _dbContext.Employees.Remove(record);
        //    return _dbContext.SaveChanges();
        //}

        //public Employee Get(int id)
        //{
        //    //var Employee = _dbContext.Employees.Local.Where(D =>D.Id == id).FirstOrDefault();   

        //    //if(Employee == null)
        //    //    Employee = _dbContext.Employees.Where(D => D.Id == id).FirstOrDefault();

        //    //return Employee;

        //    return _dbContext.Employees.Find(id);
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //    return _dbContext.Employees.AsNoTracking().ToList();
        //}
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(E=> E.Address.ToLower() == address.ToLower());
        }

        public IQueryable<Employee> GetEmployeesByName(string Name)
        {
           return _dbContext.Employees.Where(E=> E.Name.ToLower().Contains(Name.ToLower()));
        }
    }
}
