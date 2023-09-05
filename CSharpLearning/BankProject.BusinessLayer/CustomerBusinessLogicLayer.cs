using BankProject.BusinessLogicLayer.BLL_Contracts;
using BankProject.DataAccessLayer;
using BankProject.DataAccessLayer.Contracts;
using BankProject.Entities;
using BankProject.Exceptions;

namespace BankProject.BusinessLayer
{
    /// <summary>
    /// Represents customer business logic
    /// </summary>
    public class CustomerBusinessLogicLayer : ICustomerBusinessLogicLayer
    {
        #region Private Fields
        private ICustomerDataAccessLayer _customerDataAccessLayer;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that initializes CustomerDAtaAccessLayer
        /// </summary>
        public CustomerBusinessLogicLayer()
        {
            _customerDataAccessLayer = new CustomerDataAccessLayer();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Private property that represents reference of Customer Data Access Layer
        /// </summary>
        
        private ICustomerDataAccessLayer CustomerDataAccessLayer
        {
            set => _customerDataAccessLayer = value;
            get => _customerDataAccessLayer;
        }
        #endregion

        #region Methods
        public Guid AddCustomer(Customer customer)
        {
            try
            {
                // get all customers
                List<Customer> allCustomers = CustomerDataAccessLayer.GetCustomers();
                long maxCustCode = 0;
                foreach(var item in allCustomers)
                {
                    if(item.CustomerCode > maxCustCode)
                    {
                        maxCustCode = item.CustomerCode;
                    }
                }

                //generate new customer no.
                if(allCustomers.Count >= 1)
                {
                    customer.CustomerCode = maxCustCode + 1;
                }
                else
                {
                    customer.CustomerCode = Configuration.Settings.BaseCustomerNo + 1;
                }
                //Invoke DAL
                return CustomerDataAccessLayer.AddCustomer(customer);
            }
            catch (CustomerException) { throw; }
            catch (Exception) { throw; }
        }

        public bool DeleteCustomer(Guid customerId)
        {
            try
            {
                //Invoke DAL
                return CustomerDataAccessLayer.DeleteCustomer(customerId);
            }
            catch (CustomerException) { throw; }
            catch (Exception) { throw; }
        }

        public List<Customer> GetCustomers()
        {
            try
            {
                //Invoke DAL
                return CustomerDataAccessLayer.GetCustomers();
            }
            catch (CustomerException) { throw; }
            catch (Exception) { throw; }
        }

        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            try
            {
                //Invoke DAL
                return CustomerDataAccessLayer.GetCustomersByCondition(predicate);
            }
            catch (CustomerException) { throw; }
            catch (Exception) { throw; }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                //Invoke DAL
                return CustomerDataAccessLayer.UpdateCustomer(customer);
            }
            catch (CustomerException) { throw; }
            catch (Exception) { throw; }
        }
        #endregion

    }
}