using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RecipeBook.Models;
using RecipeBook.ViewModels.Home;

namespace RecipeBook.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Forward to login page if user not signed in
            if (!User.IsSignedIn())
                return RedirectToAction("Login", "Account");
                
            using (var recipeManager = new RecipeManager())
            {
                Console.WriteLine("Getting recipes for user with id '{0}'", User.GetUserId());
                
                var model = new HomeViewModel
                {
                    Recipes = recipeManager.GetRecipesForUser(User.GetUserId())
                };
                
                return View(model);
            }
        }
        
        public IActionResult Create()
        {
            // Forward to login page if user not signed in
            if (!User.IsSignedIn())
                return RedirectToAction("Login", "Account");
                
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveRecipe(CreateRecipeViewModel recipe)
        {
            // Forward to login page if user not signed in
            if (!User.IsSignedIn())
                return RedirectToAction("Login", "Account");
                
            using (var recipeManager = new RecipeManager())
            {
                // TODO Check to see if the save operation was successful
                await recipeManager.SaveRecipeAsync(User.GetUserId(), recipe.RecipeName, recipe.RecipeText);

                // Put user back on the homepage                
                var model = new HomeViewModel
                {
                    Recipes = recipeManager.GetRecipesForUser(User.GetUserId())
                };
                
                return View("Index", model);
            }
        }
        
        public IActionResult ViewRecipe(int id)
        {
            // Forward to login page if user not signed in
            if (!User.IsSignedIn())
                return RedirectToAction("Login", "Account");
                
            using (var recipeManager = new RecipeManager())
            {
                var model = new RecipeViewModel
                {
                    Recipe = recipeManager.GetRecipeById(id)
                };
                
                Console.WriteLine("HomeController:ViewRecipe got recipe -> id={0} name={1}", model.Recipe.Id, model.Recipe.Name);
                return View(model);             
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private IActionResult EnsureUserSignedIn()
        {
            if (!User.IsSignedIn())
                return RedirectToAction("Login", "Account");
            else
                return null;
        }
    }
}
