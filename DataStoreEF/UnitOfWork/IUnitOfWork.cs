using Core.Models;
using DataStoreEF.Repository;
using System;
using System.Threading.Tasks;

namespace DataStoreEF.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Hotel> Hotels { get; set; }
        IGenericRepository<Country> Countries { get; set; }

        Task<int> CommitAsync();
    }
}