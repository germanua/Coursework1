namespace DAL
{
    [Serializable]
    public class Dish
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<int> Ingredients { get; set; } = new List<int>();
        public int CookingTime { get; set; }

        public Dish(string name, decimal price, List<int> ingredients, int cookingTime)
        {
            Name = name;
            Price = price;
            Ingredients = ingredients;
            CookingTime = cookingTime;
        }
    }
}