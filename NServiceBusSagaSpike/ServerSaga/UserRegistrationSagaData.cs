using System;
using NServiceBus.Saga;

namespace ServerSaga
{
    public class UserRegistrationSagaData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }

        public virtual string Email { get; set; }
        public virtual string Ticket { get; set; }
    }
}