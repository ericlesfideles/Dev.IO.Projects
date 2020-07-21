using AppMvcBasic.Models;
using Dev.IO.Data.Context;
using DevIO.Bussiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IO.Data.Repository
{
    public class SupplierRepository : Repository<SupplierEntity>, ISupplierRepository
    {
        public SupplierRepository(MyDbContext myDbContext) : base(myDbContext){ }
        public async Task<SupplierEntity> GetSupllierAndAndress(Guid id)
        {
            return await _MyDbContext.Suppliers.AsNoTracking().Include(s => s.Andress).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<SupplierEntity> GetSupllierAndAndressAndProduct(Guid id)
        {
            return await _MyDbContext.Suppliers.AsNoTracking().Include(s => s.Andress).Include(s => s.Products).FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
