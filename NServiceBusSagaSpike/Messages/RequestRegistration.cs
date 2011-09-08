using NServiceBus;

namespace Messages
{
    public class RequestRegistration : IMessage
    {
        public string Email { get; set; }
    }
}