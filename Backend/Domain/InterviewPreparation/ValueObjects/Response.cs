using System.Collections.Generic;

namespace InterviewMaster.Domain.InterviewPreparation.ValueObjects
{
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
