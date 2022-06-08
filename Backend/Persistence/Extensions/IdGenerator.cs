using MongoDB.Bson;

namespace InterviewMaster.Persistence.Extensions
{
    public class IdGenerator : IIdGenerator
    {
        public string Generate()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
