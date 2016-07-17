using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeCalculator.Controllers
{
    using Models;
    using Repository;

    [Route("api/[controller]")]
    public class IngredientsController : Controller
    {
        public IngredientsController(IIngredientRepository repo)
        {
            _repository = repo;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Ingredient> Get()
        {
            return _repository.GetAll();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        readonly IIngredientRepository _repository;
    }
}
