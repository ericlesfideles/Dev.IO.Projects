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
    public class SupplierController : BaseController
    {
        private readonly ISupplierRepository _context;
        private readonly IMapper _mapper;
        public SupplierController(ISupplierRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>( await _context.ListAll()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _context.GetSupllierAndAndress(id));

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper.Map < SupplierEntity > (supplierViewModel);

            await _context.Create(supplier);
            
            return RedirectToAction(nameof(Index));
            
            
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _context.GetSupllierAndAndressAndProduct(id));
            if (supplierViewModel == null)
            {
                return NotFound();
            }
            return View(supplierViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();
            
            if (!ModelState.IsValid) return View(supplierViewModel);


            var supplier = _mapper.Map<SupplierEntity>(supplierViewModel);
            await _context.Edit(supplier);

            return RedirectToAction(nameof(Index));
        
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel  = _mapper.Map<SupplierViewModel>(await _context.GetSupllierAndAndress(id));
            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _context.GetSupllierAndAndress(id));
            if (supplierViewModel == null) return NotFound();

            await _context.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
