using InterviewMaster.Domain.Identity;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services
{
    public interface IUserProfileService
    {
        public Task<string> CreateUser(UserProfile user);

        public UserProfile? GetUser(string userId);

        public string GetUserId(Credentials credentials);

        public Task<string>? AddToFavourite(string questionId, string userId);
        public Task<string>? RemoveFromFavourite(string questionId, string userId);

        public bool UserExists(string id);
    }
}
