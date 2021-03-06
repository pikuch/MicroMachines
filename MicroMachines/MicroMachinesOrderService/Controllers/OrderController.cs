using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesCommon.Enums;
using MicroMachinesOrderService.Models;
using MicroMachinesOrderService.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MicroMachinesOrderService.Controllers;

[ApiController]
[Route("orders")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IStockService _stockService;
    private readonly IPaymentQueue _paymentQueue;

    public OrderController(
        ILogger<OrderController> logger,
        IMapper mapper,
        IOrderRepository orderRepository,
        IStockService stockService,
        IPaymentQueue paymentQueue)
    {
        _logger = logger;
        _mapper = mapper;
        _orderRepository = orderRepository;
        _stockService = stockService;
        _paymentQueue = paymentQueue;
    }

    [HttpGet]
    [SwaggerOperation("Gets all orders", "GET /orders")]
    public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAll()
    {
        var orders = await _orderRepository.GetAllAsync();
        if (orders == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(orders));
    }

    [HttpGet]
    [Route("user/{userId}")]
    [SwaggerOperation("Gets user's orders", "GET /orders/user/{userId}")]
    public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetForUser(int userId)
    {
        var orders = await _orderRepository.GetForUserAsync(userId);
        if (orders == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(orders));
    }

    [HttpGet("{orderId}", Name = "GetById")]
    [SwaggerOperation("Gets the order with given id", "GET /orders/{orderId}")]
    public async Task<ActionResult<OrderReadDto>> GetById(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<OrderReadDto>(order));
    }

    [HttpPost]
    [SwaggerOperation("Creates a new order", "POST /orders")]
    public async Task<ActionResult> Create(OrderCreateDto order)
    {
        var newOrder = _mapper.Map<Order>(order);
        bool sufficientStock = await _stockService.verifyStockAsync(order.Itinerary);
        if (sufficientStock)
        {
            newOrder.Status = OrderStatus.Pending;
        }
        else
        {
            newOrder.Status = OrderStatus.Denied;
        }
        var addedOrder = await _orderRepository.CreateAsync(newOrder);
        if (newOrder.Status == OrderStatus.Pending)
        {
            bool enqueued = await _paymentQueue.Enqueue(_mapper.Map<OrderReadDto>(addedOrder));
            if (!enqueued)
            {
                // deny the order when payment queue can't be reached
                addedOrder.Status = OrderStatus.Denied;
            }
        }
        return CreatedAtRoute(nameof(GetById), new { orderId = addedOrder.Id }, _mapper.Map<OrderReadDto>(addedOrder));
    }

    [HttpPut("{orderId}")]
    [SwaggerOperation("Updates the order with given id", "PUT /orders/{orderId}")]
    public async Task<ActionResult> Update(int orderId, OrderUpdateDto order)
    {
        var foundOrder = await _orderRepository.GetByIdAsync(orderId);
        if (foundOrder == null)
        {
            return NotFound();
        }
        _mapper.Map(order, foundOrder);

        bool result = await _orderRepository.UpdateAsync();
        return (result) ? Ok() : BadRequest();
    }

    [HttpDelete("{orderId}")]
    [SwaggerOperation("Deletes the order with given id", "DELETE /orders/{orderId}")]
    public async Task<ActionResult> Delete(int orderId)
    {
        var foundOrder = await _orderRepository.GetByIdAsync(orderId);
        if (foundOrder == null)
        {
            return NotFound();
        }
        bool result = await _orderRepository.DeleteAsync(orderId);
        return (result) ? Ok() : BadRequest();
    }

    [HttpPost("{orderId}")]
    [SwaggerOperation("Adds items to the chosen order", "POST /orders/{orderId}")]
    public async Task<ActionResult> AddItems(int orderId, IEnumerable<ItineraryItemCreateDto> items)
    {
        if (items.Count() == 0)
        {
            return BadRequest();
        }

        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }

        bool result = await _orderRepository.AddItemsAsync(orderId, _mapper.Map<IEnumerable<ItineraryItem>>(items));
        return (result) ? Ok() : BadRequest();
    }

    [HttpPut("{orderId}/confirm/{transactionId}")]
    [SwaggerOperation("Confirms the order with given id", "PUT /orders/{orderId}/confirm/{transactionId}")]
    public async Task<ActionResult> Confirm(int orderId, int transactionId)
    {
        var foundOrder = await _orderRepository.GetByIdAsync(orderId);
        if (foundOrder == null)
        {
            return NotFound();
        }
        if (foundOrder.Status != OrderStatus.Pending)
        {
            return BadRequest();
        }
        foundOrder.TransactionId = transactionId;
        foundOrder.PurchaseDate = DateTime.UtcNow;
        foundOrder.Status = OrderStatus.Confirmed;

        bool result = await _orderRepository.UpdateAsync();
        return (result) ? Ok() : BadRequest();
    }
}
