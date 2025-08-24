using Models.Models;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using NLog;


namespace Persistence.Repositories.EF_Implementation
{
    public class UserRepositoryEF(ContextDb context) : IRepoUser
    {
        private readonly ContextDb _context = context;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<User> Delete(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            Logger.Info("UserId primit: {0}", entity.Id);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == entity.Id) ?? throw new ArgumentException("User not found");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            Logger.Info("User cu ID {0} a fost șters din baza de date!", entity.Id);
            return user;
        }

        public async Task<List<User>> FindAll()
        {
            Logger.Info("Caut toți utilizatorii din baza de date...");
            return await _context.Users.ToListAsync();
        }

        public async Task<User> FindById(long id)
        {
            Logger.Info("Caut utilizatorul cu ID {0} în baza de date...", id);
            if (id <= 0)
            {
                Logger.Error("ID-ul utilizatorului este invalid: {0}", id);
                throw new ArgumentException("Invalid ID");
            }
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id) ?? throw new ArgumentException("User not found");
        }

        public async Task<User> FindByUsername(string username)
        {
            Logger.Info("Caut utilizatorul cu username {0} în baza de date...", username);
            if (string.IsNullOrEmpty(username))
            {
                Logger.Error("Username-ul utilizatorului este invalid: {0}", username);
                throw new ArgumentException("Invalid username");
            }
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username) ?? throw new ArgumentException("User not found");
        }

        public async Task<User> Save(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            Logger.Info("Salvez utilizatorul cu username {0} în baza de date...", entity.Username);
            if (string.IsNullOrEmpty(entity.Username))
            {
                Logger.Error("Username-ul utilizatorului este invalid: {0}", entity.Username);
                throw new ArgumentException("Invalid username");
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            entity.Password = hashedPassword;
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            Logger.Info("Utilizatorul cu username {0} a fost salvat în baza de date!", entity.Username);
            return entity;
        }

        public async Task<User> Update(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            Logger.Info("Actualizez utilizatorul cu ID {0} în baza de date...", entity.Id);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            if (string.IsNullOrEmpty(entity.Username))
            {
                Logger.Error("Username-ul utilizatorului este invalid: {0}", entity.Username);
                throw new ArgumentException("Invalid username");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == entity.Id) ?? throw new ArgumentException("User not found");
            if (!BCrypt.Net.BCrypt.Verify(entity.Password, user.Password))
            {
                entity.Password = hashedPassword;
            }

            user.Username = entity.Username;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            Logger.Info("Utilizatorul cu ID {0} a fost actualizat în baza de date!", entity.Id);
            return user;

        }
    }
}
