using NLog;
using Microsoft.Extensions.Configuration;
using Client.Views;

namespace Client
{
    internal static class ClientRun
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [STAThread]
        static void Main()
        {
            Logger.Info("Client application starting...");
            try
            {

                Logger.Info("Loading configuration...");
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                    .Build();

                int port = config.GetValue<int>("AppSettings:Port");
                string host = config.GetValue<string>("AppSettings:Host") ?? throw new Exception("Host is empty");

                if (string.IsNullOrEmpty(host))
                {
                    throw new Exception("Host is empty");
                }

                Console.WriteLine(string.Format("Server will run on {0}:{1}", host, port));
                var channel = Grpc.Net.Client.GrpcChannel.ForAddress($"http://{host}:{port}");
                GrpcProxy services = new(channel);
                Logger.Info("Service proxy initialized");

                Application.ApplicationExit += (s,e) =>
                {
                    services.Shutdown();
                };

                AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    try
                    {
                        Logger.Error("Unhandled exception: {0}", e.ExceptionObject);
                        services.Shutdown();
                        Environment.Exit(1);
                    }
                    catch (Exception ex)
                    {
                        Logger.Fatal("Fatal error during shutdown: {0}", ex.Message);
                        Environment.Exit(2);
                    }
                };

                ApplicationConfiguration.Initialize();

                Application.Run(new LoginView(services));

                Logger.Info("Client application terminated normally");
            }
            catch (Exception e)
            {
                Logger.Error("Client initialization failed: {0}", e.Message);
                MessageBox.Show("Error starting application: {0}", e.Message);
            }
        }
    }
}
