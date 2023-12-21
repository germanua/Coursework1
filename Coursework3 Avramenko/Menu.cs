using System;
using System.Collections.Generic;
using BLL;
using DAL;

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

            /*  public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. User");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Exit");

                Console.Write("Enter your choice (1 for User, 2 for Admin, 3 to Exit): ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShowUserMenu();
                            break;
                        case 2:
                            ShowAdminMenu();
                            break;
                        case 3:
                            Console.WriteLine("Exiting the program. Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.WriteLine();
            }
        }*/

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

        private void ShowFoodManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("Food Management Menu:");
                Console.WriteLine("1. View Menu");
                Console.WriteLine("2. Add Dish");
                Console.WriteLine("3. Remove Dish");
                Console.WriteLine("4. Change Dish");
                Console.WriteLine("5. Save Menu");
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
                            AddDish();
                            break;
                        case 3:
                            RemoveDish();
                            break;
                        case 4:
                            ChangeDish();
                            break;
                        case 5:
                            SaveMenu();
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

        private void ShowOrderManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("Order Management Menu:");
                Console.WriteLine("1. View Orders");
                Console.WriteLine("2. Add Order");
                Console.WriteLine("3. Delete Order");
                Console.WriteLine("4. Change Order");
                Console.WriteLine("5. Save Orders");
                Console.WriteLine("6. Back to Main Menu");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ViewOrderInformation();
                            break;
                        case 2:
                            AddOrder();
                            break;
                        case 3:
                            DeleteOrder();
                            break;
                        case 4:
                            ChangeOrder();
                            break;
                        case 5:
                            SaveOrders();
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

        private void ShowIngredientManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("Ingredient Management Menu:");
                Console.WriteLine("1. View All Ingredients");
                Console.WriteLine("2. Add Ingredient");
                Console.WriteLine("3. Remove Ingredient");
                Console.WriteLine("4. Change Ingredient");
                Console.WriteLine("5. Back to Main Menu");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ViewAllIngredients();
                            break;
                        case 2:
                            AddIngredient();
                            break;
                        case 3:
                            RemoveIngredient();
                            break;
                        case 4:
                            ChangeIngredient();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
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

        private void ShowSearchMenu()
        {
            while (true)
            {
                Console.WriteLine("Search Menu:");
                Console.WriteLine("1. Search Ingredients by Keyword");
                Console.WriteLine("2. Search Dishes by Ingredient");
                Console.WriteLine("3. Search Orders by Keyword");
                Console.WriteLine("4. Back to Main Menu");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            SearchByKeywordAmongIngredients();
                            break;
                        case 2:
                            SearchByKeywordAmongDishes();
                            break;
                        case 3:
                            SearchByKeywordAmongOrders();
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
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

        private void ViewMenu()
        {
            var menu = restaurantService.GetMenu();

            Console.WriteLine("Menu:");
            foreach (var dish in menu)
            {
                Console.WriteLine($"- {dish.Name} (${dish.Price})");
            }
        }
        
        public void GetReceipt()
        {
            var orders = orderManager.ViewOrderInformation();

            if (orders.Count > 0)
            {
                var lastOrder = orders.Last(); // Get the last confirmed order

                Console.WriteLine("Receipt:");
                foreach (var dish in lastOrder.Dishes)
                {
                    Console.WriteLine($"- {dish.Name} (${dish.Price})");
                }

                Console.WriteLine($"Total Price: ${lastOrder.CalculateTotalPrice()}");
                Console.WriteLine($"Payment Method: {lastOrder.PaymentMethod}");
        
                // Display the estimated waiting time
                Console.WriteLine($"Estimated Waiting Time: {lastOrder.EstimatedWaitingTime} minutes");
            }
            else
            {
                Console.WriteLine("No order found. Please place an order first.");
            }
        }

         private void AddDish()
        {
            // Implement logic to add a dish
            Console.WriteLine("Enter the details for the new dish:");
            Dish newDish = CreateDish();
            foodManager.AddDish(newDish);
            Console.WriteLine($"{newDish.Name} has been added to the menu.");
        }

        private void RemoveDish()
        {
            // Implement logic to remove a dish
            Console.WriteLine("Enter the name of the dish to remove:");
            string dishNameToRemove = Console.ReadLine();
            foodManager.RemoveDish(dishNameToRemove);
            Console.WriteLine($"{dishNameToRemove} has been removed from the menu.");
        }

        private void ChangeDish()
        {
            // Implement logic to change a dish
            Console.WriteLine("Enter the name of the dish to change:");
            string existingDishName = Console.ReadLine();
            Dish existingDish = foodManager.ViewDishInformation(existingDishName);

            if (existingDish != null)
            {
                Console.WriteLine("Enter the new details for the dish:");
                Dish newDish = CreateDish();
                foodManager.ChangeDish(existingDish, newDish);
                Console.WriteLine($"{existingDishName} has been updated.");
            }
            else
            {
                Console.WriteLine($"Dish with name {existingDishName} not found.");
            }
        }

        private Dish CreateDish()
        {
            Console.WriteLine("Enter the name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the price:");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter the dish type (MainDish, Appetizer, Drink, Salad, Alcohol):");
            DishType type = (DishType)Enum.Parse(typeof(DishType), Console.ReadLine(), true);

            Console.WriteLine("Enter the cooking time:");
            int cookingTime = int.Parse(Console.ReadLine());

            List<Ingredient> ingredients = new List<Ingredient>();
            string addMoreIngredients;
            do
            {
                Console.WriteLine("Enter an ingredient name:");
                string ingredientName = Console.ReadLine();
                ingredients.Add(new Ingredient(ingredientName, 0));

                Console.WriteLine("Do you want to add another ingredient? (yes/no)");
                addMoreIngredients = Console.ReadLine().ToLower();
            } while (addMoreIngredients == "yes");

            return new Dish(name, price, ingredients, type, cookingTime);
        }

        private void AddOrder()
        {
            // Implement logic to add an order
            Console.WriteLine("Enter the details for the new order:");
            // Capture user input for order details and use it to create a new Order object
            Order newOrder = CreateOrder();
            orderManager.AddOrder(newOrder);
            Console.WriteLine($"Order for Table {newOrder.TableNumber} has been added.");
        }

        private void DeleteOrder()
        {
            // Implement logic to delete an order
            Console.WriteLine("Enter the table number of the order to delete:");
            int tableNumberToDelete = int.Parse(Console.ReadLine());
            Order orderToDelete = orderManager.ViewOrderInformation().Find(o => o.TableNumber == tableNumberToDelete);

            if (orderToDelete != null)
            {
                orderManager.DeleteOrder(orderToDelete);
                Console.WriteLine($"Order for Table {tableNumberToDelete} has been deleted.");
            }
            else
            {
                Console.WriteLine($"Order for Table {tableNumberToDelete} not found.");
            }
        }

        private void ChangeOrder()
        {
            // Implement logic to change an order
            Console.WriteLine("Enter the table number of the order to change:");
            int tableNumberToChange = int.Parse(Console.ReadLine());
            Order orderToChange = orderManager.ViewOrderInformation().Find(o => o.TableNumber == tableNumberToChange);

            if (orderToChange != null)
            {
                Console.WriteLine("Enter the new details for the order:");
                Order newOrder = CreateOrder();

                // Use the NumberOfDishes method from OrderManager to get the number of dishes
                int newDishCount = orderManager.NumberOfDishes(newOrder);

                orderManager.ChangeOrder(orderToChange, newDishCount, newOrder.CalculateTotalPrice(), newOrder.TableNumber);
                Console.WriteLine($"Order for Table {tableNumberToChange} has been updated.");
            }
            else
            {
                Console.WriteLine($"Order for Table {tableNumberToChange} not found.");
            }
        }


        private void ViewOrderInformation()
        {
            var orders = orderManager.ViewOrderInformation();

            Console.WriteLine("All Orders:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Table Number: {order.TableNumber}, Total Price: {order.CalculateTotalPrice()}");
            }
        }

        private void SaveData()
        {
            SaveMenu();
            SaveOrders();
        }

        private void SaveMenu()
        {
            restaurantService.SaveMenu();
            Console.WriteLine("Menu saved successfully.");
        }

        private void SaveOrders()
        {
            restaurantService.SaveOrders();
            Console.WriteLine("Orders saved successfully.");
        }

        private void ViewAllIngredients()
        {
            var ingredients = ingredientManager.ViewAllIngredients();

            Console.WriteLine("All Ingredients:");
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"- {ingredient.Name}");
            }
        }

        private void AddIngredient()
        {
            // Implement logic to add an ingredient
            Console.WriteLine("Enter the details for the new ingredient:");
            // Capture user input for ingredient details and use it to create a new Ingredient object
            Ingredient newIngredient = CreateIngredient();
            ingredientManager.AddIngredient(newIngredient);
            Console.WriteLine($"{newIngredient.Name} has been added to the ingredients.");
        }

        private void RemoveIngredient()
        {
            // Implement logic to remove an ingredient
            Console.WriteLine("Enter the name of the ingredient to remove:");
            string ingredientNameToRemove = Console.ReadLine();
            ingredientManager.RemoveIngredient(ingredientNameToRemove, new DishRepository());
            Console.WriteLine($"{ingredientNameToRemove} has been removed from the ingredients.");
        }

        private void ChangeIngredient()
        {
            // Implement logic to change an ingredient
            Console.WriteLine("Enter the name of the ingredient to change:");
            string existingIngredientName = Console.ReadLine();
            Ingredient existingIngredient = ingredientManager.ViewAllIngredients().Find(i => i.Name.Equals(existingIngredientName, StringComparison.OrdinalIgnoreCase));

            if (existingIngredient != null)
            {
                Console.WriteLine("Enter the new details for the ingredient:");
                Ingredient newIngredient = CreateIngredient();
                ingredientManager.ChangeIngredient(existingIngredient, newIngredient);
                Console.WriteLine($"{existingIngredientName} has been updated.");
            }
            else
            {
                Console.WriteLine($"Ingredient with name {existingIngredientName} not found.");
            }
        }

        private void SearchByKeywordAmongIngredients()
        {
            Console.Write("Enter keyword to search ingredients: ");
            string keyword = Console.ReadLine();

            var result = searchManager.SearchIngredientsByKeyword(keyword);

            Console.WriteLine("Search Results for Ingredients:");
            foreach (var ingredient in result)
            {
                Console.WriteLine($"- {ingredient.Name}");
            }
        }

        private void SearchByKeywordAmongDishes()
        {
            // Implement logic to search dishes by keyword among ingredients
            Console.WriteLine("Enter the keyword to search for among dishes:");
            string keyword = Console.ReadLine();
            List<Dish> resultDishes = searchManager.SearchDishesByIngredient(keyword);

            if (resultDishes.Count > 0)
            {
                Console.WriteLine($"Dishes containing the keyword '{keyword}':");
                foreach (var dish in resultDishes)
                {
                    Console.WriteLine($" - {dish.Name}");
                }
            }
            else
            {
                Console.WriteLine($"No dishes found containing the keyword '{keyword}'.");
            }
        }

        private void SearchByKeywordAmongOrders()
        {
            Console.Write("Enter keyword to search orders: ");
            string keyword = Console.ReadLine();

            var result = searchManager.SearchOrdersByKeyword(keyword);

            Console.WriteLine("Search Results for Orders:");
            foreach (var order in result)
            {
                Console.WriteLine($"Order for Table {order.TableNumber}");
            }
        }
      

        // Helper method to create an Order object based on user input
        private Order CreateOrder()
        {
            Console.WriteLine("Enter the table number:");
            int tableNumber = int.Parse(Console.ReadLine());

            Order newOrder = new Order(tableNumber);
            string addMoreDishes;
            do
            {
                Console.WriteLine("Enter the name of the dish to add to the order:");
                string dishName = Console.ReadLine();
                Dish dishToAdd = foodManager.ViewDishInformation(dishName);

                if (dishToAdd != null)
                {
                    newOrder.AddDish(dishToAdd);
                }
                else
                {
                    Console.WriteLine($"Dish with name {dishName} not found.");
                }

                Console.WriteLine("Do you want to add another dish to the order? (yes/no)");
                addMoreDishes = Console.ReadLine().ToLower();
            } while (addMoreDishes == "yes");

            return newOrder;
        }

        // Helper method to create an Ingredient object based on user input
        private Ingredient CreateIngredient()
        {
            Console.WriteLine("Enter the name of the ingredient:");
            string name = Console.ReadLine();

            return new Ingredient(name, 0);
        }
        private void ChooseDish()
        {
            // Implement logic for the user to choose a dish from the menu
            Console.Write("Enter the name of the dish to add to your order: ");
            string dishName = Console.ReadLine();

            var menu = restaurantService.GetMenu();
            var selectedDish = menu.Find(d => d.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase));

            if (selectedDish != null)
            {
                // Assume there's an Order instance for the user (you may need to handle this based on your program structure)
                // restaurantService.AddDishToOrder(order, selectedDish);
                Console.WriteLine($"{dishName} added to the order.");
            }
            else
            {
                Console.WriteLine($"Dish '{dishName}' not found in the menu.");
            }
        }

        private void ChooseTable()
        {
            Console.Write("Enter your table number: ");
            if (int.TryParse(Console.ReadLine(), out int tableNumber))
            {
                // Implement logic to handle the chosen table
                // You may want to store the chosen table number for later use
                Console.WriteLine($"Table {tableNumber} chosen.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid table number.");
            }
        }

        private void ChoosePaymentMethod()
        {
            // Implement logic for the user to choose a payment method
            Console.WriteLine("Payment options:");
            Console.WriteLine("1. Card");
            Console.WriteLine("2. Cash");

            Console.Write("Choose a payment method (1 for Card, 2 for Cash): ");
            if (int.TryParse(Console.ReadLine(), out int paymentChoice))
            {
                // Handle the chosen payment method (use restaurantService)
                Console.WriteLine($"Payment Method: {(paymentChoice == 1 ? "Card" : "Cash")}");
            }
            else
            {
                Console.WriteLine("Invalid payment choice.");
            }
        }

       
    }
}
