using BankAccount.Data;
using BankAccount.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Routes;

public static class BankAccountRoute
{
    public static void BankAccountRoutes(this WebApplication app)
    {
        var routes = app.MapGroup("bankaccount");

        routes.MapPost("", 
            async ( BankAccountDto dto, BankAccountContext db) =>
        {
            // Aqui tu já recebes os dados do body via Swagger/Postman
            var newAccount = new BankAccountModel
            {
                Name = dto.Name,
                Balance = dto.Balance,
                Type = dto.Type,
                Status = dto.Status,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
              db.BankAccounts.Add(newAccount);
              
              await db.SaveChangesAsync();

            // Exemplo de resposta (podes depois salvar no banco)
            return Results.Ok(newAccount);
        });

        routes.MapGet("{id}",
            async (BankAccountContext db, Guid id) =>
            {
                var account = await db.BankAccounts.FindAsync(id);
              return  account is not null ? Results.Ok(account) : Results.NotFound("Conta nao encontrada");
            });
        
        routes.MapGet("",
         async   (BankAccountContext db) =>
         {
             var accounts = await db.BankAccounts.ToListAsync();
             return Results.Ok(accounts);

         });

        routes.MapPut("{id}", async (Guid id, BankAccountDto dto, BankAccountContext db) =>
        {
            var account = await db.BankAccounts.FindAsync(id);
            if (account is null)
                return Results.NotFound("Conta não encontrada.");

            account.Name = dto.Name;
            account.Balance = dto.Balance;
            account.Type = dto.Type;
            account.Status = dto.Status;
            account.Description = dto.Description;
            account.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();

            return Results.Ok(account);
        });

        routes.MapDelete("{id}", async (BankAccountContext db, Guid id)=>
       {
           var account = await db.BankAccounts.FindAsync(id);
           if (account is null)
               return Results.NotFound("Conta não encontrada.");
           db.BankAccounts.Remove(account);
           await db.SaveChangesAsync();
          return Results.Ok(account);
       });
        
        
    }
}