using AppMvcBasic.Models;
using AutoMapper;
using Dev.IO.App.ViewModels;

namespace Dev.IO.App.AutoMapper
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<SupplierEntity, SupplierViewModel>().ReverseMap();
            CreateMap<ProductEntity, ProductViewModel>().ReverseMap();
            CreateMap<AndressEntity, AndressViewModel>().ReverseMap();
        }
    }
}
