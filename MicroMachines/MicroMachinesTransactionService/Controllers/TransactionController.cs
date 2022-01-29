using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesCommon.Enums;
using MicroMachinesTransactionService.Models;
using MicroMachinesTransactionService.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MicroMachinesTransactionService.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionController(
        ILogger<TransactionController> logger,
        IMapper mapper,
        ITransactionRepository transactionRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _transactionRepository = transactionRepository;
    }

    [HttpGet]
    [SwaggerOperation("Gets all transactions", "GET /transactions")]
    public async Task<ActionResult<IEnumerable<TransactionReadDto>>> GetAll()
    {
        var transactions = await _transactionRepository.GetAllAsync();
        if (transactions == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<TransactionReadDto>>(transactions));
    }

    [HttpGet]
    [Route("user/{userId}")]
    [SwaggerOperation("Gets user's transactions", "GET /transactions/user/{userId}")]
    public async Task<ActionResult<IEnumerable<TransactionReadDto>>> GetforUser(int userId)
    {
        var transactions = await _transactionRepository.GetForUserAsync(userId);
        if (transactions == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<TransactionReadDto>>(transactions));
    }

    [HttpGet("{transactionId}", Name = "GetById")]
    [SwaggerOperation("Gets the transaction with given id", "GET /transactions/{transactionId}")]
    public async Task<ActionResult<TransactionReadDto>> GetById(int transactionId)
    {
        var transaction = await _transactionRepository.GetByIdAsync(transactionId);
        if (transaction == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<TransactionReadDto>(transaction));
    }

    [HttpPost]
    [SwaggerOperation("Creates a new transaction", "POST /transactions")]
    public async Task<ActionResult> Create(TransactionCreateDto transaction)
    {
        if (transaction.AccountFromId == transaction.AccountToId)
        {
            return BadRequest();
        }
        var unconfirmedTransaction = _mapper.Map<Transaction>(transaction);
        unconfirmedTransaction.Status = TransactionStatus.Unconfirmed;
        unconfirmedTransaction.TimeStamp = DateTime.Now;
        var newTransaction = await _transactionRepository.CreateAsync(unconfirmedTransaction);
        return CreatedAtRoute(nameof(GetById), new { transactionId = newTransaction.Id }, _mapper.Map<TransactionReadDto>(newTransaction));
    }

    [HttpPut("{transactionId}")]
    [SwaggerOperation("Updates the transaction with given id", "PUT /transactions/{transactionId}")]
    public async Task<ActionResult> Update(int transactionId, TransactionUpdateDto transaction)
    {
        var foundTransaction = await _transactionRepository.GetByIdAsync(transactionId);
        if (foundTransaction == null)
        {
            return NotFound();
        }
        _mapper.Map(transaction, foundTransaction);

        bool result = await _transactionRepository.UpdateAsync();
        return (result) ? Ok() : BadRequest();
    }

    [HttpPut("{transactionId}/confirm")]
    [SwaggerOperation("Updates the transaction with given id", "PUT /transactions/{transactionId}/confirm")]
    public async Task<ActionResult> Confirm(int transactionId)
    {
        var foundTransaction = await _transactionRepository.GetByIdAsync(transactionId);
        if (foundTransaction == null)
        {
            return NotFound();
        }
        if (foundTransaction.Status == TransactionStatus.Confirmed)
        {
            return BadRequest();
        }
        foundTransaction.Status = TransactionStatus.Confirmed;
        foundTransaction.TimeStamp = DateTime.Now;

        bool result = await _transactionRepository.UpdateAsync();
        return (result) ? Ok() : BadRequest();
    }

    [HttpDelete("{transactionId}")]
    [SwaggerOperation("Deletes the transaction with given id", "DELETE /transactions/{transactionId}")]
    public async Task<ActionResult> Delete(int transactionId)
    {
        var foundTransaction = await _transactionRepository.GetByIdAsync(transactionId);
        if (foundTransaction == null)
        {
            return NotFound();
        }
        bool result = await _transactionRepository.DeleteAsync(transactionId);
        return (result) ? Ok() : BadRequest();
    }
}
