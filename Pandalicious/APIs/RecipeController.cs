using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pandalicious.Models;
using static Pandalicious.Models.Model;

namespace Pandalicious.APIs
{
    [Route("api/Recipe")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly PandaliciousContext _context; // Access to the database

        public RecipeController(PandaliciousContext context)
        {
            _context = context;
        }

        [HttpGet, Route("IngredientList")]
        public IActionResult IngredientList()
        {
            var ingredients = _context.Ingredients.ToList();
            List<JObject> ingredientList = new List<JObject>();

            foreach (Ingredient i in ingredients)
            {
                dynamic ingredient = new JObject();
                ingredient.IngredientName = i.IngredientName;
                ingredientList.Add(ingredient);
            }

            return Json(ingredientList);
        }


    }
}