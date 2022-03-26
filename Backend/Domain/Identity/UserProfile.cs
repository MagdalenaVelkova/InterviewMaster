using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace InterviewMaster.Domain.Identity
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<string> FavouriteQuestionsIds { get; set; }

        public IEnumerable<string> UserSolutionIds { get; set; }
}
}
