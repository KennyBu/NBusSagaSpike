using NServiceBus;

namespace Messages
{
    public class CreateNewUserRequest : IMessage
    {
        public string Username { get; set; }
    }
}