using InterviewMaster.Application.Services;
using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Linq;

namespace InterviewMaster.Test.Util
{
    public class FakeQuestionsRepository : IQuestionsRespository, IDisposable
    {
        private List<InterviewQuestion> questions;

        public Task<List<InterviewQuestion>> GetAllQuestions()
        {
        return Task.FromResult(questions);
        }

        public InterviewQuestion GetQuestion(string id)
        {
            var q = questions.FirstOrDefault(x => x.Id == id);
            return q;
        }

        public Task<List<InterviewQuestion>> GetQuestionsByTopic(Topic topic)
        {
            var task =  Task.FromResult(questions.Where(x => x.Topic.Value == topic.Value).ToList());
            return task;
        }

        public bool QuestionExists(string id)
        {
            return questions.Any(x => x.Id == id);
        }

        public void AddMultiple(List<InterviewQuestion> questionsToAdd) {
            questions = questionsToAdd;
        }

        public void AddOne(InterviewQuestion questionToAdd) {
        questions.Add(questionToAdd);
        }

        public void Dispose()
        {
            questions.Clear();
        }
    }
}
