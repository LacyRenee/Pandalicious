using System;
using Microsoft.AspNetCore.Mvc;
using Pandalicious.Models;
using static Pandalicious.Models.Model;

namespace Pandalicious.Controllers
{
    public class RecipeController : Controller
    {
        private readonly PandaliciousContext _context; // Access to the database

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Pandalicious.Controllers.RecipeController"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public RecipeController(PandaliciousContext context)
        {
            _context = context;
        }

        [HttpGet, Route("Recipes")]
        public IActionResult Recipes()
        {
            return View();
        }

        [HttpGet, Route("NewRecipe")]
        public IActionResult NewRecipe()
        {
            return View();
        }
    }
}
