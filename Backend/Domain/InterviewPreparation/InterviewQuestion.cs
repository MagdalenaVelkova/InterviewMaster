using InterviewMaster.Domain.InterviewPreparation.ValueObjects;
using System;
using System.Collections.Generic;

namespace InterviewMaster.Domain.InterviewPreparation
{
    public class InterviewQuestion
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public Topic Topic { get; set; }
        public IEnumerable<Prompt> Prompts { get; set; }
        public IEnumerable<ExampleAnswer> ExampleAnswers { get; set; }
    }
}
