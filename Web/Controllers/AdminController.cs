using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> rolesManager;
        private readonly UserManager<IdentityUser> usersManager;
        private readonly IBoardRepository _boardRepository;
        private readonly IThreadRepository _threadRepository;


        public AdminController(RoleManager<IdentityRole> rolesManager,
                               UserManager<IdentityUser> usersManager,
                               IBoardRepository boardRepository,
                               IThreadRepository threadRepository)
        {
            this.rolesManager = rolesManager;
            this.usersManager = usersManager;
            this._boardRepository = boardRepository;
            this._threadRepository = threadRepository;


        }

        [HttpGet]
        public IActionResult ManageUsers()
        {

            ManageUsersViewModel model = new ManageUsersViewModel();
            model.Users = new List<ManageUserViewModel>();

            var users = usersManager.Users;
            foreach (var user in users)
            {
                var userViewModel = new ManageUserViewModel
                {
                    Email = user.Email,
                    Role = usersManager.GetRolesAsync(user).Result.FirstOrDefault()
                };
                model.Users.Add(userViewModel);
            }

            model.Roles = rolesManager.Roles.OrderBy(r => r.Name).Select(r => r.Name).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateBoard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBoard(string title)
        {
            Board newBoard = new Board
            {
                Title = title
            };
            _boardRepository.Add(newBoard);

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> ChangeRole(string email, string role)
        {
            var user = await usersManager.FindByEmailAsync(email);
            var roles = await usersManager.GetRolesAsync(user);

            await usersManager.RemoveFromRoleAsync(user, roles.First());
            await usersManager.AddToRoleAsync(user, role);

            return RedirectToAction("ManageUsers", "Admin");
        }

        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await usersManager.FindByEmailAsync(email);
            await usersManager.DeleteAsync(user);

            return RedirectToAction("ManageUsers", "Admin");
        }

        public IActionResult DeleteBoard(string id)
        {
            _boardRepository.Delete(Int32.Parse(id));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteThread(string threadId)
        {
            _threadRepository.Delete(Int32.Parse(threadId));
            return RedirectToAction("Index", "Home");
        }
    }
}
