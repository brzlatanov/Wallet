using Data;
using WalletService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IDatabaseContext>(provider => new MockDatabaseContext("./mockdb.txt"));
builder.Services.AddSingleton<IWalletService, WalletService.Services.WalletService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
