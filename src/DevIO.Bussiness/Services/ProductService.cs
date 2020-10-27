using AppMvcBasic.Models;
using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }

        public async Task Create(ProductEntity entity)
        {
            if (!ExecuteValidation(new ProductValidation(), entity)) return;
            await _productRepository.Create(entity);

        }

        public async Task Edit(ProductEntity entity)
        {
            if (!ExecuteValidation(new ProductValidation(), entity)) return;

            await _productRepository.Edit(entity);
        }

        public async Task Delete(Guid Id)
        {
            await _productRepository.Delete(Id);
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
