using System.Configuration;
using APIpetshop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ConsStr"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Super importante!!!!!!!!!!!!!!****************************************
app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});


app.UseAuthorization();

app.MapControllers();

app.Run();

