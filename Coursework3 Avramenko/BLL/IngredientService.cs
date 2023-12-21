using DAL;

namespace BLL
{
    public class IngredientService
    {
        List<Ingredient> ingredients = new();
        public IngredientRepository ingredientRepository = new();
        public IngredientService()
        {
            ingredients = ingredientRepository.LoadIngredients();
        }
        public Ingredient? GetById(int Id) => ingredients.Find(i => i.Id == Id);
        public Ingredient Insert(string Name)
        {
            int Id = 1;
            try
            {
                Id = ingredients[^1].Id + 1;
            }
            catch (Exception) { }
            Ingredient NewIngredient = new(Id, Name);
            ingredients.Add(NewIngredient);
            ingredientRepository.SaveIngredients(ingredients);
            return NewIngredient;
        }
        public void UpdateById(Ingredient input, int id)
        {
            ingredients[ingredients.FindIndex(e => e.Id == id)] = input;
            ingredientRepository.SaveIngredients(ingredients);
        }
        public Ingredient DeleteById(int id)
        {
            Ingredient Removed = GetById(id);
            ingredients.RemoveAt(ingredients.FindIndex(e => e.Id == id));
            ingredientRepository.SaveIngredients(ingredients);
            return Removed;
        }
        public List<Ingredient> Ingredients => ingredients;
        public void Load() => ingredientRepository.LoadIngredients();
        public void Save() => ingredientRepository.SaveIngredients(ingredients);
        public int Length() => ingredients.Count;
        public Ingredient this[int position]
        {
            get => ingredients[position];
            set => ingredients[position] = value;
        }

    }
}
