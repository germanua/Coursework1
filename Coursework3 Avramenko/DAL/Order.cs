namespace DAL
{
    public class Order
    {
        public int Id { get; set; } 
        public List<Dish> Dishes { get; set; } = new ();
        public int TableNumber { get; set; }
        public string PaymentMethod { get; set; }
        public int EstimatedWaitingTime { get; set; }
        public decimal Total { get; set; }

        public Order() { }
        public Order(int id, int tableNumber, string paymentMethod, int estimatedWaitingTime, List<Dish> dishes, decimal total)
        {
            Id = id;
            TableNumber = tableNumber;
            PaymentMethod = paymentMethod;
            EstimatedWaitingTime = estimatedWaitingTime;
            Dishes = dishes;
            Total = total;
        }
    }
}
