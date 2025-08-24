using Models.Models;
using Persistence.Repositories;
using NLog;


namespace Services.Service
{
    public class TicketService(IRepoTicket RepoTicket)
    {
        private readonly IRepoTicket _repoTicket = RepoTicket;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<Ticket> SellTicket(Ticket ticket)
        {
            try
            {
                Logger.Info("Ticket, {0}, {1}",ticket.Match.Id, ticket.FirstName);
                Logger.Info("Ticket Save Service Entry...");
                var user = await _repoTicket.Save(ticket);
                return user ?? throw new Exception("User could not be null!");
            }
            catch (Exception e)
            {
                Logger.Error("Exception appears in Ticket Service:{0}",e);
                throw new Exception("Sell ticket could not done: " + e.Message);
            }
        }

        public async Task<List<Ticket>> FindByNameAndAddress(string firstName, string lastName, string address)
        {
            Logger.Info("FindByNameAndAddress called with: firstName={0}, lastName={1}, address={2}", firstName, lastName, address);

            if (Equals(lastName, "") && Equals(address, ""))
            {
                Logger.Info("Searching by firstName only:{0}", firstName);
                return await _repoTicket.FindByFirstName(firstName);
            }
            if (Equals(firstName, "") && Equals(lastName, ""))
            {
                Logger.Info("Searching by address only:{0}", address);
                return await _repoTicket.FindByAddress(address);
            }
            if (Equals(firstName, "") && Equals(address, ""))
            {
                Logger.Info("Searching by lastName only:{0}", lastName);
                return await _repoTicket.FindByLastName(lastName);
            }
            if (Equals(firstName, ""))
            {
                Logger.Info("Searching by lastName={0} and address={1}", lastName, address);
                var tickets = new List<Ticket>();
                var ByAddress = await _repoTicket.FindByAddress(address);
                Logger.Debug("Found {0} tickets with address={1}", ByAddress.Count, address);

                foreach (var Addr in ByAddress)
                {
                    if (Equals(Addr.LastName, lastName))
                    {
                        Logger.Debug("Match found: Ticket ID={0}, LastName={1}", Addr.Id, Addr.LastName);
                        tickets.Add(Addr);
                    }
                }
                Logger.Info("Returned {0} tickets after filtering by lastName={1} AND address={2}", tickets.Count, lastName, address);
                return tickets;
            }
            if (Equals(lastName, ""))
            {
                Logger.Info("Searching by firstName={0} AND address={1}", firstName, address);
                var tickets = new List<Ticket>();
                var ByFirstName = await _repoTicket.FindByFirstName(firstName);
                Logger.Debug("Found {0} tickets with firstName={1}", ByFirstName.Count, firstName);

                foreach (var FrstNam in ByFirstName)
                {
                    if (Equals(FrstNam.Address, address))
                    {
                        Logger.Debug("Match found: Ticket ID={0}, Address={1}", FrstNam.Id, FrstNam.Address);
                        tickets.Add(FrstNam);
                    }
                }
                Logger.Info("Returned {0} tickets after filtering by firstName={1} AND address={2}", tickets.Count, firstName, address);
                return tickets;
            }
            if (Equals(address, ""))
            {
                Logger.Info("Searching by firstName={0} AND lastName={1}", firstName, lastName);
                var tickets = new List<Ticket>();
                var ByLastName = await _repoTicket.FindByLastName(lastName);
                Logger.Debug("Found {0} tickets with lastName={1}", ByLastName.Count, lastName);

                foreach (var LstName in ByLastName)
                {
                    if (Equals(LstName.FirstName, firstName))
                    {
                        Logger.Debug("Match found: Ticket ID={0}, FirstName={1}", LstName.Id, LstName.FirstName);
                        tickets.Add(LstName);
                    }
                }
                Logger.Info("Returned {0} tickets after filtering by firstName={1} AND lastName={2}", tickets.Count, firstName, lastName);
                return tickets;
            }

            Logger.Info("Searching by all three criteria: firstName={0}, lastName={1}, address={2}", firstName, lastName, address);
            var threeTickets = new List<Ticket>();
            var ByLMN = await _repoTicket.FindByLastName(lastName);
            Logger.Debug("Found {0} tickets with lastName={1}", ByLMN.Count, lastName);

            foreach (var LMN in ByLMN)
            {
                if (Equals(LMN.FirstName, firstName) && Equals(LMN.Address, address))
                {
                    Logger.Debug("Match found: Ticket ID={0}, FirstName={1}, Address={2}", LMN.Id, LMN.FirstName, LMN.Address);
                    threeTickets.Add(LMN);
                }
            }
            Logger.Info("Returned {0} tickets after filtering by firstName={1} AND lastName={2} AND address={3}", threeTickets.Count, firstName, lastName, address);
            return threeTickets;
        }
    }
}
