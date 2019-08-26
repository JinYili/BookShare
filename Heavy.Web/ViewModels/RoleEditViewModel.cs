using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShare.Web.ViewModels
{
    public class RoleEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
