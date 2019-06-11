using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pandalicious.Models;
using static Pandalicious.Models.Model;

namespace Pandalicious.APIs
{
    [Route("api/Ingredient")]
    [ApiController]
    public class IngredientController : Controller
    {
        private readonly PandaliciousContext _context; // Access to the database

        public IngredientController(PandaliciousContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds the ingredient.
        /// </summary>
        /// <returns>The ingredient.</returns>
        /// <param name="Ingredient">Contains the Ingredient Object.</param>
        [HttpPost, Route("AddIngredient")]
        public IActionResult AddIngredient([FromBody] Ingredient Ingredient)
        {
            var ingredient = _context.Ingredients.Find(Ingredient.IngredientId);

            if (ingredient == null)
            {
                _context.Ingredients.Add(Ingredient);
            }
            else
            {
                ingredient.IngredientName = Ingredient.IngredientName;
                ingredient.IngredientUnit = Ingredient.IngredientUnit;
            }

            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost, Route("SaveIngredient")]
        public IActionResult SaveIngredient([FromBody] JObject JsonData)
        {
            dynamic data = JsonData;
            int id = data.IngredientId;
            string name = data.IngredientName;
            string unit = data.IngredientUnit;

            var newIngredient = _context.Ingredients.Find(id);
            newIngredient.IngredientName = name;
            newIngredient.IngredientUnit = unit;

            _context.SaveChanges();

            return Json(new { success = true });
        }

        /// <summary>
        /// Edits the ingredient.
        /// </summary>
        /// <returns>The ingredient.</returns>
        /// <param name="IngredientId">Ingredient identifier.</param>
        [HttpPost, Route("EditIngredient")]
        public IActionResult EditIngredient([FromBody] int IngredientId)
        {
            var ingredient = _context.Ingredients.Find(IngredientId);
            var unit = ingredient.IngredientUnit.Split(" ");
            return Json(new { success = true, ingredientName = ingredient.IngredientName, ingredientValue = unit[0].Trim(), ingredientUnit = unit[1].Trim() });
        }

        /// <summary>
        /// Deletes the ingredient from the database
        /// </summary>
        /// <returns></returns>
        /// <param name="IngredientId"></param>
        [HttpPost, Route("DeleteIngredient")]
        public IActionResult DeleteIngredient([FromBody] int IngredientId)
        {
            _context.Ingredients.Remove(_context.Ingredients.Find(IngredientId));
            _context.SaveChanges();
            return Json(new { success = true });
        }
    }
}