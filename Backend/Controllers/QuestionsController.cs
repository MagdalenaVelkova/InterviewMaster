using InterviewMaster.Application.Services;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            this.questionsService = questionsService;
        }

        [HttpGet]
        public async Task<List<InterviewQuestion>> GetQuestions()
        {
            var questions = await questionsService.GetAllQuestions();
            return questions;
        }


        // GET api/<ValuesController>/5
        [HttpGet("/topic/{topicValue}")]
        public async Task<List<InterviewQuestion>> GetQuestionsByTopic(string topicValue)
        {
            if (topicValue.ToLower() == "all")
            {
                return await GetQuestions();
            }
            var topic = new Topic(topicValue);
            var questions = await questionsService.GetQuestionsByTopic(topic);
            return questions;
        }

        [HttpGet("{id}")]
        public ActionResult GetQuestionById(string id)
        {
            var question= questionsService.GetQuestion(id);
            if (question != null)
            {
                return Ok(question);
            }
            return NotFound();
        }

        // Needs fixing 
        [HttpPost]
        public async Task<string> Post([FromBody] InterviewQuestion interviewQuestion)
        {
         return await questionsService.PostQuestion(interviewQuestion);
        }

    }
}
