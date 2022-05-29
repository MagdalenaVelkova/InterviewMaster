using InterviewMaster.Application.Services;
using InterviewMaster.Domain.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewMaster.Test.Util
{
    
    public class FakeUserIdentityRepository : IIdentityRepository, IDisposable
    {
        private List<UserAuth> userIdentities = new List<UserAuth>();

        public Task<string> CreateIdentity(UserAuth userAuth)
        {
            userAuth.Id = ObjectId.GenerateNewId().ToString();
            userIdentities.Add(userAuth);
            return Task.FromResult(userAuth.Id);
        }

        public UserAuth GetUserIdentity(Credentials credentials)
        {
            return userIdentities.AsQueryable().Where(x => x.Email == credentials.Email).FirstOrDefault();
        }

        public bool UserCredentialsExists(string id)
        {
            return userIdentities.AsQueryable().Any(x => x.Id == id);
        }

        internal void AddOne(UserAuth userIdentity)
        {
            userIdentities.Add(userIdentity);
        }

        public bool UserEmailExists(string email)
        {
            return userIdentities.AsQueryable().Any(x => x.Email == email);
        }
        public void Dispose()
        {
            userIdentities.Clear();
        }

    }
}
