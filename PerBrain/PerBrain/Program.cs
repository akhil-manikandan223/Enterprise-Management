using PerBrain.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<DbConnectionFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<DbConnectionFactory>();
    using var connection = factory.CreateConnection();

    try
    {
        connection.Open();
        Console.WriteLine("✅ DATABASE CONNECTED SUCCESSFULLY");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ DB CONNECTION FAILED");
        Console.WriteLine(ex.Message);
    }
}

app.Run();
