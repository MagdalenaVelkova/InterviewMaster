using InterviewMaster.Domain.Identity;

namespace InterviewMaster.Application.Services
{
    public interface IJWTManagerService
    {
        Tokens Authenticate(UserAuth userAuth);
    }
}
