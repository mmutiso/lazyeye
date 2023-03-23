using LazyEye.Api;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var moderatorSettings = new ModerationSettings
{
    APIKEY = Environment.GetEnvironmentVariable("APIKEY") ?? string.Empty,
    MODERATIONENDPOINT = Environment.GetEnvironmentVariable("MODERATIONENDPOINT") ?? string.Empty,
};

builder.Services.AddSingleton(moderatorSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


    
app.UseAuthorization();

app.MapControllers();

app.Run();
