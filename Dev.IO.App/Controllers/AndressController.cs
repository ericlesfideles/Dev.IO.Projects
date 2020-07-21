using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dev.IO.App.Data;
using Dev.IO.App.ViewModels;
using DevIO.Bussiness.Interfaces;
using AutoMapper;
using AppMvcBasic.Models;

namespace Dev.IO.App.Controllers
{
    public class AndressController : BaseController
    {
        private readonly IAndressRepository _context;
        private readonly ISupplierRepository _contextSupplier;
        private readonly IMapper _mapper;

        public AndressController(IAndressRepository context
                                , ISupplierRepository contextSupplier
                                , IMapper mapper)
        {
            _context = context;
            _contextSupplier = contextSupplier;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<AndressViewModel>>( await _context.ListAll()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var andressViewModel = await GetAndress(id);
            if (andressViewModel == null) return NotFound();
            
            return View(andressViewModel);
        }

        public async Task<IActionResult> Create()
        {
           var andressViewModel =  await PopulateSuppliers(new AndressViewModel());

            return View(andressViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AndressViewModel andressViewModel)
        {
            if (!ModelState.IsValid) return View(andressViewModel);
            await _context.Create(_mapper.Map<AndressEntity>(andressViewModel));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {

            var andressViewModel = await _context.GetById(id);
            if (andressViewModel == null) return NotFound();
          
            return View(andressViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AndressViewModel andressViewModel)
        {
            if (id != andressViewModel.Id) return NotFound();
            
            if (!ModelState.IsValid) return View(andressViewModel);

            await _context.Edit(_mapper.Map<AndressEntity>(andressViewModel));
            
            return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var andressViewModel = await _context.GetById(id);
            if (andressViewModel == null) return NotFound();
            
            return View(andressViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var andressViewModel = await _context.GetById(id);
            if (andressViewModel == null) return NotFound();

            await _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<AndressViewModel> GetAndress(Guid id)
        {

            var andress = _mapper.Map<AndressViewModel>(await _context.GetById(id));

            andress.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _contextSupplier.ListAll());

            return andress;
        }
        private async Task<AndressViewModel> PopulateSuppliers(AndressViewModel andress)
        {

            andress.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _contextSupplier.ListAll());

            return andress;
        }

    }
}
