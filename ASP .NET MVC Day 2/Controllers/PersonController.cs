using ASP_.NET_MVC_Day_2.Models.DTO;
using ASP_.NET_MVC_Day_2.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_.NET_MVC_Day_2.Controllers
{
    [Route("/NashTech/Rookies/[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet]
        public IActionResult Index(int page=1)
        {
            var persons = _personService.GetAllPerson(page);
            return View(persons);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost("Create")]
        public IActionResult Create(PersonCreateDTO person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            _personService.AddPerson(person);
            return RedirectToAction("Index");
        }

        [HttpGet("Details/{id}")]
        public IActionResult Detail(Guid id)
        {
            var person = _personService.GetPersonById(id);
            return View(person);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var person = _personService.GetPersonById(id);
            return View(person);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(Guid id, PersonUpdatedDTO person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            _personService.UpdatePerson(id, person);
            return RedirectToAction("Index");
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var person = _personService.GetPersonById(id);
            return View(person);
        }

        [HttpGet("Confirmation")]
        public IActionResult Confirmation(string name)
        {
            ViewBag.Message = $"Person {name} was removed from the list successfully!";
            return View();
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var personRemoved = _personService.GetPersonById(id);
            _personService.DeletePerson(id);
            return RedirectToAction("Confirmation",new {name = $"{personRemoved.FirstName} {personRemoved.LastName}"});
        }

        [HttpGet("GetListMalePerson")]
        public IActionResult GetListMalePerson()
        {
            var persons = _personService.GetPersonIsMale();
            if (persons.Count() == 0)
            {
                return Content("No Male Found");
            }
            return View( persons);
        }

        [HttpGet("GetOldestPerson")]
        public IActionResult GetOldest()
        {
            var person = _personService.GetOldestPerson();
            return View(person);
        }

        [HttpGet("FilterPerson")]
        public IActionResult FilterPerson([FromQuery] string query, [FromQuery] int year)
        {
            var persons = _personService.FilterPerson(query, year);
            if (persons == null)
            {
                return Content("Invalid query");
            }
            return View(persons);
        }

        [HttpGet("GetFullNames")]
        public IActionResult GetFullNames(){
            var fullNames = _personService.GetFullNameList(_personService.GetAllPerson(1));
            return View(fullNames);
        }

        [HttpGet("ExportExcelFile")]
        public IActionResult ExportExcelFile(){
            var persons = _personService.GetAllPerson(1);
            if (persons == null || !persons.Any())
            {
                return Content("No data to export");
            }
            var fileContent = _personService.GenerateExcelFile(persons);
            var fileName = "Persons.xlsx";
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}