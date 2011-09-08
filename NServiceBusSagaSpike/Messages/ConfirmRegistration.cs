using System;
using NServiceBus;
using NServiceBus.Saga;

namespace Messages
{
    //public class ConfirmRegistration : ISagaMessage
    public class ConfirmRegistration : IMessage
    {
        public string Ticket { get; set; }
    }
}