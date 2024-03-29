﻿using InterviewMaster.Persistence.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Persistence.Models
{
    [ExcludeFromCodeCoverage]
    [BsonCollection("Questions")]
    public class InterviewQuestionDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonElement("question")]
        public string Question { get; set; }

        [BsonElement("topic")]
        public string Topic { get; set; }

        [BsonElement("prompts")]
        public IEnumerable<string> Prompts { get; set; }

        [BsonElement("exampleAnswers")]
        public IEnumerable<string> ExampleAnswers { get; set; }
    }
}
