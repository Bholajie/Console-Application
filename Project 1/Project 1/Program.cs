using Project_1;

List<Admin> admin = new List<Admin>();

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("***********************************");
Console.WriteLine("* Authentication Console Application *");
Console.WriteLine("***********************************");
Console.ForegroundColor = ConsoleColor.White;


string userSelection;

do
{
    Console.ForegroundColor = ConsoleColor.Cyan;

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("********************");
    Console.WriteLine("* Select an action *");
    Console.WriteLine("********************");

    Console.WriteLine("1: Admin user.");
    Console.WriteLine("2. Normal user");
    Console.WriteLine("3. Quit application");


    userSelection = Console.ReadLine();

    switch (userSelection)
    {
        case "1":
            User.LoginAdminUser();
            break;
        case "2":
            User.RegularUser();
            break;
        case "3": break;
        default:
            Console.WriteLine("Invalid selection");
            break;
    }
}
while (userSelection != "3");

Console.WriteLine("Thanks for using the application");
