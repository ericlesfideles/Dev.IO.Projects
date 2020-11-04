using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dev.IO.App.ViewModels;
using DevIO.Bussiness.Interfaces;
using AutoMapper;
using AppMvcBasic.Models;
using DevIO.Bussiness.Services;

namespace Dev.IO.App.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly ISupplierRepository _context;
        //private readonly IAndressRepository _AndressRepository;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        public SupplierController(
                                    ISupplierRepository context,
                                    IMapper mapper,
                                    //IAndressRepository andressRepository
                                    ISupplierService supplierService,
                                    INotify notify): base(notify)
        {
            _context = context;
            _mapper = mapper;
            _supplierService = supplierService;
        }

        [Route("SupplierList")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _context.ListAll()));
        }

        [Route("SupplierDetails/{id:guid}")]
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

            await _supplierService.Create(supplier);

            if(!ValidOperation()) return View(supplierViewModel);

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
            await _supplierService.Edit(supplier);

            if (!ValidOperation()) return View(supplierViewModel);

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

            await _supplierService.Delete(id);

            if (!ValidOperation()) return View(supplierViewModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAndress(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _context.GetSupllierAndAndress(id));
            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return PartialView(viewName: "_AndressDetails", supplierViewModel);
        }

        public async Task<IActionResult> UpdateAndress(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _context.GetSupllierAndAndress(id));
            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return PartialView(viewName:"_AndressUpdate", new SupplierViewModel { Andress = supplierViewModel.Andress });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAndress(SupplierViewModel supplier)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if (!ModelState.IsValid) return PartialView("_AndressUpdate", supplier);

            await _supplierService.UpdateAndress(_mapper.Map<AndressEntity>(supplier.Andress));

            var url = Url.Action("GetAndress", "Supplier", new { id = supplier.Andress.SupplierId });

            return Json(new { success = true, url });
        }

    }
}
