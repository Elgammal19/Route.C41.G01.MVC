using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Interfaces
{
    public interface IGenericRepository<T> where T :ModelBase
    {
        // IEnumerable to use any collection that implement this interface to iterate on collection to dispaly records in table
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        // Retrun type "int" to know the number of records that affected in DB
        void Add(T record);

        // Retrun type "int" to know the number of records that updated in DB
        void Update(T record);

        // Retrun type "int" to know the number of records that deleted from DB
        void Delete(T record);
    }
}
