using Microsoft.EntityFrameworkCore;
using Transferencias.App.Data;
using Transferencias.App.Helpers;
using DotNetEnv;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = $"Host={Env.GetString("POSTGRES_HOST")};" +
                       $"Port={Env.GetString("POSTGRES_PORT")};" +
                       $"Database={Env.GetString("POSTGRES_DB")};" +
                       $"Username={Env.GetString("POSTGRES_USER")};" +
                       $"Password={Env.GetString("POSTGRES_PASSWORD")}";
System.Console.WriteLine(connectionString);
//var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new BigIntegerJsonConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Logs
var logDirectory = "logs";
if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var application = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var pendingMigrations = await application.Database.GetPendingMigrationsAsync();
    if (pendingMigrations != null)
        await application.Database.MigrateAsync();
}

if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();