using AutoMapper;
using Productos.Common.Dto;
using Productos.Common.Entities;

namespace Productos.Common.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() { 
            CreateMap<Producto, ProductoDto>();
            CreateMap<ProductoDto, Producto>();
        }
    }
}
