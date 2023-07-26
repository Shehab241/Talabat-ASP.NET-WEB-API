using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
	public class StoreContextSeed
	{
		public static async Task SeedAsync (StoreContext context)
		{
			if(!context.ProductBrands.Any())
			{ 
				var brandsData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/brands.json");
				var brands= JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
				if (brands is not null && brands.Count > 0)
					{

						foreach (var brand in brands)
							await context.Set<ProductBrand>().AddAsync(brand);

						await context.SaveChangesAsync();
					}

			}

			if (!context.ProductTypes.Any())
			{
				var brandsTypes = File.ReadAllText("../Talabat.Repository/Data/Data Seed/types.json");
				var Types = JsonSerializer.Deserialize<List<ProductType>>(brandsTypes);
				if (Types is not null && Types.Count > 0)
				{

					foreach (var types in Types)
						await context.Set<ProductType>().AddAsync(types);

					await context.SaveChangesAsync();
				}

			}
			
			if (!context.Products.Any())
			{
				var ProductsData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/products.json");
				var Product = JsonSerializer.Deserialize<List<Product>>(ProductsData);
				if (Product is not null && Product.Count > 0)
				{

					foreach (var types in Product)
						await context.Set<Product>().AddAsync(types);

					await context.SaveChangesAsync();
				}

			}
		}
	}
}
