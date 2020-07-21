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
    public class AndressRepository : Repository<AndressEntity>, IAndressRepository
    {
        public AndressRepository(MyDbContext myDbContext) : base(myDbContext) {}
        public async Task<AndressEntity> GetAndressbySupplierId(Guid SupplierId)
        {
            return await _MyDbContext.Andresses.AsNoTracking().FirstOrDefaultAsync(a => a.SupplierId == SupplierId);
        }
    }
}
