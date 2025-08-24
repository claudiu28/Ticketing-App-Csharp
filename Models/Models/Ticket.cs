using System.Text.Json.Serialization;

namespace Models.Models
{
    public class Ticket : Entity<long>
    {
        public long MatchId { get; set; }
        [JsonIgnore]
        public Match Match { get; set; } = null!;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long NumberOfSeats { get; set; } 

        public Ticket() { }
        public Ticket(Match match, string firstName, string lastName, string address, long numberOfSeats)
        {
            Match = match;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            NumberOfSeats = numberOfSeats;
        }

    }
}
