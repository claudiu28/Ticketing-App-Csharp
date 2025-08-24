using Models.Models;

namespace Persistence.Repositories;
public interface IRepoTicket : IRepository<long, Ticket>
{
    Task<List<Ticket>> FindByFirstName(string firstName);
    Task<List<Ticket>>FindByLastName(string lastName);
    Task<List<Ticket>> FindByAddress(string address);
}

