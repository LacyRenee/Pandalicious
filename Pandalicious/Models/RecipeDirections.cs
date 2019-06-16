using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pandalicious.Models
{
    public class RecipeDirections
    {
        [Key]
        public int RecipeDirectionId {get; set;}

        [ForeignKey("DirectionId")]
        public int DirectionId { get; set; }
        public Direction Direction { get; set; }

        [ForeignKey("RecipeId")]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

    }
}
