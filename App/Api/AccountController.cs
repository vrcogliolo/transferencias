using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transferencias.App.Data;
using Transferencias.App.Models;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        var account = await _context.Account.FindAsync(id);
        if (account == null)
            return NotFound(new { mensaje = "Cuenta no encontrada" });

        return Ok(account);
    }
}