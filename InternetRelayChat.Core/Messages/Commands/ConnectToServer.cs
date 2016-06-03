namespace InternetRelayChat.Core.Messages.Commands
{
    /// <summary>
    /// Command for connecting to the server
    /// </summary>
    public class ConnectToServer
    {
        /// <summary>
        /// The nick of the user connecting
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// Creates a new ConnectToServer
        /// </summary>
        /// <param name="nick">The nick of the user connecting</param>
        public ConnectToServer(string nick)
        {
            Nick = nick;
        }
    }
}
