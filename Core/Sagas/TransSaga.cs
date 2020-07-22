using Automatonymous;
using MessageQueue;
using Sagas;

namespace Saga
{
    public class TransSaga : MassTransitStateMachine<TransSagaState>
    {
        public State Received { get; private set; }
        public State Registered { get; private set; }

        public Event<IRegisterLogCommand> RegisterLog { get; private set; }
        public Event<ILogRegisteredEvent> LogRegistered { get; private set; }

        public TransSaga()
        {
            InstanceState(s => s.CurrentState);

            //Event(() => RegisterLog,
            //    cc =>
            //        cc.CorrelateBy(state => state.PickupName, context =>
            //            context.Message.PickupName)
            //                .SelectId(context => Guid.NewGuid()));

            //Event(() => LogRegistered, x => x.CorrelateById(context =>
            //    context.Message.CorrelationId));

            //Initially(
            //    When(RegisterOrder)
            //        .Then(context => {
            //            context.Instance.ReceivedDateTime = DateTime.Now;
            //            context.Instance.PickupName = context.Data.PickupName;
            //            context.Instance.PickupAddress = context.Data.PickupAddress;
            //            context.Instance.PickupCity = context.Data.PickupCity;
            //            //etc
            //        })
            //        .ThenAsync(
            //            context => Console.Out.WriteLineAsync($"Order for customer" +
            //                $" {context.Data.PickupName} received"))
            //        .TransitionTo(Received)
            //        .Publish(context => new OrderReceivedEvent(context.Instance))
            //    );

            //During(Received,
            //    When(OrderRegistered)
            //        .Then(context => context.Instance.RegisteredDateTime =
            //            DateTime.Now)
            //        .ThenAsync(
            //            context => Console.Out.WriteLineAsync(
            //                $"Order for customer {context.Instance.PickupName} " +
            //                $"registered"))
            //        .Finalize()
            //    );

            SetCompletedWhenFinalized();
        }
    }
}