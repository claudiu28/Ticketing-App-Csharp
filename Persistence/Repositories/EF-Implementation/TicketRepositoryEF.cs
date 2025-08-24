using Models.Models;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using NLog;


namespace Persistence.Repositories.EF_Implementation
{
    public class TicketRepositoryEF(ContextDb context) : IRepoTicket
    {
        private readonly ContextDb _context = context;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<Ticket> Delete(Ticket entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            Logger.Info("TicketId primit: {0}", entity.Id);
            var ticket = await _context.Tickets.Include(m => m.Match).FirstOrDefaultAsync(x => x.Id == entity.Id) ?? throw new ArgumentException("Ticket not found");
            ticket.Match.RemoveTicket(ticket);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            Logger.Info("Ticket cu ID {0} a fost șters din baza de date!", entity.Id);
            return ticket;
        }

        public async Task<List<Ticket>> FindAll()
        {
            Logger.Info("Caut toate biletele din baza de date...");
            return await _context.Tickets.Include(m => m.Match).ToListAsync();
        }

        public async Task<List<Ticket>> FindByAddress(string address)
        {
            ArgumentNullException.ThrowIfNull(address);
            return await _context.Tickets.Include(m => m.Match).Where(x => x.Address == address).ToListAsync();
        }

        public async Task<List<Ticket>> FindByFirstName(string firstName)
        {
            ArgumentNullException.ThrowIfNull(firstName);
            return await _context.Tickets.Include(m => m.Match).Where(x => x.FirstName == firstName).ToListAsync();
        }

        public async Task<Ticket> FindById(long id)
        {
            Logger.Info("Caut ticketul cu ID {0} în baza de date...", id);
            return await _context.Tickets.Include(m => m.Match).FirstOrDefaultAsync(x => x.Id == id) ?? throw new ArgumentException("Ticket not found");
        }

        public async Task<List<Ticket>> FindByLastName(string lastName)
        {
            ArgumentNullException.ThrowIfNull(lastName);
            return await _context.Tickets.Include(m => m.Match).Where(x => x.LastName == lastName).ToListAsync();
        }

        public async Task<Ticket> Save(Ticket entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Logger.Info("MatchId primit: {0}", entity.Match.Id);

            var match = await _context.Matches.Include(t => t.Tickets).FirstOrDefaultAsync(x => x.Id == entity.Match.Id);
            if (match == null)
            {
                Logger.Error("Match cu ID {0} nu a fost găsit în baza de date!", entity.Match.Id);
                throw new ArgumentException("Match not found");
            }

            entity.Match = match;
            entity.MatchId = match.Id;
            match.AddTicket(entity);
            try
            {
                await _context.Tickets.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("Eroare la salvare bilet: {0}", ex.Message);
                throw;
            }

            return entity;
        }


        public async Task<Ticket> Update(Ticket entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            var ticket = await _context.Tickets.Include(t => t.Match).FirstOrDefaultAsync(x => x.Id == entity.Id) ?? throw new ArgumentException("Ticket not found");

            if (ticket.MatchId != entity.Match.Id)
            {
                ticket.Match.RemoveTicket(ticket);

                var newMatch = await _context.Matches
                    .Include(m => m.Tickets)
                    .FirstOrDefaultAsync(x => x.Id == entity.Match.Id)
                    ?? throw new ArgumentException("New match not found");

                ticket.Match = newMatch;
                ticket.MatchId = newMatch.Id;

                newMatch.AddTicket(ticket);
            }

            ticket.FirstName = entity.FirstName;
            ticket.LastName = entity.LastName;
            ticket.Address = entity.Address;
            ticket.NumberOfSeats = entity.NumberOfSeats;
            ticket.Match = await _context.Matches.FirstOrDefaultAsync(x => x.Id == entity.Match.Id) ?? throw new ArgumentException("Match not found");
            
            try
            {
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("Eroare la update bilet: {0}", ex.Message);
                throw;
            }

            return ticket;
        }
    }
}
