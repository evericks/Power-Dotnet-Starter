using Application.Mappings;
using Application.Settings;
using Domain.Context;
using Hangfire;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
const string allowSpecificOrigins = "_allowSpecificOrigins";
var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.AddSerilog();
builder.Services.AddJsonSettings(builder);
builder.Services.AddDbContext<StudentManagementDbContext>(options => options.UseSqlServer(sqlConnectionString));
builder.Services.AddHangfire(sqlConnectionString);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllersWithViews().AddNewtonsoftJsonOptions();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddCorsWithOptions(allowSpecificOrigins);
builder.Services.AddControllers().AddValidators();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDependencyInjection();
builder.Services.AddRedis();
// builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(allowSpecificOrigins);

app.UseSwagger();

app.UseSwaggerUI();

app.UseSerilogRequestLogging();

// app.UseHangfireDashboard();

// app.UseHangfire();

app.UseHttpsRedirection();

app.UseJwtAuthorization();

app.MapControllers();

app.Run();