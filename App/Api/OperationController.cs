using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transferencias.App.Data;
using Transferencias.App.Models;
using Transferencias.App.DTO;
using Microsoft.Extensions.Logging;

[Route("api/operation")]
[ApiController]
public class OperationController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OperationController> _logger;

    public OperationController(ApplicationDbContext context, ILogger<OperationController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> MakeTransfer(int origin, int target, int amount)
    {
        _logger.LogInformation("MakeTransfer received at {time}", DateTime.UtcNow);
        var cuentaOrigen = await _context.Account.FindAsync(origin);
        var cuentaDestino = await _context.Account.FindAsync(target);

        if (amount < 0){
            _logger.LogInformation("Monto invalido [origin: {origin}, origin: {target}, origin: {amount}] at {time}", origin, target, amount, DateTime.UtcNow);
            return NotFound(new { mensaje = "Monto invalido" });
        }

        if (cuentaOrigen == null || cuentaDestino == null){
            _logger.LogInformation("Cuenta origen o destino no encontrada [origin: {origin}, origin: {target}, origin: {amount}] at {time}", origin, target, amount, DateTime.UtcNow);
            return NotFound(new { mensaje = "Cuenta origen o destino no encontrada" });
        }

        if (cuentaOrigen == cuentaDestino){
            _logger.LogInformation("Cuenta origen y destino no pueden coincidir [origin: {origin}, origin: {target}, origin: {amount}] at {time}", origin, target, amount, DateTime.UtcNow);
            return NotFound(new { mensaje = "Cuenta origen y destino no pueden coincidir" });
        }

        if (cuentaOrigen.Balance < amount){
            _logger.LogInformation("Saldo insuficiente [origin: {origin}, origin: {target}, origin: {amount}] at {time}", origin, target, amount, DateTime.UtcNow);
            return BadRequest(new { mensaje = "Saldo insuficiente" });
        }

        cuentaOrigen.Balance -= amount;
        cuentaDestino.Balance += amount;

        var operation = new Operation
        {
            OriginAccountId = cuentaOrigen.Id,
            TargetAccountId = cuentaDestino.Id,
            Amount = amount
        };

        _context.Operation.Add(operation);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Transferencia realizada exitosamente" });
    }

    [HttpGet("{accounId}")]
    public async Task<IActionResult> GetOperationByAccount(int accounId)
    {
        var account = await _context.Account.FindAsync(accounId);
        if (account == null)
            return NotFound(new { mensaje = "Cuenta no encontrada" });

        var operations = await _context.Operation
                                    .Where(o => o.OriginAccountId == accounId || o.TargetAccountId == accounId)
                                    .Include(o => o.OriginAccount)
                                    .Include(o => o.TargetAccount)
                                    .ToListAsync();

        if (operations == null || operations.Count == 0)
            return NotFound(new { mensaje = "No se encontraron operaciones para esta cuenta" });
        
        var operationDtos = operations.Select(o => new OperationDto
            {
                Id = o.Id,
                OriginAccountId = o.OriginAccountId,
                TargetAccountId = o.TargetAccountId,
                OriginAccount = new AccountDto
                {
                    Id = o.OriginAccount.Id,
                    Name = o.OriginAccount.Name,
                    Doc = o.OriginAccount.Doc
                },
                TargetAccount = new AccountDto
                {
                    Id = o.TargetAccount.Id,
                    Name = o.TargetAccount.Name,
                    Doc = o.TargetAccount.Doc
                },
                Amount = o.Amount,
                Date = o.Date
            }).ToList();

        return Ok(operationDtos);
    }
}