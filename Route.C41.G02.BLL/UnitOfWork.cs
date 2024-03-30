using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL
{
    internal class UnitOfWork : IUnitOfWork 
    {
        // UnitOfWork --> Representation for DbContext 
        // UnitOfWork --> Responseable for communicate with DbContext through DbContext injection 

        private readonly ApplicationDbContext _applicationDb;
        public IEmployeeRepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set; }

        public UnitOfWork(ApplicationDbContext applicationDb)  // Ask CLR for craeting an object from "ApplicationDbContext"
        {
            EmployeeRepository = new EmployeeRepository(_applicationDb);
            DepartmentRepository = new DepartmentRepository(_applicationDb);
            _applicationDb = applicationDb;
        }

        public int Complete()
        {
            return _applicationDb.SaveChanges();
        }

        public void Dispose()
        {
            _applicationDb.Dispose();
        }
    }
}
