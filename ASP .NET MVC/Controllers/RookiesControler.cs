using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using ASP_.NET_MVC.Models;
using ASP_.NET_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP_.NET_MVC.Controllers
{
    [Route("/NashTech/[controller]/[action]")]
    public class RookiesController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ILogger<RookiesController> _logger;

        public RookiesController(ILogger<RookiesController> logger, IPersonService personService)
        {
            _personService = personService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var persons = _personService.GetAllPerson();
            return View(persons);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create( Person person)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(person);
                }
                
                _personService.AddPerson(person);
                return RedirectToAction("Index");
            }catch(Exception e)
            {
                return Content(e.Message);
            }
        }
        [HttpGet]
        public IActionResult GetListMalePerson()
        {
            var persons = _personService.GetPersonIsMale();
            if (persons.Count() == 0)
            {
                return Content("No Male Found");
            }
            return View("Index", persons);
        }
        [HttpGet]
        public IActionResult GetOldest()
        {
            var person = _personService.GetOldestPerson();
            if (person == null)
            {
                return Content("No person in list ");
            }
            return View(person);
        }
        [HttpGet]
        public IActionResult FilterPerson([FromQuery] string query, [FromQuery] int year)
        {
            var persons = _personService.FilterPerson(query, year);
            if (persons == null)
            {
                return Content("Invalid query");
            }
            return View("Index", persons);
        }
        [HttpGet]
        public IActionResult GetFullName(){
            var persons = _personService.GetAllPerson();
            var fullNames = persons.Select(p => $"{p.FirstName} {p.LastName}").ToList();
            return View(fullNames);
        }
        [HttpGet]
        public IActionResult ExportExcelFile(){
            var persons = _personService.GetAllPerson();
            if (persons == null || !persons.Any())
            {
                return Content("No data to export");
            }
            var fileContent = _personService.GenerateExcelFile(persons);
            var fileName = "Persons.xlsx";
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        
        }
        
        [HttpPost("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _personService.DeletePerson(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var person = _personService.GetPersonById(id);
            if (person == null)
            {
                return Content("Person not found");
            }
            return View(person);
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(person);
                }
                _personService.UpdatePerson(id, person);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        
    }
}