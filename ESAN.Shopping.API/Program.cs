using Microsoft.EntityFrameworkCore;
using UESAN.SHOPPING.CORE.core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var _config = builder.Configuration;
var cnx = _config.GetConnectionString("DevConnection");
builder.Services.AddDbContext<logisticaBDContext>(options => options.UseSqlServer(cnx));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
