using AppMvcBasic.Models;
using DevIO.Bussiness.Models.Validations;
using DevIO.Bussiness.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        public async Task Create(SupplierEntity entity)
        {
            if (!ExecuteValidation(new SupplierValidation(), entity)
                && !ExecuteValidation(new AndressValidation(), entity.Andress)) return;
            
        }

        public async Task Edit(SupplierEntity entity)
        {
            if (!ExecuteValidation(new SupplierValidation(), entity)) return;
        }

        public async Task UpdateAndress(AndressEntity andress)
        {
            if (!ExecuteValidation(new AndressValidation(), andress)) return;
        }

        public Task Delete(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
