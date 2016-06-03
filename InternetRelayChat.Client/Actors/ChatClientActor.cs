using System;
using Akka.Actor;
using InternetRelayChat.Core.Messages.Commands;
using InternetRelayChat.Core.Messages.Events;

namespace InternetRelayChat.Client.Actors
{
    public class ChatClientActor : TypedActor,
        IHandle<UserJoinedChannel>, IHandle<UserPartedChannel>, IHandle<UserMessageSent>, IHandle<JoinChannel>, IHandle<PartChannel>,
        IHandle<SendUserMessage>, IHandle<ConnectToServer>, IHandle<UserQuit>, IHandle<QuitServer>
    {
        private ActorSelection _ChatBackend;
        private readonly string _Hostname;
        
        public ChatClientActor(string hostname)
        {
            _Hostname = hostname;
        }

        public void Handle(UserJoinedChannel message)
        {
            Console.WriteLine($"{message.Nick} has joined {message.Channel}");
        }

        public void Handle(UserMessageSent message)
        {
            Console.WriteLine($"<{message.Nick}|#{message.Channel}> {message.Message}");
        }

        public void Handle(UserPartedChannel message)
        {
            Console.WriteLine($"{message.Nick} has left {message.Channel}");
        }

        public void Handle(JoinChannel message)
        {
            _ChatBackend.Tell(message);
        }

        public void Handle(PartChannel message)
        {
            _ChatBackend.Tell(message);
        }

        public void Handle(SendUserMessage message)
        {
            _ChatBackend.Tell(message);
        }

        public void Handle(ConnectToServer message)
        {
            _ChatBackend.Tell(message);
        }

        protected override void PreStart()
        {
            base.PreStart();

            _ChatBackend = Context.ActorSelection($"akka.tcp://ChatBackend@{_Hostname}/user/ChatServer");
        }

        public void Handle(UserQuit message)
        {
            Console.WriteLine($"{message.Nick} has quit");
        }

        public void Handle(QuitServer message)
        {
            _ChatBackend.Tell(message);
        }
    }
}
