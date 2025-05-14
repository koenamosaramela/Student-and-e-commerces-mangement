using ASPNETCore_DB.Models;


/*
*
*Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333
* Student Names: KM Ramela, T Thothela, T Fabeni, SR Letsoara, VOP Luhlabo
 
 *Question: IStudent

 *This interface defines the contract for a student repository.
 *This interface provides methods to manage student data.
 *This interface provides methods to retrieve, create, edit, and delete student records.
 */
namespace ASPNETCore_DB.Interfaces
{
    public interface IStudent
    {
        IQueryable<Student> GetStudents(string searchString, string sortOrder);
        Student Details(string id);
        Student ByEmail(string id);
        Student Create(Student student);
        Student Edit(Student student);
        bool Delete(Student student);
        bool IsExist(string id);
    }//End interface
}//End namespace
