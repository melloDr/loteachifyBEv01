using Microsoft.EntityFrameworkCore;
using TeachifyBE_Business.Services;
using TeachifyBE_Data.Entities;
using TeachifyBE_Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Database
builder.Services.AddDbContext<LoTeachify01DbContext>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("loTeachify")));

//Service and repository
builder.Services.AddScoped<IGeneralService, GeneralService>();

builder.Services.AddTransient<IGeneralRepo, GeneralRepo>();


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
