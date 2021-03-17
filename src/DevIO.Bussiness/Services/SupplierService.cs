using AppMvcBasic.Models;
using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Models.Validations;
using DevIO.Bussiness.Validations;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAndressRepository _andressRepository;

        public SupplierService(ISupplierRepository supplierRepository,
                                   IAndressRepository andressRepository,
                                   INotify notify): base(notify)
        {
            _supplierRepository = supplierRepository;
            _andressRepository = andressRepository;


        }
        public async Task Create(SupplierEntity entity)
        {
            if (!ExecuteValidation(new SupplierValidation(), entity)
                    || !ExecuteValidation(new AndressValidation(), entity.Andress)) return;

            if(_supplierRepository.Filter(e => e.Document == entity.Document).Result.Any())
            {
                Notify("Já existe fornecedor com o Documento informado.");
                return;
            }
            await _supplierRepository.Create(entity);
        }

        public async Task Edit(SupplierEntity entity)
        {
            if (!ExecuteValidation(new SupplierValidation(), entity)) return;


            if (_supplierRepository.Filter(e => e.Document == entity.Document && e.Id != entity.Id).Result.Any())
            {
                Notify("Já existe fornecedor com o Documento informado.");
                return;
            }
            await _supplierRepository.Edit(entity);
        }

        public async Task UpdateAndress(AndressEntity andress)
        {
            if (!ExecuteValidation(new AndressValidation(), andress)) return;
            await _andressRepository.Edit(andress);
        }

        public async Task Delete(Guid Id)
        {
            if (_supplierRepository.GetSupllierAndAndressAndProduct(Id).Result.Products.Any())
            {
                Notify("O fornecedor possui produtos cadastrados!");
                return;
            }

            var andress = await _andressRepository.GetAndressbySupplierId(Id);

            if(andress != null)
            {
                await _andressRepository.Delete(andress.Id);
            }

            await _supplierRepository.Delete(Id);
        }

        public void Dispose()
        {
            _supplierRepository.Dispose();
            _andressRepository.Dispose();
        }
    }
}
