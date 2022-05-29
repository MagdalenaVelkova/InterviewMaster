using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Domain.Identity
{
    [ExcludeFromCodeCoverage]
    public class UserAuth
    {

        public string Id { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string PasswordSalt { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string PasswordHash { get; set; }
    }
}
