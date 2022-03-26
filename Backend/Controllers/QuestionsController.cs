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
        [HttpGet("{topicValue}")]
        public async Task<List<InterviewQuestion>> GetQuestionsByTopic(string topicValue)
        {
            var topic = new Topic(topicValue);
            var questions = await questionsService.GetQuestionsByTopic(topic);
            return questions;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] InterviewQuestion interviewQuestion)
        {
            questionsService.PostQuestion(interviewQuestion);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
