namespace InternetRelayChat.Core.Messages.Commands
{
    /// <summary>
    /// Command for quitting/disconnecting from the server
    /// </summary>
    public class QuitServer
    {
        /// <summary>
        /// The nick of the user that quit the server
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// Constructs a new instance of the QuitServer command
        /// </summary>
        /// <param name="nick">The nick of the user quitting the server</param>
        public QuitServer(string nick)
        {
            Nick = nick;
        }
    }
}
