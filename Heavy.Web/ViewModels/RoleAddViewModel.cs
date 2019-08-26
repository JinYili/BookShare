using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookShare.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BookShare.Web.ViewModels
{
    public class RoleAddViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        [Remote(nameof(RoleController.CheckRoleExist), "Role", ErrorMessage = "Role name already existed")]
        public string RoleName { get; set; }
    }
}
