using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewMaster.Test.TestData
{
    public class InterviewQuestionTestData
    {
        public static InterviewQuestion GenerateValidTestQuestionOne()
        {
            return new InterviewQuestion() {
                Id = "61746566a01a5e8e03b788e0",
                Question = "Test Question One",
                Topic = new Topic("general"),
                Prompts = new List<Prompt>() { new Prompt("Test Prompt One") },
                ExampleAnswers = new List<ExampleAnswer>() { new ExampleAnswer("Test Example Answer One") },
            };
        }

        public static InterviewQuestion GenerateValidTestQuestionTwo()
        {
            return new InterviewQuestion()
            {
                Id = "623633119c87b612a42c587a",
                Question = "Test Question Two",
                Topic = new Topic("collaboration"),
                Prompts = new List<Prompt>() { new Prompt("Test Prompt Two") },
                ExampleAnswers = new List<ExampleAnswer>() { new ExampleAnswer("Test Example Answer Two") },
            };
        }

        public static InterviewQuestion GenerateValidTestQuestionThree()
        {
            return new InterviewQuestion()
            {
                Id = "6291e4a80403ed9f166540d1",
                Question = "Test Question Three",
                Topic = new Topic("collaboration"),
                Prompts = new List<Prompt>() { new Prompt("Test Prompt Three") },
                ExampleAnswers = new List<ExampleAnswer>() { new ExampleAnswer("Test Example Answer Three") },
            };
        }

        public static List<InterviewQuestion> GenerateMultipleQuestions() {
            var questionOne = InterviewQuestionTestData.GenerateValidTestQuestionOne();
            var questionTwo = InterviewQuestionTestData.GenerateValidTestQuestionTwo();
            var questionThree = InterviewQuestionTestData.GenerateValidTestQuestionThree();

            return new List<InterviewQuestion>() { questionOne, questionTwo, questionThree };
        }

    }
}
