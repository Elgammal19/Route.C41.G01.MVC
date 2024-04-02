using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL
{
    public class UnitOfWork : IUnitOfWork 
    {
        // UnitOfWork --> Representation for DbContext 
        // UnitOfWork --> Responseable for communicate with DbContext through DbContext injection 

        private readonly ApplicationDbContext _applicationDb;
        //public IEmployeeRepository EmployeeRepository { get; set; } = null;
        //public IDepartmentRepository DepartmentRepository { get ; set; } = null;

        private Hashtable _Repositories;

        public UnitOfWork(ApplicationDbContext applicationDb)  // Ask CLR for craeting an object from "ApplicationDbContext"
        {
            //EmployeeRepository = new EmployeeRepository(_applicationDb);
            //DepartmentRepository = new DepartmentRepository(_applicationDb);
            _applicationDb = applicationDb;
            _Repositories = new Hashtable();
        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;

            if(!_Repositories.ContainsKey(key))
            {
                if(key == nameof(Employee))
                {
                    var repositories = new EmployeeRepository(_applicationDb) ;
                    _Repositories.Add(key, repositories);
                }
                else
                {
                    var repositories = new GenericRepository<T>(_applicationDb) /*as IGenericRepository<T>*/;
                    _Repositories.Add(key, repositories);
                }
            }

            return _Repositories[key] as IGenericRepository<T>;
        }

        public async Task<int> Complete()
        {
            return await _applicationDb.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await _applicationDb.DisposeAsync();
        }

       
    }
}
