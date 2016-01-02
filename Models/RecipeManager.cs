using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class RecipeManager : IDisposable
    {
        private RecipeDbContext recipedb;

        public RecipeManager()
        {
            recipedb = new RecipeDbContext();
        }

        public void Dispose()
        {
            if (recipedb != null)
            {
                recipedb.Dispose();
            }
        }

        public List<Recipe> GetRecipesForUser(string userId)
        {
            try
            {
                return recipedb.Recipes.Where(r => r.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                // TODO Log exception in GetRecipesForUser
                Console.WriteLine("Exception was thrown when retrieving recipes for a user.");
                Console.WriteLine(ex.ToString());
                return new List<Recipe>();
            }
        }
        
        public async Task SaveRecipeAsync(string userId, string recipeName, string recipeText)
        {
            try
            {
                var recipe = new Recipe
                {
                    UserId = userId,
                    Name = recipeName,
                    RecipeText = recipeText
                };
                
                recipedb.Recipes.Add(recipe);
                await recipedb.SaveChangesAsync();
                return;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}