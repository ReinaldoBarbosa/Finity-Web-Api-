using Finity.Data;
using Finity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔗 Banco
builder.Services.AddDbContext<FinityDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// 📦 Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AssinaturaService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<PagamentoService>();
builder.Services.AddScoped<DepartamentoService>();

// 🔐 JWT
var key = Encoding.UTF8.GetBytes(
    builder.Configuration["Jwt:Key"]!
);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(key)
            };
    });

// 🌍 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// 🔐 Authorization
builder.Services.AddAuthorization();

// 🎮 Controllers
builder.Services.AddControllers();

// 🔥 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🧪 Swagger
app.UseSwagger();
app.UseSwaggerUI();

// 🌍 CORS
app.UseCors("AllowFrontend");

// 🔐 Auth
app.UseAuthentication();
app.UseAuthorization();

// 🎮 Controllers
app.MapControllers();

// 🚀 Render
app.Urls.Add("http://0.0.0.0:10000");

app.Run();