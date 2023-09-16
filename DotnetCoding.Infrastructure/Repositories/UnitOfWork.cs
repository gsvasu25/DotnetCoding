using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;
        public IProductRepository Products { get; }
        public IProductQueueRepository ProductQueqe {get; }

        public UnitOfWork(DbContextClass dbContext,
                            IProductRepository productRepository, IProductQueueRepository productQueqe)
        {
            _dbContext = dbContext;
            Products = productRepository;
            ProductQueqe = productQueqe;
        }
        public void Detach(IEnumerable<object> objects)
        {
            foreach (var obj in objects)
            {
                var entry = _dbContext.Entry(obj);
                entry.State = EntityState.Detached;
            }
        }
        public void Detach(object obj)
        {
                var entry = _dbContext.Entry(obj);
                entry.State = EntityState.Detached;
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                _dbContext.Dispose();
            }
        }

    }
}
