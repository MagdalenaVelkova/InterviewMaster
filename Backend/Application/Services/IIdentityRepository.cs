using InterviewMaster.Domain.Identity;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services

{
    public interface IIdentityRepository
    {
        public UserAuth GetUserIdentity(Credentials credentials);
        public bool UserCredentialsExists(string id);
        public Task<string> CreateIdentity(UserAuth userAuth);
    }

}

