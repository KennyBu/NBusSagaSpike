using System;
using NServiceBus;

namespace ServerSaga.Messages
{
    public class HandleCreateNewUserRequest : IHandleMessages<CreateNewUserRequest>
    {
        public IBus Bus { get; set; }
        
        public void Handle(CreateNewUserRequest message)
        {
            
        }
    }
}