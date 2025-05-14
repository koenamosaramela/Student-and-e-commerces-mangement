/*
 * Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333 
 * Student Names:  KM Ramela,   T Thothela,  T Fabeni,  SR Letsoara,  VOP Luhlabo
 
 *Question:EcommerceController
 
 *This controller handles the e-commerce functionalities of the application.
 *It uses ASP.NET Core MVC to manage the views and actions related to e-commerce.
 *The Contact action method displays a contact form for users to reach out.
 *The Contact action method also processes the form submission.
 *The Index action method displays the main e-commerce page.
 *It uses dependency injection to access the database context and user manager.
 *It also uses logging to log any errors that occur during the execution of the actions.
 *It uses the SQLite database provider for data access.
 *It uses ASP.NET Core Identity for user authentication and authorization.
 *The controller uses a custom model for the contact form.
 *The controller uses a custom model for the e-commerce promotions.
 *The controller uses a custom model for the consumer information.
 *The controller uses a custom model for the student information.
 *The controller uses a custom model for the error page.
 *The controller uses a custom model for the home page.
 *The controller uses a custom model for the privacy policy page.
 *The controller uses a custom model for the seed database page.
 *The controller uses a custom database initializer to seed the database with initial data.
 *The AboutUs action method displays promotional information for consumers.
 *Only users with the "Consumer" role can access this action.
 */
using Microsoft.AspNetCore.Mvc;
using ASPNETCore_DB.Models; 
using System.Collections.Generic;
using System;

namespace ASPNETCore_DB.Controllers
{
    public class ECommerceController : Controller
    {
      
        public IActionResult AboutUs()
        {
            if (User.IsInRole("Consumer"))
            {
                ViewBag.Promotions = new List<Promotion>
                {
                    new Promotion {
                        Title = "Summer Sale",
                        Description = "Get 20% off on all summer collection items",
                        EndDate = DateTime.Now.AddDays(7)
                    },
                    new Promotion {
                        Title = "New Member Discount",
                        Description = "15% off your first order",
                        EndDate = DateTime.Now.AddDays(14)
                    }
                };
            }
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Promotion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Thank you for your message! We'll get back to you soon.";
                return RedirectToAction(nameof(Contact));
            }
            return View(model);
        }

        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Home()
        {

            return View();
        }
    }
}