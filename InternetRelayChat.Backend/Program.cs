using Topshelf;

namespace InternetRelayChat.Backend
{
    /// <summary>
    /// Entry point for the backend console application, uses Topshelf to run either
    /// as a Windows service or as a console app
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            HostFactory.Run(x =>
            {
                x.Service<BackendService>(s =>
                {
                    s.ConstructUsing(name => new BackendService());
                    s.WhenStarted(bs => bs.Start());
                    s.WhenStopped(bs => bs.Stop());
                });

                x.RunAsNetworkService();

                x.SetDescription("Chat Backend Service");
                x.SetDisplayName("ChatBackendService");
                x.SetServiceName("ChatBackendService");
            });
        }
    }
}
