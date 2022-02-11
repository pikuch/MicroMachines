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
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;
    private readonly ITransactionService _transactionService;
    private readonly IAccountService _accountService;

    public GatewayController(
        ILogger<GatewayController> logger,
        IProductService productService,
        IUserService userService,
        IOrderService orderService,
        ITransactionService transactionService,
        IAccountService accountService)
    {
        _logger = logger;
        _productService = productService;
        _userService = userService;
        _orderService = orderService;
        _transactionService = transactionService;
        _accountService = accountService;
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

    [HttpGet]
    [Route("user/{userId}/products")]
    [SwaggerOperation("Gets all products of chosen user", "GET api/user/{userId}/products")]
    public async Task<ActionResult<IEnumerable<ItineraryItemReadDto>>> GetProductsOfUser(int userId)
    {
        var products = await _userService.GetProductsOfUserAsync(userId);
        return (products == null) ? NotFound() : Ok(products);
    }

    [HttpGet]
    [Route("user/{userId}/orders")]
    [SwaggerOperation("Gets all orders of chosen user", "GET api/user/{userId}/orders")]
    public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrdersOfUser(int userId)
    {
        var orders = await _orderService.GetOrdersOfUserAsync(userId);
        return (orders == null) ? NotFound() : Ok(orders);
    }

    [HttpGet]
    [Route("user/{userId}/transactions")]
    [SwaggerOperation("Gets all transactions of chosen user", "GET api/user/{userId}/transactions")]
    public async Task<ActionResult<IEnumerable<TransactionReadDto>>> GetTransactionsOfUser(int userId)
    {
        var transactions = await _transactionService.GetTransactionsOfUserAsync(userId);
        return (transactions == null) ? NotFound() : Ok(transactions);
    }

    [HttpGet]
    [Route("user/{userId}/accounts")]
    [SwaggerOperation("Gets all accounts of chosen user", "GET api/user/{userId}/accounts")]
    public async Task<ActionResult<IEnumerable<AccountReadDto>>> GetAccountsOfUser(int userId)
    {
        var accounts = await _accountService.GetAccountsOfUserAsync(userId);
        return (accounts == null) ? NotFound() : Ok(accounts);
    }

}
