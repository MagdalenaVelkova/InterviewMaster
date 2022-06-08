using InterviewMaster.Controllers;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using InterviewMaster.Persistence.Extensions;
using InterviewMaster.Persistence.Models;
using InterviewMaster.Persistence.Repositories;
using InterviewMaster.Test.TestData;
using InterviewMaster.Test.Util;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InterviewMaster.Test.Tests
{
    public class InterviewQuestionsShould : TestBase
    {
        private QuestionsController questionsController;
        private readonly QuestionsRepository questionsRepository;

        public InterviewQuestionsShould()
        {
            var idGenerator = AppInTest.GetService<IdGenerator>();
            questionsRepository = new QuestionsRepository(MongoDbService.MongoDatabase, idGenerator);
            questionsController = new QuestionsController(questionsRepository);
        }

        [Fact]
        public async Task Be_Fetched_By_TopicAsync()
        {
            //arrange
            var questionOne = InterviewQuestionTestData.GenerateValidTestQuestionOne();
            var questionTwo = InterviewQuestionTestData.GenerateValidTestQuestionTwo();
            var questionThree = InterviewQuestionTestData.GenerateValidTestQuestionThree();

            questionOne.Topic = "collaboration";
            questionTwo.Topic = "collaboration";
            questionThree.Topic = "general";

            var questionsInDb = new List<BsonDocument>() { questionOne.ToBsonDocument(), questionTwo.ToBsonDocument(), questionThree.ToBsonDocument() };

            MongoDbService.InsertMany(questionsInDb, QuestionsRepository.CollectionName);
            //
            //act
            var questionsResult = await questionsController.GetQuestionsByTopic("collaboration") as OkObjectResult;

            //assert
            Assert.NotNull(questionsResult);
            var interviewQuestions = questionsResult.Value as List<InterviewQuestion>;
            Assert.Equal(2, interviewQuestions.Count);


        }

        [Fact]
        public async Task Fetch_All_Questions_If_Topic_Is_All()
        {
            //arrange
            var questionsInDb = InterviewQuestionTestData.GenerateMultipleQuestions().Select(x => x.ToBsonDocument()).ToList();
            MongoDbService.InsertMany(questionsInDb, QuestionsRepository.CollectionName);

            var collection = MongoDbService.GetDocuments<InterviewQuestionDTO>(QuestionsRepository.CollectionName).ToList();

            //act
            var questionsResult = await questionsController.GetQuestionsByTopic("all") as OkObjectResult;

            //assert
            Assert.NotNull(questionsResult);
            var interviewQuestions = questionsResult.Value as List<InterviewQuestion>;
            Assert.Equal(collection.Count, interviewQuestions.Count);

        }

        [Fact]
        public async Task Not_Accept_An_Unknown_Topic()
        {
            //arrange
            var questionsInDb = InterviewQuestionTestData.GenerateMultipleQuestions().Select(x => x.ToBsonDocument()).ToList();
            MongoDbService.InsertMany(questionsInDb, QuestionsRepository.CollectionName);

            //act
            var questionsResult = await questionsController.GetQuestionsByTopic("unknownTopic");

            //assert
            Assert.IsType<BadRequestObjectResult>(questionsResult);
        }

        [Fact]
        public void Be_Fetched_By_Id()
        {
            //arrange
            var questionsInDb = InterviewQuestionTestData.GenerateMultipleQuestions();

            var questionId = ObjectId.GenerateNewId().ToString();
            questionsInDb[0].Id = questionId;

            var expected = new InterviewQuestion()
            {
                Id = questionsInDb[0].Id,
                Question = questionsInDb[0].Question,
                Topic = new Topic(questionsInDb[0].Topic),
                Prompts = questionsInDb[0].Prompts.Select(x => new Prompt(x)),
                ExampleAnswers = questionsInDb[0].ExampleAnswers.Select(x => new ExampleAnswer(x)),

            };

            MongoDbService.InsertMany(questionsInDb.Select(x => x.ToBsonDocument()).ToList(), QuestionsRepository.CollectionName);

            //act
            var questionResult = questionsController.GetQuestionById(questionId) as OkObjectResult;

            //assert
            Assert.NotNull(questionResult);
            var interviewQuestion = questionResult.Value as InterviewQuestion;

            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(interviewQuestion));
        }

        [Fact]
        public void Not_Return_If_Id_Does_Not_Exist()
        {
            //arrange
            var questionsInDb = InterviewQuestionTestData.GenerateMultipleQuestions().Select(x => x.ToBsonDocument()).ToList();
            MongoDbService.InsertMany(questionsInDb, QuestionsRepository.CollectionName);

            //act
            var randomId = "d828fe61b22baebc76fc1e4e";
            var questionResult = questionsController.GetQuestionById(randomId);

            //assert
            Assert.IsType<NotFoundResult>(questionResult);
        }

    }
}
