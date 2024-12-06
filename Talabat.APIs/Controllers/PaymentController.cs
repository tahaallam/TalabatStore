using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
   
    public class PaymentController : ApiBaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }
        [HttpPost]
      public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var Basket =await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (Basket is null) return BadRequest(new ApiResponse(400, "There Are Broblem With Your Basket"));
            else 
                return Ok(Basket);
        }
    }
}
