using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShare.Web.Data
{
    public static class ClaimTypes
    {
        public static List<string> AllClaimTypeList { get; set; } = new List<string>
        {
            "Edit Book",
            "Edit Users",
            "Edit Roles",
            "Email"
        };
    }
}
