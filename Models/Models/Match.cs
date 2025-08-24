using Models.Models.Enums;
using System.Text.Json.Serialization;

namespace Models.Models
{
    public class Match : Entity<long>
    {
        public string TeamA { get; set; } = string.Empty;
        public string TeamB { get; set; } = string.Empty;
        public MatchTypes MatchType { get; set; }
        public double PriceTicket { get; set; } 
        public long NumberOfSeatsTotal { get; set; }

        [JsonIgnore]
        public List<Ticket> Tickets { get; set; } = [];

        public Match() { }
        public Match(string teamA, string teamB, MatchTypes matchType, double priceTicket, long numberOfSeatsTotal)
        {
            TeamA = teamA;
            TeamB = teamB;
            MatchType = matchType;
            PriceTicket = priceTicket;
            NumberOfSeatsTotal = numberOfSeatsTotal;
        }

        public void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
        }

        public void RemoveTicket(Ticket ticket)
        {
            Tickets.Remove(ticket);
        }
    }
}
