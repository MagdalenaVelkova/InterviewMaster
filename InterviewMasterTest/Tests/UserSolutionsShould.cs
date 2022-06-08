using InterviewMaster.Application.Services;
using InterviewMaster.Test.Util;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using InterviewMaster.Test.TestData;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using InterviewMaster.Controllers;
using InterviewMaster.Controllers.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using InterviewMaster.Persistence.Repositories;
using InterviewMaster.Domain.Identity;
using InterviewMaster.Persistence.Extensions;
using InterviewMaster.Persistence.Models;
using System.Linq;
using Snapshooter.Xunit;

namespace InterviewMaster.Test.Tests
{
    public class UserSolutionsShould : TestBase
    {
        private readonly UserProfileRepository userProfileRepository;
        private readonly QuestionsRepository questionsRepository;
        private readonly UserSolutionsRepository userSolutionsRepository;
        private readonly TestIdGenerator idGenerator;
        private IdentityService identityService;
        private readonly UserSolutionsController userSolutionsController;

        public UserSolutionsShould()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "VrFPOluCe4zkUnnFA8Q5gIxSh6T7u6MO"},
                {"Jwt:Issuer", "InterviewMaster"},
            };

            IConfiguration fakeConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            idGenerator = AppInTest.GetService<TestIdGenerator>();
            userProfileRepository = new UserProfileRepository(MongoDbService.MongoDatabase, idGenerator);
            questionsRepository = new QuestionsRepository(MongoDbService.MongoDatabase, idGenerator);
            userSolutionsRepository = new UserSolutionsRepository(MongoDbService.MongoDatabase, idGenerator);
            var userIdentityRepository = new IdentityRepository(MongoDbService.MongoDatabase, idGenerator);


            identityService = new IdentityService(userIdentityRepository, fakeConfiguration);

            userSolutionsController = new UserSolutionsController(userSolutionsRepository, identityService,userProfileRepository);

            idGenerator.Set("61746566a01a5e8e03b788e0");
        }

        private void CreateAndAuthoriseUserForTest(string userId)
        {
            var userProfileDTO = UserProfileTestData.GenerateValidTestUserProfileOne();
            userProfileDTO.Id = userId;

            var userIdentityDTO = UserIdentityTestData.GenerateValidTestUserIdentityOne();
            userIdentityDTO.Id = userId;

            MongoDbService.InsertDocument(userProfileDTO.ToBsonDocument(), UserProfileRepository.CollectionName);
            MongoDbService.InsertDocument(userIdentityDTO.ToBsonDocument(), IdentityRepository.CollectionName);

            var userIdentity = new UserAuth()
            {
                Id = userIdentityDTO.Id,
                Email = userIdentityDTO.Email,
                PasswordHash = userIdentityDTO.PasswordHash,
                PasswordSalt = userIdentityDTO.PasswordSalt
            };

            var token = identityService.GenerateToken(userIdentity);
            userSolutionsController.ControllerContext = new ControllerContext();
            userSolutionsController.ControllerContext.HttpContext = new DefaultHttpContext();
            userSolutionsController.ControllerContext.HttpContext.Request.Headers["Authorization"] = string.Concat("Bearer ", token);
        }
        //have question id and user id
        [Fact]
        public async Task Be_SavedAsync()
        {
            //arrange
            var userId = idGenerator.Generate();
            CreateAndAuthoriseUserForTest(userId);

            var question = InterviewQuestionTestData.GenerateValidTestQuestionTwo();
            MongoDbService.InsertDocument(question.ToBsonDocument(), QuestionsRepository.CollectionName);

            var userSolution = new Controllers.DTOs.UserSolutionDTO()
            {
                InterviewQuestionId = question.Id,
                Response = "This is a test response object"
            };
            //act
            var resultObject = await userSolutionsController.PostSolution(userSolution) as OkObjectResult;
            //assert
            Assert.NotNull(resultObject);
            var userSolutionId = resultObject.Value.ToString();
            userSolutionsRepository.GetUserSolutionById(userSolutionId).MatchSnapshot();
            var userSolutionIdsForProfile = userProfileRepository.GetUser(userId).UserSolutionIds;
            Assert.True(userSolutionIdsForProfile.Contains(question.Id));
        }

        [Fact]
        public async Task Be_Fetched_By_Question_IdAsync()
        {
            //arrange
            var userId = idGenerator.Generate();
            CreateAndAuthoriseUserForTest(userId);

            var question = InterviewQuestionTestData.GenerateValidTestQuestionTwo();
            MongoDbService.InsertDocument(question.ToBsonDocument(), QuestionsRepository.CollectionName);

            var userSolution = new Controllers.DTOs.UserSolutionDTO()
            {
                InterviewQuestionId = question.Id,
                Response = "This is a test response object"
            };
            var addSolutionResultObject = await userSolutionsController.PostSolution(userSolution) as OkObjectResult;
            Assert.NotNull(addSolutionResultObject);


            //act
            var resultObject =  userSolutionsController.GetByQuestion(question.Id) as OkObjectResult ;
            //assert
            Assert.NotNull(resultObject);
            resultObject.Value.MatchSnapshot();

        }
    }
}
