using AppMvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Interfaces
{
    public interface IAndressRepository: IRepository<AndressEntity>
    {
        Task<AndressEntity> GetAndressbySupplierId(Guid SupplierId);



    }
}
