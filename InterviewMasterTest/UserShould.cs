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

namespace InterviewMaster.Test
{
    public class UserShould
    {
        private UsersController usersController;
        private FakeUserProfileRepository fakeUserProfileRepository;
        private FakeQuestionsRepository fakeQuestionsRepository;
        private FakeUserSolutionsRepository fakeUserSolutionsRepository;
        private FakeUserIdentityRepository fakeUserIdentityRepository;
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

            
            fakeUserProfileRepository = new FakeUserProfileRepository();
            fakeQuestionsRepository = new FakeQuestionsRepository();
            fakeUserSolutionsRepository = new FakeUserSolutionsRepository();
            fakeUserIdentityRepository = new FakeUserIdentityRepository();

            identityService = new IdentityService(fakeUserIdentityRepository, fakeConfiguration);

            usersController = new UsersController(
                fakeUserProfileRepository,
                fakeQuestionsRepository,
                fakeUserSolutionsRepository,
                identityService,
                fakeUserIdentityRepository);
        }

        private void CreateAndAuthoriseUserForTest(string userId)
        {
            var userProfile = UserProfileTestData.GenerateValidTestUserProfileOne();
            userProfile.UserId = userId;

            var userIdentity = UserIdentityTestData.GenerateValidTestUserIdentityOne();
            userIdentity.Id = userId;

            fakeUserProfileRepository.AddOne(userProfile);
            fakeUserIdentityRepository.AddOne(userIdentity);

            var token = identityService.GenerateToken(userIdentity); 
            usersController.ControllerContext = new ControllerContext();
            usersController.ControllerContext.HttpContext = new DefaultHttpContext();
            usersController.ControllerContext.HttpContext.Request.Headers["Authorization"] = string.Concat("Bearer ",token);
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
            
            //assert
            Assert.NotNull(resultObjectRegister);
        }

        //get a token when logging in 
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

            // how to validate token
            var token = resultObjectLogin.Value.ToString();

            var validationParameters = new TokenValidationParameters();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, validationParameters);
            
            Assert.NotNull(token);
            Assert.True(tokenValidationResult.IsValid);
            


        }
        //get unauthorized when inputing incorrect credentials
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
            //assert
            Assert.IsType<UnauthorizedResult>(resultObjectLogin);
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


            //assert
            Assert.IsType<OkObjectResult>(prerequisiteRegisterResult);
            Assert.IsType<BadRequestResult>(resultObjectRegister);

        }

        [Fact]
        public async Task Be_Able_To_Favourite_A_Question()
        {
            //arrange
            var question = InterviewQuestionTestData.GenerateValidTestQuestionOne();
            question.Id = "61746566a01a5e8e03b788e0";
            
            fakeQuestionsRepository.AddOne(question);

            var userId = ObjectId.GenerateNewId().ToString();
            CreateAndAuthoriseUserForTest(userId);

            //act
            var resultObject = await usersController.AddFavourite("61746566a01a5e8e03b788e0") as OkResult;

            //assert
            Assert.NotNull(resultObject);
        }

        [Fact]
        public async Task Be_Able_To_Remove_Question_From_Favourites()
        {
            //arrange
            var question = InterviewQuestionTestData.GenerateValidTestQuestionOne();
            var questionId = "61746566a01a5e8e03b788e0";
            question.Id = questionId;

            fakeQuestionsRepository.AddOne(question);

            var userId = ObjectId.GenerateNewId().ToString();
            CreateAndAuthoriseUserForTest(userId);

            await fakeUserProfileRepository.AddQuestionToFavourite(questionId, userId);

            //act
            var resultObject = await usersController.RemoveFavourite("61746566a01a5e8e03b788e0") as OkResult;

            //assert
            Assert.NotNull(resultObject);
        }
    }
}
