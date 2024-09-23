using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//VALIDADORES
builder.Services.AddScoped<IValidator<ProductInsertDto>, ProductInsertValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidator>();
builder.Services.AddScoped<IValidator<BrandInsertDto>, BrandInsertValidator>();
builder.Services.AddScoped<IValidator<BrandUpdateDto>, BrandUpdateValidator>();
builder.Services.AddScoped<IValidator<SaleInsertUpdateDto>, SaleInsertUpdateValidator>();

// HttoClients jsonoakceholder
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servicies
//Interfaces
builder.Services.AddScoped<ICommonService<ProductGetDto, ProductInsertDto, ProductUpdateDto>, ProductService>();
builder.Services.AddScoped<ICommonService<BrandGetDto, BrandInsertDto, BrandUpdateDto>, BrandService>();
builder.Services.AddScoped<ICommonService<SaleGetDto, SaleInsertUpdateDto, SaleInsertUpdateDto>, SaleService>();

// REPOSITORY 
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();
builder.Services.AddScoped<IRepository<Sale>, SaleRepository>();

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

app.Run();
