using Fit_API.Context;
using Fit_API.src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fit_API.src.Controllers;

[ApiController]
[Route("Training")]
public class TrainingController : ControllerBase
{
    private readonly ApiDbContext _context;

    public TrainingController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainingModel>>> GetTrainings()
    {
        var trainings = await _context.Trainings.FindAsync();
        return Ok(trainings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingModel>> GetTraining(int id)
    {
        var training = await _context.Trainings.FindAsync(id);

        if (training == null)
        {
            return NotFound();
        }

        return Ok(training);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTraining(TrainingModel training)
    {
        _context.Trainings.Add(training);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetTraining", new { id = training.IdTraining }, training);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTraining(int id, TrainingModel training)
    {
        if (training == null || id != training.IdTraining)
        {
            return BadRequest();
        }

        _context.Entry(training).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException)
        {
            if (!TrainingExists(id))
            {
                return NotFound();
            } else
            {
                throw;
            }
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTraining(int id)
    {
        var training = await _context.Trainings.FindAsync(id);

        if (training == null)
        {
            return NotFound();
        }

        _context.Trainings.Remove(training);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private bool TrainingExists(int id)
    {
        return _context.Users.Any(e => e.IdUser == id);
    }
}
