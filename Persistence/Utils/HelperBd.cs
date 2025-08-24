using Microsoft.Data.Sqlite;
using NLog;

namespace Persistence.Utils
{
    public class HelperBd
    {
        private static string? _connectionString;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static SqliteConnection NewSqlConnection()
        {
            if (!string.IsNullOrEmpty(_connectionString)) return new SqliteConnection(_connectionString);
            Logger.Info("Connection string not found");
            throw new Exception("Connection string not found");
        }
    }
}
