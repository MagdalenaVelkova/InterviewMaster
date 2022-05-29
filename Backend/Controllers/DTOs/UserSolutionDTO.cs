﻿using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Controllers.DTOs
{
    [ExcludeFromCodeCoverage]
    public class UserSolutionDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string InterviewQuestionId { get; set; }
        public string Response { get; set; }
    }
}
