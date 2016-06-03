using System.Collections.Generic;
using Akka.Actor;

namespace InternetRelayChat.Backend.Entities
{
    /// <summary>
    /// User with additional properties used by the server to maintain global state
    /// </summary>
    public class ServerUser : User
    {
        /// <summary>
        /// Dictionary of the channels the user has joined
        /// </summary>
        public IDictionary<string, IActorRef> Channels { get; private set; }

        /// <summary>
        /// Creates a new instance of ServerUser
        /// </summary>
        /// <param name="nick">The nick of the user</param>
        /// <param name="client">The client actor used to connect to the server</param>
        public ServerUser(string nick, IActorRef client) : base(nick, client)
        {
            Channels = new Dictionary<string, IActorRef>();
        }
    }
}
