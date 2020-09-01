using AppMvcBasic.Models;
using DevIO.Bussiness.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Services
{
    public class ProductService : BaseService, IProductService
    {
        public async Task Create(ProductEntity entity)
        {
            if (!ExecuteValidation(new ProductValidation(), entity)) return;
        }

        public async Task Edit(ProductEntity entity)
        {
            if (!ExecuteValidation(new ProductValidation(), entity)) return;
        }

        public async Task Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

    }
}
