using CleanArch.Domain.Entities.BankAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Data.Configurations;

public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransaction>
{
    public void Configure(EntityTypeBuilder<BankTransaction> builder)
    {
        builder.Ignore(c => c.DomainEvents);
        builder.Property(c => c.BankAccountId).IsRequired();
        builder.Property(c => c.TransactionTypeId).IsRequired();
        builder.Property(c => c.Balance).IsRequired();
        builder.Property(c => c.CategoryId).IsRequired();
        builder.Property(c => c.TransactionAmount).IsRequired();
        builder.Property(c => c.TransactionDate).IsRequired();
        builder.Property(c => c.TransactionDescription).IsRequired();
        builder.Property(c => c.TransactionReference).IsRequired();
        builder.Property(c => c.CreatedBy).IsRequired();
        builder.Property(c => c.CreatedOn).IsRequired().HasDefaultValueSql("GetDate()");
        
    }
}
