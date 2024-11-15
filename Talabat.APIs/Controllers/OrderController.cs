using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
   
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IOrderService orderService ,IMapper mapper ,IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        [ProducesResponseType(typeof(Order) ,StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status400BadRequest)]
            
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress); 
            var Order = await _orderService.CreateOrderAsync(BuyerEmail,orderDto.BasketId,orderDto.DeliveryMethodId, MappedAddress);
            if (Order is null) return BadRequest(new ApiResponse(400, "THere is Problem with Ypur Order"));
            return Ok(Order);
        }
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>) ,StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForSpecificUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Orders =await _orderService.GetOrdersForSpecificUserAsync(Email);
            if (Orders is null) return NotFound(new ApiResponse(404,"No Orders For This User"));
            var MappedOrder = _mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderItemDto>>(Orders); 
            return Ok(MappedOrder) ;
            
        }
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
           var order = await _orderService.GetOrderByIdForSpecificUserAsync(email, id);
            if (order is null) return NotFound(new ApiResponse(404, "No Orders with this id For This User"));
            var MappedOrder =_mapper.Map<Order , OrderToReturnDto>(order); 
            return Ok(MappedOrder);

        }
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var DeliveryMethod= await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return Ok(DeliveryMethod);
        }

    }
}
