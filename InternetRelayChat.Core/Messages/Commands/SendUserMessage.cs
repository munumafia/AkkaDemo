namespace InternetRelayChat.Core.Messages.Commands
{
    /// <summary>
    /// Command for sending a message to a channel
    /// </summary>
    public class SendUserMessage
    {
        /// <summary>
        /// The channel that should receive the message
        /// </summary>
        public string Channel { get; private set; }

        /// <summary>
        /// The message to send to the channel
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The nick of the user sending the message
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// Constructs a new SendUserMessage command
        /// </summary>
        /// <param name="channel">The channel to send the message to</param>
        /// <param name="message">The message to be sent</param>
        /// <param name="nick">The nick of the user sending the message</param>
        public SendUserMessage(string channel, string message, string nick)
        {
            Channel = channel;
            Message = message;
            Nick = nick;
        }
    }
}
