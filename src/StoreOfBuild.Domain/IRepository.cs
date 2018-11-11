using System.Collections.Generic;

namespace StoreOfBuild.Domain
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);

        void Save(TEntity entity);

        IEnumerable<TEntity> All();
    }
}