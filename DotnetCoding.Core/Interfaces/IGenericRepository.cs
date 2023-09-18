using DotnetCoding.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task Update(T obj);
        Task Delete(T obj);
        Task Create(T obj);
        Task<T> GetByGuid<T>(Guid id) where T : class, IGuid;
    }
}
