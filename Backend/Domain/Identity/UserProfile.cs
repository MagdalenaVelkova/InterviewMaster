using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.Identity
{
    [ExcludeFromCodeCoverage]
    public class UserProfile
    {
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string LastName { get; set; }
        public IEnumerable<string> FavouriteQuestionsIds { get => favouriteQuestionsIds; set => favouriteQuestionsIds = value; }
        public IEnumerable<string> UserSolutionIds { get => userSolutionIds; set => userSolutionIds = value; }

        private IEnumerable<string> userSolutionIds = new List<string>();

        private IEnumerable<string> favouriteQuestionsIds = new List<string>();
    }

}
