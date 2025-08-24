using Grpc.Core;
using Services.Service;
using System.Runtime.CompilerServices;
using Ticketing.Proto;
namespace Server
{
    public class TicketingServiceImpl(GlobalService globalService) : TicketingService.TicketingServiceBase
    {
        private readonly GlobalService globalService = globalService;


        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            try
            {
                var user = new Models.Models.User(request.User.Username, request.User.Password);
                var loggedUser = await globalService.LogIn(user);

                return new LoginResponse
                {
                    Success = true,
                    Message = "Login successful",
                    User = new Ticketing.Proto.User
                    {
                        Id = loggedUser.Id,
                        Username = loggedUser.Username,
                        Password = loggedUser.Password
                    }
                };
            }
            catch (Exception e)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public override async Task<LogoutResponse> Logout(LogoutRequest request, ServerCallContext context)
        {
            try
            {
                var user = new Models.Models.User(request.User.Username, request.User.Password);
                await globalService.LogOut(user.Username);
                await globalService.GetNotifyEvents().Unregister(user.Username);
                return new LogoutResponse
                {
                    Success = true,
                    Message = "Logout successful"
                };
            }
            catch (Exception e)
            {
                return new LogoutResponse
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        public override async Task<GetAllMatchesResponse> GetAllMatches(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        {
            try
            {
                GetAllMatchesResponse response = new();
                var matches = await globalService.AllMatches();
                response.Match.AddRange(matches.Select(match => new Match
                {
                    Id = match.Id,
                    TeamA = match.TeamA,
                    TeamB = match.TeamB,
                    PriceTicket = match.PriceTicket,
                    NumberOfSeats = match.NumberOfSeatsTotal,
                    MatchType = (Match.Types.Type)(int)match.MatchType

                }));
                return response;
            }
            catch (Exception e)
            {
                throw new RpcException(new Status(StatusCode.Internal, e.Message));
            }
        }

        public override Task NotifyMatchUpdated(UsernameRequest request, IServerStreamWriter<Match> response, ServerCallContext context)
        {
            globalService.GetNotifyEvents().Register(request.Username, response);
            return Task.Delay(-1, context.CancellationToken);
        }

        public override async Task<FindByNameOrAddressResponse> FindByNameOrAddress(FindByNameOrAddressRequest request, ServerCallContext context)
        {
            try
            {
                var response = new FindByNameOrAddressResponse();
                var tickets = await globalService.FindTickets(request.FirstName, request.LastName, request.Address);
                
                response.Tickets.AddRange(tickets.Select(ticket => new Ticket
                {
                    Id = ticket.Id,
                    FirstName = ticket.FirstName,
                    LastName = ticket.LastName,
                    Address = ticket.Address,
                    NumberOfSeats = ticket.NumberOfSeats,
                    Match = new Match
                    {
                        Id = ticket.Match.Id,
                        TeamA = ticket.Match.TeamA,
                        TeamB = ticket.Match.TeamB,
                        PriceTicket = ticket.Match.PriceTicket,
                        NumberOfSeats = ticket.Match.NumberOfSeatsTotal,
                        MatchType = (Match.Types.Type)(int)ticket.Match.MatchType
                    }
                }));
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public override async Task<SellTicketResponse> SellTicket(SellTicketRequest request, ServerCallContext context)
        {
            var response = new SellTicketResponse();
            try
            {
                var match = new Models.Models.Match(request.Ticket.Match.TeamA, request.Ticket.Match.TeamB, (Models.Models.Enums.MatchTypes)(int)request.Ticket.Match.MatchType, request.Ticket.Match.PriceTicket, request.Ticket.Match.NumberOfSeats) { Id = request.Ticket.Match.Id };

                var ticket = new Models.Models.Ticket(match, request.Ticket.FirstName, request.Ticket.LastName, request.Ticket.Address, request.Ticket.NumberOfSeats);
                var soldTicket = await globalService.SellTicket(ticket);
                response.Success = true;
                response.Message = "Ticket sold successfully";
                response.Match = new Match
                {

                    Id = soldTicket.Match.Id,
                    TeamA = soldTicket.Match.TeamA,
                    TeamB = soldTicket.Match.TeamB,
                    PriceTicket = soldTicket.Match.PriceTicket,
                    NumberOfSeats = soldTicket.Match.NumberOfSeatsTotal,
                    MatchType = (Match.Types.Type)(int)soldTicket.Match.MatchType

                };
                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                return response;
            }
        }
    }
}