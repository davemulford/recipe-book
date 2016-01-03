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
        
        public Recipe GetRecipeById(int recipeId)
        {
            try
            {
                Console.WriteLine("RecipeManager:GetRecipeById -> find recipe with id {0}", recipeId);
                return recipedb.Recipes
                    .Where(r => r.Id == recipeId)
                    .ToList()
                    .Select(r => new Recipe
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        Name = r.Name,
                        RecipeText = HtmlDecode(r.RecipeText) 
                    })
                    .SingleOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown when retrieving a recipe with id {recipeId}.");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<Recipe> GetRecipesForUser(string userId)
        {
            try
            {
                return recipedb.Recipes
                    .Where(r => r.UserId == userId)
                    .ToList()
                    .Select(r => new Recipe
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        Name = r.Name,
                        RecipeText = HtmlDecode(r.RecipeText) 
                    })
                    .ToList();
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
                Console.WriteLine(recipeText.ToList().Select(c => Convert.ToInt32(c).ToString()).Aggregate((sb,c) => String.Concat(sb, "-", c)));
                
                var recipe = new Recipe
                {
                    UserId = userId,
                    Name = recipeName,
                    RecipeText = HtmlEncode(recipeText)
                };
                
                recipedb.Recipes.Add(recipe);
                await recipedb.SaveChangesAsync();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown when saving a recipe. userId={0} recipeName={1} recipeText={2}", userId, recipeName, recipeText);
                Console.WriteLine(ex.ToString());
                return;
            }
        }
        
        /*
         * Encodes an html string.
         * THIS METHOD SHOULD ONLY BE USED UNTIL COREFX HAS AN HTMLDECODE METHOD.
         */
        private string HtmlEncode(string html)
        {
            return html.ToList().Select(c => Convert.ToInt32(c).ToString()).Aggregate((sb,c) => String.Concat(sb, "-", c));
        }
        
        /*
         * Decodes an html string.
         * THIS METHOD SHOULD ONLY BE USED UNTIL COREFX HAS AN HTMLDECODE METHOD.
         */
        private string HtmlDecode(string encodedText)
        {
            return new String(
                encodedText
                    .Split('-')
                    .Select(s => Convert.ToChar(Int32.Parse(s)))
                    .ToArray()
                );
        }
    }
}