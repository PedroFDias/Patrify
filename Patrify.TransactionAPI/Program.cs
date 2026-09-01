using FluentValidation;
using Patrify.MessageBus.Middleware;
using Patrify.MessageBus.RabbitMQ.Consumer;
using Patrify.MessageBus.RabbitMQ.Publish;
using Patrify.TransactionAPI.Message;
using Patrify.TransactionAPI.Service.Clients;

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

builder.Services.AddHttpClient<IAccountClient, AccountClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7243");
});

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddSingleton<IRabbitMQPublish, RabbitMQPublish>();
builder.Services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();

builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<SQLServerContext>());

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddHostedService<TransactionChangeStatusConsumer>();

builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(a => a.AddMaps(typeof(Program)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SQLServerContext>();
    var pendingMigrations = context.Database.GetPendingMigrations();

    if (pendingMigrations.Any())
    {
        Console.WriteLine("Aplicando migrações pendentes...");
        context.Database.Migrate();
        Console.WriteLine("Migrações aplicadas com sucesso.");
    }
    else
    {
        Console.WriteLine("Nenhuma migração pendente encontrada.");
    }
}

app.Run();
