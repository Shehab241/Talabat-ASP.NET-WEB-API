using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Dtos;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIS.Controllers
{
	
	public class ProductsController : BaseAPIController
	{
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<ProductBrand> _brandrop;
		private readonly IGenericRepository<ProductType> _typedrop;

		public ProductsController(
			IGenericRepository<Product> productRepo,
			IMapper mapper,
			IGenericRepository<ProductBrand> brandrop,
			IGenericRepository<ProductType> typedrop
			)
        {
			_productRepo = productRepo;
			_mapper = mapper;
			_brandrop = brandrop;
			_typedrop = typedrop;
		}

		[HttpGet]

		public async Task<IActionResult> GetProducts( [FromQuery] ProductSpecPrama SpecPrama)
			
		{
			var spec = new ProductWithBrandAndTypeSpecifications(SpecPrama);
			var product=await _productRepo.GetAllWithSpecAsync(spec);
			var map = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(product);
			var countSpec = new ProductWithFilltertionsForCountSpec(SpecPrama);

            var count = await _productRepo.GetCountWithSpecAsync(countSpec);

			return Ok(new Pagination<ProductToReturnDto>(SpecPrama.PageIndex,SpecPrama.PageSize, count, map));
		}
		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<IActionResult> GeProduct(int id)
		{
			var spec = new ProductWithBrandAndTypeSpecifications(id);
			var product= await _productRepo.GetByIdWithSpecAsync(spec);
			if (product is null)
			{
				return NotFound(new ApiResponse(404));
			}
			var map=_mapper.Map<Product, ProductToReturnDto>(product);
			return Ok(map);
		}

		[HttpGet("brands")]

		public async Task<IActionResult> GetBrands()
		{
			var brands=await _brandrop.GetAllAsync();

			return Ok(brands);
		}
		[HttpGet("types")]
		public async Task<IActionResult> GetTypes()
		{
			var Types=await _typedrop.GetAllAsync();

			return Ok(Types);
		}
    }
}
