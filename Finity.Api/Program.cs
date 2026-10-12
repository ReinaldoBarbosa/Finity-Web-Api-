
using Finity.Api.Data;
using Finity.Api.Interfaces;
using Finity.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Serviços da aplicação
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Conexão com o SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos serviços de negócio
builder.Services.AddScoped<IEmpresaService, EmpresaService>();

// Construção da aplicação
var app = builder.Build();

// Configuração dos endpoints
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
