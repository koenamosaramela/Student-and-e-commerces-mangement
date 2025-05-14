using ASPNETCore_DB.Models;
using System.Linq;

/*
 * Student Numbers: 221001482,  222043497 ,  219010964 , 221013309 ,  221014333 
 * Student Names:  KM Ramela,   T Thothela,  T Fabeni,  SR Letsoara,  VOP Luhlabo
 
 *Question:IConsumer
 
 *This interface defines the contract for a consumer repository.
 *This interface provides methods to manage consumer data.
 *The GetConsumers method retrieves a list of consumers based on search and sort criteria.
 *The Details method retrieves a specific consumer by ID.
 *The ByEmail method retrieves a consumer by email.
 *The Create method adds a new consumer.
 *The Edit method updates an existing consumer.
 *The Delete method removes a consumer.
 *The IsExist method checks if a consumer exists by ID.
 *The IConsumer interface is used in the ConsumerController to manage consumer data.
 *The IConsumer interface is implemented by the ConsumerRepository class.
 *The ConsumerRepository class is responsible for data access and business logic related to consumers.
 *The IConsumer interface is used to decouple the controller from the data access layer.
 *The IConsumer interface allows for easier unit testing and mocking of the consumer repository.
 *The IConsumer interface is used to define the methods that the ConsumerRepository class must implement.
 *The IConsumer interface is used to provide a consistent API for managing consumer data.
 *The IConsumer interface is used to define the contract for the consumer repository.
 *The IConsumer interface is used to provide a clear separation of concerns in the application.
 *Only the IConsumer interface is defined in this file.
 */


namespace ASPNETCore_DB.Interfaces
{
    public interface IConsumer
    {
        IQueryable<Consumer> GetConsumers(string searchString, string sortOrder);
        Consumer Details(string id);
        Consumer ByEmail(string id);
        Consumer Create(Consumer consumer);
        Consumer Edit(Consumer consumer);
        bool Delete(Consumer consumer);
        bool IsExist(string id);
    }//End interface
}//End namespace