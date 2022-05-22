using InterviewMaster.Domain.InterviewPreparation;
using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services
{
    public interface IQuestionsService
    {
        public Task<List<InterviewQuestion>> GetAllQuestions();

        public Task<List<InterviewQuestion>> GetQuestionsByTopic(Topic topic);

        public Task<string> PostQuestion(InterviewQuestion interviewQuestion);
        public InterviewQuestion? GetQuestion(string id);

        public bool QuestionExists(string id);
    }
}
