using Akka.Actor;

namespace InternetRelayChat.Core.Messages.Events
{
    /// <summary>
    /// Event generated when a user joins a channel
    /// </summary>
    public class UserJoinedChannel
    {
        /// <summary>
        ///  The channel the user joined
        /// </summary>
        public string Channel { get; private set; }

        /// <summary>
        /// The nick of the user that joined the channel
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// The actor that caused the event to be triggered
        /// </summary>
        public IActorRef Sender { get; private set; }

        /// <summary>
        /// Constructs a new instance of the UserJoinedChannel event
        /// </summary>
        /// <param name="channel">The channel the user joined</param>
        /// <param name="nick">The nick of the user that joined the channel</param>
        /// <param name="sender">The actor that caused the event to be triggered</param>
        public UserJoinedChannel(string channel, string nick, IActorRef sender)
        {
            Channel = channel;
            Nick = nick;
            Sender = sender;
        }
    }
}
