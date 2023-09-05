using BankProject.DataAccessLayer.Contracts;
using BankProject.Entities;
using BankProject.Exceptions;
using System.Linq.Expressions;

namespace BankProject.DataAccessLayer
{
    /// <summary>
    /// Represents DAL for bank customers
    /// </summary>
    public class CustomerDataAccessLayer: ICustomerDataAccessLayer
    {
        #region Fields
        private static List<Customer> _customer;
        #endregion

        #region Properties
        private static List<Customer> Customers
        {
            set => _customer = value;
            get => _customer;
        }
        #endregion

        #region Constructors
        static CustomerDataAccessLayer()
        {
            _customer = new List<Customer>();
        }
        #endregion

        #region Methods
        public List<Customer> GetCustomers()
        {
            try
            {
                //Create a new customer list
                List<Customer> customerList = new List<Customer>();

                //copy all the customers from the source collection into the customers list
                Customers.ForEach(item => customerList.Add(item.Clone() as Customer));
                return customerList;
            }
            catch(CustomerException) { throw; }
            catch (Exception)
            {
                throw;
            }
           
        }

        /// <summary>
        /// Returns list of customers that are matching with speific criteria
        /// </summary>
        /// <param name="predicate">Lambda expression with condition</param>
        /// <returns>List of matching customers</returns>
        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate) 
        {
            try
            {
                //create a new customer list
                List<Customer> customersList = new List<Customer>();

                // filter the collection
                List<Customer> filteredCustomers = Customers.FindAll(predicate);

                // copy all customers from source collection into the new customer list
                filteredCustomers.ForEach(item => customersList.Add(item.Clone() as Customer));
                return customersList;
            }
            catch(CustomerException) { throw; }
            catch(Exception) { throw; }
            
            
        }
        public Guid AddCustomer(Customer customer)
        {

            try
            {
                //generate new Guid
                customer.CustomerID = Guid.NewGuid();

                // Add customer
                Customers.Add(customer);

                return customer.CustomerID;
            }
            catch(CustomerException) { throw; }
            catch(Exception) { throw; }
           

        }

        public bool UpdateCustomer(Customer customer)
        {

            try
            {
                // find existing customer by CustomerID
                Customer existingCustomer = Customers.Find(item => item.CustomerID == customer.CustomerID);

                if (existingCustomer != null)
                {
                    existingCustomer.CustomerCode = customer.CustomerCode;
                    existingCustomer.CustomerName = customer.CustomerName;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.City = customer.City;
                    existingCustomer.Country = customer.Country;
                    existingCustomer.Mobile = customer.Mobile;

                    return true;
                }
                else return false;
            }
            catch (CustomerException) { throw; }
            catch (Exception) { throw; }
           
        }

        /// <summary>
        /// Deletes an existing customer based on CustomerID
        /// </summary>
        /// <param name="customerID">CustomerID to delete</param>
        /// <returns>indicates whether the customer is deleted or not</returns>
        public bool DeleteCustomer(Guid customerID)
        {
            try
            {
                if(Customers.RemoveAll(item => item.CustomerID == customerID) > 0) 
                {
                    return true;
                }
                else { return false; }
            }
            catch(CustomerException) { throw; }
            catch(Exception) { throw; }
        }
        #endregion
    }
}