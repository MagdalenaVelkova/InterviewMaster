using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.Identity
{
    [ExcludeFromCodeCoverage]
    public class UserAuth
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }
    }
}
