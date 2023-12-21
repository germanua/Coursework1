using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DAL
{
    public class IngredientRepository
    {
        private const string IngredientsFilePath = "ingredients.json";
        JsonSerializerOptions options = new() { WriteIndented = true };

        public void SaveIngredients(List<Ingredient> ingredients)
        {
            using FileStream fileStream = new(IngredientsFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(0); // Fixes issue with deleting nodes
            JsonSerializer.Serialize(fileStream, ingredients, options);
        }

        public List<Ingredient> LoadIngredients()
        {
            try
            {
                using FileStream fileStream = new(IngredientsFilePath, FileMode.Open);
                return (List<Ingredient>)JsonSerializer.Deserialize(fileStream, typeof(List<Ingredient>), options);
            }
            catch (Exception ex)
            {
                return new List<Ingredient>();
            }
        }
    }
}