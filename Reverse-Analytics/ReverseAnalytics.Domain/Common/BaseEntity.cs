using System.ComponentModel.DataAnnotations.Schema;

namespace ReverseAnalytics.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }

    private readonly List<BaseEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        if (domainEvent == null)
            return;

        if (_domainEvents.Contains(domainEvent))
            return;

        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        if (domainEvent == null)
            return;

        if (!_domainEvents.Contains(domainEvent))
            return;

        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
