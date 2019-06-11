using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pandalicious.Models;
using static Pandalicious.Models.Model;

namespace Recipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly PandaliciousContext _context; // Access to the database

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Recipes.Controllers.HomeController"/> class.
        /// </summary>
        /// <param name="context">Database</param>
        public HomeController(PandaliciousContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Home page for the website
        /// </summary>
        /// <returns>Index View</returns>
        [Route("")]
        [HttpGet, Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// About page for the website.
        /// </summary>
        /// <returns>About View</returns>
        [HttpGet, Route("About")]
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Displays all of the available ingredients
        /// </summary>
        /// <returns>Ingredients View.</returns>
        [HttpGet, Route("Ingredients")]
        public IActionResult Ingredients()
        {
            var ingredientList = _context.Ingredients.ToList();
            ViewBag.Ingredients = ingredientList;
            return View();
        }

        /// <summary>
        /// Displays the user's shopping lists
        /// </summary>
        /// <returns>MyList View</returns>
        [HttpGet, Route("MyList")]
        public  IActionResult MyList()
        {
            return View();
        }

        /// <summary>
        /// User-made error message
        /// </summary>
        /// <returns>The error.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
