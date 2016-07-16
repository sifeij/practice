using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RecipeCalculator.Controllers
{
    using Models;
    using Repository;

    public class HomeController : Controller
    {
        public HomeController(IIngredientRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            ViewData["Message"] = "Calculate Recipe";

            var recipes = new List<Recipe>()
            {
                new Recipe { IngredientName = "garlic clove", Unit = 1 },
                new Recipe { IngredientName = "lemon", Unit = 1 },
                new Recipe { IngredientName = "cup olive oil", Unit = 0.75 },
                new Recipe { IngredientName = "teaspoons of salt ", Unit = 0.75 },
                new Recipe { IngredientName = "teaspoons of pepper", Unit = 0.5 }
            };
            var result = _repo.CaculateCost(recipes);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Recipe Calculator";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Please contact me.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        readonly IIngredientRepository _repo;
    }
}
