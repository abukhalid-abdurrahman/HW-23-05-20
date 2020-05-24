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

        public IActionResult Search()
        {
            return View(null);
        }

        [HttpPost]
        public IActionResult Search(PersonViewModel person)
        {
            if(string.IsNullOrEmpty(person.LastName) || string.IsNullOrEmpty(person.FirstName) || string.IsNullOrEmpty(person.MiddleName))
                return View(null);
            return View(new ModelRepository<PersonViewModel>("Person").SelectByParam($"LastName = '{person.LastName}' and FirstName = '{person.FirstName}' and MiddleName = '{person.MiddleName}'"));
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
