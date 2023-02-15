using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlyCatsWebapp.Areas.Identity.Data;
using OnlyCatsWebapp.Core;
using OnlyCatsWebapp.Core.Repositories;
using OnlyCatsWebapp.Core.ViewModels;
using static OnlyCatsWebapp.Core.Consts;

namespace OnlyCatsWebapp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;

        //get access to signInManager to get roles assigned in login
        public UserController(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        // GET: UserController
        [Authorize(Roles = Consts.Roles.Manager)]
        public ActionResult Index()
        {
            try
            {
                var users = _unitOfWork.User.GetUsers();
                return View(users);
            }
            catch
            {
                throw new NotImplementedException("Error while getting users.");
            }
        }


        public async Task<ActionResult> Edit(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = new List<SelectListItem>().ToList();

            //let select roles in view
            foreach (var role in roles)
            {
                //check if the role exists
                var hasRole = userRoles.Any(ur => ur.Contains(role.Name));

                roleItems.Add(new SelectListItem(role.Name, role.Id, hasRole));
            }

            var viewModel = new EditUserViewModel
            {
                User = user,
                Roles = roleItems
            };
            return View(viewModel);
        }

        [Authorize(Roles = Consts.Roles.Manager)]
        public async Task<ActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);
            if (user == null)
            {
                return NotFound($"Unable to load user.");
            }

            //bind roles
            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);
            //loop through the roles in viewModel
            //check if the role is assigned in DB
            //if assigned do nothing
            //if not assigned-> add role to user
            for (int i = 0; i < data.Roles.Count; i++)
            {
                SelectListItem? role = data.Roles[i];
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        //add role
                        await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                    }
                }
                else
                {
                    if(assignedInDb != null)
                    {
                        //remove role
                        await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Text);

                    }
                }
            }

            //assign to user data object
            user.Email = data.User.Email;
            user.OrganisationName = data.User.OrganisationName;
            user.PhoneNumber = data.User.PhoneNumber;
            user.Address = data.User.Address;
            user.City = data.User.City;
            user.Description = data.User.Description;
            user.PostalCode = data.User.PostalCode;
            user.State = data.User.State;
            user.Website= data.User.Website;

            //upload to database all data through unit of work
            _unitOfWork.User.UpdateUser(user);

            return RedirectToAction("Edit", new {id = user.Id});
        }

        [Authorize(Roles = Consts.Roles.Administrator)]
        public async Task<IActionResult> Delete(string id)
        {
            var user = _unitOfWork.User.GetUser(id);

            var result = await _signInManager.UserManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }
            return RedirectToAction("Index", new { id = user.Id });
        }
    }
}
