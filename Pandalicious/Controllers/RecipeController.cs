using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandalicious.Models;
using static Pandalicious.Models.Model;

namespace Pandalicious.Controllers
{
    [Route("Recipe")]
    public class RecipeController : Controller
    {
        private readonly PandaliciousContext _context; // Access to the database

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Pandalicious.Controllers.RecipeController"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public RecipeController(PandaliciousContext context)
        {
            _context = context;
        }

        [HttpGet, Route("Recipes")]
        public IActionResult Recipes()
        {
            var allRecipes = _context.Recipes.ToList();
            ViewBag.AllRecipes = allRecipes;

            return View();
        }

        [HttpGet, Route("NewRecipe")]
        public IActionResult NewRecipe()
        {
            return View();
        }

        [HttpPost, Route("RecipePV/")]
        public IActionResult RecipePV([FromBody] int id)
        {
            Recipe recipe = _context.Recipes.Find(id);
            var ingredients = _context.Menus.Include(i => i.Ingredient).Where(x => x.RecipeId == id).ToList();
            ViewBag.Recipe = recipe;
            ViewBag.Ingredients = ingredients;
            return PartialView();
        }
    }
}
