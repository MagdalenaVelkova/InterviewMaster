using InterviewMaster.Domain.Identity;
using InterviewMaster.Domain.InterviewPractice;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services
{
    public interface IUserSolutionsRepository
    {
        public Task<string> CreateOneOrUpdate(UserSolution userSolution);
        public UserSolution? GetUserSolutionById(string id);

        public UserSolution? GetUserSolutionByUserAndQuestion(string id, string userId);
    }
}
