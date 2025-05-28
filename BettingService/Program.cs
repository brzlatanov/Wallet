using Data;
using Wallet.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IDatabaseContext>(provider => new MockDatabaseContext("./mockdb.txt"));
builder.Services.AddSingleton<IBettingService, BettingService.Services.BettingService>(); 

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
