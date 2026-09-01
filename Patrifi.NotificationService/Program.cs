using Microsoft.EntityFrameworkCore;
using Patrify.Account.Message;
using Patrify.MessageBus.RabbitMQ.Consumer;
using Patrify.NotificationService.Entities.Context;
using Patrify.NotificationService.Repository;
using Patrify.NotificationService.Service;
using Resend;

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

builder.Services.AddTransient<INotificationService, NotificationService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();

builder.Services.AddHostedService<RabbitMQNotificationConsumer>();

builder.Services.AddHttpClient<ResendClient>();

builder.Services.AddScoped<IResend, ResendClient>();

builder.Services.Configure<ResendClientOptions>(options => 
    options.ApiToken = builder.Configuration["Resend:ApiKey"]!
);

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
