using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;

namespace InterviewMaster.Persistance.Repositories
{
    public abstract class BaseRepository<T>
    {
        protected BaseRepository(IMongoDatabase database)
        {
            Collection = database.GetCollection<T>(DbCollectionName);
            InitialiseIndecies();
        }

        public abstract string DbCollectionName { get; }

        protected IMongoCollection<T> Collection { get; }
        public virtual IMongoQueryable<T> Query()
        {
            return Collection.AsQueryable();
        }

        protected virtual void InitialiseIndecies()
        { }
    }
}
