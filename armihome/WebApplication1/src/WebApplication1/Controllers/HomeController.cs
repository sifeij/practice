using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeCalculator.Repository;

namespace RecipeCalculator.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IngredientRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            ViewData["Message"] = "Calculate Recipe";
            var result = _repo.GetAll();
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

        readonly IngredientRepository _repo;
    }
}
