using Models.Models;
using NLog;
using Persistence.Utils;
namespace Persistence.Repositories
{
    public class TicketRepository(IRepoMatch matchRepository) : IRepoTicket
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IRepoMatch _matchRepository = matchRepository;

        public async Task<Ticket> Delete(Ticket entity)
        {
            Logger.Info("Deleting ticket with id:{0}", entity.Id);
            const string query = "DELETE FROM ticket WHERE id = @id";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", entity.Id);
            var affectedRows = await command.ExecuteNonQueryAsync();
            if (affectedRows == 0)
            {
                Logger.Info("Ticket could not be deleted!");
                throw new Exception("Ticket could not be deleted!");
            }
            Logger.Info("Ticket with id {0} deleted", entity.Id);
            return entity;
        }

        public async Task<List<Ticket>> FindAll()
        {
            Logger.Info("Finding all tickets");
            const string query = "SELECT id, match_id, first_name, last_name, address, number_of_seats_ticket FROM ticket";
            using var connection = HelperBd.NewSqlConnection();
            Logger.Info("Connection opened");
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            Logger.Info("Executing query");
            using var reader = await command.ExecuteReaderAsync();
            List<Ticket> tickets = [];
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                Logger.Info("Reading ticket with id {0}", id);
                var matchId = reader.GetInt64(1);
                var firstName = reader.GetString(2);
                var lastName = reader.GetString(3);
                var address = reader.GetString(4);
                var numberOfSeats = reader.GetInt64(5);

                var match = await _matchRepository.FindById(matchId);

                Ticket ticket = new(match, firstName, lastName, address, numberOfSeats)
                {
                    Id = id,
                };
                tickets.Add(ticket);
            }
            Logger.Info("Returning {0} tickets", tickets.Count);
            return tickets;
        }

        public async Task<List<Ticket>> FindByAddress(string address)
        {
            Logger.Info("Finding ticket with address:{0}", address);
            const string query = "SELECT id, match_id, first_name, last_name, address, number_of_seats_ticket FROM ticket WHERE address = @address";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            var tickets = new List<Ticket>();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@address", address);
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                Logger.Info("Ticket with address {0} found", address);
                var id = reader.GetInt64(0);
                var matchId = reader.GetInt64(1);
                var firstName = reader.GetString(2);
                var lastName = reader.GetString(3);
                var addressFromDb = reader.GetString(4);
                var numberOfSeats = reader.GetInt64(5);

                var match = await _matchRepository.FindById(matchId);

                var ticket = new Ticket(match, firstName, lastName, addressFromDb, numberOfSeats)
                {
                    Id = id,
                };
                tickets.Add(ticket);
            }
            return tickets;
        }

        public async Task<List<Ticket>> FindByFirstName(string firstName)
        {
            Logger.Info("Finding ticket with first name:{0}", firstName);
            const string query = "SELECT id, match_id, first_name, last_name, address, number_of_seats_ticket FROM ticket WHERE first_name = @first_name";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            var tickets = new List<Ticket>();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@first_name", firstName);
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                Logger.Info("Ticket with first name {0} found", firstName);
                var id = reader.GetInt64(0);
                var matchId = reader.GetInt64(1);
                var firstNameFromDb = reader.GetString(2);
                var lastName = reader.GetString(3);
                var address = reader.GetString(4);
                var numberOfSeats = reader.GetInt64(5);

                var match = await _matchRepository.FindById(matchId);

                var ticket = new Ticket(match, firstNameFromDb, lastName, address, numberOfSeats)
                {
                    Id = id,
                };
                tickets.Add(ticket);
            }
            return tickets;
        }

        public async Task<Ticket> FindById(long id)
        {
            Logger.Info("Finding ticket with id:{0}", id);
            const string query = "SELECT id, match_id, first_name, last_name, address, number_of_seats_ticket FROM ticket WHERE id = @id";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                Logger.Info("Ticket with id {0} found", id);
                var idFromDb = reader.GetInt64(0);
                var matchId = reader.GetInt64(1);
                var firstName = reader.GetString(2);
                var lastName = reader.GetString(3);
                var address = reader.GetString(4);
                var numberOfSeats = reader.GetInt64(5);


                var match = await _matchRepository.FindById(matchId);

                return new Ticket(match, firstName, lastName, address, numberOfSeats)
                {
                    Id = idFromDb,
                };
            }
            Logger.Info("Ticket with id {0} not found", id);
            throw new Exception("Ticket not found");
        }

        public async Task<List<Ticket>> FindByLastName(string lastName)
        {
            Logger.Info("Finding ticket with last name:{0}", lastName);
            const string query = "SELECT id, match_id, first_name, last_name, address, number_of_seats_ticket FROM ticket WHERE last_name = @last_name";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            var tickets = new List<Ticket>();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@last_name", lastName);
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                Logger.Info("Ticket with last name {0} found", lastName);
                var id = reader.GetInt64(0);
                var matchId = reader.GetInt64(1);
                var firstName = reader.GetString(2);
                var lastNameFromDb = reader.GetString(3);
                var address = reader.GetString(4);
                var numberOfSeats = reader.GetInt64(5);

                var match = await _matchRepository.FindById(matchId);

                var ticket = new Ticket(match, firstName, lastNameFromDb, address, numberOfSeats)
                {
                    Id = id,
                };
                tickets.Add(ticket);
            }
            return tickets;
        }

        public async Task<Ticket> FindByMatchId(long matchId)
        {
            Logger.Info("Finding ticket with match id:{0}", matchId);
            const string query = "SELECT id, match_id, first_name, last_name, address, number_of_seats_ticket FROM ticket WHERE match_id = @match_id";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@match_id", matchId);
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                Logger.Info("Ticket with match id {0} found", matchId);
                var id = reader.GetInt64(0);
                var matchIdFromDb = reader.GetInt64(1);
                var firstName = reader.GetString(2);
                var lastName = reader.GetString(3);
                var address = reader.GetString(4);
                var numberOfSeats = reader.GetInt64(5);

                var match = await _matchRepository.FindById(matchId);

                return new Ticket(match, firstName, lastName, address, numberOfSeats)
                {
                    Id = id,
                };
            }
            Logger.Info("Ticket with match id {0} not found", matchId);
            throw new Exception("Ticket not found");
        }

        public async Task<Ticket> Save(Ticket entity)
        {
            Logger.Info("Saving ticket with id:{0}", entity.Id);
            const string query = "INSERT INTO ticket (first_name, last_name, address, number_of_seats_ticket, match_id) VALUES (@first_name, @last_name, @address, @number_of_seats_ticket, @match_id); SELECT last_insert_rowid();";
            try
            {
                using var connection = HelperBd.NewSqlConnection();
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@first_name", entity.FirstName);
                command.Parameters.AddWithValue("@last_name", entity.LastName);
                command.Parameters.AddWithValue("@address", entity.Address);
                command.Parameters.AddWithValue("@number_of_seats_ticket", entity.NumberOfSeats);
                command.Parameters.AddWithValue("@match_id", entity.Match.Id);
                var id = Convert.ToInt64(await command.ExecuteScalarAsync());
                entity.Id = id;
                Logger.Info("Ticket with id {0} saved", entity.Id);
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error("Error saving ticket: {0}", ex.Message);
                throw new Exception("Error saving ticket");
            }
        }

        public async Task<Ticket> Update(Ticket entity)
        {
            Logger.Info("Updating ticket with id:{0}", entity.Id);
            const string query = "UPDATE ticket SET first_name = @first_name, last_name = @last_name, address = @address, number_of_seats_ticket = @number_of_seats_ticket, match_id = @match_id WHERE id = @id";
            try
            {
                using var connection = HelperBd.NewSqlConnection();
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@first_name", entity.FirstName);
                command.Parameters.AddWithValue("@last_name", entity.LastName);
                command.Parameters.AddWithValue("@address", entity.Address);
                command.Parameters.AddWithValue("@number_of_seats_ticket", entity.NumberOfSeats);
                command.Parameters.AddWithValue("@match_id", entity.Match.Id);
                command.Parameters.AddWithValue("@id", entity.Id);
                var affectedRows = await command.ExecuteNonQueryAsync();
                if (affectedRows == 0)
                {
                    Logger.Info("Ticket could not be updated!");
                    throw new Exception("Ticket could not be updated!");
                }
                Logger.Info("Ticket with id {0} updated", entity.Id);
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error("Error updating ticket: {0}", ex.Message);
                throw new Exception("Error updating ticket");
            }
        }
    }
}