using InterviewMaster.Application.Services;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using InterviewMaster.Persistance.Extensions;
using InterviewMaster.Persistance.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
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

        public InterviewQuestion? GetQuestion(string id)
        {
            try
            {
                var interviewQuestionDTO = Query().Where(dto => dto.Id == id).FirstOrDefault();
                if (interviewQuestionDTO != null)
                {
                    return new InterviewQuestion
                    {
                        Id = interviewQuestionDTO.Id,
                        Question = interviewQuestionDTO.Question,
                        Topic = new Topic(interviewQuestionDTO.Topic.ToString()),
                        Prompts = interviewQuestionDTO.Prompts.Select(prompt => new Prompt(prompt)),
                        ExampleAnswers = interviewQuestionDTO.ExampleAnswers.Select(exampleAnswer => new ExampleAnswer(exampleAnswer))
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (FormatException e)
            {
                // log exception
                return null;
            }          
        }
        public Task<List<InterviewQuestion>> GetQuestionsByTopic(Topic topic)
        {
            return Query().Where(dto => dto.Topic == topic.Value).Select(dto => new InterviewQuestion
            {
                Id = dto.Id,
                Question = dto.Question,
                Topic = new Topic(dto.Topic.ToString()),
                Prompts = dto.Prompts.Select(prompt => new Prompt(prompt)),
                ExampleAnswers = dto.ExampleAnswers.Select(exampleAnswer => new ExampleAnswer(exampleAnswer))
            }).ToListAsync();
        }

        public async Task<string> PostQuestion(InterviewQuestion interviewQuestion)
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

            return entity.Id;
        }

        public bool QuestionExists(string id)
        {
                return Query().Any(x => x.Id == id);
        }


        //get all question


        //get question by id

    }
}
