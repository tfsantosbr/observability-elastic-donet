using Basket.Api.Models;
using Basket.Api.Models.Validators;
using Elastic.Apm.NetCoreAll;
using Eventflix.Api.Extensions.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add validations
builder.Services.AddFluentValidation();
builder.Services.AddTransient<IValidator<BasketModel>, BasketModelValidator>();
builder.Services.AddTransient<IValidator<BasketProductItemModel>, BasketProductItemModelValidator>();

// add redis
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = builder.Configuration.GetSection("Redis:InstanceName").Value;
});

// add logs
builder.Host.AddLogs(builder.Configuration);
builder.Host.UseAllElasticApm();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();