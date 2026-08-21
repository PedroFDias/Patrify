using Microsoft.EntityFrameworkCore;
using Patrify.TransactionAPI.Entities.Context;
using Patrify.TransactionAPI.Mappings;
using Patrify.TransactionAPI.RabbitMQSender;
using Patrify.TransactionAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<SQLServerContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();

builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(TransactionProfile).Assembly);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
