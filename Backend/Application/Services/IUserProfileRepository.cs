using InterviewMaster.Domain.Identity;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services
{
    public interface IUserProfileRepository
    {
        public Task<string> CreateUser(UserProfile user);
        public UserProfile? GetUser(string userId);
        public Task<string>? AddQuestionToFavourite(string questionId, string userId);
        public Task<string>? RemoveQuestionFromFavourite(string questionId, string userId);
        public bool UserExists(string id);
    }
}
