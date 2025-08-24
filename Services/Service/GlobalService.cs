using Models.Models;
using System.Collections.Concurrent;
using NLog;
using Services.Events;

namespace Services.Service
{
    public class GlobalService(UserService userService, MatchService matchService, TicketService ticketService, NotifyEvents events)
    {
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly NotifyEvents _notifyEvents = events;
        private readonly UserService _userService = userService;
        private readonly MatchService _matchService = matchService;
        private readonly TicketService _ticketService = ticketService;
        private readonly ConcurrentDictionary<string, bool> _loggedClients = new();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public NotifyEvents GetNotifyEvents()
        {
            return _notifyEvents;
        }

        public UserService GetUserService()
        {
            return _userService;
        }

        public MatchService GetMatchService()
        {
            return _matchService;
        }

        public TicketService GetTicketService()
        {
            return _ticketService;
        }

        public bool IsUserLogged(string username)
        {
            return _loggedClients.ContainsKey(username);
        }

        public async Task<User> LogIn(User user)
        {
            Logger.Info("LogIn called with username: {0}", user.Username);
            if (IsUserLogged(user.Username))
            {
                Logger.Info("User already logged in: {0}", user.Username);
                throw new Exception("User already logged in");
            }
            var foundUser = await _userService.FindByUsername(user.Username);
            if (foundUser == null)
            {
                Logger.Info("User not found: {0}", user.Username);
                throw new Exception("User not found");
            }
            if (BCrypt.Net.BCrypt.Verify(user.Password, foundUser.Password) == false)
            {
                Logger.Info("Password mismatch for user: {0}", user.Username);
                throw new Exception("Password mismatch");
            }
            _loggedClients[user.Username] = true;
            Logger.Info("User logged in successfully: {0}", user.Username);
            return foundUser;
        }


        public async Task LogOut(string username)
        {
            Logger.Info("LogOut called with username: {0}", username);
            if (IsUserLogged(username))
            {
                _loggedClients.TryRemove(username, out _);
                Logger.Info("User logged out successfully: {0}", username);
            }
            else
            {
                Logger.Info("User not logged in: {0}", username);
                throw new Exception("User not logged in");
            }
            await Task.CompletedTask;
        }

        public async Task<Ticket> SellTicket(Ticket ticket)
        {
            if (ticket == null || ticket.Match == null)
            {
                Logger.Info("Ticket or Match is null");
                throw new Exception("Ticket or Match is null");
            }
            await _semaphore.WaitAsync();
            try {
                Match match = await _matchService.GetMatchById(ticket.Match);
                if (match == null)
                {
                    Logger.Info("Match not found");
                    throw new Exception("Match not found");
                }

                if (match.NumberOfSeatsTotal <= 0)
                {
                    Logger.Info("No seats available for match: {0}", match.Id);
                    throw new Exception("No seats available");
                }

                var newSeats = match.NumberOfSeatsTotal - ticket.NumberOfSeats;
                var matchUpdated = await _matchService.UpdateNumberOfSeats(match.Id, newSeats);
                if (matchUpdated == null)
                {
                    Logger.Info("Failed to update match seats");
                    throw new Exception("Failed to update match seats");
                }

                var soldTicket = await _ticketService.SellTicket(ticket);
                if (soldTicket == null)
                {
                    Logger.Info("Failed to sell ticket");
                    throw new Exception("Failed to sell ticket");
                }

                var protoMatch = new Ticketing.Proto.Match
                {
                    Id = matchUpdated.Id,
                    TeamA = matchUpdated.TeamA,
                    TeamB = matchUpdated.TeamB,
                    PriceTicket = matchUpdated.PriceTicket,
                    NumberOfSeats = matchUpdated.NumberOfSeatsTotal,
                    MatchType = (Ticketing.Proto.Match.Types.Type)(int) matchUpdated.MatchType,

                };

                await _notifyEvents.NotifyAll(protoMatch);
                Logger.Info("Ticket sold successfully: {0}", soldTicket.Id);
                return soldTicket;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<List<Match>> AllMatches()
        {
            Logger.Info("AllMatches called");
            var matches = await _matchService.GetAll();
            Logger.Info("Found {0} matches", matches.Count);
            return matches;
        }

        public async Task<List<Ticket>> FindTickets(string firstname, string lastname, string address)
        {
            Logger.Info("FindTickets called");
            var tickets = await _ticketService.FindByNameAndAddress(firstname, lastname, address);
            Logger.Info("Found {0} tickets", tickets.Count);
            return tickets;
        }

    }
}