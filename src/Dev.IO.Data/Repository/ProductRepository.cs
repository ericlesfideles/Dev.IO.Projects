using AppMvcBasic.Models;
using Dev.IO.Data.Context;
using DevIO.Bussiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IO.Data.Repository
{
    public class ProductRepository : Repository<ProductEntity>, IProductRepository
    {
        public ProductRepository(MyDbContext myDbContext) : base(myDbContext) { }        
        
        public async Task<ProductEntity> GetProductAndSupplier(Guid id)
        {
            return await _MyDbContext.Products.AsNoTracking().Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductEntity>> GetProductBySupplierId(Guid supplierId)
        {
            return await Filter(p => p.SupplierId == supplierId);
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsAndSuppliers()
        {
            return await _MyDbContext.Products.AsNoTracking().Include(p => p.Supplier)
                .OrderBy(p => p.Name).ToListAsync();
        }
    }
}
