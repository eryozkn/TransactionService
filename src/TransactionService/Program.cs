using Microsoft.EntityFrameworkCore;

using TransactionService.DAL.Implementations;
using TransactionService.DAL.Interfaces;
using TransactionService.Facade.Implementations;
using TransactionService.Facade.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TxStore"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add dependendent services
builder.Services
    .AddScoped<ITransactionFacade, TransactionFacade>()
    .AddScoped<IUserBalanceFacade, UserBalanceFacade>()
    .AddScoped<ITransactionRepository, TransactionRepository>()
    .AddScoped<IUserBalanceRepository, UserBalanceRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
