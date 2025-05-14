using ASPNETCore_DB.Models;
using Microsoft.EntityFrameworkCore;

/**
 *Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333
 * Student Names: KM Ramela, T Thothela, T Fabeni, SR Letsoara, VOP Luhlabo
 
 *Question:SQLiteDBContext

 *This migration file is responsible for creating the initial database schema for the application.
 *It defines the structure of the "Consumer" and "Student" tables.
 *The "Consumer" table stores information about consumers, including their ID, name, email, address, phone number, registration date, and photo.
 *The "Student" table stores information about students, including their student number, first name, surname, enrollment date, and photo.
 *The ConsumerId and StudentNumber are the primary keys for their respective tables.
 *
 */

namespace ASPNETCore_DB.Data
{
    public class SQLiteDBContext : DbContext
    {
        public SQLiteDBContext(DbContextOptions<SQLiteDBContext> options) : base(options)
        {

        }//End constructor

        public DbSet<Student> Students { get; set; }
        public DbSet<Consumer> Consumers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Consumer>().ToTable("Consumer");


        }//End method
    }//End class
}//End namespace