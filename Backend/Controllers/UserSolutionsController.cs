using InterviewMaster.Application.Services;
using InterviewMaster.Controllers.DTOs;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InterviewMaster.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserSolutionsController : ControllerBase
    {
        private readonly IUserSolutionsRepository userSolutionsRepository;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IdentityService identityService;

        public UserSolutionsController(IUserSolutionsRepository userSolutionsRepository, IdentityService identityService, IUserProfileRepository userProfileRepository)
        {
            this.userSolutionsRepository = userSolutionsRepository;
            this.identityService = identityService;
            this.userProfileRepository = userProfileRepository;
        }

        [HttpGet("user/{questionId}")]
        public IActionResult GetByQuestion(string questionId)
        {
            var token = identityService.getToken(HttpContext);
            var userId = identityService.GetUserIdFromToken(token.ToString());
            var userSolution = userSolutionsRepository.GetUserSolutionByUserAndQuestion(questionId, userId);
            if (userSolution != null)
            {
                return Ok(userSolution);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostSolution(UserSolutionDTO userSolutionDTO)
        {
            var token = identityService.getToken(HttpContext);
            var userId = identityService.GetUserIdFromToken(token.ToString());

            var userSolution = new UserSolution() {
            UserId = userId,
            InterviewQuestionId = userSolutionDTO.InterviewQuestionId,
            Response = new Response(userSolutionDTO.Response)
            };

            var userSolutionId = await userSolutionsRepository.CreateOneOrUpdate(userSolution);

            if (userSolutionId != null)
            {
                await userProfileRepository.AddQuestionToSolved(userSolution.InterviewQuestionId, userId);
                return Ok(userSolutionId);
            }
            return Problem();
        }
    }
}
