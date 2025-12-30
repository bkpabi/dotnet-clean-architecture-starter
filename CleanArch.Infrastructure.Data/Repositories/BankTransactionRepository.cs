using CleanArch.Domain.Behaviours;
using CleanArch.Domain.Entities.BankAggregate;
using CleanArch.Infrastructure.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Data.Repositories
{
    public class BankTransactionRepository : GenericRepository<BankTransaction>, IBankTransactionRepository
    {
        public BankTransactionRepository(CleanArchContext dbContext) : base(dbContext) { }
    }
}
