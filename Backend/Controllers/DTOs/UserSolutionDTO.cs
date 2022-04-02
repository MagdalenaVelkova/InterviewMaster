using InterviewMaster.Domain.InterviewPreparation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewMaster.Controllers.DTOs
{
    public class UserSolutionDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string InterviewQuestionId { get; set; }
        public string Response { get; set; }
    }
}
