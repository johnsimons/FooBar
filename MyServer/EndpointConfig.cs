using NServiceBus;
using NServiceBus.Features;

namespace MyServer
{
    [EndpointSLA("00:00:30")]
    public class EndpointConfig : IConfigureThisEndpoint, IWantCustomInitialization, AsA_Server, UsingTransport<SqlServer>
    {
        public void Init()
        {
            Configure.Transactions.Advanced(settings => settings.DisableDistributedTransactions());

            Configure.With()
               .DefaultBuilder()
               .InMemoryFaultManagement()
               .InMemorySubscriptionStorage()
               .UseNHibernateSagaPersister()
               .UseInMemoryTimeoutPersister();

            Configure.Features.Disable<SecondLevelRetries>();
            Configure.Features.Disable<AutoSubscribe>();

            Configure.Features.Enable<Sagas>();
        }
    }
}