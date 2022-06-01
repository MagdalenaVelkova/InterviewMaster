using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using Mongo2Go;
using System;
using System.Threading.Tasks;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;

namespace InterviewMaster.Test.Util
{
    public class MongoDbService : IDisposable
    {
        private const string DbName = "InterviewMaster";
        private readonly MongoDbRunner? runner;

        public IMongoDatabase MongoDatabase { get; }

        private static readonly string[] MongoCollections = { };
        public MongoDbService()
        {
            runner = MongoDbRunner.Start(singleNodeReplSet : true);

            var client = new MongoClient(runner?.ConnectionString);
            MongoDatabase = client.GetDatabase(DbName);
        }
        private IMongoCollection<BsonDocument> GetMongoCollection(string collectionName)
        {
            return MongoDatabase.GetCollection<BsonDocument>(collectionName);
        }

        private IMongoCollection<T>? GetMongoCollection<T>(string collectionName)
        {
            return MongoDatabase?.GetCollection<T>(collectionName, new MongoCollectionSettings { ReadPreference = ReadPreference.PrimaryPreferred });
        }

        public async Task<string>? GetDocument(string collectionName, string id)
        {
            var collection = GetMongoCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq(
                "_id", id
                );
            var document = await collection.Find(filter).SingleOrDefaultAsync();

            if (document != null)
            { 
            var strictJson = document.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.CanonicalExtendedJson});

                return strictJson;
            }
            return null;
        }

        public IMongoQueryable<T> GetDocuments<T>(string collectionName)
        {
            return GetMongoCollection<T>(collectionName).AsQueryable();
        }

        public void InsertDocument(BsonDocument document, string collectionName)
        {
            var collection = GetMongoCollection<BsonDocument>(collectionName);
            if (collection == null)
            {
                throw new NullReferenceException("Cannot insert document into MongoDB.");
            }
            collection.InsertOne(document);
        }

        public void InsertMany(List<BsonDocument> documents, string collectionName)
        {
            var collection = GetMongoCollection<BsonDocument>(collectionName);
            if (collection == null)
            {
                throw new NullReferenceException("Cannot insert document into MongoDB.");
            }
            collection.InsertMany(documents);
        }

        public void InsertDocument(string document, string collectionName)
        {
            var documentBson = BsonSerializer.Deserialize<BsonDocument>(document);
            InsertDocument(documentBson, collectionName);
        }
        public void Dispose()
        {
            runner?.Dispose();
        }
    }
}