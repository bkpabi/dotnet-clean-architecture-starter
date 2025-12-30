using CleanArch.Domain.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.SeedWork;

public interface IUnitOfWork
{
    IBankAccountRepository BankAccountRepository { get; }

    Guid GetScopeIdentity();
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
}
