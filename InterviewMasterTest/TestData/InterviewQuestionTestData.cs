using InterviewMaster.Domain.InterviewPractice;
using InterviewMaster.Domain.InterviewPractice.ValueObjects;
using InterviewMaster.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewMaster.Test.TestData
{
    public class InterviewQuestionTestData
    {
        public static InterviewQuestionDTO GenerateValidTestQuestionOne()
        {
            return new InterviewQuestionDTO() {
                Id = "61746566a01a5e8e03b788e2",
                Question = "Test Question One",
                Topic = "general",
                Prompts = new List<string>() { "Test Prompt One" },
                ExampleAnswers = new List<string>() {"Test Example Answer One" },
            };
        }

        public static InterviewQuestionDTO GenerateValidTestQuestionTwo()
        {
            return new InterviewQuestionDTO()
            {
                Id = "623633119c87b612a42c587a",
                Question = "Test Question Two",
                Topic = "collaboration",
                Prompts = new List<string>() { "Test Prompt Two" },
                ExampleAnswers = new List<string>() { "Test Example Answer Two" },
            };
        }

        public static InterviewQuestionDTO GenerateValidTestQuestionThree()
        {
            return new InterviewQuestionDTO()
            {
                Id = "6291e4a80403ed9f166540d1",
                Question = "Test Question Three",
                Topic = "collaboration",
                Prompts = new List<string>() {"Test Prompt Three" },
                ExampleAnswers = new List<string>() { "Test Example Answer Three" },
            };
        }

        public static List<InterviewQuestionDTO> GenerateMultipleQuestions() {
            var questionOne = GenerateValidTestQuestionOne();
            var questionTwo = GenerateValidTestQuestionTwo();
            var questionThree = GenerateValidTestQuestionThree();

            return new List<InterviewQuestionDTO>() { questionOne, questionTwo, questionThree };
        }

    }
}
