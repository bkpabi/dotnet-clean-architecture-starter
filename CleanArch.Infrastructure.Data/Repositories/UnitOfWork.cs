using CleanArch.Domain.Behaviours;
using CleanArch.Domain.SeedWork;
using CleanArch.Infrastructure.Data.DBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CleanArchContext _context;
        private readonly IMediator _mediator;

        public UnitOfWork(CleanArchContext context,  IBankAccountRepository bankAccountRepository,  IMediator mediator, IBankTransactionRepository bankTransactionRepository)
        {
            _context = context;
            BankAccountRepository = bankAccountRepository;
            _mediator = mediator;
            BankTransactionRepository = bankTransactionRepository;
        }

        public IBankAccountRepository BankAccountRepository { get; }
        public IBankTransactionRepository BankTransactionRepository { get; set; }

        public Guid GetScopeIdentity()
        {
            FormattableString sql = $"SELECT CAST(SCOPE_IDENTITY() AS VARCHAR)";
            Guid guid = new Guid(_context.Database.SqlQuery<string>(sql).FirstOrDefault());
            return guid;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //await _mediator.DispatchDomainEventsAsync(_context);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
