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
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo, IMapper mapper
            ,IGenericRepository<ProductType> TypeRepo ,IGenericRepository<ProductBrand> BrandRepo)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _typeRepo = TypeRepo;
            _brandRepo = BrandRepo;
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
        [ProducesResponseType(typeof(ProductToReturnDto) ,StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) ,StatusCodes.Status404NotFound)]
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
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypeAsync()
        {
            var Types =await _typeRepo.GetAllAsync();
            return Ok(Types);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrand()
        {
            var Brands = await _brandRepo.GetAllAsync();
            return Ok(Brands);
        }

    }
}
