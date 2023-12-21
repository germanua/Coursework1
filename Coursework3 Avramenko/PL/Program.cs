using BLL;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            DishService dishService = new();
            OrderService orderService = new();
            IngredientService ingredientService = new();
            Menu menu = new(
                dishService,
                orderService,
                ingredientService
            );

            // Choose User or Admin
            Console.WriteLine("Choose your role:");
            Console.WriteLine("1. User");
            Console.WriteLine("2. Admin");
            Console.WriteLine("0. Exit");

            Console.Write("Enter your choice (1 for User, 2 for Admin, 0 for Exit): ");
            while (true)
            {
                string? input = Console.ReadLine();
                if (input == "0")
                {
                    break;
                }
                if (input == "1")
                {
                    menu.ShowUserMenu();
                    break;
                }
                if (input == "2")
                {
                    menu.ShowAdminMenu();
                    break;
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
            //if (int.TryParse(Console.ReadLine(), out int choice))
            //{
            //    switch (choice)
            //    {
            //        case 0:
            //            return;
            //        case 1:
            //            menu.ShowUserMenu();
            //            break;
            //        case 2:
            //            menu.ShowAdminMenu();
            //            break;
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Invalid input. Exiting.");
            //}
        }
    }
}
