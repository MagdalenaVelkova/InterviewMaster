using InterviewMaster.Domain.Identity;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services
{
    public interface IUserProfileService
    {
        public Task<UserProfile> CreateUser(UserProfile user);

        public UserProfile? GetUser(string userId);
    }
}
