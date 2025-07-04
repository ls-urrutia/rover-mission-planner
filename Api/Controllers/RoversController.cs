using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

namespace Api.Controllers
{
    [ApiController]
    [Route("rovers/{roverName}/tasks")]
    public class RoversController : ControllerBase
    {
        // Simulación de almacenamiento en memoria
        private static readonly List<RoverTask> Tasks = new();

        // POST /rovers/{roverName}/tasks
        [HttpPost]
        public IActionResult CreateTask(string roverName, [FromBody] RoverTask task)
        {
            // Asigna el nombre del rover desde la URL
            task.RoverName = roverName;

            // Regla de solapamiento
            if (Tasks.Any(t => t.RoverName == roverName && t.IsOverlapping(task)))
                return Conflict("La tarea se solapa con otra existente.");

            Tasks.Add(task);
            return CreatedAtAction(nameof(GetTasks), new { roverName, date = task.StartsAt.Date.ToString("yyyy-MM-dd") }, task);
        }

        // GET /rovers/{roverName}/tasks?date=YYYY-MM-DD
        [HttpGet]
        public IActionResult GetTasks(string roverName, [FromQuery] DateTime date)
        {
            var tasks = Tasks
                .Where(t => t.RoverName == roverName && t.StartsAt.Date == date.Date)
                .OrderBy(t => t.StartsAt)
                .ToList();
            return Ok(tasks);
        }

        // GET /rovers/{roverName}/utilization?date=YYYY-MM-DD
        [HttpGet("~/rovers/{roverName}/utilization")]
        public IActionResult GetUtilization(string roverName, [FromQuery] DateTime date)
        {
            var tasks = Tasks
                .Where(t => t.RoverName == roverName && t.StartsAt.Date == date.Date)
                .ToList();
            var totalMinutes = tasks.Sum(t => t.DurationMinutes);
            var utilization = (double)totalMinutes / 1440 * 100; // 1440 minutos en un día
            return Ok(new { utilization });
        }
    }
}