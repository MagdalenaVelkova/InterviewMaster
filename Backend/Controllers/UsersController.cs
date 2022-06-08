using InterviewMaster.Application.Services;
using InterviewMaster.Controllers.DTOs;
using InterviewMaster.Domain.Identity;
using InterviewMaster.Domain.InterviewPreparation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IQuestionsRepository questionsRepository;
        private readonly IdentityService identityService;
        private readonly IIdentityRepository identityRepository;

        public UsersController(IUserProfileRepository userProfileRepository, IQuestionsRepository questionsRepository, IdentityService identityService, IIdentityRepository identityRepository)
        {
            this.userProfileRepository = userProfileRepository;
            this.questionsRepository = questionsRepository;
            this.identityService = identityService;
            this.identityRepository = identityRepository;

        }


        [HttpGet("/authorised")]
        public async Task<IActionResult> GetIsAuthorised()
        {
            var token = identityService.getToken(HttpContext);
            if (token != null)
            {
                var isAuthorised = await identityService.isAuthorised(token);
                if (isAuthorised)
                { 
                    return Ok(); 
                }
                return Unauthorized();
            }
            return Unauthorized();

        }

        [HttpGet("/userprofile")]
        public IActionResult GetUserProfile()
        {
            var token = identityService.getToken(HttpContext);
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
            var token = identityService.getToken(HttpContext);
            var id = identityService.GetUserIdFromToken(token.ToString());
            var userProfile = userProfileRepository.GetUser(id);
            var questions = new List<InterviewQuestion>();
            if (userProfile != null)
            {
                foreach (var questionId in userProfile.FavouriteQuestionsIds)
                {
                    var question = questionsRepository.GetQuestion(questionId);
                    questions.Add(question);
                }
                return Ok(questions);
            }
            return NotFound();
        }

        [HttpGet("/solutions")]
        public IActionResult GetRespondedQuestions()
        {
            var token = identityService.getToken(HttpContext);
            var userId = identityService.GetUserIdFromToken(token.ToString());
            var userProfile = userProfileRepository.GetUser(userId);
            var questions = new List<InterviewQuestion>();
            if (userProfile != null && userProfile?.UserSolutionIds != null)
            {
                foreach (var Id in userProfile.UserSolutionIds)
                { 
                    var question = questionsRepository.GetQuestion(Id);
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
            var token = identityService.getToken(HttpContext);
            var userId = identityService.GetUserIdFromToken(token.ToString());
            if (userProfileRepository.UserExists(userId) && questionsRepository.QuestionExists(questionId))
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
            var token = identityService.getToken(HttpContext);
            var userId = identityService.GetUserIdFromToken(token.ToString());
            if (userProfileRepository.UserExists(userId) && questionsRepository.QuestionExists(questionId))
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
                        Id = userId,
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
