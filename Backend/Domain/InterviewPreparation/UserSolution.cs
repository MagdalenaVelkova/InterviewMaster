using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using System;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.InterviewPreparation
{
    [ExcludeFromCodeCoverage]
    public class UserSolution
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string InterviewQuestionId { get; set; }
        public Response Response { get; set; }
    }

    // add response

    //create constructor and updaTE DATE FOR EACH RESPONSE 
}
