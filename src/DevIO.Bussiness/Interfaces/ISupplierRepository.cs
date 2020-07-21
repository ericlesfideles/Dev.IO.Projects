using AppMvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Interfaces
{
    public interface ISupplierRepository: IRepository<SupplierEntity>
    {
        Task<SupplierEntity> GetSupllierAndAndress(Guid id);
        Task<SupplierEntity> GetSupllierAndAndressAndProduct(Guid id);
    }
}
