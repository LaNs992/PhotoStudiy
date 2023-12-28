using Microsoft.EntityFrameworkCore;
using PhotoStudiy.Services.Contracts.Interface;
using PhotoStudiy.API.Extensions;
using PhotoStudiy.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegistrationControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegistrationSRC();
builder.Services.RegistrationSwagger();
builder.Services.AddDbContextFactory<PhotoStudiyContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.CustomizeSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
