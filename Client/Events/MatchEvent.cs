namespace Client.Events
{
    public class MatchEvent(Models.Models.Match match) : EventArgs
    {
        public Models.Models.Match Match { get; } = match;
    }
}
