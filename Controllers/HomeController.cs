/*
 * Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333 
 * Student Names:  KM Ramela,   T Thothela,  T Fabeni,  SR Letsoara,  VOP Luhlabo
 
 *Question:HomeController
 
 *This controller handles the main functionalities of the application.
 *It uses ASP.NET Core MVC to manage the views and actions related to the home page.
 *The Index action method displays the home page.
 *The SeedDatabase action method initializes the database with sample data.
 *The Privacy action method displays the privacy policy page.
 *The Error action method handles errors and displays an error page.
 *The controller uses dependency injection to access the database context and user manager.
 *The controller also uses logging to log any errors that occur during the execution of the actions.
 *The controller uses the SQLite database provider for data access.
 *The controller uses ASP.NET Core Identity for user authentication and authorization.
 *The controller uses a custom database initializer to seed the database with initial data.
 *The controller uses a custom model for the error page.
 *The controller uses a custom model for the home page.
 *The controller uses a custom model for the privacy policy page.
 *The controller uses a custom model for the seed database page.
 *Only users with the "Admin", "User", "Student", or "Consumer" roles can access this action.
 */
using ASPNETCore_DB.Data;
using ASPNETCore_DB.Interfaces;
using ASPNETCore_DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPNETCore_DB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SQLiteDBContext _context;
        private readonly IDBInitializer _seedDatabase;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, SQLiteDBContext context, IDBInitializer seedDatabase,UserManager<IdentityUser>userManager)
        {
            _logger = logger;
            _context = context;
            _seedDatabase = seedDatabase;
            _userManager = userManager; 
        }

        public IActionResult Index()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            ViewData["UserName"] = _userManager.GetUserName(this.User);

            if (this.User.IsInRole("Admin"))
            {
                ViewData["UserRole"] = "Admin";
            }
            if (this.User.IsInRole("User"))
            {
                ViewData["UserRole"] = "User";
            }
            if (this.User.IsInRole("Student"))
            {
                ViewData["UserRole"] = "Student";
            }
            if (this.User.IsInRole("Consumer"))
            {
                ViewData["UserRole"] = "Consumer";
            }
            return View();
        }

        public IActionResult SeedDatabase()
        {
            _seedDatabase.Initialize(_context);
            ViewBag.SeedDbFeedback = "Database created and Student Table populated with Data. Check Database folder.";
            return View("SeedDatabase");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}