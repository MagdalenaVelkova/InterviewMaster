using System.Collections.Generic;

namespace InterviewMaster.Domain.Identity
{
    public class UserProfile
    {

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> FavouriteQuestionsIds { get => favouriteQuestionsIds; set => favouriteQuestionsIds = value; }
        public IEnumerable<string> UserSolutionIds { get => userSolutionIds; set => userSolutionIds = value; }

        private IEnumerable<string> userSolutionIds = new List<string>();

        private IEnumerable<string> favouriteQuestionsIds = new List<string>();
    }

}
