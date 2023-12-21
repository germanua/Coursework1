// Inside DAL namespace

namespace DAL
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Constructor
        public Ingredient(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}