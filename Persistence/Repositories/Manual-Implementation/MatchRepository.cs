using Persistence.Utils;
using Models.Models;
using Models.Models.Enums;
using NLog;

namespace Persistence.Repositories
{
    public class MatchRepository : IRepoMatch
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<Match> Delete(Match entity)
        {
            Logger.Info("Deleting match with id: {0}", entity.Id);
            const string query = "DELETE FROM match WHERE id = @id";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", entity.Id);
            var affectedRows = await command.ExecuteNonQueryAsync();
            if (affectedRows == 0)
            {
                Logger.Warn("Match with id {0} could not be deleted", entity.Id);
                throw new Exception("Match could not be deleted!");
            }
            Logger.Info("Match with id {0} deleted successfully", entity.Id);
            return entity;
        }

        public async Task<List<Match>> FindAll()
        {
            Logger.Info("Finding all matches");
            const string query = "SELECT * FROM match";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            Logger.Debug("Connection opened");
            using var command = connection.CreateCommand();
            command.CommandText = query;
            using var reader = await command.ExecuteReaderAsync();
            var matches = new List<Match>();
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                Logger.Debug("Reading match with id {0}", id);
                var matchType = (MatchTypes)Enum.Parse(typeof(MatchTypes), reader.GetString(5));
                var match = new Match(reader.GetString(1), reader.GetString(2), matchType, reader.GetDouble(3), reader.GetInt64(4))
                {
                    Id = id,
                };
                matches.Add(match);
            }
            Logger.Info("Found {0} matches", matches.Count);
            return matches;
        }

        public async Task<Match> FindById(long id)
        {
            Logger.Info("Finding match with id: {0}", id);
            const string query = "SELECT * FROM match WHERE id = @id";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                Logger.Info("Match with id {0} found", id);
                var matchType = (MatchTypes)Enum.Parse(typeof(MatchTypes), reader.GetString(5));
                return new Match(reader.GetString(1), reader.GetString(2), matchType, reader.GetDouble(3), reader.GetInt64(4))
                {
                    Id = id,
                };
            }
            Logger.Warn("Match with id {0} not found", id);
            throw new Exception("Match not found");
        }

        public async Task<List<Match>> FindByTeamA(string teamA)
        {
            Logger.Info("Finding matches with team A: {0}", teamA);
            const string query = "SELECT * FROM match WHERE team_a = @team_a";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            var matches = new List<Match>();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@team_a", teamA);
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                Logger.Debug("Match found with team A: {0}", teamA);
                var id = reader.GetInt64(0);
                var matchType = (MatchTypes)Enum.Parse(typeof(MatchTypes), reader.GetString(5));
                var match = new Match(reader.GetString(1), reader.GetString(2), matchType, reader.GetDouble(3), reader.GetInt64(4))
                {
                    Id = id,
                };
                matches.Add(match);
            }
            Logger.Info("{0} matches found for team A: {1}", matches.Count, teamA);
            return matches;
        }

        public async Task<List<Match>> FindByTeamB(string teamB)
        {
            Logger.Info("Finding matches with team B: {0}", teamB);
            const string query = "SELECT * FROM match WHERE team_b = @team_b";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            var matches = new List<Match>();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@team_b", teamB);
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                Logger.Debug("Match found with team B: {0}", teamB);
                var id = reader.GetInt64(0);
                var matchType = (MatchTypes)Enum.Parse(typeof(MatchTypes), reader.GetString(5));
                var match = new Match(reader.GetString(1), reader.GetString(2), matchType, reader.GetDouble(3), reader.GetInt64(4))
                {
                    Id = id,
                };
                matches.Add(match);
            }
            Logger.Info("{0} matches found for team B: {1}", matches.Count, teamB);
            return matches;
        }

        public async Task<Match> FindByTeamAAndTeamB(string teamA, string teamB)
        {
            Logger.Info("Finding match with team A: {0} and team B: {1}", teamA, teamB);
            const string query = "SELECT * FROM match WHERE team_a = @team_a and team_b = @team_b";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@team_a", teamA);
            command.Parameters.AddWithValue("@team_b", teamB);
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                Logger.Info("Match found: {0} vs {1}", teamA, teamB);
                var id = reader.GetInt64(0);
                var matchType = (MatchTypes)Enum.Parse(typeof(MatchTypes), reader.GetString(5));
                var createdAt = reader.GetDateTime(3);
                return new Match(reader.GetString(1), reader.GetString(2), matchType, reader.GetDouble(3), reader.GetInt64(4))
                {
                    Id = id,
                };
            }
            Logger.Warn("Match not found between {0} and {1}", teamA, teamB);
            throw new Exception("Match not found");
        }

        public async Task<Match> Save(Match entity)
        {
            Logger.Info("Saving match between {0} and {1}", entity.TeamA, entity.TeamB);
            const string query = "INSERT INTO match (team_a, team_b, match_type, price_ticket, number_of_seats_total) VALUES (@team_a, @team_b, @match_type, @price_ticket, @number_of_seats_total); SELECT last_insert_rowid();";
            try
            {
                using var connection = HelperBd.NewSqlConnection();
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@team_a", entity.TeamA);
                command.Parameters.AddWithValue("@team_b", entity.TeamB);
                command.Parameters.AddWithValue("@match_type", entity.MatchType.ToString());
                command.Parameters.AddWithValue("@price_ticket", entity.PriceTicket);
                command.Parameters.AddWithValue("@number_of_seats_total", entity.NumberOfSeatsTotal);
                var id = Convert.ToInt64(await command.ExecuteScalarAsync());
                entity.Id = id;
                Logger.Info("Match saved with id: {0}", entity.Id);
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error saving match between {0} and {1}", entity.TeamA, entity.TeamB);
                throw new Exception("Error saving match");
            }
        }

        public async Task<Match> Update(Match entity)
        {
            Logger.Info("Updating match with id: {0}", entity.Id);
            const string query = "UPDATE match SET team_a = @team_a, team_b = @team_b, match_type = @match_type, price_ticket = @price_ticket, number_of_seats_total = @number_of_seats_total WHERE id = @id";
            try
            {
                using var connection = HelperBd.NewSqlConnection();
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@team_a", entity.TeamA);
                command.Parameters.AddWithValue("@team_b", entity.TeamB);
                command.Parameters.AddWithValue("@match_type", entity.MatchType.ToString());
                command.Parameters.AddWithValue("@price_ticket", entity.PriceTicket);
                command.Parameters.AddWithValue("@number_of_seats_total", entity.NumberOfSeatsTotal);
                command.Parameters.AddWithValue("@id", entity.Id);
                var affectedRows = await command.ExecuteNonQueryAsync();
                if (affectedRows == 0)
                {
                    Logger.Warn("Match with id {0} could not be updated", entity.Id);
                    throw new Exception("Match could not be updated!");
                }
                Logger.Info("Match with id {0} updated successfully", entity.Id);
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error updating match with id {0}", entity.Id);
                throw new Exception("Error updating match");
            }
        }
    }
}
