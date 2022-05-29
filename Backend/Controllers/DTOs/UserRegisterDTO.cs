using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Controllers.DTOs
{
    [ExcludeFromCodeCoverage]
    public class UserRegisterDTO
    {
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Password { get; set; }
    }
}