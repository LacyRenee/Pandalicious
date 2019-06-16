// This class provides a list of tags that can be used to idenfity recipes

using System.Collections.Generic;

namespace Pandalicious.Services
{
    public static class RecipeTags
    {
        public static List<string> Tags { get; } = new List<string>()
        {
            "Keto",
            "Entree",
            "Side",
            "Dessert",
            "Chicken",
            "Pork",
            "Beef",
            "Fish",
            "Other"
        };
    }
}