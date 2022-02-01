﻿using Core.Models;
using DataEF.Repository;
using System;
using System.Threading.Tasks;

namespace DataEF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Hotel> Hotels { get; set; }
        public IGenericRepository<Country> Countries { get; set; }
        
        private readonly CoreDbContext _context;
        private bool disposed = false;

        public UnitOfWork(CoreDbContext context)
        {
            _context = context;
            Hotels = Hotels ?? new GenericRepository<Hotel>(_context);
            Countries = Countries ?? new GenericRepository<Country>(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
