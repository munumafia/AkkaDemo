namespace InternetRelayChat.Core.Messages.Commands
{
    /// <summary>
    /// Command for parting a channel
    /// </summary>
    public class PartChannel
    {
        /// <summary>
        /// The name of the channel to part
        /// </summary>
        public string Channel { get; private set; }

        /// <summary>
        /// The nick of the user parting the channel
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// Constructs a new instace of the PartChannel command
        /// </summary>
        /// <param name="channel">The channel to part</param>
        /// <param name="nick">The nick of the user parting the channel</param>
        public PartChannel(string channel, string nick)
        {
            Channel = channel;
            Nick = nick;
        }
    }
}
