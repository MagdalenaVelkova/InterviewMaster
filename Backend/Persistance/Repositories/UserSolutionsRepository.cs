﻿using InterviewMaster.Application.Services;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using InterviewMaster.Persistance.Extensions;
using InterviewMaster.Persistance.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewMaster.Persistance.Repositories
{
    public class UserSolutionsRepository : BaseRepository<UserSolutionDTO>, IUserSolutionsService
    {
        private readonly IIdGenerator idGenerator;

        public UserSolutionsRepository(IMongoDatabase database, IIdGenerator idGenerator) : base(database)
        {
            this.idGenerator = idGenerator;
        }
        public override string DbCollectionName => "UserSolutions";



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

        public async Task<string> PostSolution(UserSolution userSolution)
        {
            var entity = new UserSolutionDTO
            {
                Id = idGenerator.Generate(),
                UserId = userSolution.UserId,
                InterviewQuestionId = userSolution.InterviewQuestionId,
                Response = userSolution.Response,
            };

            await Collection.InsertOneAsync(entity);

            return entity.Id;
        }


        //get all question


        //get question by id

    }
}