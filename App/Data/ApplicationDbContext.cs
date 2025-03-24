using Microsoft.EntityFrameworkCore;
using Transferencias.App.Models;

namespace Transferencias.App.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        // Representacion de las bases de datos
        public DbSet<Account> Account { get; set; }
        
        public DbSet<Operation> Operation { get; set; }
    }
}