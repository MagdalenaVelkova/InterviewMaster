using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Controllers.DTOs
{
    [ExcludeFromCodeCoverage]
    public class UserSolutionDTO
    {
        public string InterviewQuestionId { get; set; }
        public string Response { get; set; }
    }
}
