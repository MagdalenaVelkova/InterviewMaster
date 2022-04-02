using InterviewMaster.Application.Services;
using InterviewMaster.Domain.Identity;
using InterviewMaster.Persistance.Extensions;
using InterviewMaster.Persistance.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewMaster.Persistance.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfileDTO>, IUserProfileService
    {
        private readonly IIdGenerator idGenerator;

        public UserProfileRepository(IMongoDatabase database, IIdGenerator idGenerator) : base(database)
        {
            this.idGenerator = idGenerator;
        }
        public override string DbCollectionName => "UserProfile";

        // create user
        public async Task<string> CreateUser(UserProfile user)
        {
            var entity = new UserProfileDTO
            {
                Id = idGenerator.Generate(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                FavouriteQuestions = user.FavouriteQuestionsIds,
                UserSolutions = user.UserSolutionIds
            };

            await Collection.InsertOneAsync(entity);

            return entity.Id;
        }

        // find user
        public UserProfile? GetUser(string userId)
        {
            return Query().Where(x => x.Id == userId).Select(x => new UserProfile()
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FavouriteQuestionsIds = x.FavouriteQuestions,
                UserSolutionIds = x.UserSolutions
            }).FirstOrDefault();
        }



        //update user

        // deleteuser
    }
}
