using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;

namespace InterviewMaster.Persistence.Repositories
{

    // Żyła, M. 2019. Mongo Repository in .NET Core. Medium 24 December. Available at: https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e [Accessed: 4 March 2022].

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
