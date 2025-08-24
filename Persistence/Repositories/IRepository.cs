using Models.Models;

namespace Persistence.Repositories
{
    public interface IRepository<in TId, TE> where TE : Entity<TId>
    {
        Task<TE> FindById(TId id);
        Task<TE> Save(TE entity);
        Task<TE> Update(TE entity);
        Task<TE> Delete(TE entity);
        Task<List<TE>> FindAll();
    }
}