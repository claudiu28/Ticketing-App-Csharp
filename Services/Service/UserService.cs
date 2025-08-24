using NLog;
using Models.Models;
using Persistence.Repositories;

namespace Services.Service
{
    public class UserService(IRepoUser RepoUser)
    {
        private readonly IRepoUser _repoUser = RepoUser;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public async Task<User> FindByUsername(string username)
        {
            try
            {
                Logger.Info("Try to find User with username {0}", username);
                var user = _repoUser.FindByUsername(username);
                return await user ?? throw new Exception("Username not find in database");
            }
            catch (Exception e)
            {
                Logger.Error("Exception appears in User Service");
                throw new Exception("Find By Username failed: " + e.Message);
            }
        }
    }
}
