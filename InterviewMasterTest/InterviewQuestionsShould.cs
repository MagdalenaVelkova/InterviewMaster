using Application.Controllers;
using InterviewMaster.Test.Util;
using System.Threading.Tasks;
using Xunit;
using InterviewMaster.Test.TestData;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using InterviewMaster.Domain.InterviewPreparation;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InterviewMaster.Test
{
    public class InterviewQuestionsShould
    {
        private QuestionsController questionsController;
        private FakeQuestionsRepository fakeQuestionsRepository;

        public InterviewQuestionsShould()
        {
            fakeQuestionsRepository = new FakeQuestionsRepository();
            questionsController = new QuestionsController(fakeQuestionsRepository);
        }

        [Fact]
        public async Task Be_Fetched_By_TopicAsync()
        {
            //arrange
            var questionOne = InterviewQuestionTestData.GenerateValidTestQuestionOne();
            var questionTwo = InterviewQuestionTestData.GenerateValidTestQuestionTwo();
            var questionThree = InterviewQuestionTestData.GenerateValidTestQuestionThree();

            questionOne.Topic = new Topic("collaboration");
            questionTwo.Topic = new Topic("collaboration");
            questionThree.Topic = new Topic("general");

            var questionsInDb = new List<InterviewQuestion>() { questionOne, questionTwo, questionThree };
            fakeQuestionsRepository.AddMultiple(questionsInDb);

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
            var questionsInDb = InterviewQuestionTestData.GenerateMultipleQuestions();
            fakeQuestionsRepository.AddMultiple(questionsInDb);

            //act
            var questionsResult = await questionsController.GetQuestionsByTopic("all") as OkObjectResult;

            //assert
            Assert.NotNull(questionsResult);
            var interviewQuestions = questionsResult.Value as List<InterviewQuestion>;
            Assert.Equal(questionsInDb.Count, interviewQuestions.Count);


        }

        [Fact]
        public async Task Not_Accept_An_Unknown_Topic()
        {
            //arrange
            var questionsInDb = InterviewQuestionTestData.GenerateMultipleQuestions();
            fakeQuestionsRepository.AddMultiple(questionsInDb);

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
            fakeQuestionsRepository.AddMultiple(questionsInDb);
            questionsInDb[0].Id = "6291dea19b4ee53850386hjk";
            var expected = questionsInDb[0];

            //act
            var questionResult = questionsController.GetQuestionById("6291dea19b4ee53850386hjk") as OkObjectResult;
            
            //assert
            Assert.NotNull(questionResult);
            var interviewQuestion = questionResult.Value as InterviewQuestion;
            Assert.Equal(expected, interviewQuestion);


        }

        [Fact]
        public void Not_Return_If_Id_Does_Not_Exist()
        {
            //arrange
            var questionsInDb = InterviewQuestionTestData.GenerateMultipleQuestions();
            fakeQuestionsRepository.AddMultiple(questionsInDb);

            //act
            var questionResult = questionsController.GetQuestionById("7651dea19b4ee53850386hjk") as NotFoundResult;

            //assert
            Assert.NotNull(questionResult);
        }

    }
}
