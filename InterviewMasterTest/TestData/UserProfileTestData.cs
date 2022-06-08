using InterviewMaster.Persistence.Models;
using System.Collections.Generic;

namespace InterviewMaster.Test.TestData
{
    public class UserProfileTestData
    {
        public static UserProfileDTO GenerateValidTestUserProfileOne()
        {
            return new UserProfileDTO()
            {
                Id = "6291dea19b4ee53850386abc",
                FirstName = "John",
                LastName = "Doe",
                FavouriteQuestions = new List<string>(),
                UserSolutions = new List<string>(),
            };
        }
    }
}
