using CleanArch.Domain.Entities.BankAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Data.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.Ignore(c => c.DomainEvents);
        builder.Property(c => c.AccountName).IsRequired();
        builder.Property(c => c.AccountTypeId).IsRequired();
        builder.Property(c => c.InstitutionId).IsRequired();
        builder.Property(c => c.AccountNumber).IsRequired();
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.CreatedBy).IsRequired();
        builder.Property(c => c.CreatedOn).IsRequired().HasDefaultValueSql("GetDate()");

        var navigation = builder.Metadata.FindNavigation(nameof(BankAccount.Transactions));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
