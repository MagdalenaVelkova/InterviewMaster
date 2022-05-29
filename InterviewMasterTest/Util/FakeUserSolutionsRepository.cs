using InterviewMaster.Application.Services;
using InterviewMaster.Domain.InterviewPreparation;
using System.Threading.Tasks;

namespace InterviewMaster.Test.Util
{
    public class FakeUserSolutionsRepository : IUserSolutionsRepository
    {
        public FakeUserSolutionsRepository()
        {
        }

        public Task<string> CreateOneOrUpdate(UserSolution userSolution)
        {
            throw new System.NotImplementedException();
        }

        public UserSolution GetUserSolutionById(string id)
        {
            throw new System.NotImplementedException();
        }

        public UserSolution GetUserSolutionByUserAndQuestion(string id, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}