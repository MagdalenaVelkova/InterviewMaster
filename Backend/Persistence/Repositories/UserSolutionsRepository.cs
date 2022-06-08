using InterviewMaster.Application.Services;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using InterviewMaster.Persistence.Extensions;
using InterviewMaster.Persistence.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewMaster.Persistence.Repositories
{
    public class UserSolutionsRepository : BaseRepository<UserSolutionDTO>, IUserSolutionsRepository
    {
        private readonly IIdGenerator idGenerator;

        public UserSolutionsRepository(IMongoDatabase database, IIdGenerator idGenerator) : base(database)
        {
            this.idGenerator = idGenerator;
        }
        public const string CollectionName = "UserSolutions";
        public override string DbCollectionName => CollectionName;


        public UserSolution GetUserSolutionById(string id)
        {
            return Query().Where(dto => dto.Id == id).Select(dto => new UserSolution
            {
                Id = dto.Id,
                UserId = dto.UserId,
                InterviewQuestionId = dto.InterviewQuestionId,
                Response = dto.Response,
            }).FirstOrDefault();
        }

        public UserSolution GetUserSolutionByUserAndQuestion(string questionId, string userId)
        {
            return Query().Where(dto => dto.InterviewQuestionId == questionId && dto.UserId == userId).Select(dto => new UserSolution
            {
                Id = dto.Id,
                UserId = dto.UserId,
                InterviewQuestionId = dto.InterviewQuestionId,
                Response = dto.Response,
            }).FirstOrDefault();
        }

        public async Task<string> CreateOneOrUpdate(UserSolution userSolution)
        {
            var existingSolution = GetUserSolutionByUserAndQuestion(userSolution.InterviewQuestionId, userSolution.UserId);
            UserSolutionDTO entity;
            if (existingSolution!=null)
            {
                entity = new UserSolutionDTO
                {
                    Id = existingSolution.Id,
                    UserId = existingSolution.UserId,
                    InterviewQuestionId = existingSolution.InterviewQuestionId,
                    Response = userSolution.Response,
                };

                await Collection.FindOneAndReplaceAsync(x => x.Id == existingSolution.Id, entity);
            }
            else
            {
                entity = new UserSolutionDTO
                {
                    Id = idGenerator.Generate(),
                    UserId = userSolution.UserId,
                    InterviewQuestionId = userSolution.InterviewQuestionId,
                    Response = userSolution.Response,
                };

                await Collection.InsertOneAsync(entity);
            }
           

            return entity.Id;
        }


        //get all question


        //get question by id

    }
}
