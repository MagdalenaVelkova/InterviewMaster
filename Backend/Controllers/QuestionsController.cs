using InterviewMaster.Application.Services;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsRepository questionsRepository;

        public QuestionsController(IQuestionsRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }

        [HttpGet]
        public async Task<List<InterviewQuestion>> GetQuestions()
        {
            var questions = await questionsRepository.GetAllQuestions();
            return questions;
        }


        [HttpGet("/topic/{topicValue}")]
        public async Task<IActionResult> GetQuestionsByTopic(string topicValue)
        {
            if (topicValue.ToLower() == "all")
            {
                var allQuestions = await GetQuestions();
                return Ok(allQuestions);
            }
            try
            {
                var topic = new Topic(topicValue);
                var questions = await questionsRepository.GetQuestionsByTopic(topic);
                return Ok(questions);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetQuestionById(string id)
        {
            var question = questionsRepository.GetQuestion(id);
            if (question != null)
            {
                return Ok(question);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<string> Post([FromBody] InterviewQuestion interviewQuestion)
        {
            return await questionsRepository.CreateQuestion(interviewQuestion);
        }

    }
}
