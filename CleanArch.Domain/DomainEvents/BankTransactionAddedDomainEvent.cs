using CleanArch.Domain.Entities.BankAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.DomainEvents;

public record BankTransactionAddedDomainEvent(BankTransaction BankTransaction) : INotification;
