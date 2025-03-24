namespace Transferencias.App.DTO;
public class OperationDto
{
    public int Id { get; set; }
    public int OriginAccountId { get; set; }
    public int TargetAccountId { get; set; }
    public AccountDto OriginAccount { get; set; }
    public AccountDto TargetAccount { get; set; }
    public System.Numerics.BigInteger Amount { get; set; }
    public DateTime Date { get; set; }
}

public class AccountDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Doc { get; set; }
}