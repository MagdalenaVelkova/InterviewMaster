using InterviewMaster.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewMaster.Test.TestData
{
    public class UserProfileTestData
    {
        public static UserProfile GenerateValidTestUserProfileOne()
        {
            return new UserProfile()
            {
                UserId = "6291dea19b4ee53850386abc",
                FirstName = "John",
                LastName = "Doe", 
                FavouriteQuestionsIds = new List<string>(),
                UserSolutionIds = new List<string>(),
            };
        }
    }
}
