using Persistence.Utils;
using Persistence.Repositories.EF_Implementation;
using Microsoft.Extensions.Configuration;
using Services.Service;
using NLog;
using Services.Events;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;

namespace Server
{
    class ServerRun
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static async Task Main()
        {
            SQLitePCL.Batteries.Init();
            Logger.Info("Server application starting...");
            try
            {
                Logger.Info("Loading configuration...");

                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                    .Build();
                Logger.Info("Configuration loaded successfully");

                var connectionString = config.GetConnectionString("bd.url");
                if (connectionString == null)
                {
                    Logger.Error("Connection string is null in configuration");
                    throw new Exception("Connection string null");
                }

                HelperBd.SetConnectionString(connectionString);

                // init database -> EF Core
                var optionsBuilder = new DbContextOptionsBuilder<ContextDb>();
                optionsBuilder.UseSqlite(connectionString);

                var dbContext = new ContextDb(optionsBuilder.Options);
                // ------------------------------------------------------------------------------------
                // for db context with multiple clients use semaphore for all repositories 

                int port = config.GetValue<int>("AppSettings:Port");
                Logger.Debug(string.Format("Configured port: {0}", port));

                string host = config.GetValue<string>("AppSettings:Host") ?? throw new Exception("Host is empty");

                Logger.Debug(string.Format("Configured host: '{0}'", host));

                Console.WriteLine(string.Format("Server will run on {0}:{1}", host, port));

                IRepoUser userRepository = new UserRepositoryEF(dbContext);
                IRepoMatch matchRepository = new MatchRepositoryEF(dbContext);
                IRepoTicket ticketRepository = new TicketRepositoryEF(dbContext);


                matchRepository.Save(new Models.Models.Match{
                    TeamA = "Barcelona",
                    TeamB = "Real Madrid",
                    MatchType = Models.Models.Enums.MatchTypes.SEMIFINALS,
                    NumberOfSeatsTotal = 400,
                    PriceTicket = 300

                });

                UserService userService = new(userRepository);
                MatchService matchService = new(matchRepository);
                TicketService ticketService = new(ticketRepository);
                NotifyEvents notifyEvents = new();
                GlobalService globalService = new(userService, matchService, ticketService, notifyEvents);

                var server = new ServiceImpl(host, port, globalService);

                Thread thr = new(() =>
                {
                    try
                    {
                        Logger.Info("Starting server...");
                        server.Start();
                        Logger.Info("Server started successfully. Waiting for connections...");
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"Server threw exception: {e.Message}");
                    }
                });

                thr.Start();
                Console.ReadLine();
                Logger.Info("Server stopping...");
                await server.ShutdownAsync();
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Server initialization failed: {0}", e.Message));
                Logger.Error(string.Format("StackTrace: {0}", e.StackTrace));
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
                Console.ReadLine();
            }
            finally
            {
                Logger.Info("Server application terminated");
            }
        }
    }
}
