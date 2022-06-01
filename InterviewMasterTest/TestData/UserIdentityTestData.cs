using InterviewMaster.Persistance.Models;

namespace InterviewMaster.Test.TestData
{
    public class UserIdentityTestData
    {
        public static UserIdentityDTO GenerateValidTestUserIdentityOne()
        {
            return new UserIdentityDTO ()
            {
                Id = "6291dea19b4ee53850386abc",
                Email = "TestUserOne@email.me", 
                PasswordHash = "JQS3k7D1aA / AujPNt8rKRWzmQcvZg1oLu8pJF532GDs =", 
                PasswordSalt  = "Ovel5u161FlNZd898BTEUH8RYkj1DsU0Xqlk49JrpG86V67nWPrl4XOQ9WdavaRhHe0qtJvisPz+/aNa1ujooA=="
            };
        }
    }
}
