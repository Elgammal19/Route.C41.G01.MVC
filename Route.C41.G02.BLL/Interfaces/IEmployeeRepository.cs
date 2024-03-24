﻿using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        // IEnumerable to use any collection that implement this interface to iterate on collection to dispaly records in table
        IEnumerable<Employee> GetAll();

        Employee Get(int id);

        // Retrun type "int" to know the number of records that affected in DB
        int Add(Employee record);

        // Retrun type "int" to know the number of records that updated in DB
        int Update(Employee record);

        // Retrun type "int" to know the number of records that deleted from DB
        int Delete(Employee record);
    }
}
