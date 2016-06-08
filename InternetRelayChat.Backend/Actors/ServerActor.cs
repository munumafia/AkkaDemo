using System;
using System.Collections.Generic;
using Akka.Actor;
using InternetRelayChat.Backend.Entities;
using InternetRelayChat.Backend.Extensions;
using InternetRelayChat.Core.Messages.Commands;
using InternetRelayChat.Core.Messages.Events;

namespace InternetRelayChat.Backend.Actors
{
    /// <summary>
    /// Actor that represents the state of the IRC server and handles routing commands
    /// to the proper channels
    /// </summary>
    /// <remarks>
    /// There should only ever be one instance of this actor created in the actor system
    /// </remarks>
    public class ServerActor : TypedActor, 
        IHandle<ConnectToServer>, IHandle<QuitServer>, IHandle<JoinChannel>, IHandle<PartChannel>, IHandle<SendUserMessage>
    {
        /// <summary>
        /// Dictionary of channels that have been created on the server
        /// </summary>
        private readonly IDictionary<string, IActorRef> _Channels;

        /// <summary>
        /// Dictionary of users that have connected to the server
        /// </summary>
        private readonly IDictionary<string, ServerUser> _Users;

        /// <summary>
        /// Constructs a new instance of the ServerActor
        /// </summary>
        public ServerActor()
        {
            _Channels = new Dictionary<string, IActorRef>();
            _Users = new Dictionary<string, ServerUser>();
        }

        /// <summary>
        /// Creates a new ChannelActor 
        /// </summary>
        /// <param name="channelName">The name of the channel to create</param>
        /// <returns>A reference to a new ChannelActor with the specified name</returns>
        private static IActorRef CreateChannel(string channelName)
        {
            var actorName = $"channel-{channelName.Replace("#", string.Empty)}";
            var props = Props.Create(() => new ChannelActor(channelName));

            return Context.ActorOf(props, actorName);
        }

        /// <summary>
        /// Handles a command to connect to the server
        /// </summary>
        /// <param name="command">The ConnectToServer command that needs to be processed</param>
        public void Handle(ConnectToServer command)
        {
            if (!_Users.ContainsKey(command.Nick))
            {
                _Users[command.Nick] = new ServerUser(command.Nick, Sender);
            }
        }

        /// <summary>
        /// Handles a command to join a channel
        /// </summary>
        /// <param name="command">The JoinChannel command that needs to be processed</param>
        public void Handle(JoinChannel command)
        {
            var user = _Users.TryGetValue(command.Nick);
            if (user == null)
            {
                // Need to log warning here
                return;
            }

            var channel = _Channels.TryGetValue(command.Channel);
            if (channel == null)
            {
                channel = CreateChannel(command.Channel);
                _Channels[command.Channel] = channel;
            }

            if (user.Channels.ContainsKey(command.Channel))
            {
                // Already joined channel, need to log warning here
                return;
            }

            if (user.Channels.ContainsKey(command.Channel))
            {
                // User already joined channel, need to log warning here
                return;
            }

            user.Channels.Add(command.Channel, channel);

            var evt = new UserJoinedChannel(command.Channel, command.Nick, user.Client);
            channel.Tell(evt);
        }

        /// <summary>
        /// Handles a command to part a channel
        /// </summary>
        /// <param name="command">The PartChannel command that needs to be processed</param>
        public void Handle(PartChannel command)
        {
            var channel = _Channels.TryGetValue(command.Channel);
            if (channel == null)
            {
                // Channel doesn't exist
                return;
            }

            var user = _Users.TryGetValue(command.Nick);
            if (user == null)
            {
                // Need to log warning here
                return;
            }

            user.Channels.Remove(command.Channel);

            var evt = new UserPartedChannel(command.Channel, command.Nick, user.Client);
            channel.Tell(evt);
        }

        /// <summary>
        /// Handles a command to send a command to a specific channel
        /// </summary>
        /// <param name="command">The SendUserMessage command that needs to be processed</param>
        public void Handle(SendUserMessage command)
        {
            var channel = _Channels.TryGetValue(command.Channel);
            if (channel == null)
            {
                // Need to log warning here
                return;
            }

            var evt = new UserMessageSent(command.Channel, command.Message, command.Nick);
            channel.Tell(evt);
        }

        /// <summary>
        /// Handles a command to quit/disconnect from the server
        /// </summary>
        /// <param name="command">The QuitServer command that needs to be processed</param>
        public void Handle(QuitServer command)
        {
            var user = _Users.TryGetValue(command.Nick);
            if (user == null)
            {
                // Need to log warning here
                return;
            }

            var evt = new UserQuit(command.Nick);
            foreach (var channel in user.Channels.Values)
            {
                channel.Tell(evt);
            }
        }
    }
}
