using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    internal class User
    {
        static List<Admin> admin = new() { new Admin { Email = "bolaji@mail.com", Password = "bolaji" } };
        static  List<Admin> regularAdmin = new();
        static Admin currentUser;

 
        static internal void LoginAdminUser()
        {
            //string Email = "bolaji@mail.com";
            //string Password = "bolaji";
            /*var admins = new Admin
            {
                Email = "bolaji@mail.com",
                Password = "bolaji",
            };*/
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            currentUser = admin.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (currentUser == null) Console.WriteLine("Incorrect email or password");
            else
            {
                Console.WriteLine("You have logged successfully as an admin");
                AdminUser();
            }

        }
        internal static void AdminUser()
        {
           
            string userSelection;

            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("********************");
                Console.WriteLine("* This is the admin page *");
                Console.WriteLine("********************");

                Console.WriteLine("1: Register.");
                Console.WriteLine("2: Login.");
                Console.WriteLine("3. Print all users");
                Console.WriteLine("4. Delete a user");
                Console.WriteLine("5. Print all phone numbers");
                Console.WriteLine("6. Update phone numbers");
                Console.WriteLine("7. Logout");


                userSelection = Console.ReadLine();
                switch (userSelection)
                {
                    case "1":
                        RegisterUser(admin);
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        PrintUsersInJSON();
                        break;
                    case "4":
                        DeleteUser();
                        break;
                    case "5":
                        PrintPhoneNumbersInJSON();
                        break;
                    case "6":
                        UpdatePhoneNumber();
                        break;
                    case "7":
                        break;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
            }
            while (userSelection != "7");

            Console.WriteLine("Thanks for using the application");
        }

        static internal  void RegularUser()
        {


            string userSelection;

            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("********************");
                Console.WriteLine("* This is the admin page *");
                Console.WriteLine("********************");

                Console.WriteLine("1: Register.");
                Console.WriteLine("2: Login.");
                Console.WriteLine("3. Print all phone numbers");
                Console.WriteLine("4. Update phone numbers");
                Console.WriteLine("5. Logout");


                userSelection = Console.ReadLine();
                switch (userSelection)
                {
                    case "1":
                        RegisterUser(regularAdmin);
                        break;
                    case "2":
                        // Login();
                        RegularLogin();
                        break;
                    case "3":
                        PrintPhoneNumbersInJSON();
                        break;
                    case "4":
                        UpdatePhoneNumber();
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
            }
            while (userSelection != "5");

            Console.WriteLine("Thanks for using the application");
        }

        static void Login()
        {
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            currentUser = admin.FirstOrDefault(admin => admin.Email == email && admin.Password == password);
            if (currentUser != null)
            {
                Console.WriteLine("Success");
            }else
            {
                Console.WriteLine("Incorrect password or email");
            }

        }

        static void RegularLogin()
        {
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            currentUser = regularAdmin.FirstOrDefault(admin => admin.Email == email && admin.Password == password);
            if (currentUser != null)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Incorrect password or email");
            }

        }

        static void PrintUsersInJSON()
        {
            StringBuilder sb = new StringBuilder();
            string json = JsonConvert.SerializeObject(admin, Newtonsoft.Json.Formatting.Indented);
            string json2 = JsonConvert.SerializeObject(regularAdmin, Newtonsoft.Json.Formatting.Indented);
            sb.Append(json);
            sb.Append(json2);
            Console.WriteLine("Users in JSON format:");
            Console.WriteLine(sb);
        }

        static void PrintPhoneNumbersInJSON()
        {
            foreach(var eachAdmin in admin)
            {
                string json = JsonConvert.SerializeObject(eachAdmin.PhoneNumber, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json);
            }

            foreach (var eachUser in regularAdmin)
            {
                string json = JsonConvert.SerializeObject(eachUser.PhoneNumber, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json);
            }

        }

        internal static void RegisterUser(List<Admin> admins)
        {
            Console.WriteLine("Creating a user");

            Guid id = Guid.NewGuid();

            Console.Write("Enter the first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter the last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter your email: ");
            string email = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            DateTime creationDate = DateTime.Now;

            var admin = new Admin
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                CreationDate = creationDate,
            };

            AddPhoneNumbers(admin);

            admins.Add(admin);
            Console.WriteLine("You have successfully registered!");
        }

        static void AddPhoneNumbers(Admin admin)
        {
            while (admin.PhoneNumber.Count < 3)
            {
                Console.Write("Enter your phone number: ");
                int phoneNumber = int.Parse(Console.ReadLine());
                admin.PhoneNumber.Add(phoneNumber);

                if (admin.PhoneNumber.Count >= 3)
                {
                    Console.WriteLine("You already have the maximum allowed phone numbers (3).");
                    break;
                }


                Console.WriteLine("Do you want to add another number? (Yes or No)");
                var answer = Console.ReadLine();

                if (answer.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    // User doesn't want to add another number, so we exit the loop.
                    break;
                }
                else if (!answer.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Invalid selection. Please enter 'Yes' or 'No'.");
                }
            }
        }
         static void UpdatePhoneNumber()
        {
            string json = JsonConvert.SerializeObject(currentUser.PhoneNumber, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

            Console.WriteLine("Enter the old phone number you want to update");
            int oldPhoneNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new phone number");
            int newPhoneNumber = int.Parse(Console.ReadLine());

            int index = currentUser.PhoneNumber.IndexOf(oldPhoneNumber);

            if (index != -1)
            {
                currentUser.PhoneNumber[index] = newPhoneNumber;
                Console.WriteLine("Phone number updated successfully.");
            }
            else
            {
                Console.WriteLine("Phone number not found.");
            }
        }

        static void DeleteUser()
        {
                Console.WriteLine("Enter the email of the user you want to delete:");
                string email = Console.ReadLine();

                currentUser = admin.FirstOrDefault(admin => admin.Email == email);

            if (currentUser != null)
            {
                admin.Remove(currentUser);
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

    }
}
