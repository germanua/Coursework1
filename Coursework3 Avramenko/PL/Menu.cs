using BLL;
using DAL;

namespace PL
{
    public class Menu
    {
        private readonly DishService dishService;
        private readonly IngredientService ingredientService;
        private readonly OrderService orderService;
        private Order tempOrder = new();

        public Menu(DishService dishService, OrderService orderService, IngredientService ingredientService)
        {
            this.dishService = dishService;
            this.ingredientService = ingredientService;
            this.orderService = orderService;
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
                Console.WriteLine("5. Submit Order");
                Console.WriteLine("6. Get Receipts");
                Console.WriteLine("7. Back to Main Menu");

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
                            ChooseDishes();
                            break;
                        case 4:
                            ChoosePaymentMethod();
                            break;
                        case 5:
                            SubmitOrder();
                            break;
                        case 6:
                            GetReceipts();
                            break;
                        case 7:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
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
        void ViewMenu()
        {
            List<Dish> menu = dishService.Dishes;

            Console.WriteLine("Menu:");
            foreach (var dish in menu)
            {
                Console.WriteLine($"- {dish.Name} (${dish.Price})");
                foreach (int ingredientId in dish.Ingredients)
                {
                    Ingredient ingredient = ingredientService.GetById(ingredientId);
                    Console.WriteLine($" | {ingredient.Id} {ingredient.Name}");
                }
            }
        }
        void ChooseTable()
        {
            Console.Write("Enter your table number: ");
            if (int.TryParse(Console.ReadLine(), out int tableNumber))
            {
                tempOrder.TableNumber = tableNumber;
                Console.WriteLine($"Table {tableNumber} chosen.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid table number.");
            }
        }
        void ChooseDishes()
        {
            if (tempOrder.TableNumber < 0)
            {
                Console.WriteLine("No table selected!");
                return;
            }
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"You selected (${tempOrder.Total}, {tempOrder.EstimatedWaitingTime}min):");
                if (tempOrder.Dishes.Count > 0)
                {
                    int dishNumber = 1;
                    foreach (var dish in tempOrder.Dishes)
                    {
                        Console.WriteLine($"{dishNumber}. {dish.Name} (${dish.Price})");
                        foreach (int ingredientId in dish.Ingredients)
                        {
                            Ingredient ingredient = ingredientService.GetById(ingredientId);
                            Console.WriteLine($" | {ingredient.Name}");
                        }
                        dishNumber++;
                    }
                }
                else
                {
                    Console.WriteLine("Nothing to show");
                }
                Console.WriteLine("0 - exit, q - add new dish, ID - edit dish");
                string? UserInput = Console.ReadLine();
                if (string.IsNullOrEmpty(UserInput)) { continue; }
                switch (UserInput)
                {
                    case "0":
                        return;
                    case "Q":
                    case "q":
                        Console.Clear();

                        Console.WriteLine("Menu:");
                        foreach (var dish in dishService.Dishes)
                        {
                            Console.WriteLine($"- {dish.Name} (${dish.Price})");
                            foreach (int ingredientId in dish.Ingredients)
                            {
                                Ingredient ingredient = ingredientService.GetById(ingredientId);
                                Console.WriteLine($" | {ingredient.Id} {ingredient.Name}");
                            }
                        }
                        Console.WriteLine("0 - back, Name - add");
                        while (true)
                        {
                            string? UserDishInput = Console.ReadLine();
                            if (string.IsNullOrEmpty(UserDishInput)) { continue; }
                            if (UserDishInput == "0") { break; }
                            Dish? dish = dishService.FindByName(UserDishInput);
                            if (dish == null) { continue; }
                            tempOrder = OrderService.AddDish(dish, tempOrder);
                            Console.WriteLine($"Added {dish.Name}");
                        }
                        break;
                    default:
                        Dish? dishToEdit;
                        try { dishToEdit = tempOrder.Dishes[int.Parse(UserInput) - 1]; } catch (Exception) { continue; }
                        if (dishToEdit == null) { continue; }
                        while (true)
                        {
                            Console.Clear();

                            Console.WriteLine($"{dishToEdit.Name} (${dishToEdit.Price}):");
                            int ingredientNumber = 1;
                            foreach (int ingredientId in dishToEdit.Ingredients)
                            {
                                Ingredient ingredient = ingredientService.GetById(ingredientId);
                                Console.WriteLine($"{ingredientNumber}. {ingredient.Name}");
                                ingredientNumber++;
                            }
                            Console.WriteLine("0 - back, Id - remove ingredient, q - remove dish");
                            string? UserIngredientInput = Console.ReadLine();
                            if (string.IsNullOrEmpty(UserIngredientInput)) { continue; }
                            if (UserIngredientInput == "q") { tempOrder = OrderService.RemoveDish(int.Parse(UserInput) - 1, tempOrder); break; }
                            if (UserIngredientInput == "0") { break; }
                            int ingredientIndex;
                            try { ingredientIndex = int.Parse(UserIngredientInput) - 1; } catch (Exception) { continue; }
                            dishToEdit.Ingredients.RemoveAt(ingredientIndex);
                            Console.WriteLine($"Removed");
                        }
                        break;
                }
            }
        }
        void ChoosePaymentMethod()
        {
            // Implement logic for the user to choose a payment method
            Console.WriteLine("Payment options:");
            Console.WriteLine("1. Card");
            Console.WriteLine("2. Cash");

            Console.Write("Choose a payment method (1 for Card, 2 for Cash): ");
            if (int.TryParse(Console.ReadLine(), out int paymentChoice))
            {
                tempOrder.PaymentMethod = paymentChoice == 1 ? "Card" : "Cash";
                Console.WriteLine($"Payment Method: {(paymentChoice == 1 ? "Card" : "Cash")}");
            }
            else
            {
                Console.WriteLine("Invalid payment choice.");
            }
        }
        void SubmitOrder()
        {
            if (tempOrder.Dishes == null || tempOrder.Dishes.Count == 0)
            {
                Console.WriteLine("No dishes selected!");
                return;
            }
            if (tempOrder.TableNumber < 0)
            {
                Console.WriteLine("No table selected!");
                return;
            }
            if (String.IsNullOrEmpty(tempOrder.PaymentMethod))
            {
                Console.WriteLine("No payment method selected!");
                return;
            }

            orderService.Insert(tempOrder.TableNumber, tempOrder.PaymentMethod, tempOrder.EstimatedWaitingTime, tempOrder.Dishes, tempOrder.Total);
            tempOrder.EstimatedWaitingTime = 0;
            tempOrder.Dishes = new List<Dish>();

            Console.WriteLine("Submited!");
        }
        void GetReceipts()
        {
            if (tempOrder.TableNumber < 0)
            {
                Console.WriteLine("No table selected!");
                return;
            }
            List<Order> orders = orderService.GetByTable(tempOrder.TableNumber);

            if (orders.Count == 0)
            {
                Console.WriteLine("No order found. Please place an order first.");
                return;
            }

            Console.Clear();
            Console.WriteLine($"{orders.Count} Orders:");
            foreach (Order order in orders)
            {
                Console.WriteLine($"{order.Id}. ${order.Total} {order.EstimatedWaitingTime}min");
                foreach (Dish dish in order.Dishes)
                {
                    Console.WriteLine($"- {dish.Name} (${dish.Price}, {dish.CookingTime}min)");
                    foreach (int ingredientId in dish.Ingredients)
                    {
                        Ingredient ingredient = ingredientService.GetById(ingredientId);
                        Console.WriteLine($" | {ingredient.Id} {ingredient.Name}");
                    }
                }
            }
        }



