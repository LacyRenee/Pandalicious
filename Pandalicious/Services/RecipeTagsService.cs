﻿// This class provides a list of tags that can be used to idenfity recipes

using System.Collections.Generic;
using Pandalicious.Models;

namespace Pandalicious.Services
{
    public static class RecipeTagsService
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