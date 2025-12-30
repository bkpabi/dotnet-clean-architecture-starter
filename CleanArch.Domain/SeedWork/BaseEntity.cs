using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.SeedWork;

public class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedOn { get; protected set; }
    public DateTime? UpdatedOn { get; protected set; }
    public string CreatedBy { get; protected set; } = string.Empty;
    public string? UpdatedBy { get; protected set; }

    private List<INotification> _domainEvents;

    // Ignore the domainEvent in entity configuration 
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(INotification domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}
