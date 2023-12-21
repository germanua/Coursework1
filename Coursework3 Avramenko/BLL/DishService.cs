using DAL;

namespace BLL
{
    public class DishService
    {
        List<Dish> dishes = new();
        public DishRepository dishRepository = new ();
        public DishService()
        {
            dishes = dishRepository.Load();
        }
        public Dish Insert(Dish dish)
        {
            Dish NewDish = dish;
            dishes.Add(NewDish);
            dishRepository.Save(dishes);
            return NewDish;
        }
        public void Update(Dish input, int index)
        {
            dishes[index] = input;
            dishRepository.Save(dishes);
        }
        public Dish Delete(int index)
        {
            Dish Removed = dishes[index];
            dishes.RemoveAt(index);
            dishRepository.Save(dishes);
            return Removed;
        }
        public void RemoveIngredientById(int ingredientId)
        {
            dishes.ForEach(dish => dish.Ingredients.Remove(ingredientId));
            dishRepository.Save(dishes);
        }
        public Dish? FindByName(string query, List<Dish> array) => array.Find(i => i.Name.ToLower().Contains(query.ToLower()));
        public Dish? FindByName(string query) => FindByName(query, dishes);
        public List<Dish> Dishes => dishes;
        public void Load() => dishRepository.Load();
        public void Save() => dishRepository.Save(dishes);
        public int Length() => dishes.Count;
        public Dish this[int position]
        {
            get => dishes[position];
            set => dishes[position] = value;
        }

    }
}
