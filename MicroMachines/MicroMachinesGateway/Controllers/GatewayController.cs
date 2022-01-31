using MicroMachinesCommon.Dtos;
using MicroMachinesGateway.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MicroMachinesGateway.Controllers;
[ApiController]
[Route("api")]
public class GatewayController : ControllerBase
{
    private readonly ILogger<GatewayController> _logger;
    private readonly IProductService _productService;

    public GatewayController(
        ILogger<GatewayController> logger,
        IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    [Route("products")]
    [SwaggerOperation("Gets all products", "GET api/products")]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        return (products == null) ? NotFound() : Ok(products);
    }

    [HttpGet]
    [Route("category/{categoryId}/products")]
    [SwaggerOperation("Gets all products from chosen category", "GET api/category/{categoryId}/products")]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProductsFromCategory(int categoryId)
    {
        var products = await _productService.GetAllFromCategoryAsync(categoryId);
        return (products == null) ? NotFound() : Ok(products);
    }


}
