using ASPNETCore_DB.Data;
using ASPNETCore_DB.Interfaces;
using ASPNETCore_DB.Models;

/**
*
*Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333
* Student Names: KM Ramela, T Thothela, T Fabeni, SR Letsoara, VOP Luhlabo
 
 *Question:IDBInitializer

 *This interface defines the contract for a database initializer.
 *This interface provides a method to initialize the database context.
 *This interface is used to seed the database with initial data.
 *This interface is used to configure the database context.
 *This interface is used to manage the database schema.
 *Only the IDBInitializer interface is defined in this file.
 */

namespace ASPNETCore_DB.Interfaces
{
    public interface IDBInitializer
    {
        void Initialize(SQLiteDBContext context);
    }//End interface
}//End namespace
