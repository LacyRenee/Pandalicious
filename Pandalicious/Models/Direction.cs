using System;
using System.ComponentModel.DataAnnotations;

namespace Pandalicious.Models
{
    public class Direction
    {
        [Key]
        public int DirectionId { get; set; }
        public int DirectionStep { get; set; }
        public string DirectionDescription { get; set; }
    }
}
