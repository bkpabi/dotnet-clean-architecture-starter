using CleanArch.Domain.Entities.BankAggregate;
using CleanArch.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Behaviours;

public interface IBankAccountRepository : IGenericRepository<BankAccount>
{
    List<BankAccount> GetBankAccountsByUser(string userId, CancellationToken cancellationToken);
}
