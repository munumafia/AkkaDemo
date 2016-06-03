using Akka.Actor;
using Akka.Configuration;
using InternetRelayChat.Backend.Actors;

namespace InternetRelayChat.Backend
{
    public class BackendService
    {
        /// <summary>
        /// The actor system that contains all the actors for our chat server
        /// </summary>
        private ActorSystem _ActorSystem;

        public void Start()
        {
            _ActorSystem = ActorSystem.Create("ChatBackend", ConfigurationFactory.Load());
            var server = _ActorSystem.ActorOf(Props.Create<ServerActor>(), "ChatServer");
        }

        public void Stop()
        {
            _ActorSystem.Terminate().Wait();
            _ActorSystem.WhenTerminated.Wait();
        }
    }
}
