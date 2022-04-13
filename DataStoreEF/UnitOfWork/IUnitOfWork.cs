using Core.Models;
using DataEF.Repository;
using System;
using System.Threading.Tasks;

namespace DataEF.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Hotel> Hotels { get; set; }
        IGenericRepository<Country> Countries { get; set; }

        Task<int> CommitAsync();
    }
}