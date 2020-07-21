using AppMvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Interfaces
{
    public interface IProductRepository: IRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> GetProductBySupplierId(Guid supplierId);
        Task<IEnumerable<ProductEntity>> GetProductsAndSuppliers();
        Task<ProductEntity> GetProductAndSupplier(Guid id);

    }
}
