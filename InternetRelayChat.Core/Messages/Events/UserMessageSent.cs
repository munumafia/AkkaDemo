namespace InternetRelayChat.Core.Messages.Events
{
    /// <summary>
    /// Event generated when a message is sent to a channel
    /// </summary>
    public class UserMessageSent
    {
        /// <summary>
        /// The channel the message was sent to
        /// </summary>
        public string Channel { get; private set; }

        /// <summary>
        /// The message that was sent
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The nick of the user that sent the message
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// Constructs a new instance of UserMessageSent
        /// </summary>
        /// <param name="channel">The channel the message was sent to</param>
        /// <param name="message">The message that was sent</param>
        /// <param name="nick">The nick of the user that sent the message</param>
        public UserMessageSent(string channel, string message, string nick)
        {
            Channel = channel;
            Message = message;
            Nick = nick;
        }
    }
}
