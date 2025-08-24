using System.Collections.Concurrent;
using Grpc.Core;
using NLog;
using Ticketing.Proto;


namespace Services.Events
{
    public class NotifyEvents
    {
        private readonly ConcurrentDictionary<string, IServerStreamWriter<Match>> observers = new();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Task Register(string username, IServerStreamWriter<Match> observersResponse)
        {
            if (observers.ContainsKey(username))
            {
                Logger.Info("User already registered: {0}", username);
                return Task.CompletedTask;
            }
            observers[username] = observersResponse;
            Logger.Info("User registered: {0}", username);
            return Task.CompletedTask;
        }

        public Task Unregister(string username)
        {
            if (observers.TryRemove(username, out _))
            {
                Logger.Info("User unregistered: {0}", username);
            }
            else
            {
                Logger.Info("User not found: {0}", username);
            }
            return Task.CompletedTask;
        }

        public async Task NotifyAll(Match match)
        {
            var keysToRemove = new List<string>();
            var snapshot = observers.ToList();

            var tasks = snapshot.Select(async observer =>
            {
                try
                {
                    Logger.Info("Sending notification to {0}", observer.Key);
                    await observer.Value.WriteAsync(match);
                }
                catch (Exception e)
                {
                    Logger.Info("Error sending notification to {0}: {1}", observer.Key, e.Message);
                    keysToRemove.Add(observer.Key);
                }
            });

            await Task.WhenAll(tasks);

            foreach (var key in keysToRemove)
            {
                _ = Unregister(key);
            }
        }
    }
}
