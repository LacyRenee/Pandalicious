using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pandalicious.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Ingredient must have a name")]
        public string IngredientName { get; set; }

        public string IngredientUnit { get; set; }

        [ForeignKey("BookId")]
        public int BookId { get; set; }
    }
}