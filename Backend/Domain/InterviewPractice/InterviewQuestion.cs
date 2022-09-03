using InterviewMaster.Domain.InterviewPractice.ValueObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.InterviewPractice
{
    [ExcludeFromCodeCoverage]
    public class InterviewQuestion
    {
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Question { get; set; }
        [DisallowNull]
        public Topic Topic { get; set; }
        public IEnumerable<Prompt> Prompts { get; set; }
        public IEnumerable<ExampleAnswer> ExampleAnswers { get; set; }
    }
}
