using FelipeApiDapperTemplate.Application.Services;
using FelipeApiDapperTemplate.Domain.Services;
using System.Data;
using Microsoft.Data.SqlClient;
using FelipeApiDapperTemplate.Domain.Repositories;
using FelipeApiDapperTemplate.Infrastructure.Repositories;
using FelipeApiDapperTemplate.Application.Mapper;
using FelipeApiDapperTemplate.Domain.Services.Authentication;
using FelipeApiDapperTemplate.Application.Services.Authentication;
using FelipeApiDapperTemplate.Application.Validators;
using FelipeApiDapperTemplate.Domain.Models.DTOs;
using FluentValidation;
using FelipeApiDapperTemplate.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Conexão com banco de dados
builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(
    builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));

// Autenticação com Toeken JWT
builder.AddJwtTokenAuthentication(builder.Configuration["Jwt:PrivateKey"]);

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAllOrigins", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Validadores com Fluent Validation
builder.Services.AddScoped<IValidator<CreateCustomerDTO>, CreateCustomerValidator>();
builder.Services.AddScoped<IValidator<UpdateCustomerDTO>, UpdateCustomerValidator>();

// Swagger
builder.AddSwagger();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
