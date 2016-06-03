namespace InternetRelayChat.Core.Messages.Events
{
    /// <summary>
    /// Event generated when a user quits their connection to the server
    /// </summary>
    public class UserQuit
    {
        /// <summary>
        /// The nick of the user that quit
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// Constructs a new instance of the UserQuit event
        /// </summary>
        /// <param name="nick">The nick of the user that quit</param>
        public UserQuit(string nick)
        {
            Nick = nick;
        }
    }
}
