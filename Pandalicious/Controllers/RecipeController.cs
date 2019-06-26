using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandalicious.Models;
using static Pandalicious.Models.Model;

namespace Pandalicious.Controllers
{
    [Route("Recipe/")]
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

        /// <summary>
        /// Allows the user to view all of the recipes
        /// </summary>
        /// <returns>All of the recipes view</returns>
        [HttpGet, Route("AllRecipes")]
        public IActionResult Recipes()
        {
            var allRecipes = _context.Recipes.ToList();
            ViewBag.AllRecipes = allRecipes;

            return View();
        }

        [HttpGet, Route("EditRecipe/{id}")]
        public IActionResult EditRecipe(int id)
        {
            var recipe = _context.Recipes.Where(x => x.RecipeId == id).First();
            var ingredients = _context.Menus.Include(i => i.Ingredient).Where(x => x.RecipeId == id).ToList();
            var directions = _context.RecipeDirections.Include(d => d.Direction).Where(x => x.RecipeId == id).ToList();

            ViewBag.Recipe = recipe;
            ViewBag.Ingredients = ingredients;
            ViewBag.Directions = directions;
            ViewBag.Tags = recipe.Tags;
            return View();
        }

       

        /// <summary>
        /// Allows the user to create a new recipe
        /// </summary>
        /// <returns>The recipe.</returns>
        [HttpGet, Route("NewRecipe")]
        public IActionResult NewRecipe()
        {
            return View();
        }

        /// <summary>
        /// Returns the partial view for the selected recipe
        /// </summary>
        /// <returns>The partial view</returns>
        /// <param name="id">Identifier.</param>
        [HttpPost, Route("RecipePV/")]
        public IActionResult RecipePV([FromBody] int id)
        {
            // Find the specific recipe
            Recipe recipe = _context.Recipes.Find(id);
            ViewBag.Recipe = recipe;

            // Find all of the ingredients for the recipe
            var ingredients = _context.Menus.Include(i => i.Ingredient).Where(x => x.RecipeId == id).ToList();
            ViewBag.Ingredients = ingredients;

            // Find all of the directions for the recipe
            var directions = _context.RecipeDirections.Include(d => d.Direction).Where(x => x.RecipeId == id).ToList();
            ViewBag.Directions = directions;

            // Find all of the tags for the recipe
            var tags = _context.RecipeTags.Include(t => t.Tag).Where(x => x.RecipeId == id).ToList();
            ViewBag.Tags = tags;
            return PartialView();
        }

        [HttpGet, Route("Entrees")]
        public IActionResult Entrees()
        {
            var entrees = _context.RecipeTags.Include(t => t.Tag).Where(x => x.Tag.TagName == "Entree").Select(r => r.Recipe).ToList();
            ViewBag.Entrees = entrees;
            return View();
        }

        [HttpGet, Route("Sides")]
        public IActionResult Sides()
        {
            return View();
        }

        [HttpGet, Route("Desserts")]
        public IActionResult Desserts()
        {
            return View();
        }
    }
}
