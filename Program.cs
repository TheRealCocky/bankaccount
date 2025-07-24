using BankAccount.Data;
using BankAccount.Routes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ler a connection string de ambiente (Render) ou appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Se estiver em produção no Render, converte o DATABASE_URL para formato PostgreSQL
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(databaseUrl))
{
    var databaseUri = new Uri(databaseUrl);
    var userInfo = databaseUri.UserInfo.Split(':');
    
    connectionString =
        $"Host={databaseUri.Host};Port={databaseUri.Port};Database={databaseUri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
}

// Registra o DbContext
builder.Services.AddDbContext<BankAccountContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Sempre habilita o Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.BankAccountRoutes();

app.Run();

