using InterviewMaster.Application.Services;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using InterviewMaster.Persistance.Extensions;
using InterviewMaster.Persistance.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewMaster.Persistance.Repositories
{
    public class QuestionsRepository : BaseRepository<InterviewQuestionDTO>, IQuestionsService
    {
        private readonly IIdGenerator idGenerator;

        public QuestionsRepository(IMongoDatabase database, IIdGenerator idGenerator) : base(database)
        {
            this.idGenerator = idGenerator;
        }
        public override string DbCollectionName => "Questions";

        public Task<List<InterviewQuestion>> GetAllQuestions()
        {
             return Query().Select(dto => new InterviewQuestion
            {
                Id = dto.Id,
                Question = dto.Question,
                Topic = new Topic(dto.Topic.ToString()),
                Prompts = dto.Prompts.Select(prompt => new Prompt(prompt)),
                ExampleAnswers = dto.ExampleAnswers.Select(exampleAnswer => new ExampleAnswer(exampleAnswer))
            }).ToListAsync();
        }

        public Task<List<InterviewQuestion>> GetQuestionsByTopic(Topic topic)
        {
            return Query().Where(dto => dto.Topic==topic.Value.ToLower()).Select(dto => new InterviewQuestion
            {
                Id = dto.Id,
                Question = dto.Question,
                Topic = new Topic(dto.Topic.ToString()),
                Prompts = dto.Prompts.Select(prompt => new Prompt(prompt)),
                ExampleAnswers = dto.ExampleAnswers.Select(exampleAnswer => new ExampleAnswer(exampleAnswer))
            }).ToListAsync();
        }

        public async Task<InterviewQuestion> PostQuestion(InterviewQuestion interviewQuestion)
        {
            var entity = new InterviewQuestionDTO
            {
                Id = idGenerator.Generate(),
                Question = interviewQuestion.Question,
                Topic = interviewQuestion.Topic.Value,
                Prompts = interviewQuestion.Prompts.Select(prompt => prompt.Value),
                ExampleAnswers = interviewQuestion.ExampleAnswers.Select(exampleAnswer => exampleAnswer.Value)
            };

            await Collection.InsertOneAsync(entity);

            return interviewQuestion;
        }


        //get all question


        //get question by id

    }
}
