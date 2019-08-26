using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookShare.Web.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        { 
            Logins = new List<IdentityUserLogin<string>>();
            Tokens = new List<IdentityUserToken<string>>();
            UserRoles = new List<IdentityUserRole<string>>();
        }

        [MaxLength(18)]
        public string IdCardNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } 

        public ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
