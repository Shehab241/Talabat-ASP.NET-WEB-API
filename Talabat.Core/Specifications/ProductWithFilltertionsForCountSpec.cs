using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class ProductWithFilltertionsForCountSpec:BaseSpecifications<Product>
	{
        public ProductWithFilltertionsForCountSpec(ProductSpecPrama SpecPrama)
            : base(p =>
                (string.IsNullOrEmpty(SpecPrama.Search) || p.Name.ToLower().Contains(SpecPrama.Search)) &&
                (!SpecPrama.brandId.HasValue || p.ProductBrandId == SpecPrama.brandId.Value) &&
                (!SpecPrama.typeId.HasValue || p.ProductTypeId == SpecPrama.typeId.Value))
        {
            
        }
    }
}
