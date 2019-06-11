using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pandalicious.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        public string MenuMeasurement { get; set; }

        [ForeignKey("RecipeId")]
        public int RecipeId { get; set; }

        [ForeignKey("IngredientId")]
        public int IngredientId { get; set; }

    }
}
