using ASPNETCore_DB.Data;
using ASPNETCore_DB.Interfaces;
using ASPNETCore_DB.Models;
using System;
using System.Linq;

/*
*
*Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333
* Student Names: KM Ramela, T Thothela, T Fabeni, SR Letsoara, VOP Luhlabo
* 
*/

namespace ASPNETCore_DB.Repositories
{
    public class DBInitializerRepo : IDBInitializer
    {
        public void Initialize(SQLiteDBContext context)
        {
            context.Database.EnsureCreated();

          
            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student{StudentNumber="221000001",FirstName="Alexander",Surname = "May",
                EnrollmentDate=DateTime.Parse("2021-02-03"),Photo="DefaultPic.png",Email="DefaultEmail@gmail.com"},
                new Student{StudentNumber="212000002",
                FirstName="Meredith",Surname="Alonso",EnrollmentDate=DateTime.Parse("2021-02-01"),Photo="DefaultPic.png",Email="DefaultEmail@gmail.com"},
                new Student{StudentNumber="221000003",
                FirstName="Arturo",Surname="Anand",EnrollmentDate=DateTime.Parse("2021-02-04"),Photo="DefaultPic.png",Email="DefaultEmail@gmail.com"}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }

            var consumers = new Consumer[]
            {
    new Consumer{ConsumerId="789900",Name="katlego phamela",
    Email="phamela@gmail.com.com",Photo="DefaultPic.png",Address="1 king edward",Phone="0876753451",RegistrationDate=DateTime.Parse("2024-02-03")},
    new Consumer{ConsumerId="556666",
    Name="koena ramela",Email="koena@gmail.com",Photo="DefaultPic.png",Address="8 victoria ",Phone="0791345671",RegistrationDate=DateTime.Parse("2019-02-01")},
    new Consumer{ConsumerId="55666",
    Name="peter madiope",Email="peter@gmail.com",Photo="DefaultPic.png",Address="7 church",Phone="0876785467",RegistrationDate=DateTime.Parse("2021-02-04")}
            };
            foreach (Consumer c in consumers)
            {
                context.Consumers.Add(c);
            }
            context.SaveChanges();

            context.SaveChanges();
        }
    }
}