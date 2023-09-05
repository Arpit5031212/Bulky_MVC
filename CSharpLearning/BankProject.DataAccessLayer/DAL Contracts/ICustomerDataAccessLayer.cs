using BankProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.DataAccessLayer.Contracts
{
    /// <summary>
    /// Interface that represents customer data access layer
    /// </summary>
    public interface ICustomerDataAccessLayer
    {
        /// <summary>
        /// Returns all existing customers
        /// </summary>
        /// <returns></returns>
        List<Customer> GetCustomers();

        /// <summary>
        /// Return a set of customers that matches with specified criteria
        /// </summary>
        /// <param name="predicate">Lambda expression that contains condition to check</param>
        /// <returns>The list of matching customers</returns>
        List<Customer> GetCustomersByCondition(Predicate<Customer> predicate);
        /// <summary>
        /// Adds a new customer to the existing customer list
        /// </summary>
        /// <param name="customer">the customer object to add</param>
        /// <returns>returns true, that indicates the customer is added successfully</returns>
        Guid AddCustomer(Customer customer);
        /// <summary>
        /// Updates a existing customer
        /// </summary>
        /// <param name="customer">customer object that contains the customers details to update</param>
        /// <returns>Returns True, that indicates the customer is updated successfully.</returns>
        bool UpdateCustomer(Customer customer);
        /// <summary>
        /// Deletes an existing customer
        /// </summary>
        /// <param name="customerId">Guid of the user to be deleted</param>
        /// <returns>Return true, that indicates the customer is deleted succesfully</returns>
        bool DeleteCustomer(Guid customerId);
    }
}
