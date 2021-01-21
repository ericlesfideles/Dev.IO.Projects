using AppMvcBasic.Models;
using AutoMapper;
using DevIO.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.API.AutoMapper
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AndressEntity, Andress>().ReverseMap();
            CreateMap<ProductEntity, Product>().ReverseMap();
            CreateMap<SupplierEntity, Supplier>().ReverseMap();
        }

    }
}
