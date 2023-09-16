

using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IProductQueueRepository ProductQueqe { get; }
    int Save();
    void Detach(IEnumerable<object> obj);
    void Detach(object product);
}
