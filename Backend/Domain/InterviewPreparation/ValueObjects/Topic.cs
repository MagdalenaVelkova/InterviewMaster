using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.InterviewPreparation.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class Topic : ValueObject
    {
        public string Value { get; }
        private readonly HashSet<string> topics = new HashSet<string> { "general", "collaboration", "problem solving", "adaptability", "organisation" };
        public Topic(string value)
        {
            if (!topics.Contains(value.ToLower()))
            {
                throw new ArgumentException();
            }
            Value = value.ToLower();
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
