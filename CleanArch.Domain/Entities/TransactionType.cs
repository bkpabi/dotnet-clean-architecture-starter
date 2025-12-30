using CleanArch.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entities;

public class TransactionType : BaseEntity
{
    public TransactionType() { }
    public TransactionType(string name, string createdBy, DateTime createdOn, string updatedBy, DateTime updatedOn)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        UpdatedBy = updatedBy;
        UpdatedOn = updatedOn;
    }

    public string Name { get; private set; } = string.Empty;
}
