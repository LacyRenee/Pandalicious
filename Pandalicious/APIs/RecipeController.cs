using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pandalicious.Models;
using Pandalicious.Services;
using static Pandalicious.Models.Model;
using RecipeTags = Pandalicious.Models.RecipeTags;

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

        [HttpPost, Route("DeleteRecipe")]
        public IActionResult DeleteRecipe([FromBody] int id)
        {
            var recipe = _context.Recipes.Find(id);

            var directions = _context.RecipeDirections.Include(d => d.Direction).Where(x => x.DirectionId == recipe.RecipeId).ToList();

            foreach(var d in directions)
            {
                _context.Directions.Remove(d.Direction);
                _context.RecipeDirections.Remove(d);
            }

            var tags = _context.RecipeTags.Include(t => t.Tag).Where(x => x.RecipeId == recipe.RecipeId).ToList();
            foreach(var t in tags)
            {
                _context.Tags.Remove(t.Tag);
                _context.RecipeTags.Remove(t);
            }

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            return Json(new { sucess = true });
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
                    case "RecipeNotes":
                        newRecipe.RecipeNotes = parsedObject.GetValue("value").ToString();
                        break;
                    case "Keto":
                        Tag ketoTag = new Tag()
                        {
                            TagName = "Keto"
                        };

                        _context.Tags.Add(ketoTag);

                        RecipeTags ketokRecipeTag = new RecipeTags()
                        {
                            TagId = ketoTag.TagId,
                            Tag = ketoTag,
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.RecipeTags.Add(ketokRecipeTag);
                        break;
                    case "Entree":
                        Tag entreeTag = new Tag()
                        {
                            TagName = "Entree"
                        };

                        _context.Tags.Add(entreeTag);

                        RecipeTags entreekRecipeTag = new RecipeTags()
                        {
                            TagId = entreeTag.TagId,
                            Tag = entreeTag,
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.RecipeTags.Add(entreekRecipeTag);
                        break;
                    case "Side":
                        Tag sideTag = new Tag()
                        {
                            TagName = "Side"
                        };

                        _context.Tags.Add(sideTag);

                        RecipeTags sidekRecipeTag = new RecipeTags()
                        {
                            TagId = sideTag.TagId,
                            Tag = sideTag,
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.RecipeTags.Add(sidekRecipeTag);
                        break;
                    case "Dessert":
                        Tag dessertTag = new Tag()
                        {
                            TagName = "Dessert"
                        };

                        _context.Tags.Add(dessertTag);

                        RecipeTags dessertkRecipeTag = new RecipeTags()
                        {
                            TagId = dessertTag.TagId,
                            Tag = dessertTag,
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.RecipeTags.Add(dessertkRecipeTag);
                        break;
                    case "Chicken":
                        Tag chickenTag = new Tag()
                        {
                            TagName = "Chicken"
                        };

                        _context.Tags.Add(chickenTag);

                        RecipeTags chickenkRecipeTag = new RecipeTags()
                        {
                            TagId = chickenTag.TagId,
                            Tag = chickenTag,
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.RecipeTags.Add(chickenkRecipeTag);
                        break;
                    case "Pork":
                        Tag porkTag = new Tag()
                        {
                            TagName = "Pork"
                        };

                        _context.Tags.Add(porkTag);

                        RecipeTags porkRecipeTag = new RecipeTags()
                        {
                            TagId = porkTag.TagId,
                            Tag = porkTag,
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.RecipeTags.Add(porkRecipeTag);
                        break;
                    case "Beef":
                        Tag beefTag = new Tag()
                        {
                            TagName = "Beef"
                        };

                        _context.Tags.Add(beefTag);

                        RecipeTags beefRecipeTag = new RecipeTags()
                        {
                            TagId = beefTag.TagId,
                            Tag = beefTag,
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.RecipeTags.Add(beefRecipeTag);

                        break;
                    case "Fish":
                        Tag fishTag = new Tag()
                        {
                            TagName = "Fish"
                        };

                        _context.Tags.Add(fishTag);

                        RecipeTags fishRecipeTag = new RecipeTags()
                        {
                            TagId = fishTag.TagId,
                            Tag = fishTag,
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.RecipeTags.Add(fishRecipeTag);

                        break;
                    case "Other":
                        Tag otherTag = new Tag()
                        {
                            TagName = "Other"
                        };

                        _context.Tags.Add(otherTag);

                        RecipeTags otherRecipeTag = new RecipeTags()
                        {
                            TagId = otherTag.TagId,
                            Tag = otherTag,
                            RecipeId = newRecipe.RecipeId,
                            Recipe = newRecipe
                        };

                        _context.RecipeTags.Add(otherRecipeTag);

                        break;
                    default:
                        throw new Exception("Property not found");
                }

            }

            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost, Route("SaveRecipe")]
        public IActionResult SaveRecipe([FromBody] JArray data)
        {
            JArray parsedArray = JArray.Parse(data.ToString());
            int id = 0;
            foreach (JObject parsedObject in parsedArray)
            {
                string propertyName = parsedObject.GetValue("name").ToString();
                if (propertyName == "RecipeId")
                    id = Int32.Parse(parsedObject.GetValue("value").ToString());

            }
            Recipe recipe = _context.Recipes.Find(id);
            var ingredients = _context.Menus.Include(i => i.Ingredient).Where(x => x.RecipeId == recipe.RecipeId).ToList();
            var directions = _context.RecipeDirections.Include(d => d.Direction).Where(x => x.RecipeId == recipe.RecipeId).ToList();
            var tags = _context.RecipeTags.Include(t => t.Tag).Where(x => x.RecipeId == recipe.RecipeId).ToList();
            string ingredientValue = string.Empty;
            string ingredientName = string.Empty;
        
            // Loop through the form data and create the recipe model 
            foreach (JObject parsedObject in parsedArray)
            {
      
                string propertyName = parsedObject.GetValue("name").ToString();
                if (propertyName == null)
                    propertyName = String.Empty;
                switch (propertyName)
                {
                    case "RecipeNotes":
                        recipe.RecipeNotes = parsedObject.GetValue("value").ToString();
                        break;
                    default:
                        break;
                }
            }

            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}