using AutoMapper;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Patrify.Account.Entities.Context;
using Patrify.Account.IRepository;
using Patrify.Account.Message;
using Patrify.Account.Repository;
using Patrify.Account.Services;
using Patrify.MessageBus.RabbitMQ.Consumer;
using Patrify.MessageBus.RabbitMQ.Publish;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<SQLServerContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer")
    )
);

builder.Services.AddScoped<IAccountService, AccountService>();
 
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddSingleton<IRabbitMQPublish, RabbitMQPublish>(); 

builder.Services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();

builder.Services.AddHostedService<RabbitMQMessageConsumer>();

builder.Services.AddAutoMapper(a => a.AddMaps(typeof(Program).Assembly));

builder.Services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<SQLServerContext>());

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
