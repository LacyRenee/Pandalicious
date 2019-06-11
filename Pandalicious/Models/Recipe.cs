﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pandalicious.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int RecipeServings { get; set; }
        [DataType(DataType.Date)]
        public DateTime RecipeDuration { get; set; }
        public string RecipeDirections { get; set; }
        public List<Ingredient> RecipeIngredients { get; set; }

        [NotMapped]
        public List<string> Tags { get; set; }
    }
}
