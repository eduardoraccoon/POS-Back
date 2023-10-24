﻿using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<BaseEntityResponse<Product>> GetAllProducts();
    }
}
