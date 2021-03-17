using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppMvcBasic.Models;
using AutoMapper;
using DevIO.API.ViewModel;
using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : MainController
    {
        public readonly ISupplierRepository _SupplierRepository;
        public readonly IMapper _mapper;
        public readonly ISupplierService _service;

        public SuppliersController(ISupplierRepository supplierRepository,
                                   IMapper mapper,
                                   ISupplierService service,
                                   INotify notify) : base(notify)
        {
            _SupplierRepository = supplierRepository;
            _mapper = mapper;
            _service = service;

        }


        [Route("ListAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> ListAll()
      {
            var supplier = _mapper.Map<IEnumerable<Supplier>>( await _SupplierRepository.ListAll());

            return Ok(supplier);

        }

        //[Route("GetById/{id:guid}")]
        [HttpGet("GetById/{id:guid}")]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetById(Guid id)
        {
            var supplier = _mapper.Map<Supplier>(await _SupplierRepository.GetById(id));
            if (supplier == null) NotFound();
            return Ok(supplier);

        }

        [HttpGet("GetSupllierAndAndressAndProduct/{id:guid}")]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSupllierAndAndressAndProduct(Guid id)
        {
            var supplier = _mapper.Map<Supplier>(await _SupplierRepository.GetSupllierAndAndressAndProduct(id));

            if (supplier == null) return NotFound();

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> Create(Supplier supplier)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _service.Create(_mapper.Map<SupplierEntity>(supplier));    
           
            return CustomResponse(supplier);
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Supplier>> Update(Guid id, Supplier supplier)
        {
            if (id != supplier.Id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState); 

            await _service.Edit(_mapper.Map<SupplierEntity>(supplier));

            return CustomResponse(supplier);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Supplier>> Delete(Guid id)
        {
            var supplier = _mapper.Map<Supplier>(await _SupplierRepository.GetSupllierAndAndress(id));
            if (supplier == null) return NotFound();

            await _service.Delete(id);

            return CustomResponse();
        }

    }


}
