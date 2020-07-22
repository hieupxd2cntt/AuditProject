using System;
using Automatonymous;

namespace Sagas
{
    public class TransSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public State CurrentState { get; set; }

        public DateTime ReceivedDateTime { get; set; }
        public DateTime RegisteredDateTime { get; set; }


    }
}