﻿using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSContext _context;

        public ICategoryRepository Category { get; private set; }

        public IUserRepository User { get; private set; }

        public IClientRepository Client { get; private set; }

        public IProviderRepository Provider { get; private set; }

        public IProductRepository Product { get; private set; }

        public UnitOfWork(POSContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);
            Client = new ClientRepository(_context);
            Provider = new ProviderRepository(_context);
            Product = new ProductRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
