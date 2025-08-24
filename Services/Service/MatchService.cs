using Models.Models;
using Persistence.Repositories;
using NLog;


namespace Services.Service
{
    public class MatchService(IRepoMatch RepoMatch)
    {
        private readonly IRepoMatch _RepoMatch = RepoMatch;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<Match> GetMatchById (Match match)
        {
            try
            {
                Logger.Info("Find Match by id");
                var foundMatch = await _RepoMatch.FindById(match.Id) ?? throw new Exception("Match could not be null!");
                return foundMatch;
            }
            catch (Exception e)
            {
                Logger.Info("Exception in find match by id services: {0}", e.Message);
                throw new Exception("Get Match Failed: " + e.Message);
            }
        }


        public async Task<List<Match>> GetAll()
        {
            try
            {
                Logger.Info("Find All Matches services...");
                return await _RepoMatch.FindAll();
            }catch(Exception e)
            {
                Logger.Info("Exception in find all match services: {0}" , e.Message);
                throw new Exception("Get All Failde: " + e.Message);
            }
        }

        public async Task<Match> UpdateNumberOfSeats(long IdMatch, long NumberOfSeats)
        {
            try
            {
                Logger.Info("Try to find match by id");
                var match = await _RepoMatch.FindById(IdMatch) ?? throw new Exception("Match could not be null!");
                match.NumberOfSeatsTotal = NumberOfSeats;
                Logger.Info("Update match with number of seats");
                return await _RepoMatch.Update(match);
            }
            catch (Exception e)
            {
                Logger.Info("Exception appear in UpdateNumberOfServices:", e.Message);
                throw new Exception("Match not found :" + e.Message);
            }
        }


    }
}
