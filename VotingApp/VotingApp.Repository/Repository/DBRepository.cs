using Microsoft.EntityFrameworkCore;
using VotingApp.Repository.ApplicationDBContext;

namespace VotingApp.Repository.Repository
{
    public class DbRepository<TEntity> : IDbRepository<TEntity> where TEntity : class
    {
        private readonly AppDBContext _appDbContext;
        private DbSet<TEntity> _entities;

        public DbRepository(AppDBContext appDbContext)
        {
            this._appDbContext = appDbContext;
            _entities = appDbContext.Set<TEntity>();
        }


        public List<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            try
            {
                _entities.Add(entity);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _appDbContext.Update(entity);
            _appDbContext.SaveChanges();
        }
    }
}
