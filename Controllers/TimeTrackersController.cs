using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using TaskAgent.Data;
using TaskAgent.Models;

namespace TaskAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTrackersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILoggerManager _logger;

        public TimeTrackersController(ApplicationDbContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/TimeTrackers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeTracker>>> GettimeTrackers()
        {
            return await _context.timeTrackers.ToListAsync();
        }

        // GET: api/TimeTrackers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeTracker>> GetTimeTracker(int id)
        {
            var timeTracker = await _context.timeTrackers.FindAsync(id);

            if (timeTracker == null)
            {
                return NotFound();
            }

            _logger.LogInfo("TimeTracker Data obteined satisfacorily!");
            return timeTracker;
        }

        // PUT: api/TimeTrackers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeTracker(int id, TimeTracker timeTracker)
        {
            if (id != timeTracker.Id)
            {
                _logger.LogError("Bad Request from TimeTracker");
                return BadRequest();
            }

            _context.Entry(timeTracker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInfo("TimeTracker Data updated satisfacorily!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeTrackerExists(id))
                {
                    _logger.LogError("An error ocurred updating TimeTracker");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TimeTrackers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TimeTracker>> PostTimeTracker(TimeTracker timeTracker)
        {
            _context.timeTrackers.Add(timeTracker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeTracker", new { id = timeTracker.Id }, timeTracker);
        }

        // DELETE: api/TimeTrackers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeTracker(int id)
        {
            var timeTracker = await _context.timeTrackers.FindAsync(id);
            if (timeTracker == null)
            {
                _logger.LogError("An error ocurred deleting a TimeTracker");
                return NotFound();
            }

            _context.timeTrackers.Remove(timeTracker);//Modificar para cambiar estado y no eliminar
            await _context.SaveChangesAsync();

            _logger.LogInfo("TimeTracker deleted!");
            return NoContent();
        }

        private bool TimeTrackerExists(int id)
        {
            return _context.timeTrackers.Any(e => e.Id == id);
        }
    }
}
