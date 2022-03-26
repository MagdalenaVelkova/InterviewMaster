using InterviewMaster.Application.Services;
using InterviewMaster.Domain.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileService userProfileService;

        public UserController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
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

        [HttpPost]
        public async Task<UserProfile> Post(UserProfile userProfile)
        {
            return await userProfileService.CreateUser(userProfile);
        }
    }
}
