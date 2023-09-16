using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _dbContext;

        protected GenericRepository(DbContextClass context)
        {
            _dbContext = context;
        }       

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public void Detach(object obj)
        {
            var entry = _dbContext.Entry(obj);
            entry.State = EntityState.Detached;
        }
    }
}
