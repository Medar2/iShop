using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public interface IGenericRepository<T> where T: class
    {
        //---><T> Anotacion Diamante

        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int Id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistsAsync(int Id);


    }
}
