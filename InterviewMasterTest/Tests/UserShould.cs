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
using InterviewMaster.Persistance.Repositories;
using InterviewMaster.Domain.Identity;
using InterviewMaster.Persistance.Extensions;
using InterviewMaster.Persistance.Models;
using System.Linq;
using Snapshooter.Xunit;

namespace InterviewMaster.Test.Tests
{
    public class UserShould : TestBase
    {
        private UsersController usersController;
        private TestIdGenerator idGenerator;
        private UserProfileRepository userProfileRepository;
        private QuestionsRepository questionsRepository;
        private UserSolutionsRepository userSolutionsRepository;
        private IdentityRepository userIdentityRepository;
        private IdentityService identityService;

        public UserShould()
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
            userIdentityRepository = new IdentityRepository(MongoDbService.MongoDatabase, idGenerator);

            identityService = new IdentityService(userIdentityRepository, fakeConfiguration);

            usersController = new UsersController(
                userProfileRepository,
                questionsRepository,
                userSolutionsRepository,
                identityService,
                userIdentityRepository);

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
            usersController.ControllerContext = new ControllerContext();
            usersController.ControllerContext.HttpContext = new DefaultHttpContext();
            usersController.ControllerContext.HttpContext.Request.Headers["Authorization"] = string.Concat("Bearer ", token);
        }

        [Fact]
        public async Task Ba_Able_To_RegisterAsync()
        {
            //arrange
            var userInfo = new UserRegisterDTO()
            {
                FirstName = "Test Name",
                LastName = "Test Surname",
                Email = "test@email.com",
                Password = "TestPassword"
            };

            //act
            var resultObjectRegister = await usersController.Register(userInfo) as OkObjectResult;

            var identityCollection = MongoDbService.GetDocuments<UserIdentityDTO>(IdentityRepository.CollectionName).ToList();
            var profileCollection = MongoDbService.GetDocuments<UserProfileDTO>(UserProfileRepository.CollectionName).ToList();

            //assert
            Assert.NotNull(resultObjectRegister);
            Assert.Single(identityCollection);
            profileCollection.MatchSnapshot("UserShould.Ba_Able_To_RegisterAsync.Profile");
        }

        [Fact]
        public async Task Get_Token_When_Logging_InAsync()
        {
            //arrange
            var userRegisterDTO = new UserRegisterDTO()
            {
                FirstName = "Test Name",
                LastName = "Test Surname",
                Email = "test@email.com",
                Password = "TestPassword"
            };

            var userLoginDTO = new UserLoginDTO()
            {
                Email = "test@email.com",
                Password = "TestPassword"
            };

            var resultObjectRegister = await usersController.Register(userRegisterDTO);
            Assert.IsType<OkObjectResult>(resultObjectRegister);

            //act
            var resultObjectLogin = usersController.Login(userLoginDTO) as OkObjectResult;

            //assert
            Assert.NotNull(resultObjectLogin);

            var token = resultObjectLogin.Value.ToString();

            Assert.NotNull(token);
            var isValid = await identityService.isAuthorised(token);
            Assert.True(isValid);
        }

        [Fact]
        public void Not_Login_If_Credentials_Are_Incorrect()
        {
            //arrange
            var userLoginDTO = new UserLoginDTO()
            {
                Email = "InvalidEmail",
                Password = "InvalidPassword"
            };
            //act
            var resultObjectLogin = usersController.Login(userLoginDTO);
            var identityCollection = MongoDbService.GetDocuments<UserIdentityDTO>(IdentityRepository.CollectionName).ToList();
            var profileCollection = MongoDbService.GetDocuments<UserProfileDTO>(UserProfileRepository.CollectionName).ToList();
            //assert
            Assert.IsType<UnauthorizedResult>(resultObjectLogin);
            Assert.Empty(identityCollection);
            Assert.Empty(profileCollection);
        }

        [Fact]
        public async Task Provide_A_Unique_EmailAsync()
        {
            //arrange
            var userOne = new UserRegisterDTO()
            {
                FirstName = "Test Name",
                LastName = "Test Surname",
                Email = "test@email.com",
                Password = "TestPassword"
            };
            var prerequisiteRegisterResult = await usersController.Register(userOne) as OkObjectResult;

            var duplicatedEmailUser = new UserRegisterDTO()
            {
                FirstName = "Test Name Two",
                LastName = "Test Surname Two",
                Email = "test@email.com",
                Password = "TestPasswordTwo"
            };
            //act
            var resultObjectRegister = await usersController.Register(duplicatedEmailUser);
            var identityCollection = MongoDbService.GetDocuments<UserIdentityDTO>(IdentityRepository.CollectionName).ToList();
            var profileCollection = MongoDbService.GetDocuments<UserProfileDTO>(UserProfileRepository.CollectionName).ToList();

            //assert
            Assert.IsType<OkObjectResult>(prerequisiteRegisterResult);
            Assert.IsType<BadRequestResult>(resultObjectRegister);
            Assert.Single(identityCollection);
            profileCollection.MatchSnapshot("UserShould.Provide_A_Unique_EmailAsync.Profile");
        }

        [Fact]
        public async Task Be_Able_To_Favourite_A_Question()
        {
            //arrange

            var questionId = "61746566a01a5e8e03b788e0";
            var questionsInDb = InterviewQuestionTestData.GenerateMultipleQuestions();
            questionsInDb[1].Id = questionId;
            MongoDbService.InsertMany(questionsInDb.Select(x => x.ToBsonDocument()).ToList(), QuestionsRepository.CollectionName);

            var userId = ObjectId.GenerateNewId().ToString();
            CreateAndAuthoriseUserForTest(userId);

            //act
            var resultObject = await usersController.AddFavourite(questionId);

            //assert
            Assert.IsType<OkResult>(resultObject);
            var updatedUserProfile = userProfileRepository.GetUser(userId);
            Assert.Contains(questionId, updatedUserProfile.FavouriteQuestionsIds);

        }

        [Fact]
        public async Task Be_Able_To_Remove_Question_From_Favourites()
        {
            //arrange
            var question = InterviewQuestionTestData.GenerateValidTestQuestionTwo();
            var questionId = "61746566a01a5e8e03b788e0";
            question.Id = questionId;

            MongoDbService.InsertDocument(question.ToBsonDocument(), QuestionsRepository.CollectionName);

            var userId = ObjectId.GenerateNewId().ToString();
            CreateAndAuthoriseUserForTest(userId);

            await userProfileRepository.AddQuestionToFavourite(questionId, userId);
            var userProfile = userProfileRepository.GetUser(userId);

            Assert.Contains(questionId, userProfile.FavouriteQuestionsIds);
            //act
            var resultObject = await usersController.RemoveFavourite("61746566a01a5e8e03b788e0") as OkResult;

            //assert
            Assert.NotNull(resultObject);
            var updatedUserProfile = userProfileRepository.GetUser(userId);
            Assert.DoesNotContain(questionId, updatedUserProfile.FavouriteQuestionsIds);


        }
    }
}
