using InterviewMaster.Domain.Identity;
using InterviewMaster.Domain.InterviewPreparation;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services
{
    public interface IUserSolutionsService
    {
        public Task<string> CreateOneOrUpdate(UserSolution userSolution);
        public UserSolution? GetUserSolutionById(string id);

        public UserSolution? GetUserSolutionByUserAndQuestion(string id, string userId);
    }
}
