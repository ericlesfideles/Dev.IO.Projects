﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dev.IO.App.ViewModels;
using DevIO.Bussiness.Interfaces;
using AutoMapper;
using AppMvcBasic.Models;

namespace Dev.IO.App.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductRepository _context;
        private readonly ISupplierRepository _contextSupplier;

        private readonly IMapper _mapper;
        public ProductController(IProductRepository context, ISupplierRepository contextSupplier, IMapper mapper)
        {
            _context = context;
            _contextSupplier = contextSupplier;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _context.GetProductsAndSuppliers()));

        }

        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var productViewModel = await PopulateSuppliers(new ProductViewModel());

            return View(productViewModel);
        }

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
            await _context.Create(product);

           return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Edit(Guid id)
        {

            var productViewModel = await GetProduct(id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            
            return View(productViewModel);
        }

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
            await _context.Edit(_mapper.Map<ProductEntity>(productUpdate));

            return RedirectToAction(nameof(Index));
           
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = await GetProduct(id);
            if (productViewModel == null) return NotFound();
            
            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var productViewModel = await GetProduct(id);
            if (productViewModel == null) NotFound();

            await _context.Delete(id);
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