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
    public class GenericRepository<T>: IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) // Ask CLR to creating an object from "ApplicationDbContext" 
        {
            _dbContext = dbContext;
        }

        public int Add(T record)
        {
            _dbContext.Set<T>().Add(record);
            return _dbContext.SaveChanges();
        }

        public int Update(T record)
        {
            _dbContext.Set<T>().Update(record);
            return _dbContext.SaveChanges();
        }

        public int Delete(T record)
        {
            _dbContext.Set<T>().Remove(record);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            //var Employee = _dbContext.Employees.Local.Where(D =>D.Id == id).FirstOrDefault();   

            //if(Employee == null)
            //    Employee = _dbContext.Employees.Where(D => D.Id == id).FirstOrDefault();

            //return Employee;

            return _dbContext.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking().ToList();
        }
    }
}
