using Fit_API.Context;
using Fit_API.src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitAPI.Controllers;


[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private readonly ApiDbContext _context;

    public UserController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
    {
        return _context.Users.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserId(int id, UserModel user)
    {
        if (id != user.IdUser || user == null)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            } else
            {
                throw;
            }
        }

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> PostUser(UserModel user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.IdUser == id);
    }
}
