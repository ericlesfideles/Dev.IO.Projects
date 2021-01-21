using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevIO.API.ViewModel;
using DevIO.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : MainController
    {
        public readonly ISupplierRepository _SupplierRepository;
        public readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository,
                                   IMapper mapper )
        {
            _SupplierRepository = supplierRepository;
            _mapper = mapper;

        }

        public async Task<ActionResult<IEnumerable<Supplier>>> ListAll()
        {
            var supplier = _mapper.Map<IEnumerable<Supplier>>( await _SupplierRepository.ListAll());

            return Ok(supplier);

        }

    }


}
