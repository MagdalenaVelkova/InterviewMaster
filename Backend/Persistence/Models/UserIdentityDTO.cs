using InterviewMaster.Persistence.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Persistence.Models
{
    [ExcludeFromCodeCoverage]
    [BsonCollection("Identity")]
    public class UserIdentityDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonRequired]
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

        [BsonRequired]
        [BsonElement("passwordSalt")]
        public string PasswordSalt { get; set; }

    }
}
