using InterviewMaster.Application.Services;
using InterviewMaster.Controllers.DTOs;
using InterviewMaster.Domain.Identity;
using InterviewMaster.Domain.InterviewPreparation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Authorize]
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

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var userProfile = userProfileService.GetUser(id);
            if (userProfile != null)
            {
                return Ok(userProfile);
            }
            return NotFound();
        }

        [HttpGet("{id}/favourites")]
        public IActionResult GetFavouriteQuestions(string id)
        {
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

        [HttpGet("{id}/solutions")]
        public IActionResult GetRespondedQuestions(string id)
        {
            var userProfile = userProfileService.GetUser(id);
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
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<string> Register(UserProfile userProfile)
        {
            return await userProfileService.CreateUser(userProfile);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] UserLoginDTO userCredentials)
        {
            var token = identityService.Authenticate(userCredentials.Email, userCredentials.Password);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
