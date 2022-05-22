using InterviewMaster.Domain.Identity;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services
{
    public interface IUserProfileService
    {
        public Task<string> CreateUser(UserProfile user);

        public UserProfile? GetUser(string userId);

        public UserProfile? GetUserProfile(string email, string password);

        public Task<string>? AddToFavourite(string questionId, string userId);

        public bool UserExists(string id);
    }
}
