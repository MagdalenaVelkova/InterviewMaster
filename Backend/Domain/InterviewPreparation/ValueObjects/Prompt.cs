using System.Collections.Generic;

namespace InterviewMaster.Domain.InterviewPreparation.ValueObjects
{
    public class Prompt : ValueObject
    {
        public string Value { get; }

        public Prompt(string value)
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
