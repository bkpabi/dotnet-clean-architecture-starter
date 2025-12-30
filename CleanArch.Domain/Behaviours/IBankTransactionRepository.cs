using CleanArch.Domain.Entities.BankAggregate;
using CleanArch.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Behaviours;

public interface IBankTransactionRepository : IGenericRepository<BankTransaction>
{
}
