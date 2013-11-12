using System;
using MyMessages;
using NServiceBus.Saga;

namespace MyServer
{
    public class MySaga : Saga<MySaga.MySagaData>, IAmStartedByMessages<FirstMessage>, IHandleTimeouts<TimeMessage>
    {

        public class MySagaData : ContainSagaData
        {
            public virtual string Name { get; set; }
        }

        public void Handle(FirstMessage message)
        {
            Data.Name = message.Name;

            Console.Out.WriteLine("Saga started");
            RequestTimeout<TimeMessage>(TimeSpan.FromSeconds(10));
        }

        public void Timeout(TimeMessage state)
        {
            MarkAsComplete();
            Console.Out.WriteLine("Saga completed");

        }
    }
}