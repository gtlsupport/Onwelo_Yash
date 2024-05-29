namespace VotingApp.Repository.Repository
{
    public interface IDbRepository<TEntity>
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        List<TEntity> GetAll();

    }
}
