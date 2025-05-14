using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/**
 *Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333
 * Student Names: KM Ramela, T Thothela, T Fabeni, SR Letsoara, VOP Luhlabo
 
 *Question:LoginDBContextModelSnapshot

 *This migration file is responsible for creating the initial database schema for the authentication system.
 *It defines the structure of the authentication tables, including roles, users, claims, logins, and tokens.
 */
namespace ASPNETCore_DB.Data
{
    public class LoginDBContext : IdentityDbContext
    {
        public LoginDBContext(DbContextOptions<LoginDBContext> options) : base(options)
        {
        }
    }
}