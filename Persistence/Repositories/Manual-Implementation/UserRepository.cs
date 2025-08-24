using Models.Models;
using Persistence.Utils;
using NLog;

namespace Persistence.Repositories
{
    public class UserRepository : IRepoUser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public async Task<User> Delete(User entity)
        {
            Logger.Info("Deleting user {0}", entity.Username);
            const string query = "DELETE FROM user WHERE id = @id";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", entity.Id);
            var affectedRows = await command.ExecuteNonQueryAsync();
            if (affectedRows == 0)
            {
                Logger.Warn("User with id {0} could not be deleted!", entity.Id);
                throw new Exception("User could not be deleted!");
            }
            Logger.Info("User with id {0} deleted successfully", entity.Id);
            return entity;
        }

        public async Task<List<User>> FindAll()
        {
            Logger.Info("Finding all users");
            const string query = "SELECT * FROM user";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            Logger.Debug("Connection opened");
            using var command = connection.CreateCommand();
            command.CommandText = query;
            Logger.Debug("Executing query to retrieve all users");
            using var reader = await command.ExecuteReaderAsync();
            List<User> users = [];
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                Logger.Debug("Reading user with id {0}", id);
                User user = new(reader.GetString(1), reader.GetString(2))
                {
                    Id = id
                };
                users.Add(user);
            }
            Logger.Info("Returning {0} users", users.Count);
            return users;
        }

        public async Task<User> FindById(long id)
        {
            Logger.Info("Finding user with id {0}", id);
            const string query = "SELECT * FROM user WHERE id = @id";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                Logger.Info("User with id {0} found", id);
                return new User(reader.GetString(1), reader.GetString(2))
                {
                    Id = id
                };
            }
            Logger.Warn("User with id {0} not found", id);
            throw new Exception("User not found");
        }

        public async Task<User> FindByUsername(string username)
        {
            Logger.Info("Finding user with username {0}", username);
            const string query = "SELECT * FROM user WHERE username = @username";
            using var connection = HelperBd.NewSqlConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@username", username);
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                var id = reader.GetInt64(0);
                Logger.Info("User with username {0} found", username);
                return new User(reader.GetString(1), reader.GetString(2))
                {
                    Id = id
                };
            }
            Logger.Warn("User with username {0} not found", username);
            throw new Exception("User not found");
        }

        public async Task<User> Save(User entity)
        {
            Logger.Info("Saving user {0}", entity.Username);
            const string query = "INSERT INTO user (username, password) VALUES (@username, @password); SELECT last_insert_rowid();";
            try
            {
                using var connection = HelperBd.NewSqlConnection();
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = query;

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(entity.Password);
                command.Parameters.AddWithValue("@username", entity.Username);
                command.Parameters.AddWithValue("@password", hashedPassword);
                var id = Convert.ToInt64(await command.ExecuteScalarAsync());
                entity.Id = id;
                entity.Password = hashedPassword;
                Logger.Info("User {0} saved with id {1}", entity.Username, entity.Id);
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error saving user {0}", entity.Username);
                throw new Exception("User could not be saved!");
            }
        }

        public async Task<User> Update(User entity)
        {
            Logger.Info("Updating user with id {0}", entity.Id);
            const string query = "UPDATE user SET username = @username, password = @password WHERE id = @id";
            try
            {
                using var connection = HelperBd.NewSqlConnection();
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = query;

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(entity.Password);
                command.Parameters.AddWithValue("@username", entity.Username);
                command.Parameters.AddWithValue("@password", hashedPassword);
                command.Parameters.AddWithValue("@id", entity.Id);
                var affectedRows = await command.ExecuteNonQueryAsync();
                if (affectedRows == 0)
                {
                    Logger.Warn("User with id {0} could not be updated!", entity.Id);
                    throw new Exception("User could not be updated!");
                }
                Logger.Info("User with id {0} updated successfully", entity.Id);
                entity.Password = hashedPassword;
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error updating user with id {0}", entity.Id);
                throw new Exception("User could not be updated!");
            }
        }
    }
}
