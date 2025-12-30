using CleanArch.Domain.Entities;
using CleanArch.Domain.Entities.BankAggregate;
using CleanArch.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Data.DBContext;

public class CleanArchContext : IdentityDbContext<ApplicationUser>
{
    public CleanArchContext()
    {

    }
    public CleanArchContext(DbContextOptions<CleanArchContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        var decimalProps = builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

        foreach (var property in decimalProps)
        {
            property.SetPrecision(18);
            property.SetScale(2);
        }

    }

    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<BankTransaction> BankTransactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
}
