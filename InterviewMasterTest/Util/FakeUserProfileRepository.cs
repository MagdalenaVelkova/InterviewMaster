using InterviewMaster.Application.Services;
using InterviewMaster.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewMaster.Test.Util
{
    public class FakeUserProfileRepository : IUserProfileRepository, IDisposable
    {
        private List<UserProfile> userProfiles = new List<UserProfile>();


        public bool UserExists(string id)
        {
            return userProfiles.Any(x => x.UserId == id);
        }
        public Task<string> CreateUser(UserProfile user)
        {
            userProfiles.Add(user);
            return Task.FromResult(user.UserId);
        }

        public UserProfile GetUser(string userId)
        {
            return userProfiles.FirstOrDefault(x => x.UserId == userId);
        }
        public Task<string> AddQuestionToFavourite(string questionId, string userId)
        {
            var userProfile = userProfiles.FirstOrDefault(x => x.UserId == userId);
            if (userProfile != null)
            {
                var temp = new List<string>();
                temp.AddRange(userProfile.FavouriteQuestionsIds);
                temp.Add(questionId);
                userProfile.FavouriteQuestionsIds = temp;
                return Task.FromResult(questionId);
            }
            return null;
        }
        public Task<string> RemoveQuestionFromFavourite(string questionId, string userId)
        {
            var userProfile = userProfiles.FirstOrDefault(x => x.UserId == userId);
            if (userProfile != null)
            { 
                userProfile.FavouriteQuestionsIds = userProfile.FavouriteQuestionsIds.Where(x => x != questionId);
                return Task.FromResult(questionId);
            }
            return null;
        }

        public Task<string> AddQuestionToSolved(string questionId, string userId)
        {
            var userProfile = userProfiles.FirstOrDefault(x => x.UserId == userId);
            if (userProfile != null)
            {
                var temp = new List<string>();
                temp.AddRange(userProfile.UserSolutionIds);
                if (userProfile.UserSolutionIds.Contains(questionId))
                {
                    return Task.FromResult(questionId);
                }
                temp.Add(questionId);
                userProfile.UserSolutionIds = temp;
                return Task.FromResult(questionId);
            }

            return null;
        }

        public void AddOne(UserProfile userProfile)
        {
            userProfiles.Add(userProfile);
        }

        public void Dispose()
        {
            userProfiles.Clear();
        }


    }
}