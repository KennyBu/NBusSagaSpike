using NServiceBus;

namespace ServerSaga.Messages
{
    public class CreateNewUserRequest : IMessage
    {
        public string Username { get; set; }
    }
}