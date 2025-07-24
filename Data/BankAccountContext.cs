using BankAccount.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Data
{
    public class BankAccountContext : DbContext
    {
        public BankAccountContext(DbContextOptions<BankAccountContext> options) : base(options)
        {
        }

        public DbSet<BankAccountModel> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccountModel>()
                .HasKey(b => b.Id); // Garante que o EF reconheça o Id como chave primária

            base.OnModelCreating(modelBuilder);
        }
    }
}