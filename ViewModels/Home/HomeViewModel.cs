using System.Collections.Generic;
using RecipeBook.Models;

namespace RecipeBook.ViewModels.Home
{
    public class HomeViewModel
    {
        public IList<Recipe> Recipes { get; set; }
    }
}