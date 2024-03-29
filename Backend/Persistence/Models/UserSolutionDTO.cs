﻿using InterviewMaster.Domain.InterviewPractice.ValueObjects;
using InterviewMaster.Persistence.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Persistence.Models
{
    [ExcludeFromCodeCoverage]
    [BsonCollection("UserSolutions")]
    public class UserSolutionDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("interviewQuestionId")]
        public string InterviewQuestionId { get; set; }

        [BsonElement("response")]
        public Response Response { get; set; }

    }
}
