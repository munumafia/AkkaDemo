using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetRelayChat.Core.Messages.Commands
{
    /// <summary>
    /// Command for joining a channel
    /// </summary>
    public class JoinChannel
    {
        /// <summary>
        /// The name of the channel to join
        /// </summary>
        public string Channel { get; private set; }

        /// <summary>
        /// The nick of the user joining the channel
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// Creates a new instance of the JoinChannel command
        /// </summary>
        /// <param name="channel">The channel to join</param>
        /// <param name="nick">The nick of the user that is joining the channel</param>
        public JoinChannel(string channel, string nick)
        {
            Channel = channel;
            Nick = nick;
        }
    }
}