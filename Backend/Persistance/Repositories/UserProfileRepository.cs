using InterviewMaster.Application.Services;
using InterviewMaster.Domain.Identity;
using InterviewMaster.Persistance.Extensions;
using InterviewMaster.Persistance.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewMaster.Persistance.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfileDTO>, IUserProfileRepository
    {
        private readonly IIdGenerator idGenerator;

        public UserProfileRepository(IMongoDatabase database, IIdGenerator idGenerator) : base(database)
        {
            this.idGenerator = idGenerator;
        }
        public override string DbCollectionName => "UserProfile";

        // create user profile
        public async Task<string> CreateUser(UserProfile user)
        {
            var entity = new UserProfileDTO
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FavouriteQuestions = new List<string>(),
                UserSolutions = new List<string>()
            };

            await Collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public bool UserExists(string id)
        {
            return Query().Any(x => x.Id == id);
        }

        // find user profile
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
        public async Task<string>? AddQuestionToFavourite(string questionId, string userId)
        {

            var filterProifile = Builders<UserProfileDTO>.Filter.Eq(x => x.Id, userId);
            var updateFavourite = Builders<UserProfileDTO>.Update.AddToSet(x => x.FavouriteQuestions, questionId);

            var result = await Collection.FindOneAndUpdateAsync<UserProfileDTO>(filterProifile, updateFavourite);

            if (result == null)
            {
                return null;

            }
            return questionId;
        }
        public async Task<string> RemoveQuestionFromFavourite(string questionId, string userId)
        {
            {

                var filterProfile = Builders<UserProfileDTO>.Filter.Eq(x => x.Id, userId);
                var updateFavourite = Builders<UserProfileDTO>.Update.Pull(x => x.FavouriteQuestions, questionId);

                var result = await Collection.FindOneAndUpdateAsync<UserProfileDTO>(filterProfile, updateFavourite);

                if (result == null)
                {
                    return null;

                }
                return questionId;
            }
        }

        public async Task<string> AddQuestionToSolved(string questionId, string userId)
        {
            var filterProifile = Builders<UserProfileDTO>.Filter.Eq(x => x.Id, userId);
            var updateFavourite = Builders<UserProfileDTO>.Update.AddToSet(x => x.UserSolutions, questionId);

            var result = await Collection.FindOneAndUpdateAsync<UserProfileDTO>(filterProifile, updateFavourite);

            if (result == null)
            {
                return null;

            }
            return questionId;
        }
    }
}
