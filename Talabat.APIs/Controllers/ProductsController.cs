using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : ApiBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo, IMapper mapper)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec = new ProductWithTypeAndBrandSpecifications(); 
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProduct = _mapper.Map<IEnumerable<Product> ,IEnumerable<ProductToReturnDto>>(Products); 
            return Ok(MappedProduct);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Spec = new ProductWithTypeAndBrandSpecifications(id);
            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);
            if (Product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            var MappedProduct = _mapper.Map<Product , ProductToReturnDto>(Product);
            return Ok(MappedProduct);

        }

    }
}
