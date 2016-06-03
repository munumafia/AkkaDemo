using Akka.Actor;

namespace InternetRelayChat.Core.Messages.Events
{
    /// <summary>
    /// Event generated when a user parts a channel
    /// </summary>
    public class UserPartedChannel
    {
        /// <summary>
        ///  The channel the user parted
        /// </summary>
        public string Channel { get; private set; }

        /// <summary>
        /// The nick of the user that parted the channel
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// The actor that caused the event to be generated
        /// </summary>
        public IActorRef Sender { get; private set; }

        /// <summary>
        /// Constructs a new instance of the UserPartedChannel event
        /// </summary>
        /// <param name="channel">The channel the user parted</param>
        /// <param name="nick">The nick of the user that parted the channel</param>
        /// <param name="sender">The actor that caused the event to be generated</param>
        public UserPartedChannel(string channel, string nick, IActorRef sender)
        {
            Channel = channel;
            Nick = nick;
            Sender = sender;
        }
    }
}
