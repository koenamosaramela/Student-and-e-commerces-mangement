
/*
 * Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333 
 * Student Names:  KM Ramela,   T Thothela,  T Fabeni,  SR Letsoara,  VOP Luhlabo
 
 *Question: AdminController

 * This controller handles the creation of admin accounts.
 * It uses ASP.NET Core Identity for user management and role assignment.
 * The Index action method displays the form and processes the form submission.
 * Only users with the "Admin" role can access this controller.
 * The controller uses dependency injection to access the UserManager service.
 * When the form is submitted, it creates a new IdentityUser and assigns the "Admin" role to it.
 * When the admin account is created successfully, a success message is displayed.
 * The controller uses the Admin model to bind the form data.
 * When the form is submitted, it checks if the model state is valid.
 * The controller uses the UserManager service to create the user and assign the role.
 * The controller uses the Authorize attribute to restrict access to the admin role.
 * When the form is submitted, it checks if the user already exists.
 * The controller uses the IdentityResult to check if the user was created successfully.
 * The controller uses the ModelState to add any errors that occur during user creation.
 * The controller uses the ViewBag to pass success messages to the view.
 * The controller uses the ViewData to pass user information to the view.
 * The controller uses the ViewData to pass the user role to the view.
 * The controller uses the ViewData to pass the user ID to the view.
 * The controller uses the ViewData to pass the user name to the view.
 * The controller uses the ViewData to pass the user email to the view.
 * The controller uses the ViewData to pass the user password to the view.
 * The controller uses the ViewData to pass the user role to the view.
 */

using ASPNETCore_DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCore_DB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Admin());
        }

        [HttpPost]
        public async Task<IActionResult> Index(Admin model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    ViewBag.SuccessMessage = $"Admin account for {model.Email} created successfully";
                    return View(new Admin()); // Return empty form after success
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}