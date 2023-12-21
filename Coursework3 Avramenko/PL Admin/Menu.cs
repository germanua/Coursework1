using BLL;

namespace PL
{
    public class Menu
    {
        private readonly RestaurantService restaurantService;
        private readonly FoodManager foodManager;
        private readonly IngredientManager ingredientManager;
        private readonly OrderManager orderManager;
        private readonly SearchManager searchManager;

        public Menu(
            RestaurantService restaurantService,
            FoodManager foodManager,
            IngredientManager ingredientManager,
            OrderManager orderManager,
            SearchManager searchManager)
        {
            this.restaurantService = restaurantService;
            this.foodManager = foodManager;
            this.ingredientManager = ingredientManager;
            this.orderManager = orderManager;
            this.searchManager = searchManager;
        }

        public void ShowUserMenu()
        {
            while (true)
            {
                Console.WriteLine("User Menu:");
                Console.WriteLine("1. View Menu");
                Console.WriteLine("2. Choose a Table");
                Console.WriteLine("3. Choose a Dish");
                Console.WriteLine("4. Choose a Payment Method");
                Console.WriteLine("5. Get Receipt");
                Console.WriteLine("6. Back to Main Menu");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ViewMenu();
                            break;
                        case 2:
                            ChooseTable();
                            break;
                        case 3:
                            ChooseDish();
                            break;
                        case 4:
                            ChoosePaymentMethod();
                            break;
                        case 5:
                            GetReceipt();
                            break;
                        case 6:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.WriteLine();
            }
        }

        public void ShowAdminMenu()
        {
            while (true)
            {
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Food Management");
                Console.WriteLine("2. Order Management");
                Console.WriteLine("3. Ingredient Management");
                Console.WriteLine("4. Search");
                Console.WriteLine("5. Save Data");
                Console.WriteLine("6. Back to Main Menu");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShowFoodManagementMenu();
                            break;
                        case 2:
                            ShowOrderManagementMenu();
                            break;
                        case 3:
                            ShowIngredientManagementMenu();
                            break;
                        case 4:
                            ShowSearchMenu();
                            break;
                        case 5:
                            SaveData();
                            break;
                        case 6:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.WriteLine();
            }
        }
    }
    }
