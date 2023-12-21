using DAL;

namespace BLL
{
    public class OrderService
    {
        List<Order> orders = new();
        public OrderRepository orderRepository = new();
        public OrderService()
        {
            orders = orderRepository.LoadOrders();
        }
        public Order? GetById(int Id) => orders.Find(i => i.Id == Id);
        public List<Order> GetByTable(int TableNumber) => new List<Order>(orders.Where(i => i.TableNumber == TableNumber));
        public Order Insert(int tableNumber, string paymentMethod, int estimatedWaitingTime, List<Dish> dishes, decimal total)
        {
            int Id = orders.Count == 0 ? 1 : orders[^1].Id + 1;
            Order NewOrder = new(Id, tableNumber, paymentMethod, estimatedWaitingTime, dishes, total);
            orders.Add(NewOrder);
            orderRepository.SaveOrders(orders);
            return NewOrder;
        }
        public static Order AddDish(Dish dish, Order order)
        {
            order.Dishes.Add(dish);
            order.EstimatedWaitingTime += dish.CookingTime;
            order.Total += dish.Price;
            return order;
        }
        public static Order RemoveDish(int index, Order order)
        {
            Dish dish = order.Dishes[index];
            order.Dishes.RemoveAt(index);
            order.EstimatedWaitingTime -= dish.CookingTime;
            order.Total -= dish.Price;
            return order;
        }
        public void UpdateById(Order input, int id)
        {
            orders[orders.FindIndex(el => el.Id == id)] = input;
            orderRepository.SaveOrders(orders);
        }
        public Order DeleteById(int id)
        {
            Order Removed = orders[orders.FindIndex(el => el.Id == id)];
            orders.RemoveAt(orders.FindIndex(el => el.Id == id));
            orderRepository.SaveOrders(orders);
            return Removed;
        }
        public List<Order> Orders => orders;
        public void Load() => orderRepository.LoadOrders();
        public void Save() => orderRepository.SaveOrders(orders);
        public int Length() => orders.Count;
        public Order this[int position]
        {
            get => orders[position];
            set => orders[position] = value;
        }

    }
}
