using Microsoft.EntityFrameworkCore;
using TeachifyBE_Business.Services;
using TeachifyBE_Data.Entities;
using TeachifyBE_Data.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static Microsoft.AspNetCore.Identity.IdentityBuilder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TeachifyBE_Data.Models.ConfigModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Database
builder.Services.AddDbContext<LoTeachify01DbContext>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("loTeachify")));


#region Jwt configuration
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

#endregion

#region Subscribe service and repository
builder.Services.AddScoped<IGeneralService, GeneralService>();

builder.Services.AddTransient<IGeneralRepo, GeneralRepo>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
