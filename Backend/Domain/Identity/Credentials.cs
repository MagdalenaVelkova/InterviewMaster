using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.Identity
{
    [ExcludeFromCodeCoverage]
    public class Credentials
    {
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Password { get; set; }
    }
}
