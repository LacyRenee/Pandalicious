using Microsoft.EntityFrameworkCore;

namespace Pandalicious.Models
{
    public class Model
    {
        public class PandaliciousContext : DbContext
        {
            public PandaliciousContext(DbContextOptions<PandaliciousContext> options)
                : base(options)
            { }

            public DbSet<Ingredient> Ingredients { get; set; }
            public DbSet<Recipe> Recipes { get; set; }
            public DbSet<Menu> Menus { get; set; }
            public DbSet<Direction> Directions { get; set; }
            public DbSet<RecipeDirections> RecipeDirections { get; set; }
            public DbSet<Tags> Tags { get; set; }
        }
    }
}
