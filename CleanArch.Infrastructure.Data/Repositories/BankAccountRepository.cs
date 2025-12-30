using CleanArch.Domain.Behaviours;
using CleanArch.Domain.Entities.BankAggregate;
using CleanArch.Domain.SeedWork;
using CleanArch.Infrastructure.Data.DBContext;
using CleanArch.Infrastructure.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Data.Repositories;

public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
{
    public BankAccountRepository(CleanArchContext dbContext) : base(dbContext) { }

    public List<BankAccount> GetBankAccountsByUser(string userId, CancellationToken cancellationToken = default)
    {
        var bankAccounts = Find(new GetBankAccountsByUser(userId));
        return bankAccounts.ToList();
    }
}
