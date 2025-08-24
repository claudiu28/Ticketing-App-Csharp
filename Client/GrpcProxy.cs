using Grpc.Core;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;
using Models.Models;
using NLog;
using Client.Views;
namespace Client
{
    public class GrpcProxy(GrpcChannel channel)
    {
        private readonly GrpcChannel _channel = channel;
        private readonly Ticketing.Proto.TicketingService.TicketingServiceClient _client = new(channel);
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<Ticketing.Proto.LoginResponse> LogIn(User user)
        {
            var request = new Ticketing.Proto.LoginRequest
            {
                User = new Ticketing.Proto.User
                {
                    Username = user.Username,
                    Password = user.Password
                }
            };
            return await _client.LoginAsync(request);
        }

        public async Task<Ticketing.Proto.LogoutResponse> LogOut(User user)
        {
            var request = new Ticketing.Proto.LogoutRequest
            {
                User = new Ticketing.Proto.User
                {
                    Username = user.Username,
                    Password = user.Password
                }
            };
            return await _client.LogoutAsync(request);
        }

        public async Task<Ticketing.Proto.SellTicketResponse> SellTicket(Ticket ticket)
        {
            var request = new Ticketing.Proto.SellTicketRequest
            {
                Ticket = new Ticketing.Proto.Ticket
                {
                    Match = new Ticketing.Proto.Match
                    {
                        Id = ticket.Match.Id,
                        TeamA = ticket.Match.TeamA,
                        TeamB = ticket.Match.TeamB,
                        PriceTicket = ticket.Match.PriceTicket,
                        NumberOfSeats = ticket.Match.NumberOfSeatsTotal,
                        MatchType = (Ticketing.Proto.Match.Types.Type)ticket.Match.MatchType
                    },
                    FirstName = ticket.FirstName,
                    LastName = ticket.LastName,
                    Address = ticket.Address,
                    NumberOfSeats = ticket.NumberOfSeats,
                }
            };
            return await _client.SellTicketAsync(request);
        }


        public async Task<Ticketing.Proto.GetAllMatchesResponse> GetAllMatches()
        {
            var request = new Empty();
            return await _client.GetAllMatchesAsync(request);
        }

        public async Task<Ticketing.Proto.FindByNameOrAddressResponse> FindTickets(string firstName, string lastName, string address)
        {
            var request = new Ticketing.Proto.FindByNameOrAddressRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address
            };
            return await _client.FindByNameOrAddressAsync(request);
        }

        public async Task ListenToMatchUpdates(string username, ClientObserver clientObserver)
        {
            var request = new Ticketing.Proto.UsernameRequest { Username = username };
            var responseStream = _client.NotifyMatchUpdated(request);
            try
            {
                await foreach (var match in responseStream.ResponseStream.ReadAllAsync())
                {
                    clientObserver.HandleMatchUpdate(match);
                }
            }
            catch (RpcException ex)
            {
                Logger.Warn($"Error while listening to match updates: {ex.Status.Detail}");
            }
        }

        public void Shutdown()
        {
            try
            {
                _channel?.ShutdownAsync().Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception e)
            {
                Logger.Warn($"Error shutting down channel: {e.Message}");
            }
        }
    }

}
