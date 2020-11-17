using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dev.IO.App.ViewModels;
using DevIO.Bussiness.Interfaces;
using AutoMapper;
using AppMvcBasic.Models;
using DevIO.Bussiness.Services;
using DevIO.Bussiness.Notifications;
using Microsoft.AspNetCore.Authorization;
using Dev.IO.App.Extensions;

namespace Dev.IO.App.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _context;
        private readonly ISupplierRepository _contextSupplier;
        private readonly IProductService _productService;

        private readonly IMapper _mapper;
        public ProductController(IProductRepository context, ISupplierRepository contextSupplier, IMapper mapper, IProductService productService, INotify notify): base(notify)
        {
            _context = context;
            _contextSupplier = contextSupplier;
            _mapper = mapper;
            _productService = productService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _context.GetProductsAndSuppliers()));

        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product","Create")]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await PopulateSuppliers(new ProductViewModel());

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            string imgPrefix = Guid.NewGuid() + "_";

            productViewModel = await PopulateSuppliers(productViewModel);

            if (!ModelState.IsValid) return View(productViewModel);

           if(! await UploadImage(productViewModel.ImageUpload, imgPrefix)) return View(productViewModel);

            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;

            var product = _mapper.Map<ProductEntity>(productViewModel);
            await _productService.Create(product);

            if(!ValidOperation()) return View(productViewModel);

            return RedirectToAction(nameof(Index));
            
        }

        [ClaimsAuthorize("Product", "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {

            var productViewModel = await GetProduct(id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            
            return View(productViewModel);
        }


        [ClaimsAuthorize("Product", "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id) return NotFound();

            var productUpdate = await GetProduct(id);

            productViewModel.Supplier = productUpdate.Supplier;
            productViewModel.Image = productUpdate.Image;
            
            if (!ModelState.IsValid) return View(productViewModel);

            if(productViewModel.ImageUpload != null)
            {
                string imgPrefix = Guid.NewGuid() + "_";

                if (!await UploadImage(productViewModel.ImageUpload, imgPrefix)) return View(productViewModel);
                productUpdate.Image = imgPrefix + productViewModel.ImageUpload.FileName;

            }

            productUpdate.Name = productViewModel.Name;
            productUpdate.Description = productViewModel.Description;
            productUpdate.Value = productViewModel.Value;
            productUpdate.IsActive = productViewModel.IsActive;
            productUpdate.Supplier = null;
            await _productService.Edit(_mapper.Map<ProductEntity>(productUpdate));


            if (!ValidOperation()) return View(productViewModel);

            return RedirectToAction(nameof(Index));
           
        }


        [ClaimsAuthorize("Product", "Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = await GetProduct(id);
            if (productViewModel == null) return NotFound();
            
            return View(productViewModel);
        }


        [ClaimsAuthorize("Product", "Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var productViewModel = await GetProduct(id);
            if (productViewModel == null) NotFound();

            await _productService.Delete(id);
            
            if (!ValidOperation()) return View(productViewModel);

            TempData["Sucess"] = "Produto Excluído com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {

            var product = _mapper.Map<ProductViewModel>(await _context.GetProductAndSupplier(id));

            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _contextSupplier.ListAll());

            return product;
        }
        private async Task<ProductViewModel> PopulateSuppliers(ProductViewModel product)
        {

            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _contextSupplier.ListAll());

            return product;
        }


    }
}
