using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesToContract
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T?> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}
