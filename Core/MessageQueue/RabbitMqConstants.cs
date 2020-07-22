namespace MessageQueue
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri = "rabbitmq://20.36.36.172/";
        public const string UserName = "audit";
        public const string Password = "123456a@";
        public const string LoggerServiceQueue = "logger.service";
        public const string NotificationServiceQueue = "notification.service";
        public const string SagaQueue = "saga.service";
    }
}
