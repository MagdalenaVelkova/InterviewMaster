using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.InterviewPreparation.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class Response : ValueObject
    {
        public string Value { get; }

        public Response(string value)
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
