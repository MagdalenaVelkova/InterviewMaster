using MongoDB.Bson;

namespace InterviewMaster.Persistance.Extensions
{
    public class IdGenerator : IIdGenerator
    {
        public string Generate()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
