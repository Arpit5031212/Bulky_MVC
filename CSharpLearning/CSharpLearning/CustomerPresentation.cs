using BankProject.BusinessLayer;
using BankProject.BusinessLogicLayer.BLL_Contracts;
using BankProject.Entities;

namespace BankProject.Presentation
{
    static class CustomerPresentation
    {
        internal static void AddCustomer()
        {
            try
            {
                // Create an object of customer
                Customer customer = new Customer();

                //read all details from the user
                Console.WriteLine("\n*******ADD CUSTOMER*******");
                Console.Write("Customer Name: ");
                customer.CustomerName = Console.ReadLine();
                Console.Write("Address: ");
                customer.Address = Console.ReadLine();
                Console.Write("Landmark: ");
                customer.Landmark = Console.ReadLine();
                Console.Write("City: ");
                customer.City = Console.ReadLine();
                Console.Write("Country: ");
                customer.Country = Console.ReadLine();
                Console.Write("Mobile: ");
                customer.Mobile = Console.ReadLine();

                //Create BL object
                ICustomerBusinessLogicLayer customersBusinessLogicLayer = new CustomerBusinessLogicLayer();
                Guid newGuid = customersBusinessLogicLayer.AddCustomer(customer);

                List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(item => item.CustomerID == newGuid);
                if(matchingCustomers.Count >= 1) 
                {
                    Console.WriteLine("New Customer Code: " + matchingCustomers[0].CustomerCode);
                    Console.WriteLine("Customer Added.\n");
                }

                Console.WriteLine(newGuid);
                Console.WriteLine("Customer Added.\n");
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
  
        }

        internal static void ViewCustomers()
        {
            try
            {
                //Create BL object
                ICustomerBusinessLogicLayer customerBusinessLogicLayer = new CustomerBusinessLogicLayer();

                List<Customer> allCustomers = customerBusinessLogicLayer.GetCustomers();
                Console.WriteLine("\n*******ALL CUSTOMERS*******");


                //read all customers

                foreach(var item in allCustomers)
                {
                    Console.WriteLine("Customer Code: " + item.CustomerCode);
                    Console.WriteLine("Customer Name: " + item.CustomerName);
                    Console.WriteLine("Address: " + item.Address);
                    Console.WriteLine("Landmark: " + item.Landmark);
                    Console.WriteLine("City: " + item.City);
                    Console.WriteLine("Country: " + item.Country);
                    Console.WriteLine("Mobile: " + item.Mobile);
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }
    }
}
