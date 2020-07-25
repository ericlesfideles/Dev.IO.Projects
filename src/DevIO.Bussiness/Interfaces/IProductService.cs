using AppMvcBasic.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Services
{
    public interface IProductService
    {
        Task Create(ProductEntity entity);

        Task Edit(ProductEntity entity);

        Task Delete(Guid Id);
    }
}