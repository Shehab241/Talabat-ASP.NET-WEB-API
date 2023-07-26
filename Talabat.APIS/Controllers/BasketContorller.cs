using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIS.Controllers
{
    
    public class BasketContorller : BaseAPIController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketContorller(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet("{id}")]
        public async Task <ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket=await _basketRepository.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var cretedOrUpdateBasket=await _basketRepository.UpdateBasketAsync(basket);
            if (cretedOrUpdateBasket is null) BadRequest(new ApiResponse(400));
            return cretedOrUpdateBasket;
        }

        [HttpDelete]
        public async Task DeleteBasket(string basketId)
        {
             await _basketRepository.DeleteBasketAsunc(basketId);
        } 
    }
}
