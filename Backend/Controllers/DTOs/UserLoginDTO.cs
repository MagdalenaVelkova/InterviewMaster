using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InterviewMaster.Controllers.DTOs
{
    [ExcludeFromCodeCoverage]
    public class UserLoginDTO
    {
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false), DisallowNull]
        public string Password { get; set; }
    }
}