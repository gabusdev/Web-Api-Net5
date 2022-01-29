using Core.Models;
using System;
using System.Threading.Tasks;

namespace Web_Api_Net5.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Hotel> Hotels { get; set; }
        IGenericRepository<Country> Countries { get; set; }

        Task<int> CommitAsync();
    }
}