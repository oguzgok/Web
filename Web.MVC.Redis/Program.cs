using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Web.Library.Cache;
using Web.MVC.Redis.Models;
using Web.MVC.Redis.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository>(sp =>
{
    var appDbContext = sp.GetRequiredService<AppDbContext>();
    var productRepository = new ProductRepository(appDbContext);
    var redisService = sp.GetRequiredService<RedisService>();

    return new ProductRepositoryWithCacheDecorator(productRepository, redisService);
});

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseInMemoryDatabase("myDatabase");
});

builder.Services.AddScoped<RedisService>(sp =>
{
    return new RedisService(builder.Configuration["CacheOptions:Url"]);
});

builder.Services.AddScoped<IDatabase>(sp =>
{
    var redisService = sp.GetRequiredService<RedisService>();

    return redisService.GetDb(0);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

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
