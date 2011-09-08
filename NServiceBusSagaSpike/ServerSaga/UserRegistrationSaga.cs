using System;
using Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace ServerSaga
{
    public class UserRegistrationSaga : Saga<UserRegistrationSagaData>,
        IAmStartedByMessages<RequestRegistration>,
        IHandleMessages<ConfirmRegistration>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<RequestRegistration>(saga => saga.Email, message => message.Email);
            ConfigureMapping<ConfirmRegistration>(saga => saga.Ticket, message => message.Ticket);
        }

        public void Handle(RequestRegistration message)
        {
            // generate new ticket if it has not been generated
            if (Data.Ticket == null)
            {
                var random = new Random();
                var randomNumber = random.Next(0, 1000);

                Data.Ticket = randomNumber.ToString();
            }

            Data.Email = message.Email;
            
            Console.WriteLine("New registration request for email {0} - ticket is {1}", Data.Email, Data.Ticket);
        }

        public void Handle(ConfirmRegistration message)
        {
            Console.WriteLine("Confirming email {0}", Data.Email);

            // tell NServiceBus that this saga can be cleaned up afterwards
            MarkAsComplete();
        }
    }
}