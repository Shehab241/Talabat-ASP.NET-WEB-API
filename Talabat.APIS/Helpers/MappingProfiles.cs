using AutoMapper;
using Talabat.APIS.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIS.Helpers
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.ProductBrand,O=>O.MapFrom(s=>s.ProductBrand.Name)).
                ForMember(d=>d.ProductType,O=>O.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
