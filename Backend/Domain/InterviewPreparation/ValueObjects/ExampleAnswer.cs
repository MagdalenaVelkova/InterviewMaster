using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.InterviewPreparation.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ExampleAnswer : ValueObject
    {
        public string Value { get; }

        public ExampleAnswer(string value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return base.ToString();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
