using Models.Models;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories.EF_Implementation
{
    public class MatchRepositoryEF(ContextDb context) : IRepoMatch
    {
        private readonly ContextDb _context = context;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public async Task<Match> Delete(Match entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            try
            {
                Logger.Info("MatchId primit: {0}", entity.Id);
                var match = await _context.Matches.Include(t => t.Tickets).FirstOrDefaultAsync(x => x.Id == entity.Id) ?? throw new ArgumentException("Match not found");
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
                Logger.Info("MatchId sters: {0}", match.Id);
                return match;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Eroare la ștergerea meciului cu ID {0}", entity.Id);
                throw;
            }
        }

        public async Task<List<Match>> FindAll()
        {
            Logger.Info("Caut toate meciurile");
            return await _context.Matches.Include(t => t.Tickets).ToListAsync();
        }

        public async Task<Match> FindById(long id)
        {
            Logger.Info("Caut meciul cu ID {0}", id);
            return await _context.Matches.Include(t => t.Tickets).FirstOrDefaultAsync(x => x.Id == id) ?? throw new ArgumentException("Match not found");
        }

        public async Task<List<Match>> FindByTeamA(string teamA)
        {
            Logger.Info("Caut meciurile pentru echipa {0}", teamA);
            if (string.IsNullOrEmpty(teamA))
            {
                Logger.Error("Echipa nu poate fi null sau goală");
                throw new ArgumentException("TeamA cannot be null or empty");
            }
            return await _context.Matches.Include(t => t.Tickets).Where(x => x.TeamA == teamA).ToListAsync();
        }

        public async Task<Match> FindByTeamAAndTeamB(string teamA, string TeamB)
        {
            Logger.Info("Caut meciul pentru echipele {0} si {1}", teamA, TeamB);
            if (string.IsNullOrEmpty(teamA))
            {
                Logger.Error("Echipa nu poate fi null sau goală");
                throw new ArgumentException("TeamA cannot be null or empty");
            }
            if (string.IsNullOrEmpty(TeamB))
            {
                Logger.Error("Echipa nu poate fi null sau goală");
                throw new ArgumentException("TeamB cannot be null or empty");
            }
            return await _context.Matches.Include(t => t.Tickets).FirstOrDefaultAsync(x => x.TeamA == teamA && x.TeamB == TeamB) ?? throw new ArgumentException("Match not found");
        }

        public async Task<List<Match>> FindByTeamB(string teamB)
        {
            Logger.Info("Caut meciurile pentru echipa {0}", teamB);
            if (string.IsNullOrEmpty(teamB))
            {
                Logger.Error("Echipa nu poate fi null sau goală");
                throw new ArgumentException("TeamB cannot be null or empty");
            }
            return await _context.Matches.Include(t => t.Tickets).Where(x => x.TeamB == teamB).ToListAsync();
        }

        public async Task<Match> Save(Match entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            if (string.IsNullOrEmpty(entity.TeamA))
            {
                Logger.Error("Echipa nu poate fi null sau goală");
                throw new ArgumentException("TeamA cannot be null or empty");
            }
            if (string.IsNullOrEmpty(entity.TeamB))
            {
                Logger.Error("Echipa nu poate fi null sau goală");
                throw new ArgumentException("TeamB cannot be null or empty");
            }
            if (entity.PriceTicket <= 0)
            {
                Logger.Error("Prețul biletului nu poate fi mai mic sau egal cu 0");
                throw new ArgumentException("PriceTicket must be greater than 0");
            }
            if (entity.NumberOfSeatsTotal <= 0)
            {
                Logger.Error("Numărul total de locuri nu poate fi mai mic sau egal cu 0");
                throw new ArgumentException("NumberOfSeatsTotal must be greater than 0");
            }
            await _context.Matches.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Match> Update(Match entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            if (string.IsNullOrEmpty(entity.TeamA))
            {
                Logger.Error("Echipa nu poate fi null sau goală");
                throw new ArgumentException("TeamA cannot be null or empty");
            }
            if (string.IsNullOrEmpty(entity.TeamB))
            {
                Logger.Error("Echipa nu poate fi null sau goală");
                throw new ArgumentException("TeamB cannot be null or empty");
            }
            if (entity.PriceTicket <= 0)
            {
                Logger.Error("Prețul biletului nu poate fi mai mic sau egal cu 0");
                throw new ArgumentException("PriceTicket must be greater than 0");
            }
            if (entity.NumberOfSeatsTotal <= 0)
            {
                Logger.Error("Numărul total de locuri nu poate fi mai mic sau egal cu 0");
                throw new ArgumentException("NumberOfSeatsTotal must be greater than 0");
            }
            Logger.Info("Meciul cu ID {0} va fi actualizat", entity.Id);
            var match = await _context.Matches
                            .Include(m => m.Tickets)
                            .FirstOrDefaultAsync(x => x.Id == entity.Id)
                            ?? throw new ArgumentException("Match not found"); 
            match.TeamA = entity.TeamA;
            match.TeamB = entity.TeamB;
            match.MatchType = entity.MatchType;
            match.PriceTicket = entity.PriceTicket;
            match.NumberOfSeatsTotal = entity.NumberOfSeatsTotal;
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
            Logger.Info("Meciul cu ID {0} a fost actualizat", match.Id);
            return match;
        }
    }
}
