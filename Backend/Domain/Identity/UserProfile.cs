using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace InterviewMaster.Domain.Identity
{
    public class UserProfile
    {

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public IEnumerable<string> FavouriteQuestionsIds { get; set; }

        public IEnumerable<string> UserSolutionIds { get; set; }


        //hash password here method? 
        public static string HashPassword(string rawPassword)
        {
            // this.PasswordHash = string.Empty;
            return rawPassword;
        }
    }

}
