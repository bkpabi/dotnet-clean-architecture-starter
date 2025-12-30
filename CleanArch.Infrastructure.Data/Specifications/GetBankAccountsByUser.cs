using CleanArch.Domain.Entities.BankAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Data.Specifications;

public class GetBankAccountsByUser : BaseSpecification<BankAccount>
{
    public GetBankAccountsByUser(string userId) : base(e => e.UserId == userId)
    {

    }
}
