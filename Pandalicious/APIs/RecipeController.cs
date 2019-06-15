using System;
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

        [HttpPost, Route("CreateRecipe")]
        public IActionResult CreateRecipe([FromBody] JArray data)
        {
            JArray parsedArray = JArray.Parse(data.ToString());
            Recipe newRecipe = new Recipe();

            List<Ingredient> ingredientList = new List<Ingredient>();
            string ingredientValue = string.Empty;
            string ingredientName = string.Empty;

            List<string> recipeDirections = new List<string>();
            List<string> recipeTags = new List<string>();

            // Loop through the form data and create the recipe model 
            foreach (JObject parsedObject in parsedArray)
            {

                string propertyName = parsedObject.GetValue("name").ToString();

                switch (propertyName)
                {
                    case "RecipeName":
                        newRecipe.RecipeName = parsedObject.GetValue("value").ToString();
                        break;
                    case "RecipeServings":
                        newRecipe.RecipeServings = int.Parse(parsedObject.GetValue("value").ToString());
                        break;
                    case "RecipeDuration":
                        newRecipe.RecipeDuration = parsedObject.GetValue("value").ToString();
                        break;
                    case "IngredientValue":
                        ingredientValue = parsedObject.GetValue("value").ToString() + " ";
                        break;
                    case "IngredientUnit":
                        ingredientValue += parsedObject.GetValue("value").ToString();
                        break;
                    case "IngredientName":
                        ingredientName = parsedObject.GetValue("value").ToString();

                        Ingredient newIngredient = new Ingredient
                        {
                            IngredientName = ingredientName,
                            IngredientUnit = ingredientValue
                        };

                        if (!_context.Ingredients.Where(x => x.IngredientName == ingredientName).Any())
                        {
                            _context.Ingredients.Add(newIngredient);
                            _context.SaveChanges();
                        }

                        ingredientList.Add(newIngredient);
                        break;
                    case "RecipeDirection":
                        string newDirection = parsedObject.GetValue("value").ToString();
                        recipeDirections.Add(newDirection);
                        break;
                    case "Keto":
                    case "Entree":
                    case "Sides":
                    case "Dessert":
                    case "Chicken":
                    case "Pork":
                    case "Beef":
                    case "Fish":
                    case "Other":
                        string newTag = parsedObject.GetValue("value").ToString();
                        recipeTags.Add(newTag);
                        break;
                    default:
                        throw new Exception("Property not found");
                }

            }

            // Create the recipe
            newRecipe.RecipeIngredients = ingredientList;
            newRecipe.RecipeDirections = recipeDirections;
            newRecipe.Tags = recipeTags;

            // Add the recipe to the database and save changes
            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}