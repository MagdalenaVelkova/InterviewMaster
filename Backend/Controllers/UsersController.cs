using InterviewMaster.Application.Services;
using InterviewMaster.Controllers.DTOs;
using InterviewMaster.Domain.Identity;
using InterviewMaster.Domain.InterviewPreparation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewMaster.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IQuestionsRespository questionsRespository;
        private readonly IUserSolutionsRepository userSolutionsRepository;
        private readonly IdentityService identityService;
        private readonly IIdentityRepository identityRepository;

        public UsersController(IUserProfileRepository userProfileRepository, IQuestionsRespository questionsRespository, IUserSolutionsRepository userSolutionsRepository, IdentityService identityService, IIdentityRepository identityRepository)
        {
            this.userProfileRepository = userProfileRepository;
            this.questionsRespository = questionsRespository;
            this.userSolutionsRepository = userSolutionsRepository;
            this.identityService = identityService;
            this.identityRepository = identityRepository;

        }

        private string getToken()
        {
            StringValues bearerToken;
            var tokenExists = HttpContext.Request.Headers.TryGetValue("Authorization", out bearerToken);

            if (tokenExists)
            {
                var token = bearerToken.ToString().Split(" ")[1];

                identityService.GetUserIdFromToken(token.ToString());
                return token;
            }
            return null;
        }

        [HttpGet("/authorised")]
        public IActionResult GetIsAuthorised()
        {
            return Ok();
        }

        [HttpGet("/userprofile")]
        public IActionResult GetUserProfile()
        {
            var token = getToken();
            var id = identityService.GetUserIdFromToken(token.ToString());
            var userProfile = userProfileRepository.GetUser(id);
            if (userProfile != null)
            {
                return Ok(userProfile);
            }

            return NotFound();
        }

        [HttpGet("/favourites")]
        public IActionResult GetFavouriteQuestions()
        {
            var token = getToken();
            var id = identityService.GetUserIdFromToken(token.ToString());
            var userProfile = userProfileRepository.GetUser(id);
            var questions = new List<InterviewQuestion>();
            if (userProfile != null)
            {
                foreach (var questionId in userProfile.FavouriteQuestionsIds)
                {
                    var question = questionsRespository.GetQuestion(questionId);
                    questions.Add(question);
                }
                return Ok(questions);
            }
            return NotFound();
        }

        [HttpGet("/solutions")]
        public IActionResult GetRespondedQuestions()
        {
            var token = getToken();
            var userId = identityService.GetUserIdFromToken(token.ToString());
            var userProfile = userProfileRepository.GetUser(userId);
            var questions = new List<InterviewQuestion>();
            if (userProfile != null)
            {
                foreach (var userSolutionId in userProfile.UserSolutionIds)
                {
                    var userSolution = userSolutionsRepository.GetUserSolutionById(userSolutionId);
                    var question = questionsRespository.GetQuestion(userSolution.InterviewQuestionId);
                    if (question != null)
                    {
                        questions.Add(question);
                    }
                }
                return Ok(questions);
            }
            return NotFound();
        }

        [HttpPost("addfavourite/{questionId}")]
        public async Task<IActionResult> AddFavourite(string questionId)
        {
            var token = getToken();
            var userId = identityService.GetUserIdFromToken(token.ToString());
            if (userProfileRepository.UserExists(userId) && questionsRespository.QuestionExists(questionId))
            {
                var result = await userProfileRepository.AddQuestionToFavourite(questionId, userId);
                if (result != null)
                {
                    return Ok();
                }
                return NotFound();
            }

            return BadRequest();

        }


        [HttpPost("removefavourite/{questionId}")]
        public async Task<IActionResult> RemoveFavourite(string questionId)
        {
            var token = getToken();
            var userId = identityService.GetUserIdFromToken(token.ToString());
            if (userProfileRepository.UserExists(userId) && questionsRespository.QuestionExists(questionId))
            {
                var result = await userProfileRepository.RemoveQuestionFromFavourite(questionId, userId);
                if (result != null)
                {
                    return Ok();
                }
                return NotFound();
            }

            return BadRequest();

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var emailExists = identityRepository.UserEmailExists(userRegisterDTO.Email);
            if (!emailExists)
            {
                var credentials = new Credentials()
                {
                    Email = userRegisterDTO.Email,
                    Password = userRegisterDTO.Password,
                };

                var userIdentity = identityService.GenerateUserIdentityFromCredentials(credentials);

                // transaction here? 
                var userId = await identityRepository.CreateIdentity(userIdentity);

                if (userId != null)
                {
                    var userProfile = new UserProfile()
                    {
                        UserId = userId,
                        FirstName = userRegisterDTO.FirstName,
                        LastName = userRegisterDTO.LastName,
                    };

                    await userProfileRepository.CreateUser(userProfile);

                    return Ok(userId);
                }
            }

            return BadRequest();

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var credentials = new Credentials
            {
                Email = userLoginDTO.Email,
                Password = userLoginDTO.Password,
            };
            var token = identityService.Authenticate(credentials);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
