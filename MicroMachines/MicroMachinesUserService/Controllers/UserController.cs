using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesUserService.Models;
using MicroMachinesUserService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroMachinesUserService.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserController(
        ILogger<UserController> logger,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        if (users == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserReadDto>> GetById(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<UserReadDto>(user));
    }

    [HttpPost]
    public async Task<ActionResult> Create(UserCreateDto user)
    {
        var newUser = await _userRepository.CreateAsync(_mapper.Map<User>(user));
        return CreatedAtRoute(nameof(GetById), new { userId = newUser.Id }, _mapper.Map<UserReadDto>(newUser));
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult> Update(int userId, UserCreateDto user)
    {
        var foundUser = await _userRepository.GetByIdAsync(userId);
        if (foundUser == null)
        {
            return NotFound();
        }
        _mapper.Map(user, foundUser);

        bool result = await _userRepository.UpdateAsync(userId, foundUser);
        return (result) ? NoContent() : BadRequest();
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> Delete(int userId)
    {
        var foundUser = await _userRepository.GetByIdAsync(userId);
        if (foundUser == null)
        {
            return NotFound();
        }
        bool result = await _userRepository.DeleteAsync(userId);
        return (result) ? NoContent() : BadRequest();
    }
}
