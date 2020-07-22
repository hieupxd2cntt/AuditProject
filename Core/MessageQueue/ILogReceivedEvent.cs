using System;

namespace MessageQueue
{
    public interface ILogReceivedEvent
    {
        Guid CorrelationId { get; }
        string ErrCode { get; }
        string ErrDesc { get; }
    }
}