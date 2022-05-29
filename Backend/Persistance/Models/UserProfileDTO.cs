using InterviewMaster.Persistance.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Persistance.Models
{
    [ExcludeFromCodeCoverage]
    [BsonCollection("UserProfiles")]
    public class UserProfileDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonRequired]
        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("favouriteQuestions")]
        public IEnumerable<string> FavouriteQuestions { get; set; }

        [BsonElement("userSolutions")]
        public IEnumerable<string> UserSolutions { get; set; }
    }
}
