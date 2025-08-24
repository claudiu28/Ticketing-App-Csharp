using Services.Service;
using NLog;
using Grpc.Core;
namespace Server
{
    class ServiceImpl(string host, int port, GlobalService globalService)
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly string host = host;
        private readonly int port = port;
        private Grpc.Core.Server? _server;
        private readonly GlobalService _globalService = globalService;

        public void Start()
        {
            Logger.Info("Starting server on {0}:{1}", host, port);
            _server = new Grpc.Core.Server
            {
                Services = { Ticketing.Proto.TicketingService.BindService(new TicketingServiceImpl(_globalService)) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };
            _server.Start();
            Logger.Info("Server started successfully");
        }

        public async Task ShutdownAsync()
        {
            if (_server != null)
            {
                Logger.Info("Shutting down server...");
                await _server.ShutdownAsync();
                Logger.Info("Server shut down successfully.");
            }
        }
    }
}
