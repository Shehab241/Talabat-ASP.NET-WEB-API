using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product>
	{
		public ProductWithBrandAndTypeSpecifications( ProductSpecPrama SpecPrama)
			:base(p=>
			(string.IsNullOrEmpty(SpecPrama.Search)||p.Name.ToLower().Contains(SpecPrama.Search))&&
			(!SpecPrama.brandId.HasValue||p.ProductBrandId== SpecPrama.brandId.Value)&&
				(!SpecPrama.typeId.HasValue || p.ProductTypeId == SpecPrama.typeId.Value)
				
			)
		{
			Includes.Add(P => P.ProductBrand);
			Includes.Add(P => P.ProductType);

			if (!string.IsNullOrEmpty(SpecPrama.Sort))
			{
				switch (SpecPrama.Sort)
				{
					case "priceAsc":
						AddOrderBy(P => P.Price);
						break;
					case "priceDsc":
						AddOrderByDescending(P => P.Price);
						break;
					default:
						AddOrderBy(p => p.Name); 
						break;
				}
			}

			ApplyPagintion(SpecPrama.PageSize *(SpecPrama.PageIndex-1),SpecPrama.PageSize);
		}

		
		public ProductWithBrandAndTypeSpecifications(int id):base(P=>P.Id==id)
		{
			Includes.Add(P => P.ProductBrand);
			Includes.Add(P => P.ProductType);
		}

       
    }
}
