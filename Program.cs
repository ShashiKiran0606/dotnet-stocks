using System.Text;
using dotnet_stocks_api.Controllers;
using dotnet_stocks_api.DBFirstApproach.Models;
using dotnet_stocks_api.Interfaces;
using dotnet_stocks_api.Repositories;
using dotnet_stocks_api.Services;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using Scalar.AspNetCore;
var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("NLog.config").GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };
});
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<StocksDbContext>();
builder.Services.AddSingleton<ProfileRepo>();
builder.Services.AddSingleton<IProfileService, ProfileService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Host.UseNLog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // //app.UseSwaggerUI();

    // app.UseSwagger(options =>
    // {
    //     options.RouteTemplate = "/openapi/{documentName}.json";
    // });
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
