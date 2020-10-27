using AppMvcBasic.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Services
{
    public interface ISupplierService: IDisposable
    {
        Task Create(SupplierEntity entity);

        Task Edit(SupplierEntity entity);

        Task Delete(Guid Id);

        Task UpdateAndress(AndressEntity andress);

    }
}