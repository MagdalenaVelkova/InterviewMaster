using InterviewMaster.Persistence.Extensions;
using MongoDB.Bson;

namespace InterviewMaster.Test.Util
{
    public  class TestIdGenerator : IIdGenerator
    {
        public string Id { get; set; }
        public string Generate()
        {
            if (Id == "")
            {
                Id = ObjectId.GenerateNewId().ToString();
                return Id;
            }
            else 
            {
            return Id;
            }
        }

        public void Set(string id)
        {
            Id = id;
        }

        public void Clear()
        {
            Id = "";
        }
    }
}