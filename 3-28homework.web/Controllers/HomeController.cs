using _3_27homework.Data;
using _3_28homework.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _3_27homework.web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data source=.\sqlexpress; initial catalog=People; integrated security=true;";
        public IActionResult Index()
        {
            var db = new PeopleDb(_connectionString);
            var vm = new IndexViewModel
            {
                People = db.GetAll()
            };
            if (TempData["message"] != null)
            {
                vm.Message = (string)TempData["message"];
            }
            return View(vm);
        }
        public IActionResult ShowAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(List<Person> people)
        {
            var db = new PeopleDb(_connectionString);
            db.AddPeople(people);
            TempData["message"] = $"{people.Count} people have been added successfully";
            return Redirect("/");
        }
    }
}