        public void ShowAdminMenu()
        {
            while (true)
            {
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Dishes");
                Console.WriteLine("2. Ingredients");
                Console.WriteLine("3. Orders");
                Console.WriteLine("7. Back to Main Menu");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            EditDishes();
                            break;
                        case 2:
                            EditIngredients();
                            break;
                        case 3:
                            EditOrders();
                            break;
                        case 7:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
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
        void EditDishes()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Total: {dishService.Dishes.Count}");
                if (dishService.Dishes.Count > 0)
                {
                    int dishNumber = 1;
                    foreach (var dish in dishService.Dishes)
                    {
                        Console.WriteLine($"{dishNumber}. {dish.Name} (${dish.Price})");
                        foreach (int ingredientId in dish.Ingredients)
                        {
                            Ingredient ingredient = ingredientService.GetById(ingredientId);
                            Console.WriteLine($" | {ingredient.Name}");
                        }
                        dishNumber++;
                    }
                }
                else
                {
                    Console.WriteLine("Nothing to show");
                }
                Console.WriteLine("0 - exit, q - add new dish, ID - edit dish");
                string? DishNumber = Console.ReadLine();
                if (string.IsNullOrEmpty(DishNumber)) { continue; }
                switch (DishNumber)
                {
                    case "0":
                        return;
                    case "Q":
                    case "q":
                        Console.Clear();

                        Console.WriteLine("Enter name:");
                        string? DishName = Console.ReadLine();
                        while (string.IsNullOrEmpty(DishName))
                        {
                            Console.WriteLine("Wrong name, try again:");
                            DishName = Console.ReadLine();
                        }

                        Console.WriteLine("Enter price:");
                        string? DishPriceInput = Console.ReadLine();
                        decimal DishPrice;
                        while (string.IsNullOrEmpty(DishPriceInput) || !Decimal.TryParse(DishPriceInput, out DishPrice))
                        {
                            Console.WriteLine("Wrong price, try again:");
                            DishPriceInput = Console.ReadLine();
                        }

                        Console.WriteLine("Enter cooking time:");
                        string? CookingTimeInput = Console.ReadLine();
                        int CookingTime;
                        while (string.IsNullOrEmpty(CookingTimeInput) || !Int32.TryParse(CookingTimeInput, out CookingTime))
                        {
                            Console.WriteLine("Wrong time, try again:");
                            CookingTimeInput = Console.ReadLine();
                        }

                        dishService.Insert(new Dish(DishName, DishPrice, new(), CookingTime));

                        break;
                    default:
                        Dish? dishToEdit;
                        try { dishToEdit = dishService.Dishes[int.Parse(DishNumber) - 1]; } catch (Exception) { continue; }
                        if (dishToEdit == null) { continue; }
                        while (true)
                        {
                            Console.Clear();

                            Console.WriteLine($"{dishToEdit.Name} ${dishToEdit.Price} {dishToEdit.CookingTime}min:");
                            int ingredientNumber = 1;
                            foreach (int ingredientId in dishToEdit.Ingredients)
                            {
                                Ingredient ingredient = ingredientService.GetById(ingredientId);
                                Console.WriteLine($"{ingredientNumber}. {ingredient.Name}");
                                ingredientNumber++;
                            }
                            Console.WriteLine("0 - back, Id - remove ingredient, w - add ingredient, e - set price, r - set name, t - set cooking time, q - delete dish");
                            string? UserIngredientInput = Console.ReadLine();
                            if (string.IsNullOrEmpty(UserIngredientInput)) { continue; }
                            if (UserIngredientInput == "q") { dishService.Delete(int.Parse(DishNumber) - 1); break; }
                            if (UserIngredientInput == "w")
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Available ingredients:");
                                    foreach (Ingredient ingredient in ingredientService.Ingredients)
                                    {
                                        if (!dishToEdit.Ingredients.Contains(ingredient.Id))
                                        {
                                            Console.WriteLine($"{ingredient.Id}. {ingredient.Name}");
                                        }
                                    }
                                    Console.WriteLine("0 - back, Id - add ingredient");
                                    string? UserIngredientAddInput = Console.ReadLine();
                                    if (string.IsNullOrEmpty(UserIngredientAddInput)) { continue; }
                                    if (UserIngredientAddInput == "0") { break; }
                                    Ingredient? IngredientToAdd;
                                    try { IngredientToAdd = ingredientService.GetById(int.Parse(UserIngredientAddInput)); } catch (Exception) { continue; }
                                    if (IngredientToAdd == null) { continue; }
                                    dishToEdit.Ingredients.Add(IngredientToAdd.Id);
                                    dishService.Update(dishToEdit, int.Parse(DishNumber) - 1);
                                }
                                continue;
                            }
                            if (UserIngredientInput == "e")
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter new price:");
                                    string? NewDishPriceInput = Console.ReadLine();
                                    decimal NewDishPrice;
                                    while (string.IsNullOrEmpty(NewDishPriceInput) || !Decimal.TryParse(NewDishPriceInput, out NewDishPrice))
                                    {
                                        Console.WriteLine("Wrong price, try again:");
                                        NewDishPriceInput = Console.ReadLine();
                                    }
                                    dishToEdit.Price = NewDishPrice;
                                    dishService.Update(dishToEdit, int.Parse(DishNumber) - 1);
                                    break;
                                }
                                continue;
                            }
                            if (UserIngredientInput == "r")
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter new name:");
                                    string? NewDishName = Console.ReadLine();
                                    while (string.IsNullOrEmpty(NewDishName))
                                    {
                                        Console.WriteLine("Wrong name, try again:");
                                        NewDishName = Console.ReadLine();
                                    }
                                    dishToEdit.Name = NewDishName;
                                    dishService.Update(dishToEdit, int.Parse(DishNumber) - 1);
                                    break;
                                }
                                continue;
                            }
                            if (UserIngredientInput == "t")
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter new cooking time:");
                                    string? NewCookingTimeInput = Console.ReadLine();
                                    int NewCookingTime;
                                    while (string.IsNullOrEmpty(NewCookingTimeInput) || !Int32.TryParse(NewCookingTimeInput, out NewCookingTime))
                                    {
                                        Console.WriteLine("Wrong name, try again:");
                                        NewCookingTimeInput = Console.ReadLine();
                                    }
                                    dishToEdit.CookingTime = NewCookingTime;
                                    dishService.Update(dishToEdit, int.Parse(DishNumber) - 1);
                                    break;
                                }
                                continue;
                            }
                            if (UserIngredientInput == "0") { break; }
                            int ingredientIndex;
                            try { ingredientIndex = int.Parse(UserIngredientInput) - 1; } catch (Exception) { continue; }
                            dishToEdit.Ingredients.RemoveAt(ingredientIndex);
                            dishService.Update(dishToEdit, int.Parse(DishNumber) - 1);
                            Console.WriteLine($"Removed");
                        }
                        break;
                }
            }
        }
        void EditIngredients()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Total: {ingredientService.Ingredients.Count}");
                if (ingredientService.Ingredients.Count > 0)
                {
                    foreach (var ingredient in ingredientService.Ingredients)
                    {
                        Console.WriteLine($"{ingredient.Id}. {ingredient.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Nothing to show");
                }
                Console.WriteLine("0 - exit, q - add new ingredient, ID - edit ingredient");
                string? IngredientId = Console.ReadLine();
                if (string.IsNullOrEmpty(IngredientId)) { continue; }
                switch (IngredientId)
                {
                    case "0":
                        return;
                    case "Q":
                    case "q":
                        Console.Clear();

                        Console.WriteLine("Enter name:");
                        string? IngredientName = Console.ReadLine();
                        while (string.IsNullOrEmpty(IngredientName))
                        {
                            Console.WriteLine("Wrong name, try again:");
                            IngredientName = Console.ReadLine();
                        }

                        ingredientService.Insert(IngredientName);

                        break;
                    default:
                        Ingredient? ingredientToEdit;
                        try { ingredientToEdit = ingredientService.GetById(int.Parse(IngredientId)); } catch (Exception) { continue; }
                        if (ingredientToEdit == null) { continue; }
                        while (true)
                        {
                            Console.Clear();

                            Console.WriteLine($"{ingredientToEdit.Name}:");
                            Console.WriteLine("0 - back, e - set name, q - delete ingredient");
                            string? UserIngredientInput = Console.ReadLine();
                            if (string.IsNullOrEmpty(UserIngredientInput)) { continue; }
                            if (UserIngredientInput == "q")
                            {
                                ingredientService.DeleteById(int.Parse(IngredientId));
                                dishService.RemoveIngredientById(int.Parse(IngredientId));
                                break;
                            }
                            if (UserIngredientInput == "e")
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter new name:");
                                    string? NewIngredientName = Console.ReadLine();
                                    while (string.IsNullOrEmpty(NewIngredientName))
                                    {
                                        Console.WriteLine("Wrong name, try again:");
                                        NewIngredientName = Console.ReadLine();
                                    }
                                    ingredientToEdit.Name = NewIngredientName;
                                    ingredientService.UpdateById(ingredientToEdit, int.Parse(IngredientId));
                                    break;
                                }
                                continue;
                            }
                            if (UserIngredientInput == "0") { break; }
                        }
                        break;
                }
            }
        }
        void EditOrders()
        {
            int page = 1;
            int pageSize = 15;
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Page: {page}, Total orders: {orderService.Orders.Count}");
                if (orderService.Orders.Count > 0)
                {
                    int orderNumber = 1;
                    foreach (var order in orderService.Orders)
                    {
                        if (orderNumber > page * pageSize || orderNumber <= (page - 1) * pageSize) { break; }
                        Console.WriteLine($"{order.Id}. Table {order.TableNumber} ${order.Total} in {order.PaymentMethod} {order.EstimatedWaitingTime}min");
                        foreach (Dish dish in order.Dishes)
                        {
                            Console.WriteLine($"  - {dish.Name} (${dish.Price}, {dish.CookingTime}min)");
                            foreach (int ingredientId in dish.Ingredients)
                            {
                                Ingredient ingredient = ingredientService.GetById(ingredientId);
                                Console.WriteLine($"   | {ingredient.Name}");
                            }
                        }
                        orderNumber++;
                    }
                }
                else
                {
                    Console.WriteLine("Nothing to show");
                }
                Console.WriteLine("0 - exit, ID - edit order, p0 - change page");
                while (true)
                {
                    string? userOrderInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(userOrderInput)) { continue; }
                    if (userOrderInput == "0") { return; }
                    if (userOrderInput.StartsWith("p"))
                    {
                        try { page = int.Parse(userOrderInput.Substring(1)); } catch (Exception) { continue; }
                        break;
                    }
                    Order? orderToEdit;
                    try { orderToEdit = orderService.GetById(int.Parse(userOrderInput)); } catch (Exception) { continue; }
                    if (orderToEdit == null) { continue; }
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"{orderToEdit.Id}. Table {orderToEdit.TableNumber} ${orderToEdit.Total} in {orderToEdit.PaymentMethod} {orderToEdit.EstimatedWaitingTime}min");
                        foreach (Dish dish in orderToEdit.Dishes)
                        {
                            Console.WriteLine($"  - {dish.Name} (${dish.Price}, {dish.CookingTime}min)");
                            foreach (int ingredientId in dish.Ingredients)
                            {
                                Ingredient ingredient = ingredientService.GetById(ingredientId);
                                Console.WriteLine($"   | {ingredient.Name}");
                            }
                        }
                        Console.WriteLine("0 - back, e - set table number, y - set total, r - set payment method, t - set estimated waiting time, q - delete order, w - change dishes");
                        string? UserOrderInput = Console.ReadLine();
                        if (string.IsNullOrEmpty(UserOrderInput)) { continue; }
                        if (UserOrderInput == "0") { break; }
                        if (UserOrderInput == "q")
                        {
                            orderService.DeleteById(orderToEdit.Id);
                            break;
                        }
                        if (UserOrderInput == "e")
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Enter new table number:");
                                string? NewTableNumberInput = Console.ReadLine();
                                int NewTableNumber;
                                while (string.IsNullOrEmpty(NewTableNumberInput) || !Int32.TryParse(NewTableNumberInput, out NewTableNumber))
                                {
                                    Console.WriteLine("Wrong number, try again:");
                                    NewTableNumberInput = Console.ReadLine();
                                }
                                orderToEdit.TableNumber = NewTableNumber;
                                orderService.UpdateById(orderToEdit, orderToEdit.Id);
                                break;
                            }
                            continue;
                        }
                        if (UserOrderInput == "r")
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Enter new payment method (1 - Card, 2 - Cash):");
                                string? NewPaymentMethod = Console.ReadLine();
                                while (true)
                                {
                                    if (NewPaymentMethod == "1")
                                    {
                                        NewPaymentMethod = "Card";
                                        break;
                                    }
                                    if (NewPaymentMethod == "2")
                                    {
                                        NewPaymentMethod = "Cash";
                                        break;
                                    }
                                    Console.WriteLine("Wrong number, try again:");
                                    NewPaymentMethod = Console.ReadLine();
                                }
                                orderToEdit.PaymentMethod = NewPaymentMethod;
                                orderService.UpdateById(orderToEdit, orderToEdit.Id);
                                break;
                            }
                            continue;
                        }
                        if (UserOrderInput == "t")
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Enter new estimated waiting time:");
                                string? NewEstimatedWaitingTimeInput = Console.ReadLine();
                                int NewEstimatedWaitingTime;
                                while (string.IsNullOrEmpty(NewEstimatedWaitingTimeInput) || !Int32.TryParse(NewEstimatedWaitingTimeInput, out NewEstimatedWaitingTime))
                                {
                                    Console.WriteLine("Wrong number, try again:");
                                    NewEstimatedWaitingTimeInput = Console.ReadLine();
                                }
                                orderToEdit.EstimatedWaitingTime = NewEstimatedWaitingTime;
                                orderService.UpdateById(orderToEdit, orderToEdit.Id);
                                break;
                            }
                            continue;
                        }
                        if (UserOrderInput == "w")
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Change dishes");
                                int dishNumber = 1;
                                foreach (Dish dish in orderToEdit.Dishes)
                                {
                                    Console.WriteLine($"{dishNumber}. {dish.Name} (${dish.Price}, {dish.CookingTime}min)");
                                    foreach (int ingredientId in dish.Ingredients)
                                    {
                                        Ingredient ingredient = ingredientService.GetById(ingredientId);
                                        Console.WriteLine($" | {ingredient.Name}");
                                    }
                                    dishNumber++;
                                }
                                Console.WriteLine("0 - back, number - delete, q - new dish");
                                string? UserDishInput = Console.ReadLine();
                                if (string.IsNullOrEmpty(UserDishInput)) { continue; }
                                if (UserDishInput == "0") { break; }
                                if (UserDishInput == "q")
                                {
                                    Console.Clear();

                                    Console.WriteLine("Menu:");
                                    foreach (var dish in dishService.Dishes)
                                    {
                                        Console.WriteLine($"- {dish.Name} (${dish.Price})");
                                        foreach (int ingredientId in dish.Ingredients)
                                        {
                                            Ingredient ingredient = ingredientService.GetById(ingredientId);
                                            Console.WriteLine($" | {ingredient.Id} {ingredient.Name}");
                                        }
                                    }
                                    Console.WriteLine("0 - back, Name - add");
                                    while (true)
                                    {
                                        string? UserDishAddInput = Console.ReadLine();
                                        if (string.IsNullOrEmpty(UserDishAddInput)) { continue; }
                                        if (UserDishAddInput == "0") { break; }
                                        Dish? dish = dishService.FindByName(UserDishAddInput);
                                        if (dish == null) { continue; }
                                        orderToEdit = OrderService.AddDish(dish, orderToEdit);
                                        Console.WriteLine($"Added {dish.Name}");
                                        break;
                                    }
                                    continue;
                                }
                                int dishIndex;
                                try { dishIndex = int.Parse(UserDishInput) - 1; } catch (Exception) { continue; }
                                orderToEdit = OrderService.RemoveDish(dishIndex, orderToEdit);
                                orderService.UpdateById(orderToEdit, orderToEdit.Id);
                            }
                        }
                        if(UserOrderInput == "y")
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Enter new total:");
                                string? NewTotalInput = Console.ReadLine();
                                decimal NewTotal;
                                while (string.IsNullOrEmpty(NewTotalInput) || !Decimal.TryParse(NewTotalInput, out NewTotal))
                                {
                                    Console.WriteLine("Wrong number, try again:");
                                    NewTotalInput = Console.ReadLine();
                                }
                                orderToEdit.Total = NewTotal;
                                orderService.UpdateById(orderToEdit, orderToEdit.Id);
                                break;
                            }
                            continue;
                        }   
                    }
                    break;
                }
            }
        }
    }
}