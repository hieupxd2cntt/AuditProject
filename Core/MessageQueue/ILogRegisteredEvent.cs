using System;

namespace MessageQueue
{
    public interface ILogRegisteredEvent
    {
        Guid CorrelationId { get; }
    }
}
