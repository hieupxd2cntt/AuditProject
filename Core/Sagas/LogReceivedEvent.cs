using System;
using MessageQueue;

namespace Sagas
{
    public class LogReceivedEvent : ILogReceivedEvent
    {
        private readonly TransSagaState transSagaState;

        public LogReceivedEvent(TransSagaState orderSagaState)
        {
            this.transSagaState = orderSagaState;
        }

        public Guid CorrelationId => transSagaState.CorrelationId;

        public string ErrCode => throw new NotImplementedException();

        public string ErrDesc => throw new NotImplementedException();
        //public string Errcode => transSagaState.er;
        //public string ErrDesc => transSagaState.PickupAddress;

    }
}