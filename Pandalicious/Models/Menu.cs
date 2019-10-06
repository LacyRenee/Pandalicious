using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pandalicious.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        public string MenuValue { get; set; }
        public string MenuUnit { get; set; }

        [ForeignKey("RecipeId")]
        public int RecipeId { get; set; }

        [ForeignKey("IngredientId")]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}
