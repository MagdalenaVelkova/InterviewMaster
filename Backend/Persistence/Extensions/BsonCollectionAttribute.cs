using System;

namespace InterviewMaster.Persistence.Extensions
{
    // Żyła, M. 2019. Mongo Repository in .NET Core. Medium 24 December. Available at: https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e [Accessed: 4 March 2022].

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
