using Microsoft.AspNetCore.Mvc;
using Users.Idp.Domain;
using Users.Idp.Infrastructure.Context;
using Users.Idp.Models;

namespace Users.Idp.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly UsersDbContext _context;

    public AccountController(ILogger<AccountController> logger, UsersDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateAccount request)
    {
        var user = new User(request.Name, request.Email, request.Password);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Created($"account/{user.Id}", user);
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(Guid userId)
    {
        return NoContent();
    }
}
