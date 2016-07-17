using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RecipeCalculator.Controllers
{
    using Models;
    using Repository;
    using Recipe = Dictionary<int, double>;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Calculate Recipe";
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
