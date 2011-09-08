using System;
using Messages;
using NBTY.Core.Containers.Ninject.NServiceBus;
using NBTY.Core.Containers.Ninject.Registration;
using NServiceBus;

namespace ClientSaga
{
    public class Endpoint : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
            .Ninject<FileBasedKernelConfiguration>()
            .XmlSerializer();
        }
    }

    public class Runner : IWantToRunAtStartup
    {
        public void Run()
        {
            Console.WriteLine("Please Enter Email");
            var email = Console.ReadLine();
            Bus.Send<RequestRegistration>(m => m.Email = email);

            Console.WriteLine("Please Enter Reg Number");
            var ticket = Console.ReadLine();
            Bus.Send<ConfirmRegistration>(m => m.Ticket = ticket);
        }

        public void Stop()
        {
        }

        public IBus Bus { get; set; }
    }
}
