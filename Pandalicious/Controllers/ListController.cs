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
    [Route("List")]
    public class ListController : Controller
    {
        private readonly PandaliciousContext _context; // Access to the database

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Recipes.Controllers.HomeController"/> class.
        /// </summary>
        /// <param name="context">Database</param>
        public ListController(PandaliciousContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Home page for the website
        /// </summary>
        /// <returns>Index View</returns>

        public IActionResult List()
        {
            return View();
        }
    }
}
