using CleanArch.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entities;

public class AccountType : BaseEntity
{
    public AccountType() { }

    public AccountType(string name, bool isActive, string createdBy, DateTime createdOn, string updatedBy, DateTime updatedOn)
    {
        AccountTypeName = name ?? throw new ArgumentNullException(nameof(name));
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        UpdatedBy = updatedBy;
        UpdatedOn = updatedOn;
    }
    public string AccountTypeName { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}
