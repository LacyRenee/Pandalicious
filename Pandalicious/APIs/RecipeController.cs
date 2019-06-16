using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pandalicious.Models;
using Pandalicious.Services;
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

            int directionStep = 0;

            // Loop through the form data and create the recipe model 
            foreach (JObject parsedObject in parsedArray)
            {

                string propertyName = parsedObject.GetValue("name").ToString();

                switch (propertyName)
                {
                    case "RecipeName":
                        if (parsedObject.GetValue("value").ToString() == String.Empty)
                        {
                            newRecipe.RecipeName = "Unnamed Recipe";
                        }
                        else
                        {
                            newRecipe.RecipeName = parsedObject.GetValue("value").ToString();
                        }
                        break;
                    case "RecipeServings":
                        if (parsedObject.GetValue("value").ToString() != String.Empty)
                        {
                            newRecipe.RecipeServings = int.Parse(parsedObject.GetValue("value").ToString());
                        }
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
                        if (parsedObject.GetValue("value").ToString() != String.Empty)
                        {
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
                        }
                        break;
                    case "RecipeDirection":
                        if (parsedObject.GetValue("value").ToString() != String.Empty)
                        {
                            Direction newDirection = new Direction
                            {
                                DirectionStep = ++directionStep,
                                DirectionDescription = parsedObject.GetValue("value").ToString()
                            };
                            _context.Directions.Add(newDirection);
                            _context.SaveChanges();

                            RecipeDirections newRecipeDirections = new RecipeDirections();
                            _context.RecipeDirections.Add(newRecipeDirections);

                            newRecipeDirections.DirectionId = newDirection.DirectionId;
                            newRecipeDirections.Direction = newDirection;
                            newRecipeDirections.RecipeId = newRecipe.RecipeId;
                            newRecipeDirections.Recipe = newRecipe;
                        }
                        break;
                    case "Keto":
                        Tags ketoTag = new Tags()
                        {
                            TagName = "Keto",
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.Tags.Add(ketoTag);
                        break;
                    case "Entree":
                        Tags entreeTag = new Tags()
                        {
                            TagName = "Entree",
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.Tags.Add(entreeTag);
                        break;
                    case "Side":
                        Tags sideTag = new Tags()
                        {
                            TagName = "Side",
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.Tags.Add(sideTag);
                        break;
                    case "Dessert":
                        Tags dessertTag = new Tags()
                        {
                            TagName = "Dessert",
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.Tags.Add(dessertTag);
                        break;
                    case "Chicken":
                        Tags chickenTag = new Tags()
                        {
                            TagName = "Chicken",
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.Tags.Add(chickenTag);
                        break;
                    case "Pork":
                        Tags porkTag = new Tags()
                        {
                            TagName = "Pork",
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.Tags.Add(porkTag);
                        break;
                    case "Beef":
                        Tags beefTag = new Tags()
                        {
                            TagName = "Beef",
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.Tags.Add(beefTag);
                        break;
                    case "Fish":
                        Tags fishTag = new Tags()
                        {
                            TagName = "Fish",
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.Tags.Add(fishTag);
                        break;
                    case "Other":
                        Tags otherTag = new Tags()
                        {
                            TagName = "Other",
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.Tags.Add(otherTag);

                        break;
                    default:
                        throw new Exception("Property not found");
                }

            }

            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}