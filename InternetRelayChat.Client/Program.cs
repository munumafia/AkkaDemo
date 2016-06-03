using System;
using Akka.Actor;
using Akka.Configuration;
using InternetRelayChat.Client.Actors;
using InternetRelayChat.Core.Messages.Commands;

namespace InternetRelayChat.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ConfigurationFactory.Load();
            var actorSystem = ActorSystem.Create("client", config);
            var chatClient = actorSystem.ActorOf(Props.Create(() => new ChatClientActor("localhost:9000")), "chatclient");

            var nick = Guid.NewGuid().ToString("N").Substring(0, 5);
            chatClient.Tell(new ConnectToServer(nick));

            var currentChannel = string.Empty;

            while (true)
            {
                var input = Console.ReadLine();
                if (input == null)
                {
                    continue;
                }

                if (input.StartsWith("/join"))
                {
                    var channel = input.Substring(5).Trim();
                    currentChannel = channel;

                    chatClient.Tell(new JoinChannel(channel, nick));
                }
                else if (input.StartsWith("/part"))
                {
                    var channel = input.Substring(5).Trim();
                    chatClient.Tell(new PartChannel(channel, nick));
                }
                else if (input.StartsWith("/quit"))
                {
                    chatClient.Tell(new QuitServer(nick));
                    break;
                }
                else
                {
                    chatClient.Tell(new SendUserMessage(currentChannel, input, nick));
                }

                Console.Write($"<{nick}|{currentChannel}> ");
            }

            Console.WriteLine("Terminating...");

            actorSystem.Terminate().Wait();
            actorSystem.WhenTerminated.Wait();
        }
    }
}
