using Application.Interface;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Configuração de serviços
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // Ajuste conforme necessário
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
});

// Adicione os serviços ao contêiner.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registre os repositórios e serviços
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITarefaService, TarefaService>();

var app = builder.Build();

// Configure o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();









//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = true;
//});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateActor = false,
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        RequireExpirationTime = true,
//        ValidateIssuerSigningKey = true,
//        ClockSkew = TimeSpan.Zero,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
//    };
//});

//// Authorization
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("WebAdminPolicy", policy => policy.RequireRole("WebAdmin"));
//    options.AddPolicy("WebInscricoesPolicy", policy => policy.RequireRole("WebInscricoes"));
//});


