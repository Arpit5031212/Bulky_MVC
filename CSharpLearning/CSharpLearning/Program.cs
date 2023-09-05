using BankProject.Presentation;
using System.Reflection.Emit;

class Program
{
    static void Main()
    {
        Console.WriteLine("********pascal bank********");
        Console.WriteLine("::Login Page::");

        String? userName = null;
        String? password = null;
        
        loginCreds:
        Console.WriteLine("Username:");
        userName = Console.ReadLine();

        if(userName != null) {
            Console.WriteLine("Password:");
            password = Console.ReadLine();
        }

        if (userName == "system" && password == "manager")
        {
            int mainMenuChoice = -1;

            do
            {
                Console.WriteLine(":::Main menu:::");
                Console.WriteLine("1. Customers");
                Console.WriteLine("2. Accounts");
                Console.WriteLine("3. Funds Transfer");
                Console.WriteLine("4. Funds Transfer Statement");
                Console.WriteLine("5. Account Statement");
                Console.WriteLine("0. EXIT");

                Console.Write("Enter Choice: ");
                mainMenuChoice = int.Parse(Console.ReadLine());

                switch (mainMenuChoice)
                {
                    case 1:
                        CustomersMenu();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 0:
                        break;
                } 
            } while (mainMenuChoice != 0);

        }
        else
            {
                
                Console.Clear();
                Console.WriteLine("********pascal bank********");
                Console.WriteLine("::Login Page::");
                Console.WriteLine("Wrong Credentials. Please Try again");
                goto loginCreds;
  
            }
        

        Console.WriteLine("Thank you! Visit again.");
        System.Console.ReadKey();

        static void CustomersMenu()
        {
            int customerMenuChoice = -1;

            do
            {
                Console.WriteLine(":::Customer Menu:::");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Delete Customer");
                Console.WriteLine("3. Update Customer");
                Console.WriteLine("4. Search Customers");
                Console.WriteLine("5. View Customers");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("Enter Choice: ");
                customerMenuChoice = Convert.ToInt32(Console.ReadLine());

                // switch case
                switch(customerMenuChoice)
                {
                    case 1: CustomerPresentation.AddCustomer(); break;
                    case 5: CustomerPresentation.ViewCustomers(); break;

                }
            } while (customerMenuChoice != 0);
        }



    }
}