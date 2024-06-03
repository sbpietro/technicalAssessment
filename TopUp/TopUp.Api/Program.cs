using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TopUp.Api;
using TopUp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers(config =>
{
    config.Filters.Add<GlobalExceptionFilter>();
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TopUpContext>();
    var env = app.Services.GetService<IWebHostEnvironment>();

    if (!env!.IsEnvironment("Specs") && !env!.IsEnvironment("IntegrationTests"))
    {
        await context.Database.MigrateAsync(CancellationToken.None);
    }
}

app.Run();
