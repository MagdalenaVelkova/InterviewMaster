namespace InterviewMaster.Domain.Identity
{
    public class UserAuth
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }
    }
}
