using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly POSContext _context;
        public ProductRepository(POSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<Product>> GetAllProducts()
        {
            var response = new BaseEntityResponse<Product>();
            var items = await _context.Products.Include(e => e.CategoryId).ToListAsync();

            response.TotalRecords = items.Count();
            response.Items = items;

            return response;
        }
    }
}
