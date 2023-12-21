using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DAL
{
    public class DishRepository
    {
        private const string FilePath = "menu.json";
        JsonSerializerOptions options = new() { WriteIndented = true };

        public void Save(List<Dish> dishes)
        {
            using FileStream fileStream = new(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(0); // Fixes issue with deleting nodes
            JsonSerializer.Serialize(fileStream, dishes, options);
        }

        public List<Dish> Load()
        {
            try
            {
                using FileStream fileStream = new(FilePath, FileMode.Open);
                return (List<Dish>)JsonSerializer.Deserialize(fileStream, typeof(List<Dish>), options);
            }
            catch (Exception ex)
            {
                return new List<Dish>();
            }
        }
    }
}
