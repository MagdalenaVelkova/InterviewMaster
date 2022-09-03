using InterviewMaster.Domain.InterviewPractice.ValueObjects;
using System;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.InterviewPractice
{
    [ExcludeFromCodeCoverage]
    public class UserSolution
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string InterviewQuestionId { get; set; }
        public Response Response { get; set; }
    }
}
