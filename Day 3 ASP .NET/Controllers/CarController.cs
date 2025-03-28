using Day_3_ASP_.NET.Interface;
using Day_3_ASP_.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day_3_ASP_.NET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("view-car")]
        public IActionResult GetCar(int id)
        {
            var cars = _carService.Get(id);
            return Ok(cars);
        }
        [HttpPost]
        public IActionResult AddCar([FromBody] Car car)
        {
            try
            {
                _carService.Add(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAllCar()
        {
            try
            {
                var cars = _carService.GetAll();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult UpdateCarDateMaintenance([FromQuery] int id, [FromQuery] DateTime lastDateMaintenance)
        {
            try
            {
                _carService.Update(id, lastDateMaintenance);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult DeleteCar([FromQuery] int id)
        {
            try
            {
                _carService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
