using NLog;
using Models.Models;
using Models.Models.Enums;
using Client.Events;

namespace Client.Views
{
    public class ClientObserver
    {
        public event EventHandler<MatchEvent>? MatchUpdated;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public void HandleMatchUpdate(Ticketing.Proto.Match match)
        {

            Match matchModels = new
            (
                match.TeamA,
                match.TeamB,
                (MatchTypes)match.MatchType,
                match.PriceTicket,
                match.NumberOfSeats
            )
            { Id = match.Id };

            MatchUpdated?.Invoke(this, new MatchEvent(matchModels));
        }
    }
}
