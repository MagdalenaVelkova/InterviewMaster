using InterviewMaster.Application.Services;
using InterviewMaster.Controllers.DTOs;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSolutionsController : ControllerBase
    {
        private readonly IUserSolutionsService userSolutionsService;

        public UserSolutionsController(IUserSolutionsService userSolutionsService)
        {
            this.userSolutionsService = userSolutionsService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var userProfile = userSolutionsService.GetUserSolutionById(id);
            if (userProfile != null)
            {
                return Ok(userProfile);
            }
            return NotFound();
        }

        [HttpGet("{questionId}/{userId}")]
        public IActionResult Get(string questionId, string userId)
        {
            var userSolution = userSolutionsService.GetUserSolutionByUserAndQuestion(questionId, userId);
            if (userSolution != null)
            {
                return Ok(userSolution);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<string> PostSolution(UserSolutionDTO userSolutionDTO)
        {
            var userSolution = new UserSolution() {
            Id = userSolutionDTO.Id,
            UserId = userSolutionDTO.UserId,
            InterviewQuestionId = userSolutionDTO.InterviewQuestionId,
            Response = new Response(userSolutionDTO.Response)
            };

            return await userSolutionsService.CreateOneOrUpdate(userSolution);
        }
    }
}
