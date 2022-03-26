using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using System;

namespace InterviewMaster.Domain.InterviewPreparation
{
    class UserSolution
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InterviewQuestionId { get; set; }
        public Response Response { get; set; }
        public DateTime Date { get; set; }
    }
}
