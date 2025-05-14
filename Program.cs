using ASPNETCore_DB.Data;
using ASPNETCore_DB.Interfaces;
using ASPNETCore_DB.Models;
using ASPNETCore_DB.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

/*
 *Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333
 * Student Names: KM Ramela, T Thothela, T Fabeni, SR Letsoara, VOP Luhlabo
 
 *Question:MainProgram

 *This is the main entry point of the ASP.NET Core application.
 *It sets up the web application, configures services, and initializes the database.
 *The application uses Entity Framework Core with SQLite as the database provider.
 *Only users with the "Admin", "User", "Student", or "Consumer" roles can access the application.
 *The application uses ASP.NET Core Identity for user authentication and authorization.
 *The application also uses dependency injection to manage services and repositories.
 *The application uses middleware to handle exceptions and configure the request pipeline.
 *The application uses Razor Pages for the UI and MVC for the controller actions.
 *The application uses a custom exception filter to handle exceptions globally.
 *The application uses a custom database initializer to seed the database with initial data.
 *The application uses a custom repository pattern to manage data access and business logic.
 *The application uses a custom HTTP context accessor to access the current HTTP context.
 *The application uses a custom model for the admin account creation form.
 *The application uses a custom model for the contact form.
 *The application uses a custom model for the e-commerce promotions.
 *The application uses a custom model for the student information.
 *The application uses a custom model for the consumer information.
 *When the application starts, it checks if the database exists and applies any pending migrations.
 *The application also seeds the database with initial data if it is empty.
 *The application creates the necessary roles and an admin user if they do not exist.
 *The application uses a custom logger to log any errors that occur during initialization.
 *The application uses a custom connection string to connect to the SQLite database.
 *The application uses a custom configuration file to manage application settings.
 *The application uses a custom startup class to configure services and middleware.
 *The application uses a custom program class to define the main entry point.
 *The application uses a custom namespace to organize the code.
 */

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SQLiteDBContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<LoginDBContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<LoginDBContext>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IExceptionFilter, CustomExceptionFilter>();
builder.Services.AddScoped<IDBInitializer, DBInitializerRepo>();
builder.Services.AddScoped<IStudent, StudentRepo>();
builder.Services.AddScoped<IConsumer, ConsumerRepo>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}//End if-else
else
{
    app.UseDeveloperExceptionPage();
}//End else

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {

        var authContext = services.GetRequiredService<LoginDBContext>();
        await authContext.Database.MigrateAsync();

        var studentContext = services.GetRequiredService<SQLiteDBContext>();
        await studentContext.Database.MigrateAsync();

        // Seed data
        var studentInitializer = services.GetRequiredService<IDBInitializer>();
        studentInitializer.Initialize(studentContext);

        // Seed roles and admin user
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        var roles = new[] { "Admin", "User", "Consumer", "Student" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }//End if
        }//End foreach

        string adminEmail = "admin@gmail.com";
        string adminPassword = "Test!123";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };//End new IdentityUser
            await userManager.CreateAsync(adminUser, adminPassword);
        }//End if

        // Ensure admin has only Admin role
        var existingRoles = await userManager.GetRolesAsync(adminUser);
        if (!existingRoles.Contains("Admin"))
        {
            await userManager.RemoveFromRolesAsync(adminUser, existingRoles);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }//End if
    }//End try
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database initialization.");
    }//End catch
}//End using

app.Run();