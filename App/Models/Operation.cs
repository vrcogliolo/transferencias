using System.Numerics;

namespace Transferencias.App.Models;

public class Operation
{
    public int Id { get; set; }
    public int OriginAccountId { get; set; }
    public int TargetAccountId { get; set; }
    public Account? OriginAccount { get; set; }
    public Account? TargetAccount { get; set; }
    public BigInteger Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}