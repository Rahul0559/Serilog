using Microsoft.EntityFrameworkCore;
using ExpressionFilterApi.Data;
using EmployeeApi.Interfaces;
using ExpressionFilterApi.Repository.Interface;
using EmployeeApi.Repositories;
using Serilog;
using EmployeeApi.Repository.Interface;
using ExpressionFilterApi.Repository;
using ExpressionFilterApi.DTOs.Helper;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

//Setp Serilog fro every end pont
builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
));
// Add controllers
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeDepartmentGet, EmployeeDepartmentGet>();
builder.Services.AddScoped<IEmployeeAdavnceFilter, EmployeeAdavnceFilter>();
builder.Services.AddScoped<IEmployeeGet, EmployeeGet>();
builder.Services.AddScoped<ApplyFilters>();


var app = builder.Build();
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.MapControllers();

app.Run();

