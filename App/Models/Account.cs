using System.Numerics;

namespace Transferencias.App.Models;

public class Account
{
    public int Id { get; set; }

    public String Name { get; set; }
    public String Doc { get; set; }
    public BigInteger Balance { get; set; }
}