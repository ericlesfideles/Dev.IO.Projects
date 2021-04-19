using AppMvcBasic.Models;
using AutoMapper;
using DevIO.API.ViewModel;
using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Services;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: MainController
    {
        protected readonly IProductRepository _repository;
        protected readonly IProductService _service;
        protected readonly IMapper _mapper;
        public ProductsController(IProductRepository repository,
                                  IProductService service,
                                  INotify notify,
                                  IMapper mapper) : base(notify)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("ListAllProducts")]
        public async Task<IEnumerable<Product>> ListAllProducts()
        {
            return _mapper.Map<IEnumerable<Product>>(await _repository.GetProductsAndSuppliers());
        }

        [HttpGet("GetProductById/{id:guid}")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var product = _mapper.Map<Product>(await _repository.GetById(id));

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpGet("GetProductAndSupllierById/{id:guid}")]
        public async Task<ActionResult<Product>> GetProductAndSupllierById(Guid id)
        {
            var product = _mapper.Map<Product>(await _repository.GetProductAndSupplier(id));

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpGet("GetProductBySupplierId/{id:guid}")]
        public async Task<ActionResult<Product>> GetProductBySupplierId(Guid id)
        {
            var product = _mapper.Map<Product>(await _repository.GetProductBySupplierId(id));

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            if (!ModelState.IsValid) CustomResponse(ModelState);

            var imgName = Guid.NewGuid() + "_" + product.Image;

            if (!UploadFile(product.ImageUpload, imgName)) return CustomResponse();

            product.Image = imgName;

            await _service.Create(_mapper.Map<ProductEntity>(product));

            return CustomResponse(product);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Product>> Update(Guid id, Product product)
        {
            if (id != product.Id) BadRequest();

            var productEdit = _mapper.Map<Product>(await _repository.GetById(id));
            product.Image = productEdit.Image;
            if (!ModelState.IsValid) CustomResponse(ModelState);

            if(product.ImageUpload != null)
            {
                var imgName = Guid.NewGuid() + "_" + product.Image;

                if (!UploadFile(product.ImageUpload, imgName)) return CustomResponse();

                product.Image = imgName;
            }
            productEdit.Name = product.Name;
            productEdit.Description = product.Description;
            productEdit.Value = product.Value;
            productEdit.IsActive = product.IsActive;

            await _service.Edit(_mapper.Map<ProductEntity>(product));

            return CustomResponse(product);

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = _mapper.Map<Product>(await _repository.GetById(id));

            if (product == null) return NotFound();

            await _service.Delete(id);

            return CustomResponse();

        }

        private bool UploadFile(string file, string imgName)
        {
            var imgDataByteArray = Convert.FromBase64String(file);

            if(string.IsNullOrEmpty(file))
            {
                NotifyErro("Forneça uma imagem para este produto!");
                return false;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgName);

            if (System.IO.File.Exists(filePath))
            {
                NotifyErro("Já existe um arquivo com esse Nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imgDataByteArray);

            return true;
        }

    }
}
