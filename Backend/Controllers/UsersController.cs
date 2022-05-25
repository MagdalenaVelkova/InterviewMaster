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

namespace Application.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProfileService userProfileService;
        private readonly IQuestionsService questionsService;
        private readonly IUserSolutionsService userSolutionsService;
        private readonly IdentityService identityService;

        public UsersController(IUserProfileService userProfileService, IQuestionsService questionsService, IUserSolutionsService userSolutionsService, IdentityService identityService)
        {
            this.userProfileService = userProfileService;
            this.questionsService = questionsService;
            this.userSolutionsService = userSolutionsService;
            this.identityService = identityService;

        }

        private string getToken()
        {
            StringValues bearerToken;
            var tokenExists = HttpContext.Request.Headers.TryGetValue("Authorization", out bearerToken);

            if (tokenExists)
            {
                var token = bearerToken.ToString().Split(" ")[1];

                var id = identityService.GetUserIdFromToken(token.ToString());
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
            var userProfile = userProfileService.GetUser(id);
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
            var userProfile = userProfileService.GetUser(id);
            var questions = new List<InterviewQuestion>();
            if (userProfile != null)
            {
                foreach (var questionId in userProfile.FavouriteQuestionsIds)
                {
                    var question = questionsService.GetQuestion(questionId);
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
            var userProfile = userProfileService.GetUser(userId);
            var questions = new List<InterviewQuestion>();
            if (userProfile != null)
            {
                foreach (var userSolutionId in userProfile.UserSolutionIds)
                {
                    var userSolution = userSolutionsService.GetUserSolutionById(userSolutionId);
                    var question = questionsService.GetQuestion(userSolution.InterviewQuestionId);
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
            if (userProfileService.UserExists(userId) && questionsService.QuestionExists(questionId))
            {
                var result = await userProfileService.AddToFavourite(questionId, userId);
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
            if (userProfileService.UserExists(userId) && questionsService.QuestionExists(questionId))
            {
                var result = await userProfileService.RemoveFromFavourite(questionId, userId);
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
            var userProfile = new UserProfile()
            {
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Email = userRegisterDTO.Email,
                PasswordHash = UserProfile.HashPassword(userRegisterDTO.Password),
            };
            var userId = await userProfileService.CreateUser(userProfile);

            return Ok(userId);
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
