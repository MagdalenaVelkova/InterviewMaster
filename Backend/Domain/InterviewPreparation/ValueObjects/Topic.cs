using System.Collections.Generic;

namespace InterviewMaster.Domain.InterviewPreparation.ValueObjects
{
    public class Topic : ValueObject
    {
        public string Value { get; }

        public Topic(string value)
        {
            var topics = new HashSet<string> { "general", "collaboration", "problem solving", "adaptability", "organisation" };
            if (topics.Contains(value.ToLower()))
            {
                Value = value;
            }
            else
            {
                Value = "general";
            }
        }
        public override string ToString()
        {
            return Value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
