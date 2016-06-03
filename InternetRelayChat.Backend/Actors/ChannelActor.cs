using System;
using System.Collections.Generic;
using Akka.Actor;
using InternetRelayChat.Backend.Entities;
using InternetRelayChat.Core.Messages.Events;

namespace InternetRelayChat.Backend.Actors
{
    public class ChannelActor : TypedActor,
        IHandle<UserMessageSent>, IHandle<UserJoinedChannel>, IHandle<UserPartedChannel>, IHandle<UserQuit>
    {
        /// <summary>
        /// The name of the channel
        /// </summary>
        private readonly string _ChannelName;

        /// <summary>
        /// Users that have joined the channel
        /// </summary>
        private readonly IDictionary<string, User> _Users;

        /// <summary>
        /// Creates a new instance of ChannelActor
        /// </summary>
        /// <param name="channelName">The name of the channel</param>
        public ChannelActor(string channelName)
        {
            _ChannelName = channelName;
            _Users = new Dictionary<string, User>();
        }

        /// <summary>
        /// Handles the UserMesssageSent event that is raised when a user sends a message
        /// to the channel
        /// </summary>
        /// <param name="evt">The event to handle</param>
        public void Handle(UserMessageSent evt)
        {
            if (evt.Channel != _ChannelName)
            {
                // Need to log a warning here, we received an event
                // intended for a different channel
                return;
            }

            foreach (var user in _Users.Values)
            {
                if (user.Nick != evt.Nick)
                {
                    user.Client.Tell(evt);
                }
            }
        }

        /// <summary>
        /// Handles the UserJoinedChannel event that is raised when a user sends a message
        /// to the channel
        /// </summary>
        /// <param name="evt">The event to handle</param>
        public void Handle(UserJoinedChannel evt)
        {
            if (_Users.ContainsKey(evt.Nick))
            {
                // Need to log a warning here, we shouldn't ever receive
                // this event for a user who has already joined the channel
                return;
            }

            foreach (var user in _Users.Values)
            {
                user.Client.Tell(evt);
            }

            var newUser = new User(evt.Nick, evt.Sender);
            _Users[newUser.Nick] = newUser;
        }

        /// <summary>
        /// Handles the UserPartedChannel event that is raised when a user sends a message
        /// to the channel
        /// </summary>
        /// <param name="evt">The event to handle</param>
        public void Handle(UserPartedChannel evt)
        {
            if (!_Users.ContainsKey(evt.Nick))
            {
                // Need to log a warning here, we shouldn't ever receive
                // this event for a user who isn't in the channel
                return;
            }

            _Users.Remove(evt.Nick);

            foreach (var user in _Users.Values)
            {
                user.Client.Tell(evt);
            }
        }

        public void Handle(UserQuit message)
        {
            if (!_Users.ContainsKey(message.Nick))
            {
                // Need to log a warning here, we shouldn't ever receive
                // this event for a user who isn't in the channel
                return;
            }

            _Users.Remove(message.Nick);

            foreach (var user in _Users.Values)
            {
                user.Client.Tell(message);
            }
        }
    }
}
