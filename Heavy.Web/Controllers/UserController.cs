using System.Linq;
using System.Threading.Tasks;
using BookShare.Web.Data;
using BookShare.Web.Models;
using BookShare.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BookShare.Web.Controllers
{

    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[IgnoreAntiforgeryToken]
        // public async Task<IActionResult> AddUser([Bind("AnotherUser")] UserAddViewModel userAddViewModel)
        public async Task<IActionResult> AddUser(UserAddViewModel userAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userAddViewModel);
            }

            var user = new ApplicationUser
            {
                UserName = userAddViewModel.UserName,
                Email = userAddViewModel.Email,
                IdCardNo = userAddViewModel.IdCardNo,
                BirthDate = userAddViewModel.BirthDate
            };

            var result = await _userManager.CreateAsync(user, userAddViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(userAddViewModel);

        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var claims = await _userManager.GetClaimsAsync(user);

            var vm = new UserEditViewModel
            {
                Id = user.Id,
                BirthDate = user.BirthDate,
                Email = user.Email,
                IdCardNo = user.IdCardNo,
                UserName = user.UserName,
                Claims = claims.Select(x => x.Value).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, UserEditViewModel userEditViewModel)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            user.UserName = userEditViewModel.UserName;
            user.Email = userEditViewModel.Email;
            user.IdCardNo = userEditViewModel.IdCardNo;
            user.BirthDate = userEditViewModel.BirthDate;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Error occurs in editing");
            return View(userEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Error occurs in deleting");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User can't be found");
            }

            return View("Index", await _userManager.Users.ToListAsync());
        }
    }
}
