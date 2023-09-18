using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using System.Security.Principal;

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
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task Update(T obj)
        {
            _dbContext.Update(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T obj)
        {
            _dbContext.Remove(obj);
            _dbContext.Entry(obj).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
        public async Task Create(T obj)
        {
            _dbContext.Add(obj);
            _dbContext.Entry(obj).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }
        public Task<T> GetByGuid<T>(Guid id) where T : class, IGuid
        {
            return _dbContext.Set<T>().SingleAsync<T>(x => x.GUID == id);
        }
    }
}
