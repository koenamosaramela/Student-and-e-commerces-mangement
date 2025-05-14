/*
 * Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333 
 * Student Names:  KM Ramela,   T Thothela,  T Fabeni,  SR Letsoara,  VOP Luhlabo
 
 *Question:ConsumerController
 
 *This controller handles the CRUD operations for the Consumer entity.
 *It uses the IConsumer interface for data access and ASP.NET Core Identity for user management.
 *The Index action method displays a paginated list of consumers with sorting and searching capabilities.
 *The Details action method displays the details of a specific consumer.
 *The Create action method allows the creation of a new consumer.
 *The Edit action method allows the editing of an existing consumer.
 *The Delete action method allows the deletion of a consumer.
 *The controller uses the CustomExceptionFilter to handle exceptions globally.
 *The controller also uses the IWebHostEnvironment to manage file uploads.
 *The controller uses the IHttpContextAccessor to access the current HTTP context.
 *The controller uses the UserManager and SignInManager to manage user roles and authentication.
 *The controller uses the PaginatedList class to manage pagination.
 *The controller uses the WebConstants class to manage file paths.
 *The controller uses the Consumer class to represent the consumer entity.
 *The controller uses the IConsumer interface to define the data access methods.
 *The controller uses the SQLiteDBContext class to manage the database context.
 *The controller uses the CustomExceptionFilter class to handle exceptions globally.
 *Only users with the "Admin", "User" and "Consumer" role can access this controller.
 */
using ASPNETCore_DB.Interfaces;
using ASPNETCore_DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore_DB.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class ConsumerController : Controller
    {
        private readonly IConsumer _consumerRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ConsumerController(
            IConsumer consumerRepo,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment)
        {
            _consumerRepo = consumerRepo;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            try
            {
                var consumers = _consumerRepo.GetConsumers(searchString, sortOrder).AsNoTracking();
                int pageSize = 3;
                return View(PaginatedList<Consumer>.Create(consumers, pageNumber ?? 1, pageSize));
            }
            catch (Exception ex)
            {
                throw new Exception("No consumer records detected");
            }
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var consumer = _consumerRepo.ByEmail(User.Identity?.Name);
                if (consumer == null)
                {
                    return NotFound();
                }
                return View(consumer);
            }
            else
            {
                var consumer = _consumerRepo.Details(id);
                if (consumer == null)
                {
                    return NotFound();
                }


                if (!User.IsInRole("Admin") && consumer.Email != User.Identity?.Name)
                {
                    return Forbid();
                }

                return View(consumer);
            }
        }


        [Authorize(Roles = "Consumer,User")]
        [HttpGet]
        public IActionResult Create()
        {
            var consumerExist = _consumerRepo.ByEmail(User.Identity?.Name);
            if (consumerExist != null)
            {
                return RedirectToAction("Details", new { id = consumerExist.ConsumerId });
            }

            return View(new Consumer
            {
                ConsumerId = Generate10DigitId(), 
                Photo = "Default.png",
                Email = User.Identity?.Name,
                RegistrationDate = DateTime.Now
            });
        }

        private string Generate10DigitId()
        {
            var random = new Random();
           
            return random.Next(0, 1000000000).ToString("D10");
        }

        [Authorize(Roles = "Consumer,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Consumer consumer)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string upload = webRootPath + WebConstants.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    await files[0].CopyToAsync(fileStream);
                }
                consumer.Photo = fileName + extension;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    consumer.Email = User.Identity?.Name;
                    _consumerRepo.Create(consumer);

                    var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                    var signInManager = HttpContext.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
                    var user = await userManager.FindByEmailAsync(consumer.Email);

                    if (user != null)
                    {
                     
                        var currentRoles = await userManager.GetRolesAsync(user);
                        await userManager.RemoveFromRolesAsync(user, currentRoles);

                        await userManager.AddToRoleAsync(user, "Consumer");

                        await signInManager.RefreshSignInAsync(user);
                    }

                    var createdConsumer = _consumerRepo.ByEmail(User.Identity?.Name);
                    return RedirectToAction("Details", new { id = createdConsumer?.ConsumerId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Unable to save consumer record: {ex.Message}");
                }
            }

            return View(consumer);
        }

        [Authorize(Roles = "Consumer")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var consumer = _consumerRepo.Details(id);
            if (consumer == null)
            {
                return NotFound();
            }

            if (consumer.Email != User.Identity?.Name)
            {
                return Forbid();
            }

            return View(consumer);
        }

        [Authorize(Roles = "Consumer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string photoName, Consumer consumer)
        {
            if (consumer.Email != User.Identity?.Name)
            {
                return Forbid();
            }

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                string upload = webRootPath + WebConstants.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                var oldFile = Path.Combine(upload, photoName);
                if (System.IO.File.Exists(oldFile) && photoName != "Default.png")
                {
                    System.IO.File.Delete(oldFile);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                consumer.Photo = fileName + extension;
            }
            else
            {
                consumer.Photo = photoName;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _consumerRepo.Edit(consumer);
                    return RedirectToAction("Details", new { id = consumer.ConsumerId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");
                }
            }

            return View(consumer);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var consumer = _consumerRepo.Details(id);
            if (consumer == null)
            {
                return NotFound();
            }

            return View(consumer);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var consumer = _consumerRepo.Details(id);
            if (consumer == null)
            {
                return NotFound();
            }

            try
            {
                if (consumer.Photo != "Default.png")
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string upload = webRootPath + WebConstants.ImagePath;
                    var oldFile = Path.Combine(upload, consumer.Photo);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }

                _consumerRepo.Delete(consumer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Unable to delete consumer: {ex.Message}");
                return View(consumer);
            }
        }
    }
}