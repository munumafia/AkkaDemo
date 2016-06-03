using System.Collections;
using System.Collections.Generic;
using Akka.Actor;

namespace InternetRelayChat.Backend.Entities
{
    /// <summary>
    /// Entity representing a user in the system
    /// </summary>
    public class User
    {
        /// <summary>
        /// Reference to the actor that the user used to connect to the server
        /// </summary>
        public IActorRef Client { get; private set; }

        /// <summary>
        /// The nick of the user
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// Constructs a new instance of the User entity
        /// </summary>
        /// <param name="nick">The nick of the user</param>
        /// <param name="client">Reference to the actor that the user used to connect to the server</param>
        public User(string nick, IActorRef client)
        {
            Client = client;
            Nick = nick;
        }
    }
}
