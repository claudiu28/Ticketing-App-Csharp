using Models.Models;

namespace Persistence.Repositories
{
    public interface IRepoMatch : IRepository<long, Match>
    {
        Task<List<Match>> FindByTeamA(string teamA);
        Task<List<Match>> FindByTeamB(string teamB);
        Task<Match> FindByTeamAAndTeamB(string teamA, string TeamB);
    }

}