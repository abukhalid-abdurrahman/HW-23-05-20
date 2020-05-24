using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Practics.Models;

namespace Practics.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public IActionResult Index()
        {
            return View(new ModelRepository<PersonViewModel>("Person").Read());
        }

        public IActionResult Person()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Selection(int? id)
        {
            if(id == null)
                return RedirectToAction("Index");
            return View(new ModelRepository<PersonViewModel>("Person").SelectById(id));
        }

        [HttpPost]
        public IActionResult Person(PersonViewModel person)
        {
            new ModelRepository<PersonViewModel>("Person").Create(person, "LastName, FirstName, MiddleName", "@LastName, @FirstName, @MiddleName");
            return View();
        }
    }
}
