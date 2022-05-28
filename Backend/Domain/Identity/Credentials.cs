using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.Identity
{
    [ExcludeFromCodeCoverage]
    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
