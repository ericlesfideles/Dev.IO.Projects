using AppMvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Services
{
    public class ProductService : BaseService, IProductService
    {
        public Task Create(ProductEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(ProductEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
