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
            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

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

                        if (_context.Ingredients.Where(x => x.IngredientName == ingredientName).Any())
                        {
                            Ingredient existingIngredient = _context.Ingredients.Where(x => x.IngredientName == ingredientName).FirstOrDefault();

                            Menu newItem = new Menu()
                            {
                                MenuMeasurement = ingredientValue,
                                IngredientId = existingIngredient.IngredientId,
                                RecipeId = newRecipe.RecipeId
                            };

                            _context.Menus.Add(newItem);
                            _context.SaveChanges();
                        }
                        else
                        {
                            Ingredient newIngredient = new Ingredient()
                            {
                                IngredientName = ingredientName
                            };

                            _context.Ingredients.Add(newIngredient);
                            _context.SaveChanges();

                            Menu newItem = new Menu()
                            {
                                MenuMeasurement = ingredientValue,
                                IngredientId = newIngredient.IngredientId,
                                Ingredient = newIngredient,
                                RecipeId = newRecipe.RecipeId
                            };
                            _context.Menus.Add(newItem);
                            _context.SaveChanges();
                        }

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
            newRecipe.RecipeDirections = recipeDirections;
            newRecipe.Tags = recipeTags;

            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}