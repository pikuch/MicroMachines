using AutoMapper;
using MicroMachinesAccountService.Models;
using MicroMachinesAccountService.Services;
using MicroMachinesCommon.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MicroMachinesAccountService.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;

    public AccountController(
        ILogger<AccountController> logger,
        IMapper mapper,
        IAccountRepository accountRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _accountRepository = accountRepository;
    }

    [HttpGet]
    [SwaggerOperation("Gets all accounts", "GET /accounts")]
    public async Task<ActionResult<IEnumerable<AccountReadDto>>> GetAll()
    {
        var accounts = await _accountRepository.GetAllAsync();
        if (accounts == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<AccountReadDto>>(accounts));
    }

    [HttpGet("user/{userId}")]
    [SwaggerOperation("Gets all accounts of given user", "GET /accounts/user/{userId}")]
    public async Task<ActionResult<IEnumerable<AccountReadDto>>> GetAllForUser(int userId)
    {
        var accounts = await _accountRepository.GetAllForUserAsync(userId);
        if (accounts == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<AccountReadDto>>(accounts));
    }

    [HttpGet("{accountId}", Name = "GetById")]
    [SwaggerOperation("Gets the account with given id", "GET /accounts/{accountId}")]
    public async Task<ActionResult<AccountReadDto>> GetById(int accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<AccountReadDto>(account));
    }

    [HttpPost]
    [SwaggerOperation("Adds a new account", "POST /accounts")]
    public async Task<ActionResult> Create(AccountCreateDto account)
    {
        var newAccount = await _accountRepository.CreateAsync(_mapper.Map<Account>(account));
        return CreatedAtRoute(nameof(GetById), new { accountId = newAccount.Id }, _mapper.Map<AccountReadDto>(newAccount));
    }

    [HttpPut("{accountId}")]
    [SwaggerOperation("Updates the account with given id", "PUT /accounts/{accountId}")]
    public async Task<ActionResult> Update(int accountId, AccountUpdateDto account)
    {
        var foundAccount = await _accountRepository.GetByIdAsync(accountId);
        if (foundAccount == null)
        {
            return NotFound();
        }
        _mapper.Map(account, foundAccount);

        bool result = await _accountRepository.UpdateAsync();
        return (result) ? Ok() : BadRequest();
    }

    [HttpDelete("{accountId}")]
    [SwaggerOperation("Deletes the account with given id", "DELETE /accounts/{accountId}")]
    public async Task<ActionResult> Delete(int accountId)
    {
        var foundAccount = await _accountRepository.GetByIdAsync(accountId);
        if (foundAccount == null)
        {
            return NotFound();
        }
        bool result = await _accountRepository.DeleteAsync(accountId);
        return (result) ? Ok() : BadRequest();
    }

    [HttpPut]
    [Route("{accountId}/charge/{amount}")]
    [SwaggerOperation("Removes given amount of money from the account with given id", "PUT /accounts/{accountId}/charge/{amount}")]
    public async Task<ActionResult> ChargeAccount(int accountId, decimal amount)
    {
        if (amount <= 0)
        {
            return BadRequest();
        }
        var account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            return NotFound();
        }
        if (account.IsClosed || account.Balance < amount)
        {
            return BadRequest();
        }

        account.Balance -= amount;
        bool result = await _accountRepository.UpdateAsync();
        return (result) ? Ok() : BadRequest();
    }
}
